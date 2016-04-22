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
				var transponesCages = new CageType[Width, Height];
				for (int i = 0; i < Width; i++)
				{
					for (int j = 0; j < Height; j++)
					{
						transponesCages[j, i] = Cages[i, j];
					}
				}
				return transponesCages.Cast<CageType>().ToArray();
			}
		}

		private List<Func<CageType[], int[], CageType[]>> Methods;

		public int[][] TopNumbers { get; set; }
		public int[][] LeftNumbers { get; set; }

		public CrosswordSolver(int[][] topNumbers, int[][] leftNumbers)
		{
			TopNumbers = topNumbers;
			LeftNumbers = leftNumbers;

			Width = topNumbers.GetLength(0);
			Height = leftNumbers.GetLength(0);

			Cages = new CageType[Height, Width];
			Methods = new List<Func<CageType[], int[], CageType[]>>();

			InitializeMethods();
		}

		private void InitializeMethods()
		{
			Methods.Add(Method0001);
			Methods.Add(Method0002);
			Methods.Add(Method0003_CheckForFullLines);
			Methods.Add(Method0004_DrawOnSides);
			Methods.Add(Method0005_CheckForMaxValues);
			Methods.Add(Method0006_FillAllFreeWhiteToBlack);
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
			foreach (var method in Methods)
			{
				ApplyMethodForRow(rowIndex, method);
			}
		}
		public void ApplyAllToColumn(int columnIndex)
		{
			foreach (var method in Methods)
			{
				ApplyMethodForColumn(columnIndex, method);
			}
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


		// Самый простой метод считает справа и слева числа и заполняет
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

		// Если скраю поля черная клетка - дорисовывает число начиная с нее
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
				for (int i = 0; i < numbers[numbers.Length - 1]; i++)
				{
					cages[cages.Length - 1 - i] = CageType.Black;
				}
				if (cages.Length >= numbers[numbers.Length - 1])
				{
					cages[cages.Length - 1 - numbers[numbers.Length - 1]] = CageType.Point;
				}
			}

			return cages;
		}

		// Проверяет если полоса зарисована - доставляет точки в пустые клетки
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

		// Дорисовывает черные линии по краям если они немножко помещаются
		public CageType[] Method0004_DrawOnSides(CageType[] cages, int[] numbers)
		{
			var firstPoint = 0;
			while (firstPoint < cages.Length && cages[firstPoint] != CageType.Point)
			{
				firstPoint++;
			}

			if (firstPoint > 0 && cages[firstPoint - 1] == CageType.Black && numbers[0] * 2 > firstPoint - 1 && numbers[0] <= firstPoint)
			{
				var skipIndex = firstPoint - numbers[0];
				for (int i = 0; i < 2 * numbers[0] - firstPoint; i++)
				{
					cages[skipIndex] = CageType.Black;
					skipIndex++;
				}
			}

			cages = cages.Reverse().ToArray();
			numbers = numbers.Reverse().ToArray();

			firstPoint = 0;
			while (firstPoint < cages.Length && cages[firstPoint] != CageType.Point)
			{
				firstPoint++;
			}

			if (firstPoint > 0 && cages[firstPoint - 1] == CageType.Black && numbers[0] * 2 > firstPoint - 1 && numbers[0] <= firstPoint)
			{
				var skipIndex = firstPoint - numbers[0];
				for (int i = 0; i < 2 * numbers[0] - firstPoint; i++)
				{
					cages[skipIndex] = CageType.Black;
					skipIndex++;
				}
			}




			return cages.Reverse().ToArray();
		}

		// Если на линии есть n черных подряд а в числах нет числа больше n то по краям ставятся точки
		public CageType[] Method0005_CheckForMaxValues(CageType[] cages, int[] numbers)
		{
			var maxValue = numbers.Max();

			var tempLengthCounter = 0;
			var tempIndexCounter = 0;
			var isFound = false;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					if (!isFound)
					{
						tempIndexCounter = i;
						tempLengthCounter = 1;
						isFound = true;
					}
					else
					{
						tempLengthCounter++;
					}
				}
				else
				{
					if (tempLengthCounter == maxValue)
					{
						if (i != cages.Length - 1)
						{
							cages[i] = CageType.Point;
						}
						if (tempIndexCounter != 0)
						{
							cages[tempIndexCounter - 1] = CageType.Point;
						}
					}
					tempLengthCounter = 0;
					isFound = false;
				}
			}
			return cages;
		}

		// Если на линии можно зарисовать не точки черными клетками и все совпадет
		public CageType[] Method0006_FillAllFreeWhiteToBlack(CageType[] cages, int[] numbers)
		{
			var freeCount = cages.Where(w => w != CageType.Point).Count();
			if (freeCount == numbers.Sum())
			{
				for (int i = 0; i < cages.Length; i++)
				{
					if (cages[i] == CageType.White)
					{
						cages[i] = CageType.Black;
					}
				}
			}
			return cages;
		}

		public CageType[] Method0007_PointsForBigNumbersOnSides(CageType[] cages, int[] numbers)
		{
			var leftNumber = numbers[0];

			var blackIndex = 0;
			var blackCount = 0;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					blackIndex = i;
				}
				if(cages[i] != CageType.Black)
				{
					if(blackIndex != 0)
					{
						
					}
				}
			}

			if (blackIndex <= leftNumber)
			{

			}

			throw new NotImplementedException();
		}
	}
}
