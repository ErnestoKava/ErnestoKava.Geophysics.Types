using System;
using System.Collections.Generic;
using System.Linq;

namespace ErnestoKava.Geophysics.Types.Grids
{
	public class Grid3d : BaseGrid
	{
		public double zTop { get; set; }
		public double zSize { get; set; }
		public int nLayer { get; set; }
		public double[, ,] Data;

		public override IEnumerator<GridPoint> GetEnumerator()
		{
			for (int i = 0; i < nCol; i++)
				for (int j = 0; j < nRow; j++)
					for (int k = 0; k < nLayer; k++)
					{
						yield return new GridPoint(i, j, k, Data[i, j, k], this);
					}
		}

		private void Initialize(int nCol, int nRow, int nLayer, double xLL, double yLL, double zTop, double xSize, double ySize, double zSize)
		{
			_blank = DefaultBlank;

			this.nCol = nCol;
			this.nRow = nRow;
			this.nLayer = nLayer;

			this._xLL = xLL;
			this._yLL = yLL;

			this.zTop = zTop;

			this.XSize = xSize;
			this.YSize = ySize;
			this.YSize = zSize;

			Data = new double[nCol, nRow, nLayer];
		}

		public Grid3d(Grid3d cube) :
			this(cube.nCol, cube.nRow, cube.nLayer, cube.xLL, cube.yLL, cube.zTop, cube.xSize, cube.ySize, cube.zSize)
		{
			// Copy constructor
		}

		public Grid3d(int nCol, int nRow, int nLayer, double xLL, double yLL, double zTop, double xSize, double ySize, double zSize)
		{
			Initialize(nCol, nRow, nLayer, xLL, yLL, zTop, xSize, ySize, zSize);
		}

		public Grid3d(Grid[] grids, double zTop, double zSize)
		{
			if (!grids.Any())
				throw new Exception("Empty grid list");

			Grid grid = grids[0];

			Initialize(grid.nCol, grid.nRow, grids.Count(), grid.xLL, grid.yLL, zTop, grid.xSize, grid.ySize, zSize);
			for (int i = 0; i < grids.Count(); i++)
			{
				for (int x = 0; x < grid.nCol; x++)
					for (int y = 0; y < grid.nRow; y++)
					{
						Data[x, y, i] = grids[i].data[x, y];
					}
			}
		}

		public Grid ExtractPlane(int height)
		{
			double[,] plane = new double[nCol, nRow];
			for (int i = 0; i < nCol; i++)
				for (int j = 0; j < nRow; j++)
					plane[i, j] = Data[i, j, height];

			return new Grid(this, plane);
		}
	}
}
