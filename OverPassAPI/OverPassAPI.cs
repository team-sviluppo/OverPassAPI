using System.Collections.Generic;
﻿using System;
using System.Globalization;
using OverPass;
using OverPass.Utility;

namespace OverPass
{
    public class OverPassAPI : OverPass
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

        public OverPassAPI(string bbox) : base(bbox) => this.Init(this.BBox);
        public OverPassAPI(string bbox, Dictionary<string, List<string>>? query) : this(bbox) => this.Query = query;
        public OverPassAPI(NetTopologySuite.Geometries.Geometry filter) : base(filter) => this.Init(this.BBox);
        public OverPassAPI(NetTopologySuite.Geometries.Geometry filter, Dictionary<string, List<string>>? query) : this(filter) => this.Query = query;
        public OverPassAPI(NetTopologySuite.Geometries.Geometry filter, string bbox) : base(bbox) => this.Init(bbox);
        
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

            this.AllTags.Add(new(this.BBox, "natural", "*"));
            this.AllTags.Add(new(this.BBox, "place", "*"));
            this.AllTags.Add(new(this.BBox, "amenity", "*"));
            this.AllTags.Add(new(this.BBox, "religion", "*"));
            this.AllTags.Add(new(this.BBox, "denomination", "*"));
            this.AllTags.Add(new(this.BBox, "office", "*"));
            this.AllTags.Add(new(this.BBox, "landuse", "*"));
            this.AllTags.Add(new(this.BBox, "leisure", "*"));
            this.AllTags.Add(new(this.BBox, "sport", "*"));
            this.AllTags.Add(new(this.BBox, "shop", "*"));
            this.AllTags.Add(new(this.BBox, "tourism", "*"));
            this.AllTags.Add(new(this.BBox, "tourist", "*"));
            this.AllTags.Add(new(this.BBox, "man_made", "*"));
            this.AllTags.Add(new(this.BBox, "emergency", "*"));
            this.AllTags.Add(new(this.BBox, "highway", "*"));
            this.AllTags.Add(new(this.BBox, "waterway", "*"));
            this.AllTags.Add(new(this.BBox, "railway", "*"));
            this.AllTags.Add(new(this.BBox, "military", "*"));
            this.AllTags.Add(new(this.BBox, "aeroway", "*"));
            this.AllTags.Add(new(this.BBox, "aerialway", "*"));
            this.AllTags.Add(new(this.BBox, "boundary", "*"));

            this.Tags = new Dictionary<TagType, List<OTag>>() {
                { this.Type, this.AllTags }
            };
        }
        
    }
}

