using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErnestoKava.Geophysics.Types.Vectors
{
	public class Vector2d : Vector
	{
		public override int Length => 2;
		public double Y { get; set; }
		public Vector2d(double x, double y)
			: base(x)
		{
			Y = y;
		}

		public override double this[int i]
		{
			get
			{
				if (i == 0) return X;
				if (i == 1) return Y;

				throw new InvalidOperationException();
			}
			set
			{
				if (i == 0) X = value;
				else if (i == 1) Y = value;

				throw new InvalidOperationException();
			}
		}
	}
}
