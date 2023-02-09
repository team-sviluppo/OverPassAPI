using System.Collections.Generic;
using System.ComponentModel;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using OverPass.Utility;

namespace OverPass
{
    public class OverPass
    {
		protected string QueryStart = "[out:json][timeout:25];(";
		protected string QueryEnd = ");out geom;>;out body qt;";
        public string? OverPassUrl { get; set; } = "https://overpass-api.de/api/interpreter";
        public string BBox { get; set; } = "";
        public Dictionary<string, List<string>>? Query;
        public TagType Type { get; set; } = TagType.PLACES;
        public Dictionary<TagType, List<OTag>>? Tags;

        public OverPass(string bbox)
        {
            this.BBox = bbox;
        }

        public OverPass(string bbox, Dictionary<string, List<string>>? query) : this(bbox)
        {
            this.Query = query;
        }

        public OverPass(string bbox, Dictionary<string, List<string>>? query, string? overpassUrl) : this(bbox, query)
        {
            this.OverPassUrl = overpassUrl;
        }

        /**
         * Get List of tag query
         */
        public List<string>? TagKeys
        {
            get
            {
                List<string>? q = new();
                foreach (OTag t in this.Tags![this.Type])
                {
                    if (!q.Contains(t.KeyTag))
                        q.Add(t.KeyTag);
                }
                return q;
            }
        }

        public List<string>? TagValues
        {
            get
            {
                List<string>? q = new();
                foreach (OTag t in this.Tags![this.Type])
                {
                    if (!q.Contains(t.ValueTag))
                        q.Add(t.ValueTag);
                }
                return q;
            }
        }

        public Dictionary<string, List<string>>? AllTags
        {
            get
            {
                Dictionary<string, List<string>>? q = new();

                foreach (var t in this.Tags![this.Type])
                {
                    // Console.WriteLine($"{month.Key}: {month.Value}");
                    if (!q!.ContainsKey(t.KeyTag))
                        q.Add(t.KeyTag, new() { "*" });
                }

                /*
                this.Tags![this.Type].AsParallel().ForAll(c =>
                {
                    if (!q!.ContainsKey(c.KeyTag))
                        q.Add(c.KeyTag, new() {"*"});
                });
                */

                return q;
            }
        }

        private string? Body
        {
            get
            {
                string? q = null;

                foreach (OTag t in this.Tags![this.Type])
                {
                    if (this.Query == null && t.ValueTag == "*")
                        q += t.Query;
                    else if (this.Query != null &&
                                this.Query.ContainsKey(t.KeyTag) &&
                                this.Query[t.KeyTag].Contains(t.ValueTag))
                        q += t.Query;
                }
                return q;
            }
        }

        private static async Task<string?> RunOverPassQuery(string url, string query)
        {

            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };

            using HttpClient client = new(handler);
            var body = new Dictionary<string, string>
            {
                {"data", $"{query}"}
            };

            using HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(body));
            return await response.Content.ReadAsStringAsync();
            
        }

        protected Root? Response
        {
            get
            {
                try
                {
                    string? b = this.Body;
                    if (b != null && this.OverPassUrl != null)
                    {
                        string q = $"{this.QueryStart}{b}{this.QueryEnd}";
                        Task<string?> r = RunOverPassQuery(this.OverPassUrl, q);
                        if (r != null)
                        {
                            string? res = r.Result;
                            if (res != null)
                                return OverPassUtility.JSonDeserializeResponse(res);
                            else
                                return null;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}

