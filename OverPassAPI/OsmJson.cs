using Newtonsoft.Json;

namespace OverPass
{

    public class Bounds
    {
        public double? minlat { get; set; }
        public double? minlon { get; set; }
        public double? maxlat { get; set; }
        public double? maxlon { get; set; }
    }

    public class Element
    {
        public string? type { get; set; }
        public object? id { get; set; }
        public double? lat { get; set; }
        public double? lon { get; set; }
        public Tags? tags { get; set; }
        public Bounds? bounds { get; set; }
        public List<object>? nodes { get; set; }
        public List<Geometry>? geometry { get; set; }
    }

    public class Geometry
    {
        public double? lat { get; set; }
        public double? lon { get; set; }
    }

    public class Osm3s
    {
        public DateTime? timestamp_osm_base { get; set; }
        public string? copyright { get; set; }
    }

    public class Root
    {
        public double? version { get; set; }
        public string? generator { get; set; }
        public Osm3s? osm3s { get; set; }
        public List<Element>? elements { get; set; }
    }

    public class Tags
    {
        public string? name { get; set; }

        public string? highway { get; set; }
        public string? lanes { get; set; }
        public string? lit { get; set; }
        public string? maxspeed { get; set; }
        public string? oneway { get; set; }
        public string? surface { get; set; }

        public string? description { get; set; }
        public string? landuse { get; set; }
        public string? old_name { get; set; }

        [JsonProperty("was:landuse")]
        public string? waslanduse { get; set; }

        public string? amenity { get; set; }
        public string? wheelchair { get; set; }
        public string? wikidata { get; set; }
        public string? wikipedia { get; set; }

        [JsonProperty("addr:city")]
        public string? addrcity { get; set; }

        [JsonProperty("addr:housenumber")]
        public string? addrhousenumber { get; set; }

        [JsonProperty("addr:postcode")]
        public string? building { get; set; }
        public string? addrpostcode { get; set; }

        [JsonProperty("addr:street")]
        public string? addrstreet { get; set; }
        public string? fountain { get; set; }
        public string? wikimedia_commons { get; set; }
        public string? cuisine { get; set; }
        public string? capacity { get; set; }
        public string? note { get; set; }
        public string? access { get; set; }
        public string? bicycle_parking { get; set; }
        public string? @operator { get; set; }

        [JsonProperty("addr:country")]
        public string? addrcountry { get; set; }
        public string? phone { get; set; }

        [JsonProperty("ref:vatin")]
        public string? refvatin { get; set; }

        [JsonProperty("ice_cream:type")]
        public string? ice_creamtype { get; set; }

        [JsonProperty("restaurant:type:it")]
        public string? restauranttypeit { get; set; }
        public string? shop { get; set; }
        public string? supervised { get; set; }
        public string? brand { get; set; }

        [JsonProperty("ref:mise")]
        public string? refmise { get; set; }
        public string? website { get; set; }
        public string? fee { get; set; }
        public string? backrest { get; set; }
        public string? outdoor_seating { get; set; }
        public string? takeaway { get; set; }
        public string? opening_hours { get; set; }
        public string? reservation { get; set; }
        public string? smoking { get; set; }
        public string? created_by { get; set; }
        public string? parking { get; set; }

        [JsonProperty("building:levels")]
        public string? buildinglevels { get; set; }
        public string? healthcare { get; set; }
        public string? religion { get; set; }
        public string? source { get; set; }
        public string? alt_name { get; set; }

        [JsonProperty("operator:wikidata")]
        public string? operatorwikidata { get; set; }

        public string? leisure { get; set; }
        public string? sport { get; set; }

        public string? intermittent { get; set; }

        [JsonProperty("name:de")]
        public string? namede { get; set; }

        [JsonProperty("name:es")]
        public string? namees { get; set; }

        [JsonProperty("name:fr")]
        public string? namefr { get; set; }

        [JsonProperty("name:la")]
        public string? namela { get; set; }

        [JsonProperty("name:nl")]
        public string? namenl { get; set; }
        public string? waterway { get; set; }

        public string? public_transport { get; set; }
        public string? railway { get; set; }
        public string? tram { get; set; }

    }
}