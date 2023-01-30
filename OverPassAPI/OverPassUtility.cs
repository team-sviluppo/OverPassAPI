using System;
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
    }
}

