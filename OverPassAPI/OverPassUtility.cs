using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using ProjNet.CoordinateSystems.Transformations;

namespace OverPass.Utility
{
    public class OverPassUtility
	{
		public OverPassUtility()
		{
		}

        public static Root? JSonDeserializeResponse(string json)
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringReader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return serializer.Deserialize<Root>(jsonReader);
            }
        }

        public static string SerializeFeatures(NetTopologySuite.Features.FeatureCollection? features)
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                serializer.Serialize(jsonWriter, features);
                return stringWriter.ToString();
            }
        }

        public static NetTopologySuite.Geometries.Geometry? DeSerializeGeometry(string geometry)
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringReader = new StringReader(geometry))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return serializer.Deserialize<NetTopologySuite.Geometries.Geometry>(jsonReader);
            }
        }

        public static NetTopologySuite.Features.FeatureCollection? FeatureCollection(List<NetTopologySuite.Features.Feature>? features)
        {
            if (features is not null)
            {
                NetTopologySuite.Features.FeatureCollection fColl = new();
                foreach (NetTopologySuite.Features.Feature f in features)
                    fColl.Add(f);
                return fColl;
            }

            return null;
        }

        /** Get attributes from respons eopenstreetmap */
        public static AttributesTable GetProperties(Element e)
        {
            AttributesTable attributes = new();
            if (e.id is not null)
                attributes.Add("id", e.id);
            if (e.tags is not null)
                foreach (var p in e.tags.GetType().GetProperties())
                {
                    var value = p.GetValue(e.tags, null);
                    if (value is not null)
                        attributes.Add(p.Name, value);
                }

            return attributes;
        }

        public static GeometryFactory CreateGeometryFactory(int? code)
        {
            int c = 3857;
            if (code != null) c = (int)code;
            return NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(c);
        }

        /** fetch data by OpenStreetMap Data */
        public static async Task<string?> RunOverPassQuery(string url, string query)
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

        /** convert coordinate from WGS84 (EPSG:4326) to EPSG:3857 */
        public static Coordinate ConvertCoordinatesToWebMercator(double[] coords)
        {
            List<Coordinate> bounds = new();

            /** convert from WGS84 (EPSG:4326) to EPSG:3857 */
            var cs4326 = ProjNet.CoordinateSystems.GeographicCoordinateSystem.WGS84;
            var cs3857 = ProjNet.CoordinateSystems.ProjectedCoordinateSystem.WebMercator;

            // Transformation
            var ctfac = new CoordinateTransformationFactory();
            var trans = ctfac.CreateFromCoordinateSystems(cs4326, cs3857);

            double[] fromPoint = new double[] { coords[0], coords[1] };
            double[] toPoint = trans.MathTransform.Transform(fromPoint);
            return new Coordinate(toPoint[0], toPoint[1]);
        }

        /** Get attributes from response openstreetmap */
        public static Coordinate GetPoint(double[] coords, bool ToWebMercator)
        {
            if (ToWebMercator)
                return ConvertCoordinatesToWebMercator(coords);
            else
                return new(coords[0], coords[1]);
        }

        /** get coordinate from element openstreetmap response */
        public static Coordinate[]? GetCoordinates(Element e, bool ToWebMercator)
        {
            List<Coordinate>? points = new();

            if (e.geometry is not null)
            {
                foreach (Geometry g in e.geometry)
                {
                    double[]? coords = new double[] { Convert.ToDouble(g.lon), Convert.ToDouble(g.lat) };
                    Coordinate p = GetPoint(coords, ToWebMercator);
                    points.Add(p);
                }
                return points.ToArray();
            }

            return null;
        }

        /** geo bbox */
        public static Envelope? GetBBox(Element e)
        {
            if (e.bounds is not null)
            {
                Coordinate minCoordinate = new(Convert.ToDouble(e.bounds.minlon), Convert.ToDouble(e.bounds.minlat));
                Coordinate maxCoordinate = new(Convert.ToDouble(e.bounds.maxlon), Convert.ToDouble(e.bounds.maxlat));
                Envelope result = new(minCoordinate, maxCoordinate);
                return result;
            }

            return null;
        }

        public static Dictionary<TagType, List<OTag>>? UnionTags(Dictionary<TagType, List<OTag>>? a, Dictionary<TagType, List<OTag>>? b)
        {
            if (b is not null)
            {
                if (a is null)
                    return b;
                else
                    foreach (var item in b)
                    {
                        bool keyExists = a!.TryGetValue(item.Key, out List<OTag>? value);
                        if (!keyExists)
                            a.Add(item.Key, new());
                        if (item.Value != null)
                            a[item.Key].AddRange(item.Value);
                    }
            }

            return a;
        }
    }
}

