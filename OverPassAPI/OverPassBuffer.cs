using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace OverPass
{
	public class OverPassBuffer : OverPassAPI
	{
		public double Buffer { get; set; } = 10.0; /** meters */

        public OverPassBuffer(string bbox, double buffer) : base(bbox)
        {
            this.Buffer = buffer;
        }

        public OverPassBuffer(string bbox, double buffer, Dictionary<string, List<string>>? query) : base(bbox, query)
        {
            this.Buffer = buffer;
        }

        public OverPassBuffer(string bbox, double buffer, Dictionary<string, List<string>>? query, string overpassUrl) : base(bbox, query, overpassUrl)
        {
            this.Buffer = buffer;
        }

        public override NetTopologySuite.Features.FeatureCollection? FeatureCollection
		{
			get
			{
				if (this.Features != null)
				{
					NetTopologySuite.Features.FeatureCollection? fColl = new();

					foreach (NetTopologySuite.Features.Feature f in this.Features)
					{
						NetTopologySuite.Features.Feature newF = f;
						newF.Geometry = f.Geometry.Buffer(this.Buffer * 0.00001, 8);
						fColl.Add(newF);
					}

					return fColl;
				}
				else
					return null;
            }
		}
    }
}

