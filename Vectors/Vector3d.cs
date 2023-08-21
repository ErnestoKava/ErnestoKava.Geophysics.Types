using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErnestoKava.Geophysics.Types.Vectors
{
	public class Vector3d : Vector2d
	{
		public override int Length => 3;
		public double Z { get; set; }

		public Vector3d(double x, double y, double z)
			: base(x, y)
		{
			Z = z;
		}

		public override double this[int i]
		{
			get
			{
				if (i == 0) return X;
				if (i == 1) return Y;
				if (i == 2) return Z;

				throw new InvalidOperationException();
			}
			set
			{
				if (i == 0) X = value;
				else if (i == 1) Y = value;
				else if (i == 2) Z = value;

				throw new InvalidOperationException();
			}
		}
	}
}
