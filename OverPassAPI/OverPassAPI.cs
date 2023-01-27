using NetTopologySuite.IO;
using Newtonsoft.Json;
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

        public OverPassAPI(string bbox)
        {
            this.BBox = bbox;

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

        public OverPassAPI(string bbox, Dictionary<string, List<string>>? query) : this(bbox)
        {
            this.Query = query;

            if (this.Places != null)
                this.Places.Query = query;
            if (this.PoI != null)
                this.PoI.Query = query;
            if (this.PoFW != null)
                this.PoFW.Query = query;
            if (this.Natural != null)
                this.Natural.Query = query;
            if (this.Traffic != null)
                this.Traffic.Query = query;
            if (this.Transport != null)
                this.Transport.Query = query;
            if (this.Roads != null)
                this.Roads.Query = query;
            if (this.Railways != null)
                this.Railways.Query = query;
            if (this.Waterways != null)
                this.Waterways.Query = query;
            if (this.Buildings != null)
                this.Buildings.Query = query;
            if (this.Landuse != null)
                this.Landuse.Query = query;
            if (this.Water != null)
                this.Water.Query = query;
        }

        public OverPassAPI(string bbox, Dictionary<string, List<string>>? query, string overpassUrl) : this(bbox, query)
        {
            if (this.Places != null)
                this.Places.OverPassUrl = overpassUrl;
            if (this.PoI != null)
                this.PoI.OverPassUrl = overpassUrl;
            if (this.PoFW != null)
                this.PoFW.OverPassUrl = overpassUrl;
            if (this.Natural != null)
                this.Natural.OverPassUrl = overpassUrl;
            if (this.Traffic != null)
                this.Traffic.OverPassUrl = overpassUrl;
            if (this.Transport != null)
                this.Transport.OverPassUrl = overpassUrl;
            if (this.Roads != null)
                this.Roads.OverPassUrl = overpassUrl;
            if (this.Railways != null)
                this.Railways.OverPassUrl = overpassUrl;
            if (this.Waterways != null)
                this.Waterways.OverPassUrl = overpassUrl;
            if (this.Buildings != null)
                this.Buildings.OverPassUrl = overpassUrl;
            if (this.Landuse != null)
                this.Landuse.OverPassUrl = overpassUrl;
            if (this.Water != null)
                this.Water.OverPassUrl = overpassUrl;
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
                if (this.Places != null)
                    this.AddFeatures(this.Places.Features);

                if (this.PoI != null)
                    this.AddFeatures(this.PoI.Features);

                if (this.PoFW != null)
                    this.AddFeatures(this.PoFW.Features);

                if (this.Natural != null)
                    this.AddFeatures(this.Natural.Features);

                if (this.Traffic != null)
                    this.AddFeatures(this.Traffic.Features);

                if (this.Transport != null)
                    this.AddFeatures(this.Transport.Features);

                if (this.Roads != null)
                    this.AddFeatures(this.Roads.Features);

                if (this.Railways != null)
                    this.AddFeatures(this.Railways.Features);

                if (this.Waterways != null)
                    this.AddFeatures(this.Waterways.Features);

                if (this.Buildings != null)
                    this.AddFeatures(this.Buildings.Features);

                if (this.Landuse != null)
                    this.AddFeatures(this.Landuse.Features);

                if (this.Water != null)
                    this.AddFeatures(this.Water.Features);

                return this.ListFeatures;
            }
        }
    }
}

