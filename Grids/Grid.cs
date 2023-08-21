using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ErnestoKava.Geophysics.Types.Grids
{
	public partial class Grid : BaseGrid
	{
		public double[,] data;

		#region Property

		public double xRR { get { return xLL + (nCol - 1) * xSize; } }
		public double yRR { get { return yLL + (nRow - 1) * ySize; } }

		#endregion

		public Grid(int nCol, int nRow, double xLL, double yLL, double xSize, double ySize):
			this(new double[nCol, nRow], xLL, yLL, xSize, ySize)
		{
		}

		public Grid(BaseGrid sample, double[,] data = null)
			: this(new double[sample.nCol, sample.nRow], sample.xLL, sample.yLL, sample.xSize, sample.ySize)
		{
			this.data = data;
		}

		public Grid(double[,] data, double xLL, double yLL, double xSize, double ySize)
		{
			this.data = data;

			this._xLL = xLL;
			this._yLL = yLL;
			this.XSize = xSize;
			this.YSize = ySize;
			this._blank = DefaultBlank;

			this.nCol = data.GetLength(0);
			this.nRow = data.GetLength(1);
		}

		public double this[double x, double y]
		{
			get
			{
				if (x < _xLL)
					return _blank;
				if (y < _yLL)
					return _blank;
				if (x > (_xLL + XSize * (nCol - 1)))
					return _blank;
				if (y > (_yLL + YSize * (nRow - 1)))
					return _blank;

				double nCol_ = ((x - _xLL) / XSize);
				double nRow_ = ((y - _yLL) / YSize);

				int nColRounded = (int)Math.Round(nCol_);
				int nRowRounded = (int)Math.Round(nRow_);

				return data[nColRounded, nRowRounded];
			}
			set
			{
				if (x < _xLL)
					return;
				if (y < _yLL)
					return;
				if (x > (_xLL + XSize * (nCol - 1)))
					return;
				if (y > (_yLL + YSize * (nRow - 1)))
					return;

				double nCol_ = ((x - _xLL) / XSize);
				double nRow_ = ((y - _yLL) / YSize);

				int nColRounded = (int)Math.Round(nCol_);
				int nRowRounded = (int)Math.Round(nRow_);
				data[nColRounded, nRowRounded] = value;
			}
		}

		public Grid Clone()
		{
			var grd = new Grid(this, new double[nCol,nRow]);
			for (int x = 0; x < nCol; x++)
				for (int y = 0; y < nRow; y++)
					grd.data[x, y] = data[x, y];

			return grd;
		}

		public override IEnumerator<GridPoint> GetEnumerator()
		{
			for (int i = 0; i < nCol; i++)
				for (int j = 0; j < nRow; j++)
				{
					yield return new GridPoint(i, j, data[i, j], this);
				}
		}
	}
}
