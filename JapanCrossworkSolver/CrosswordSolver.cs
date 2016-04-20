using JapanCrossworkSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapanCrossworkSolver
{
	public class CrosswordSolver
	{
		public int Width { get; set; }
		public int Height { get; set; }

		public CageType[,] Cages { get; set; }
		public int[][] TopNumbers { get; set; }
		public int[][] LeftNumbers { get; set; }

		public CrosswordSolver(int[][] topNumbers, int[][] leftNumbers)
		{
			TopNumbers = topNumbers;
			LeftNumbers = leftNumbers;

			Width = topNumbers.GetLength(0);
			Height = leftNumbers.GetLength(0);
		}

		public void Solve()
		{
			while (!IsCompleted())
			{
				for (int i = 0; i < Height; i++)
				{
					ApplyAllToRow(i);
				}
				for (int i = 0; i < Width; i++)
				{
					ApplyAllToColumn(i);
				}
			}
		}

		public void ApplyMethodForRow(int rowIndex, Func<CageType[], int[], CageType[]> method)
		{
			CageType[] row = new CageType[Width];
			for (int i = 0; i < Width; i++)
			{
				row[i] = Cages[rowIndex, i];
			}
			row = method(row, LeftNumbers[rowIndex]);
			for (int i = 0; i < Width; i++)
			{
				Cages[rowIndex, i] = row[i];
			}
		}
		public void ApplyMethodForColumn(int columnIndex, Func<CageType[], int[], CageType[]> method)
		{
			CageType[] column = new CageType[Height];
			for (int i = 0; i < Width; i++)
			{
				column[i] = Cages[columnIndex, i];
			}
			column = method(column, LeftNumbers[columnIndex]);
			for (int i = 0; i < Width; i++)
			{
				Cages[columnIndex, i] = column[i];
			}
		}

		public void ApplyAllToRow(int rowIndex)
		{
			ApplyMethodForRow(rowIndex, Method0001);
			ApplyMethodForRow(rowIndex, Method0002);
		}
		public void ApplyAllToColumn(int columnIndex)
		{
			ApplyMethodForColumn(columnIndex, Method0001);
			ApplyMethodForColumn(columnIndex, Method0002);
		}

		public bool IsCompleted()
		{
			for (int i = 0; i < Height; i++)
			{
				for (int j = 0; j < Width; j++)
				{
					if (Cages[i, j] == CageType.White)
					{
						return false;
					}
				}
			}
			return true;
		}




		public CageType[] Method0001(CageType[] cages, int[] numbers)
		{
			throw new NotImplementedException();
		}

		public CageType[] Method0002(CageType[] cages, int[] numbers)
		{
			throw new NotImplementedException();
		}
	}
}
