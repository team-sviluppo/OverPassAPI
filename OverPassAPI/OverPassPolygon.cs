using OverPass;

public class OverPassPolygon : OverPassLine
{
    public OverPassPolygon(string bbox) : base(bbox)
    {
    }

    public OverPassPolygon(string bbox, Dictionary<string, List<string>>? query) : base(bbox, query)
    {
    }

    public OverPassPolygon(string bbox, TagType type) : base(bbox)
    {
        this.Type = type;
        this.Tags = new();
        List<OTag> list = new List<OTag>();
        switch (this.Type)
        {
            case TagType.BUILDINGS:
                this.AllTags.Add(new(this.BBox, "building", "*"));
                break;
            case TagType.LANDUSE:
                list.Add(new(this.BBox, "landuse", "*"));
                list.Add(new(this.BBox, "natural", "*"));
                list.Add(new(this.BBox, "leisure", "*"));
                list.Add(new(this.BBox, "boundary", "*"));
                list.Add(new(this.BBox, "landuse", "forest"));
                list.Add(new(this.BBox, "natural", "wood"));
                list.Add(new(this.BBox, "leisure", "park"));
                list.Add(new(this.BBox, "leisure", "common"));
                list.Add(new(this.BBox, "landuse", "residential"));
                list.Add(new(this.BBox, "landuse", "industrial"));
                list.Add(new(this.BBox, "landuse", "cemetery"));
                list.Add(new(this.BBox, "landuse", "allotments"));
                list.Add(new(this.BBox, "landuse", "meadown"));
                list.Add(new(this.BBox, "landuse", "commercial"));
                list.Add(new(this.BBox, "leisure", "nature_reserve"));
                list.Add(new(this.BBox, "leisure", "recreation_ground"));
                list.Add(new(this.BBox, "leisure", "retail"));
                list.Add(new(this.BBox, "landuse", "military"));
                list.Add(new(this.BBox, "landuse", "quarry"));
                list.Add(new(this.BBox, "landuse", "orchad"));
                list.Add(new(this.BBox, "landuse", "vineyard"));
                list.Add(new(this.BBox, "landuse", "scrub"));
                list.Add(new(this.BBox, "landuse", "grass"));
                list.Add(new(this.BBox, "natural", "health"));
                list.Add(new(this.BBox, "boundary", "national_park"));
                list.Add(new(this.BBox, "landuse", "farmland"));
                list.Add(new(this.BBox, "landuse", "farmyard"));
                break;
            case TagType.WATER:
                list.Add(new(this.BBox, "natural", "*"));
                list.Add(new(this.BBox, "landuse", "*"));
                list.Add(new(this.BBox, "waterway", "*"));
                list.Add(new(this.BBox, "natural", "water"));
                list.Add(new(this.BBox, "landuse", "reservoir"));
                list.Add(new(this.BBox, "waterway", "riverbank"));
                list.Add(new(this.BBox, "waterway", "dock"));
                list.Add(new(this.BBox, "natural", "glacier"));
                list.Add(new(this.BBox, "natural", "wetland"));
                break;
        }

        this.Tags.Add(this.Type, list);
    }

    public OverPassPolygon(string bbox, TagType type, Dictionary<string, List<string>>? query) : this(bbox, type)
    {
        this.Query = query;
    }

    public OverPassPolygon(string bbox, TagType type, Dictionary<string, List<string>>? query, string? overpassUrl) : this(bbox, type, query)
    {
        this.OverPassUrl = overpassUrl;
    }
}

