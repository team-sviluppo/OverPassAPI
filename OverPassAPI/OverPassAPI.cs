using System;
using System.Globalization;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using OverPass;
using OverPass.Utility;

namespace OverPass
{
    public class OverPassAPI
	{
        public OverPassPoint? Places { get; set; }
        public OverPassPoint? PoI { get; set; }
        public OverPassPoint? PoFW { get; set; }
        public OverPassPoint? Natural { get; set; }
        public OverPassPoint? Traffic { get; set; }
        public OverPassPoint? Transport { get; set; }
        public OverPassLine? Roads { get; set; }
        public OverPassLine? Railways { get; set; }
        public OverPassLine? Waterways { get; set; }
        public OverPassPolygon? Buildings { get; set; }
        public OverPassPolygon? Landuse { get; set; }
        public OverPassPolygon? Water { get; set; }

        public string BBox { get; set; } = "41.888221345535,12.484095096588,41.895009993745,12.495414018631"; // Rome
        public string? OverPassUrl { get; set; } = "https://overpass-api.de/api/interpreter";
        public Dictionary<string, List<string>>? Query;

        private List<NetTopologySuite.Features.Feature>? ListFeatures { get; set; } = new();

        public bool IsIntersects { get; set; } = true;
        public NetTopologySuite.Geometries.Geometry? Filter { get; set; }

        public OverPassAPI(string bbox)
        {
            this.BBox = bbox;
            this.Init(bbox);
        }

        private void Init(string bbox)
        {
            /** Point */
            this.Places = new(bbox, TagType.PLACES);
            this.PoI = new(bbox, TagType.POI);
            this.PoFW = new(bbox, TagType.POFW);
            this.Natural = new(bbox, TagType.NATURAL);
            this.Traffic = new(bbox, TagType.TRAFFIC);
            this.Transport = new(bbox, TagType.TRANSPORT);

            /** LineString */
            this.Roads = new(bbox, TagType.ROADS);
            this.Railways = new(bbox, TagType.RAILWAYS);
            this.Waterways = new(bbox, TagType.WATERWAYS);

            /** Polygons */
            this.Buildings = new(bbox, TagType.BUILDINGS);
            this.Landuse = new(bbox, TagType.LANDUSE);
            this.Water = new(bbox, TagType.WATER);
        }

        private void SetUpUrl(string overpassUrl)
        {
            string url = $"{overpassUrl}/api/interpreter";

            this.Places!.OverPassUrl = url;
            this.PoI!.OverPassUrl = url;
            this.PoFW!.OverPassUrl = url;
            this.Natural!.OverPassUrl = url;
            this.Traffic!.OverPassUrl = url;
            this.Transport!.OverPassUrl = url;
            this.Roads!.OverPassUrl = url;
            this.Railways!.OverPassUrl = url;
            this.Waterways!.OverPassUrl = url;
            this.Buildings!.OverPassUrl = url;
            this.Landuse!.OverPassUrl = url;
            this.Water!.OverPassUrl = url;
        }

        public Dictionary<string, List<string>>? AllTags
        {
            get
            {
                Dictionary<string, List<string>>? q = new();

                q = OverPassUtility.AddDictionary(this.Places!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.PoI!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.PoFW!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Natural!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Traffic!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Transport!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Roads!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Railways!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Waterways!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Buildings!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Landuse!.AllTags, q);
                q = OverPassUtility.AddDictionary(this.Water!.AllTags, q);

                return q;
            }
        }

        private void SetUpQuery(Dictionary<string, List<string>>? query)
        {
            this.Query = query;
            this.Places!.Query = query;
            this.PoI!.Query = query;
            this.PoFW!.Query = query;
            this.Natural!.Query = query;
            this.Traffic!.Query = query;
            this.Transport!.Query = query;
            this.Roads!.Query = query;
            this.Railways!.Query = query;
            this.Waterways!.Query = query;
            this.Buildings!.Query = query;
            this.Landuse!.Query = query;
            this.Water!.Query = query;
        }

        public OverPassAPI(string bbox, Dictionary<string, List<string>>? query) : this(bbox)
        {
            this.SetUpQuery(query);
        }

        public OverPassAPI(string bbox, Dictionary<string, List<string>>? query, string overpassUrl) : this(bbox, query)
        {
            this.SetUpUrl(overpassUrl);
        }

        public OverPassAPI(NetTopologySuite.Geometries.Geometry filter, Dictionary<string, List<string>>? query)
        {
            this.Filter = filter;
            NetTopologySuite.Geometries.Envelope Env = filter.EnvelopeInternal;
            this.BBox = $"{Env.MinY.ToString(CultureInfo.InvariantCulture)},{Env.MinX.ToString(CultureInfo.InvariantCulture)},{Env.MaxY.ToString(CultureInfo.InvariantCulture)},{Env.MaxX.ToString(CultureInfo.InvariantCulture)}"; ;
            this.Init(this.BBox);
            this.SetUpQuery(query);
        }

        public OverPassAPI(NetTopologySuite.Geometries.Geometry filter, Dictionary<string, List<string>>? query, bool isIntersects) : this(filter, query)
        {
            this.IsIntersects = isIntersects; 
        }

        public OverPassAPI(NetTopologySuite.Geometries.Geometry filter, Dictionary<string, List<string>>? query, bool isIntersects, string overpassUrl) : this(filter, query, isIntersects)
        {
            this.SetUpUrl(overpassUrl);
        }

        private void AddFeatures(List<NetTopologySuite.Features.Feature>? f)
        {
            if (f != null && this.ListFeatures != null)
                this.ListFeatures.AddRange(f);
        }

        public virtual string GeoJSon
        {
            get => OverPassUtility.SerializeFeatures(this.FeatureCollection);
        }

        public virtual NetTopologySuite.Features.FeatureCollection? FeatureCollection
        {
            get => OverPassUtility.FeatureCollection(this.Features);
        }

        public List<NetTopologySuite.Features.Feature>? Features
        {
            get
            {
                List<NetTopologySuite.Features.Feature>? features = new();

                this.AddFeatures(this.Places!.Features);
                this.AddFeatures(this.PoI!.Features);
                this.AddFeatures(this.PoFW!.Features);
                this.AddFeatures(this.Natural!.Features);
                this.AddFeatures(this.Traffic!.Features);
                this.AddFeatures(this.Transport!.Features);
                this.AddFeatures(this.Roads!.Features);
                this.AddFeatures(this.Railways!.Features);
                this.AddFeatures(this.Waterways!.Features);
                this.AddFeatures(this.Buildings!.Features);
                this.AddFeatures(this.Landuse!.Features);
                this.AddFeatures(this.Water!.Features);

                /** Intersection all geometry */
                if (this.IsIntersects && this.Filter != null && this.ListFeatures != null)
                {
                    foreach (NetTopologySuite.Features.Feature f in this.ListFeatures)
                    {
                        NetTopologySuite.Features.Feature fI = f;
                        fI.Geometry = f.Geometry.Intersection(this.Filter);
                        if (!fI.Geometry.IsEmpty)
                            features.Add(fI);
                    }
                }
                else
                    features = this.ListFeatures;

                if (features != null && features.Count > 0)
                    return features;
                else
                    return null;
            }
        }
    }
}

