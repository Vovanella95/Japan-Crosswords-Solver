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
		public CageType[] ArrayOfCages
		{
			get
			{
				return Cages.Cast<CageType>().Reverse().ToArray();
			}
		}

		public int[][] TopNumbers { get; set; }
		public int[][] LeftNumbers { get; set; }

		public CrosswordSolver(int[][] topNumbers, int[][] leftNumbers)
		{
			TopNumbers = topNumbers;
			LeftNumbers = leftNumbers;

			Width = topNumbers.GetLength(0);
			Height = leftNumbers.GetLength(0);

			Cages = new CageType[Height, Width];
		}

		public void MakeStep()
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
			for (int i = 0; i < Height; i++)
			{
				column[i] = Cages[i, columnIndex];
			}
			column = method(column, TopNumbers[columnIndex]);
			for (int i = 0; i < Height; i++)
			{
				Cages[i, columnIndex] = column[i];
			}
		}

		public void ApplyAllToRow(int rowIndex)
		{
			ApplyMethodForRow(rowIndex, Method0001);
			ApplyMethodForRow(rowIndex, Method0002);
			ApplyMethodForRow(rowIndex, Method0003_CheckForFullLines);
			ApplyMethodForRow(rowIndex, Method0004_DrawOnSides);
		}
		public void ApplyAllToColumn(int columnIndex)
		{
			ApplyMethodForColumn(columnIndex, Method0001);
			ApplyMethodForColumn(columnIndex, Method0002);
			ApplyMethodForColumn(columnIndex, Method0003_CheckForFullLines);
			ApplyMethodForColumn(columnIndex, Method0004_DrawOnSides);
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


		// Simple drawing cages in lines using first rule
		public CageType[] Method0001(CageType[] cages, int[] numbers)
		{
			var l = numbers.Sum() + numbers.Length - 1;
			var L = cages.Length;
			var n = L - l;

			int counter = 0;
			foreach (var number in numbers)
			{
				if (number > n)
				{
					int skipedIndex = counter + n;
					for (int i = 0; i < number - n; i++)
					{
						cages[skipedIndex] = CageType.Black;
						skipedIndex++;
					}

				}
				counter += (1 + number);
			}

			return cages;
		}

		public CageType[] Method0002(CageType[] cages, int[] numbers)
		{
			if (cages[0] == CageType.Black)
			{
				for (int i = 0; i < numbers[0]; i++)
				{
					cages[i] = CageType.Black;
				}
				if (cages.Length >= numbers[0])
				{
					cages[numbers[0]] = CageType.Point;
				}
			}

			if (cages[cages.Length - 1] == CageType.Black)
			{
				for (int i = 0; i < numbers[0]; i++)
				{
					cages[cages.Length - 1 - i] = CageType.Black;
				}
				if (cages.Length >= numbers[0])
				{
					cages[cages.Length - 1 - numbers[0]] = CageType.Point;
				}
			}

			return cages;
		}

		public CageType[] Method0003_CheckForFullLines(CageType[] cages, int[] numbers)
		{
			var coloredCount = cages.Where(w => w == CageType.Black).Count();
			var numbersCount = numbers.Sum();

			if (coloredCount == numbersCount)
			{
				for (int i = 0; i < cages.Length; i++)
				{
					if (cages[i] == CageType.White)
					{
						cages[i] = CageType.Point;
					}
				}
			}
			return cages;
		}

		public CageType[] Method0004_DrawOnSides(CageType[] cages, int[] numbers)
		{
			// leftSide
			var firstPoint = 0;
			while (firstPoint < cages.Length && cages[firstPoint] != CageType.Point)
			{
				firstPoint++;
			}

			if (numbers[0] * 2 > firstPoint - 1 && numbers[0] <= firstPoint)
			{
				var skipIndex = firstPoint - numbers[0];
				for (int i = 0; i < 2 * numbers[0] - firstPoint; i++)
				{
					cages[skipIndex] = CageType.Black;
					skipIndex++;
				}
			}



			return cages;
		}
	}
}
