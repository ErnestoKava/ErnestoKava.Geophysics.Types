using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErnestoKava.Geophysics.Types.Vectors
{
	public class Vector : IEnumerable<double>
	{
		public virtual int Length => 1;
		public double X { get; set; }

		public Vector(){ }
		public Vector(double x)
		{
			X = x;
		}

		public virtual double this[int i]
		{
			get
			{
				if (i == 0)
					return X;
				
				throw new InvalidOperationException();
			}
			set
			{
				if (i == 0)
				{
					X = value;
					return;
				}

				throw new InvalidOperationException();
			}
		}

		public IEnumerator<double> GetEnumerator()
		{
			for (int i = 0; i < Length; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public static T Multiply<T>(double a, T b) where T : Vector, new()
		{
			var result = new T();
			for (int i = 0; i < b.Length; i++)
				result[i] = a * b[i];

			return result;
		}

		public static T Plus<T>(T a, T b) where T : Vector, new()
		{
			var result = new T();
			for (int i = 0; i < b.Length; i++)
				result[i] = a[i] + b[i];

			return result;
		}

		public static T Minus<T>(T a, T b) where T : Vector, new()
		{
			var result = new T();
			for (int i = 0; i < b.Length; i++)
				result[i] = a[i] - b[i];

			return result;
		}

		public static T Multiply<T>(T a, T b) where T : Vector, new()
		{
			var result = new T();
			for (int i = 0; i < b.Length; i++)
				result[i] = a[i] * b[i];

			return result;
		}

		public static T Div<T>(T a, T b) where T : Vector, new()
		{
			var result = new T();
			for (int i = 0; i < b.Length; i++)
			{
				if (b[i] == 0)
					throw new DivideByZeroException();

				result[i] = a[i] / b[i];
			}

			return result;
		}
	}
}
