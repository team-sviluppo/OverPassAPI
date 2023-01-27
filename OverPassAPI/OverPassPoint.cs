using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using OverPass.Utility;

namespace OverPass
{
    public class OverPassPoint : OverPass
    {
        
        public OverPassPoint(string bbox) : base(bbox)
        {
        }

        public OverPassPoint(string bbox, Dictionary<string, List<string>>? query) : base(bbox, query)
        {
        }

        public OverPassPoint(string bbox, Dictionary<string, List<string>>? query, string? overpassUrl) : base(bbox, query, overpassUrl)
        {
        }

        public OverPassPoint(string bbox, TagType type) : base(bbox)
        {
            this.Type = type;
            this.Tags = new();

            List<OTag> list = new List<OTag>();
            switch (this.Type)
            {
                case TagType.NATURAL:
                    list.Add(new(this.BBox, "natural", "*"));
                    list.Add(new(this.BBox, "natural", "spring"));
                    list.Add(new(this.BBox, "natural", "glacier"));
                    list.Add(new(this.BBox, "natural", "peak"));
                    list.Add(new(this.BBox, "natural", "cliff"));
                    list.Add(new(this.BBox, "natural", "volcano"));
                    list.Add(new(this.BBox, "natural", "tree"));
                    list.Add(new(this.BBox, "natural", "mine"));
                    list.Add(new(this.BBox, "natural", "cave_entrance"));
                    list.Add(new(this.BBox, "natural", "beach"));
                    break;
                case TagType.PLACES:
                    list.Add(new(this.BBox, "place", "*"));
                    list.Add(new(this.BBox, "place", "city"));
                    list.Add(new(this.BBox, "place", "town"));
                    list.Add(new(this.BBox, "place", "village"));
                    list.Add(new(this.BBox, "place", "hamlet"));
                    list.Add(new(this.BBox, "place", "suburb"));
                    list.Add(new(this.BBox, "place", "island"));
                    list.Add(new(this.BBox, "place", "farm"));
                    list.Add(new(this.BBox, "place", "isolated_dwelling"));
                    list.Add(new(this.BBox, "place", "region"));
                    list.Add(new(this.BBox, "place", "county"));
                    list.Add(new(this.BBox, "place", "locality"));
                    break;
                case TagType.POFW:
                    list.Add(new(this.BBox, "amenity", "*"));
                    list.Add(new(this.BBox, "religion", "*"));
                    list.Add(new(this.BBox, "denomination", "*"));
                    list.Add(new(this.BBox, "amenity", "place_of_worship"));
                    list.Add(new(this.BBox, "religion", "christian"));
                    list.Add(new(this.BBox, "religion", "jewish"));
                    list.Add(new(this.BBox, "religion", "muslim"));
                    list.Add(new(this.BBox, "denomination", "anglican"));
                    list.Add(new(this.BBox, "denomination", "catholic"));
                    list.Add(new(this.BBox, "denomination", "evangelical"));
                    list.Add(new(this.BBox, "denomination", "lutheran"));
                    list.Add(new(this.BBox, "denomination", "methodist"));
                    list.Add(new(this.BBox, "denomination", "orthodox"));
                    list.Add(new(this.BBox, "denomination", "protestant"));
                    list.Add(new(this.BBox, "denomination", "baptist"));
                    list.Add(new(this.BBox, "denomination", "mormon"));
                    list.Add(new(this.BBox, "denomination", "sunni"));
                    list.Add(new(this.BBox, "denomination", "shia"));
                    list.Add(new(this.BBox, "religion", "buddhist"));
                    list.Add(new(this.BBox, "religion", "hindu"));
                    list.Add(new(this.BBox, "religion", "taoist"));
                    list.Add(new(this.BBox, "religion", "shintoist"));
                    list.Add(new(this.BBox, "religion", "sikh"));
                    break;
                case TagType.POI:
                    list.Add(new(this.BBox, "amenity", "*"));
                    list.Add(new(this.BBox, "office", "*"));
                    list.Add(new(this.BBox, "landuse", "*"));
                    list.Add(new(this.BBox, "leisure", "*"));
                    list.Add(new(this.BBox, "sport", "*"));
                    list.Add(new(this.BBox, "shop", "*"));
                    list.Add(new(this.BBox, "tourism", "*"));
                    list.Add(new(this.BBox, "tourist", "*"));
                    list.Add(new(this.BBox, "man_made", "*"));
                    list.Add(new(this.BBox, "emergency", "*"));
                    list.Add(new(this.BBox, "highway", "*"));
                    list.Add(new(this.BBox, "amenity", "police"));
                    list.Add(new(this.BBox, "amenity", "fire_station"));
                    list.Add(new(this.BBox, "amenity", "post_office"));
                    list.Add(new(this.BBox, "amenity", "telephone"));
                    list.Add(new(this.BBox, "amenity", "library"));
                    list.Add(new(this.BBox, "amenity", "townhall"));
                    list.Add(new(this.BBox, "amenity", "courthouse"));
                    list.Add(new(this.BBox, "amenity", "prison"));
                    list.Add(new(this.BBox, "amenity", "embassy"));
                    list.Add(new(this.BBox, "office", "diplomatic"));
                    list.Add(new(this.BBox, "amenity", "community_centre"));
                    list.Add(new(this.BBox, "amenity", "nursing_home"));
                    list.Add(new(this.BBox, "amenity", "arts_centre"));
                    list.Add(new(this.BBox, "amenity", "grave_yard"));
                    list.Add(new(this.BBox, "landuse", "cemetery"));
                    list.Add(new(this.BBox, "amenity", "marketplace"));
                    list.Add(new(this.BBox, "amenity", "university"));
                    list.Add(new(this.BBox, "amenity", "school"));
                    list.Add(new(this.BBox, "amenity", "kindergarten"));
                    list.Add(new(this.BBox, "amenity", "college"));
                    list.Add(new(this.BBox, "amenity", "public_building"));
                    list.Add(new(this.BBox, "amenity", "pharmacy"));
                    list.Add(new(this.BBox, "amenity", "hospital"));
                    list.Add(new(this.BBox, "amenity", "doctors"));
                    list.Add(new(this.BBox, "amenity", "dentist"));
                    list.Add(new(this.BBox, "amenity", "veterinary"));
                    list.Add(new(this.BBox, "amenity", "theatre"));
                    list.Add(new(this.BBox, "amenity", "nightclub"));
                    list.Add(new(this.BBox, "amenity", "cinema"));
                    list.Add(new(this.BBox, "leisure", "park"));
                    list.Add(new(this.BBox, "leisure", "playground"));
                    list.Add(new(this.BBox, "leisure", "dog_park"));
                    list.Add(new(this.BBox, "leisure", "sports_centre"));
                    list.Add(new(this.BBox, "leisure", "pitch"));
                    list.Add(new(this.BBox, "amenity", "swimming_pool"));
                    list.Add(new(this.BBox, "leisure", "swimming_pool"));
                    list.Add(new(this.BBox, "sport", "swimming"));
                    list.Add(new(this.BBox, "leisure", "water_park"));
                    list.Add(new(this.BBox, "sport", "tennis"));
                    list.Add(new(this.BBox, "leisure", "golf_course"));
                    list.Add(new(this.BBox, "leisure", "stadium"));
                    list.Add(new(this.BBox, "leisure", "ice_rink"));
                    list.Add(new(this.BBox, "amenity", "restaurant"));
                    list.Add(new(this.BBox, "amenity", "fast_food"));
                    list.Add(new(this.BBox, "amenity", "cafe"));
                    list.Add(new(this.BBox, "amenity", "pub"));
                    list.Add(new(this.BBox, "amenity", "bar"));
                    list.Add(new(this.BBox, "amenity", "food_court"));
                    list.Add(new(this.BBox, "amenity", "biergarten"));
                    list.Add(new(this.BBox, "tourism", "hotel"));
                    list.Add(new(this.BBox, "tourism", "motel"));
                    list.Add(new(this.BBox, "tourism", "bed_and_breakfast"));
                    list.Add(new(this.BBox, "tourism", "guest_house"));
                    list.Add(new(this.BBox, "tourism", "hostel"));
                    list.Add(new(this.BBox, "tourism", "chalet"));
                    list.Add(new(this.BBox, "amenity", "shelter"));
                    list.Add(new(this.BBox, "tourism", "camp_site"));
                    list.Add(new(this.BBox, "tourism", "alpine_hut"));
                    list.Add(new(this.BBox, "tourism", "caravan_site"));
                    list.Add(new(this.BBox, "shop", "supermarket"));
                    list.Add(new(this.BBox, "shop", "bakery"));
                    list.Add(new(this.BBox, "shop", "kiosk"));
                    list.Add(new(this.BBox, "shop", "mall"));
                    list.Add(new(this.BBox, "shop", "department_store"));
                    list.Add(new(this.BBox, "shop", "general"));
                    list.Add(new(this.BBox, "shop", "convenience"));
                    list.Add(new(this.BBox, "shop", "clothes"));
                    list.Add(new(this.BBox, "shop", "florist"));
                    list.Add(new(this.BBox, "shop", "chemist"));
                    list.Add(new(this.BBox, "shop", "books"));
                    list.Add(new(this.BBox, "shop", "butcher"));
                    list.Add(new(this.BBox, "shop", "shoes"));
                    list.Add(new(this.BBox, "shop", "alcohol"));
                    list.Add(new(this.BBox, "shop", "beverages"));
                    list.Add(new(this.BBox, "shop", "optician"));
                    list.Add(new(this.BBox, "shop", "jewerly"));
                    list.Add(new(this.BBox, "shop", "gift"));
                    list.Add(new(this.BBox, "shop", "sports"));
                    list.Add(new(this.BBox, "shop", "stationery"));
                    list.Add(new(this.BBox, "shop", "outdoor"));
                    list.Add(new(this.BBox, "shop", "mobile_phone"));
                    list.Add(new(this.BBox, "shop", "toys"));
                    list.Add(new(this.BBox, "shop", "newsagent"));
                    list.Add(new(this.BBox, "shop", "greengrocer"));
                    list.Add(new(this.BBox, "shop", "beauty"));
                    list.Add(new(this.BBox, "shop", "video"));
                    list.Add(new(this.BBox, "shop", "car"));
                    list.Add(new(this.BBox, "shop", "bicycle"));
                    list.Add(new(this.BBox, "shop", "doityourself"));
                    list.Add(new(this.BBox, "shop", "hardware"));
                    list.Add(new(this.BBox, "shop", "furniture"));
                    list.Add(new(this.BBox, "shop", "computer"));
                    list.Add(new(this.BBox, "shop", "garden_centre"));
                    list.Add(new(this.BBox, "shop", "hairdresser"));
                    list.Add(new(this.BBox, "shop", "car_repair"));
                    list.Add(new(this.BBox, "amenity", "car_rental"));
                    list.Add(new(this.BBox, "amenity", "car_wash"));
                    list.Add(new(this.BBox, "amenity", "car_sharing"));
                    list.Add(new(this.BBox, "amenity", "bicycle_rental"));
                    list.Add(new(this.BBox, "amenity", "travel_agency"));
                    list.Add(new(this.BBox, "amenity", "laundry"));
                    list.Add(new(this.BBox, "amenity", "dry_cleaning"));
                    list.Add(new(this.BBox, "shop", "travel_agency"));
                    list.Add(new(this.BBox, "shop", "laundry"));
                    list.Add(new(this.BBox, "shop", "dry_cleaning"));
                    list.Add(new(this.BBox, "amenity", "vending_machine"));
                    list.Add(new(this.BBox, "amenity", "bank"));
                    list.Add(new(this.BBox, "amenity", "atm"));
                    list.Add(new(this.BBox, "tourist", "information"));
                    list.Add(new(this.BBox, "tourist", "attraction"));
                    list.Add(new(this.BBox, "historic", "monument"));
                    list.Add(new(this.BBox, "historic", "memorial"));
                    list.Add(new(this.BBox, "tourist", "artwork"));
                    list.Add(new(this.BBox, "historic", "castle"));
                    list.Add(new(this.BBox, "historic", "ruins"));
                    list.Add(new(this.BBox, "historic", "archaeological_site"));
                    list.Add(new(this.BBox, "historic", "wayside_criss"));
                    list.Add(new(this.BBox, "historic", "wayside_shrine"));
                    list.Add(new(this.BBox, "historic", "battlefield"));
                    list.Add(new(this.BBox, "historic", "fort"));
                    list.Add(new(this.BBox, "tourism", "picnic_site"));
                    list.Add(new(this.BBox, "tourism", "viewpoint"));
                    list.Add(new(this.BBox, "tourism", "zoo"));
                    list.Add(new(this.BBox, "tourism", "theme_park"));
                    list.Add(new(this.BBox, "amenity", "toilets"));
                    list.Add(new(this.BBox, "amenity", "bench"));
                    list.Add(new(this.BBox, "amenity", "drinking_water"));
                    list.Add(new(this.BBox, "amenity", "fountain"));
                    list.Add(new(this.BBox, "amenity", "hunting_stand"));
                    list.Add(new(this.BBox, "amenity", "waste_basket"));
                    list.Add(new(this.BBox, "man_made", "surveillance"));
                    list.Add(new(this.BBox, "amenity", "emergency_phone"));
                    list.Add(new(this.BBox, "emergency", "phone"));
                    list.Add(new(this.BBox, "amenity", "fire_hydrant"));
                    list.Add(new(this.BBox, "highway", "emergency_access_point"));
                    list.Add(new(this.BBox, "man_made", "tower"));
                    break;

                case TagType.TRAFFIC:
                    list.Add(new(this.BBox, "highway", "*"));
                    list.Add(new(this.BBox, "amenity", "*"));
                    list.Add(new(this.BBox, "leisure", "*"));
                    list.Add(new(this.BBox, "man_made", "*"));
                    list.Add(new(this.BBox, "waterway", "*"));
                    list.Add(new(this.BBox, "highway", "traffic_signals"));
                    list.Add(new(this.BBox, "highway", "mini_roundabou"));
                    list.Add(new(this.BBox, "highway", "stop"));
                    list.Add(new(this.BBox, "highway", "crossing"));
                    list.Add(new(this.BBox, "highway", "level_crossing"));
                    list.Add(new(this.BBox, "highway", "ford"));
                    list.Add(new(this.BBox, "highway", "motorway_junction"));
                    list.Add(new(this.BBox, "highway", "turning_circle"));
                    list.Add(new(this.BBox, "highway", "speed_camera"));
                    list.Add(new(this.BBox, "highway", "street_lamp"));
                    list.Add(new(this.BBox, "amenity", "fuel"));
                    list.Add(new(this.BBox, "highway", "services"));
                    list.Add(new(this.BBox, "amenity", "park"));
                    list.Add(new(this.BBox, "highway", "bicycle_parking"));
                    list.Add(new(this.BBox, "leisure", "slipway"));
                    list.Add(new(this.BBox, "leisure", "marina"));
                    list.Add(new(this.BBox, "man_made", "pier"));
                    list.Add(new(this.BBox, "waterway", "dam"));
                    list.Add(new(this.BBox, "waterway", "waterfall"));
                    list.Add(new(this.BBox, "waterway", "lock_gate"));
                    list.Add(new(this.BBox, "waterway", "weir"));
                    break;
                case TagType.TRANSPORT:
                    list.Add(new(this.BBox, "railway", "*"));
                    list.Add(new(this.BBox, "highway", "*"));
                    list.Add(new(this.BBox, "amenity", "*"));
                    list.Add(new(this.BBox, "military", "*"));
                    list.Add(new(this.BBox, "aeroway", "*"));
                    list.Add(new(this.BBox, "aerialway", "*"));
                    list.Add(new(this.BBox, "railway", "station"));
                    list.Add(new(this.BBox, "railway", "halt"));
                    list.Add(new(this.BBox, "railway", "tram_stop"));
                    list.Add(new(this.BBox, "highway", "bus_stop"));
                    list.Add(new(this.BBox, "amenity", "bus_station"));
                    list.Add(new(this.BBox, "amenity", "taxi"));
                    list.Add(new(this.BBox, "amenity", "airport"));
                    list.Add(new(this.BBox, "amenity", "airfield"));
                    list.Add(new(this.BBox, "military", "airfield"));
                    list.Add(new(this.BBox, "aeroway", "helipad"));
                    list.Add(new(this.BBox, "aeroway", "apron"));
                    list.Add(new(this.BBox, "amenity", "ferry_terminal"));
                    list.Add(new(this.BBox, "aerialway", "station"));
                    break;
            }

            this.Tags.Add(this.Type, list);
        }

        public OverPassPoint(string bbox, TagType type, Dictionary<string, List<string>>? query) : this(bbox, type)
        {
            this.Query = query;
        }

        public OverPassPoint(string bbox, TagType type, Dictionary<string, List<string>>? query, string? overpassUrl) : this(bbox, type, query)
        {
            this.OverPassUrl = overpassUrl;
        }

        /** Get attributes from respons eopenstreetmap */
        protected static AttributesTable GetProperties(Element e)
        {
            AttributesTable attributes = new AttributesTable();
            attributes.Add("id", e.id);
            if (e.tags != null)
                foreach (var p in e.tags.GetType().GetProperties())
                {
                    var value = p.GetValue(e.tags, null);
                    if (value != null)
                        attributes.Add(p.Name, value);
                }

            return attributes;
        }

        /** Get attributes from response openstreetmap */
        protected static NetTopologySuite.Geometries.Coordinate GetPoint(Element e)
        {
            return new(Convert.ToDouble(e.lon), Convert.ToDouble(e.lat));
        }

        public virtual string GeoJSon
        {
            get => OverPassUtility.SerializeFeatures(this.FeatureCollection);
        }

        public virtual NetTopologySuite.Features.FeatureCollection? FeatureCollection
        {
            get => OverPassUtility.FeatureCollection(this.Features);
        }

        public virtual List<NetTopologySuite.Features.Feature>? Features
        {
            get
            {
                Root? resp = this.Response;
                List<NetTopologySuite.Features.Feature>? features = new();
                
                if (resp != null && resp.elements != null)
                {
                    foreach (Element e in resp.elements)
                    {
                        if (e.type == "node")
                        {
                            NetTopologySuite.Geometries.Point geom = (NetTopologySuite.Geometries.Point)new(GetPoint(e));
                            NetTopologySuite.Features.Feature? f = new(geom, GetProperties(e));
                            features.Add(f);
                        }
                    }

                    return features;
                }
                else
                    return null;
            }
        }
    }
}

