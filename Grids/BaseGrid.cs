using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErnestoKava.Geophysics.Types.Grids;

namespace ErnestoKava.Geophysics.Types.Grids
{
	public class GridPoint
	{
		private BaseGrid _grid;
		private double _value;
		public int i { get; private set; }
		public int j { get; private set; }
		public int k { get; private set; }
		public double X { get; private set; }
		public double Y { get; private set; }
		public double Z { get; private set; }
		public bool IsBlank { get; private set; }

		public double Value
		{
			get { return _value; }
			set
			{
				_value = value;
				if (_grid is Grid)
				{
					var g = _grid as Grid;
					g.data[i, j] = value;
				}
				else if (_grid is Grid3d)
				{
					var g = _grid as Grid3d;
					g.Data[i, j, k] = value;
				}
				else
					throw new Exception("Unknown grid type");
			}
		}

		public GridPoint(int i, int j, double value, Grid grid)
		{
			_grid = grid;
			this.i = i;
			this.j = j;
			this.k = 0;

			this._value = value;
			this.IsBlank = value == grid.Blank;

			X = grid.xLL + i * grid.xSize;
			Y = grid.yLL + j * grid.ySize;
			Z = 0;
		}

		public GridPoint(int i, int j, int k, double value, Grid3d grid)
		{
			_grid = grid;
			this.i = i;
			this.j = j;
			this.k = k;

			this._value = value;
			this.IsBlank = value == grid.Blank;

			X = grid.xLL + i * grid.xSize;
			Y = grid.yLL + j * grid.ySize;
			Z = grid.zTop + k * grid.zSize;
		}
	}

	public abstract class BaseGrid : IEnumerable<GridPoint>
	{
		public const double DefaultBlank = 1.70141E+38;

		public double _xLL, _yLL, XSize, YSize, _blank;

		public int nRow { get; protected set; }
		public int nCol { get; protected set; }

		public double Blank
		{
			get { return _blank; }
			set { _blank = value; }
		}

		public double ySize
		{
			get { return YSize; }
		}

		public double xSize
		{
			get { return XSize; }
		}

		public double yLL
		{
			get { return _yLL; }
		}

		public double xLL
		{
			get { return _xLL; }
		}

		public double yUR
		{
			// -1 is correct!
			get { return _yLL + (nRow - 1) * YSize; }
		}

		public double xUR
		{
			// -1 is correct!
			get { return _xLL + (nCol - 1) * XSize; }
		}

		public abstract IEnumerator<GridPoint> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
