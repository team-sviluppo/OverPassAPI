using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using OverPass.Utility;

namespace OverPass
{
    public class OverPassLine : OverPassPoint
    {
        public OverPassLine(string bbox) : base(bbox)
        {
        }

        public OverPassLine(string bbox, Dictionary<string, List<string>>? query) : base(bbox, query)
        {
        }

        public OverPassLine(string bbox, TagType type) : base(bbox)
        {
            this.Type = type;
            this.Tags = new();
            List<OTag> list = new List<OTag>();
            switch (this.Type)
            {
                case TagType.RAILWAYS:
                    list.Add(new(this.BBox, "railway", "*"));
                    list.Add(new(this.BBox, "aerialway", "*"));
                    list.Add(new(this.BBox, "railway", "rail"));
                    list.Add(new(this.BBox, "railway", "light_rail"));
                    list.Add(new(this.BBox, "railway", "subway"));
                    list.Add(new(this.BBox, "railway", "tram"));
                    list.Add(new(this.BBox, "railway", "monorail"));
                    list.Add(new(this.BBox, "railway", "narrow_gauge"));
                    list.Add(new(this.BBox, "railway", "miniature"));
                    list.Add(new(this.BBox, "railway", "funicular"));
                    list.Add(new(this.BBox, "railway", "rack"));
                    list.Add(new(this.BBox, "aerialway", "drag_lift"));
                    list.Add(new(this.BBox, "aerialway", "chair_lift"));
                    list.Add(new(this.BBox, "aerialway", "cable_car"));
                    list.Add(new(this.BBox, "aerialway", "gondola"));
                    list.Add(new(this.BBox, "aerialway", "goods"));
                    break;
                case TagType.ROADS:
                    list.Add(new(this.BBox, "highway", "*"));
                    list.Add(new(this.BBox, "highway", "motorway"));
                    list.Add(new(this.BBox, "highway", "trunk"));
                    list.Add(new(this.BBox, "highway", "primary"));
                    list.Add(new(this.BBox, "highway", "secondary"));
                    list.Add(new(this.BBox, "highway", "tertiary"));
                    list.Add(new(this.BBox, "highway", "unclassified"));
                    list.Add(new(this.BBox, "highway", "residential"));
                    list.Add(new(this.BBox, "highway", "living_street"));
                    list.Add(new(this.BBox, "highway", "pedestrian"));
                    list.Add(new(this.BBox, "highway", "busway"));
                    list.Add(new(this.BBox, "highway", "motorway_link"));
                    list.Add(new(this.BBox, "highway", "trunk_link"));
                    list.Add(new(this.BBox, "highway", "primary_link"));
                    list.Add(new(this.BBox, "highway", "secondary_link"));
                    list.Add(new(this.BBox, "highway", "tertiary_link"));
                    list.Add(new(this.BBox, "highway", "service"));
                    list.Add(new(this.BBox, "highway", "track"));
                    list.Add(new(this.BBox, "highway", "bridleway"));
                    list.Add(new(this.BBox, "highway", "cycleway"));
                    list.Add(new(this.BBox, "highway", "footway"));
                    list.Add(new(this.BBox, "highway", "path"));
                    list.Add(new(this.BBox, "highway", "steps"));
                    break;
                case TagType.WATERWAYS:
                    list.Add(new(this.BBox, "waterway", "*"));
                    list.Add(new(this.BBox, "waterway", "river"));
                    list.Add(new(this.BBox, "waterway", "stream"));
                    list.Add(new(this.BBox, "waterway", "canal"));
                    list.Add(new(this.BBox, "waterway", "drain"));
                    break;
            }

            this.Tags.Add(this.Type, list);
        }

        public OverPassLine(string bbox, TagType type, Dictionary<string, List<string>>? query) : this(bbox, type) => this.Query = query;
        public OverPassLine(string bbox, TagType type, Dictionary<string, List<string>>? query, string? overpassUrl) : this(bbox, type, query) => this.OverPassUrl = overpassUrl;
    }
}

