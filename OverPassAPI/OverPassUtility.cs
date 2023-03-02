using System;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace OverPass.Utility
{
	public class OverPassUtility
	{
		public OverPassUtility()
		{
		}

        public static Root JSonDeserializeResponse(string json)
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

        public static NetTopologySuite.Geometries.Geometry DeSerializeGeometry(string geometry)
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringReader = new StringReader(geometry))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return serializer.Deserialize<NetTopologySuite.Geometries.Geometry>(jsonReader);
            }
        }

        public static string SerializeGeometry(NetTopologySuite.Geometries.Geometry? geometry)
        {
            var serializer = GeoJsonSerializer.Create();
            using (var stringWriter = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(stringWriter))
            {
                serializer.Serialize(jsonWriter, geometry);
                return stringWriter.ToString();
            }
        }

        public static NetTopologySuite.Features.FeatureCollection? FeatureCollection(List<NetTopologySuite.Features.Feature>? features)
        {
            if (features != null)
            {
                NetTopologySuite.Features.FeatureCollection fColl = new();
                foreach (NetTopologySuite.Features.Feature f in features)
                    fColl.Add(f);
                return fColl;
            }
            else
                return null;
        }

        /** Get attributes from respons eopenstreetmap */
        public static AttributesTable GetProperties(Element e)
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
        public static Coordinate GetPoint(Element e)
        {
            return new(Convert.ToDouble(e.lon), Convert.ToDouble(e.lat));
        }

        public static NetTopologySuite.Geometries.Coordinate[]? GetCoordinates(Element e)
        {
            List<NetTopologySuite.Geometries.Coordinate>? points = new();

            if (e.geometry != null)
            {
                foreach (Geometry g in e.geometry)
                {
                    NetTopologySuite.Geometries.Coordinate p = new(Convert.ToDouble(g.lon), Convert.ToDouble(g.lat));
                    points.Add(p);
                }
                return points.ToArray();
            }
            else
                return null;
        }

        public static NetTopologySuite.Geometries.Envelope? GetBBox(Element e)
        {
            if (e.bounds != null)
            {
                NetTopologySuite.Geometries.Coordinate minCoordinate = new(Convert.ToDouble(e.bounds.minlon), Convert.ToDouble(e.bounds.minlat));
                NetTopologySuite.Geometries.Coordinate maxCoordinate = new(Convert.ToDouble(e.bounds.maxlon), Convert.ToDouble(e.bounds.maxlat));
                NetTopologySuite.Geometries.Envelope result = new(minCoordinate, maxCoordinate);
                return result;
            }
            else
                return null;
        }

        public static Dictionary<TagType, List<OTag>>? UnionTags(Dictionary<TagType, List<OTag>>? a, Dictionary<TagType, List<OTag>>? b)
        {
            if (b != null)
            {
                if (a == null)
                    return b;
                else
                    foreach (var item in b)
                    {
                        List<OTag>? value = null;
                        bool keyExists = a!.TryGetValue(item.Key, out value);
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

