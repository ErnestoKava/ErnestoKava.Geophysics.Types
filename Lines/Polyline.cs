using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErnestoKava.Geophysics.Types.Vectors;

namespace ErnestoKava.Geophysics.Types.Lines
{
	public class Polyline<V>
		where V : Vector
	{
		public class PointDescriptoinPair
		{
			public V Point { get; set; }
			public string Description { get; set; }

			public PointDescriptoinPair(V point, string description = null)
			{
				Point = point;
				Description = description;
			}

			public PointDescriptoinPair() {}
		}

		public string Title { get; set; }
		public List<PointDescriptoinPair> Points { get; } = new List<PointDescriptoinPair>();
	}
}
