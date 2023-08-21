using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErnestoKava.Geophysics.Types.Functions
{
	public class LinearPiecewiseFunction : Function
	{
		public struct ABPair
		{
			public double A;
			public double B;
		}

		private double[] _boundingValues;
		private ABPair[] _linearFunctionParams;
		private bool _rightOpenInterval;

		public LinearPiecewiseFunction(double[] boundingValues, ABPair[] linearFunctionParams, bool rightOpenInterval = true)
		{
			_boundingValues = boundingValues;
			_linearFunctionParams = linearFunctionParams;
			_rightOpenInterval = rightOpenInterval;
		}

		public override double GetValue(double arg)
		{
			Func<double, int> findIntervalIndexForValue = d => {
				for (int i = 0; i < _boundingValues.Length - 1; i++)
				{
					var interval = new Tuple<double, double>(_boundingValues[i], _boundingValues[i + 1]);
					if (_rightOpenInterval && d == interval.Item1)
						return i;

					if (interval.Item1 < arg && arg < interval.Item2)
						return i;

					if (!_rightOpenInterval && d == interval.Item2)
						return i;
				}

				return -1;
			};

			var index = findIntervalIndexForValue(arg);

			return _linearFunctionParams[index].A * arg + _linearFunctionParams[index].B;

		}
	}
}
