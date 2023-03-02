using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OverPass.Utility;

namespace OverPass
{
    public class OverPass
    {
        protected string QueryStart = "[out:json][timeout:25];(";
        protected string QueryEnd = ");out geom;>;out body qt;";
        public string? OverPassUrl { get; set; } = "https://overpass-api.de/api/interpreter";
        public string BBox { get; set; } = "41.888221345535,12.484095096588,41.895009993745,12.495414018631"; // Rome
        public Dictionary<string, List<string>>? Query;
        public TagType Type { get; set; } = TagType.ALL;
        public Dictionary<TagType, List<OTag>>? Tags;
        public NetTopologySuite.Geometries.Geometry? Filter { get; set; }
        public List<OTag> AllTags { get; set; } = new List<OTag>();

        public OverPass(string bbox)
        {
            this.BBox = bbox;
        }

        public OverPass(string bbox, Dictionary<string, List<string>>? query) : this(bbox)
        {
            this.BBox = bbox;
        }

        public OverPass(NetTopologySuite.Geometries.Geometry filter)
        {
            this.Filter = filter;
            NetTopologySuite.Geometries.Envelope Env = filter.EnvelopeInternal;
            this.BBox = String.Format(CultureInfo.GetCultureInfo("en-US"), "{0},{1},{2},{3}", Env.MinY, Env.MinX, Env.MaxY, Env.MaxX);
        }

        public OverPass(NetTopologySuite.Geometries.Geometry filter, Dictionary<string, List<string>>? query) : this(filter)
        {
            this.Query = query;
        }

        public void BaseUrl(string overpassUrl)
        {
            string url = $"{overpassUrl}/api/interpreter";
            this.OverPassUrl = url;
        }

        /**
         * Get List of tag query
         */
        public List<string>? TagKeys
        {
            get
            {
                if (this.Tags != null)
                {
                    List<string>? q = new();
                    foreach (OTag t in this.Tags[this.Type])
                    {
                        if (!q.Contains(t.KeyTag))
                            q.Add(t.KeyTag);
                    }
                    return q;
                }
                else
                    return null;
            }
        }

        public List<string>? TagValues
        {
            get
            {
                if (this.Tags != null)
                {
                    List<string>? q = new();
                    foreach (OTag t in this.Tags[this.Type])
                    {
                        if (!q.Contains(t.ValueTag))
                            q.Add(t.ValueTag);
                    }
                    return q;
                }
                else
                    return null;
            }
        }

        public Dictionary<TagType, List<OTag>>? AllTagsDictonary
        {
            get {
                return new()
                {
                    { this.Type, this.AllTags }
                };
            }
            
        }

        private string? GetBody()
        {
            string? q = null;

            foreach (OTag t in this.Tags![this.Type])
            {
                if ((this.Query == null || this.Query.Count == 0) && t.ValueTag == "*")
                    q += t.Query;
                else
                {
                    List<string>? value = null;
                    bool keyExists = this.Query!.TryGetValue(t.KeyTag, out value);
                    if (keyExists && value != null)
                        if (value!.Contains(t.ValueTag)) q += t.Query;
                }
            }

            return q;
        }

        private static async Task<string?> RunOverPassQuery(string url, string query)
        {

            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15)
            };

            using HttpClient client = new(handler);
            var body = new Dictionary<string, string>
            {
                {"data", $"{query}"}
            };

            using HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(body));
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync();
            else
                throw new Exception("Non sono riuscito a leggere le geometrie da OpenstreetMap");
        }

        protected async Task<Root?> Response()
        {
            string? b = this.GetBody();

            if (b != null && this.OverPassUrl != null)
            {
                string q = $"{this.QueryStart}{b}{this.QueryEnd}";
                try
                {
                    string? r = await RunOverPassQuery(this.OverPassUrl, q);
                    return OverPassUtility.JSonDeserializeResponse(r!);
                }
                catch (Exception e)
                {
                    // Log Error
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            else
                return null;
        }

        public virtual async Task<List<NetTopologySuite.Features.Feature>?> Features()
        {
            Root? resp = await this.Response();
            List<NetTopologySuite.Features.Feature> features = new();
            if (resp != null && resp.elements != null)
            {
                foreach (Element e in resp.elements)
                {
                    NetTopologySuite.Features.Feature? f = null;

                    if (e.geometry != null)
                    {
                        if (e.type == "node")
                        {
                            /** Point */
                            NetTopologySuite.Geometries.Point geom = (NetTopologySuite.Geometries.Point)new(OverPassUtility.GetPoint(e));
                            f = new(geom, OverPassUtility.GetProperties(e));
                        }
                        else if (e.type == "way")
                        {
                            NetTopologySuite.Geometries.Coordinate[]? coordinates = OverPassUtility.GetCoordinates(e);
                            NetTopologySuite.Geometries.LineString line = new(coordinates);

                            if (line.IsClosed)
                            {
                                /** Polygon */
                                NetTopologySuite.Geometries.LinearRing lineRing = (NetTopologySuite.Geometries.LinearRing)new(coordinates);
                                NetTopologySuite.Geometries.Polygon poly = (NetTopologySuite.Geometries.Polygon)new(lineRing);
                                f = new(poly, OverPassUtility.GetProperties(e));
                            }
                            else
                                /** Linestring */
                                f = new(line, OverPassUtility.GetProperties(e));
                        }

                        f!.BoundingBox = OverPassUtility.GetBBox(e);
                    }

                    if (f != null)
                        features.Add(f);
                }
            }

            return features;
        }
    }
}

