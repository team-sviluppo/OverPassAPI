using System;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace OverPass
{
	public class OverPassBuffer : OverPassAPI
	{
		public double Buffer { get; set; } = 10.0; /** meters */

        public OverPassBuffer(string bbox) : base(bbox)
        {
        }

        public OverPassBuffer(string bbox, Dictionary<string, List<string>>? query) : base(bbox)
        {  
        }

        public OverPassBuffer(NetTopologySuite.Geometries.Geometry filter) : base(filter)
        {
        }

        public OverPassBuffer(NetTopologySuite.Geometries.Geometry filter, Dictionary<string, List<string>>? query) : base(filter)
        {
        }

        public OverPassBuffer(NetTopologySuite.Geometries.Geometry filter, string bbox) : base(bbox)
        {
        }

        public void SetUpBuffer(double buffer)
        {
            this.Buffer = buffer;
        }

        public override async Task<NetTopologySuite.Features.FeatureCollection?> FeatureCollection()
		{
		    List<NetTopologySuite.Features.Feature>? features = await this.Features();
            NetTopologySuite.Features.FeatureCollection? fColl = new();

			foreach (NetTopologySuite.Features.Feature f in features!)
			{
				NetTopologySuite.Features.Feature newF = f;
				newF.Geometry = f.Geometry.Buffer(this.Buffer * 0.00001, 8);
				fColl.Add(newF);
			}

            return fColl;
        }
    }
}

