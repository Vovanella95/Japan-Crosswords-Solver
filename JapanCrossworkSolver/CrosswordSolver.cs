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
		string log = string.Empty;

		public int Width { get; set; }
		public int Height { get; set; }

		public CageType[,] Cages { get; set; }
		public CageType[] ArrayOfCages
		{
			get
			{
				var transponesCages = new CageType[Width, Height];
				for (int i = 0; i < Height; i++)
				{
					for (int j = 0; j < Width; j++)
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
			Methods.Add(Method0004_DrawOnSidesReversed);
			Methods.Add(Method0005_CheckForMaxValues);
			Methods.Add(Method0006_FillAllFreeWhiteToBlack);
			Methods.Add(Method0007_UpDrawOneNumber);
			Methods.Add(Method0008_PickPointsWhereCannotBeBlack);
			Methods.Add(Method0009_FindMaxValueAndPickPoints);
			Methods.Add(Method0010_FillMinSpacesBetweenPoints);
			Methods.Add(Method0011_DrawLinesIfItHaveBegin);
			Methods.Add(Method0012_DrawBeginIfIsItBegin);
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
			row = ApplyMethodForLine(row, LeftNumbers[rowIndex], method);
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
			column = ApplyMethodForLine(column, TopNumbers[columnIndex], method);
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

		private CageType[] ApplyMethodForLine(CageType[] cages, int[] numbers, Func<CageType[], int[], CageType[]> method)
		{
			if (Cages[0, 7] == CageType.Black)
			{

			}

			if (cages.All(w => w != CageType.White))
			{
				return method(cages, numbers);
			}

			int startIndex = 0;
			int endIndex = cages.Length - 1;
			for (int i = 0; i < cages.Length - 1 && cages[i] != CageType.White; i++)
			{
				startIndex++;
			}
			for (int i = cages.Length - 1; i >= 0 && cages[i] != CageType.White; i--)
			{
				endIndex--;
			}

			if (startIndex != 0 && cages[startIndex - 1] == CageType.Black)
			{
				for (int i = startIndex; i > 0 && cages[i - 1] == CageType.Black; i--)
				{
					startIndex--;
				}
			}

			if (endIndex != cages.Length - 1 && cages[endIndex + 1] == CageType.Black)
			{
				for (int i = endIndex; i < cages.Length - 1 && cages[i + 1] == CageType.Black; i++)
				{
					endIndex++;
				}
			}


			int startCount = 0;
			bool isBlack = false;
			for (int i = 0; i < startIndex; i++)
			{
				if (cages[i] == CageType.Black)
				{
					if (!isBlack)
					{
						startCount++;
					}
					isBlack = true;
				}
				else
				{
					isBlack = false;
				}
			}


			int endCount = 0;
			isBlack = false;
			for (int i = cages.Length - 1; i > endIndex; i--)
			{
				if (cages[i] == CageType.Black)
				{
					if (!isBlack)
					{
						endCount++;
					}
					isBlack = true;
				}
				else
				{
					isBlack = false;
				}
			}


			var newCages = cages.Skip(startIndex).Take(endIndex - startIndex + 1).ToArray();
			var newNumbers = numbers.Skip(startCount).Take(numbers.Length - startCount - endCount).ToArray();

			if (newCages.Length == 0 || newNumbers.Length == 0)
			{
				return method(cages, numbers);
			}


			newCages = method(newCages, newNumbers);
			for (int i = startIndex; i <= endIndex; i++)
			{
				cages[i] = newCages[i - startIndex];
			}
			return cages;
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
				if (cages.Length > numbers[0])
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
				if (cages.Length > numbers[numbers.Length - 1])
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
			if (cages[1] == CageType.Black && numbers[0] == 1)
			{
				cages[0] = CageType.Point;
				return cages;
			}

			if (cages[0] == CageType.Black || cages[cages.Length - 1] == CageType.Black) return cages;

			int pointIndex = 0;
			var isHaveBlack = false;
			for (int i = 0; i < cages.Length && cages[i] != CageType.Point; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isHaveBlack = true;
				}
				pointIndex++;
			}
			if (!isHaveBlack) return cages;
			int number = numbers[0];

			var neededNumber = pointIndex / 2 + 1;
			if (number < neededNumber) return cages;

			var endIndex = number;
			var startIndex = pointIndex - number;
			for (int i = startIndex; i >= 0 && i < endIndex; i++)
			{
				cages[i] = CageType.Black;
			}
			return cages;
		}

		public CageType[] Method0004_DrawOnSidesReversed(CageType[] cages, int[] numbers)
		{
			return Method0004_DrawOnSides(cages.Reverse().ToArray(), numbers.Reverse().ToArray()).Reverse().ToArray();
		}

		// Если на линии есть n черных подряд а в числах нет числа больше n то по краям ставятся точки
		public CageType[] Method0005_CheckForMaxValues(CageType[] cages, int[] numbers)
		{
			if (numbers.Length == 0)
			{
				return cages;
			}
			var maxValue = numbers.Max();

			if (maxValue == 1)
			{
				for (int i = 1; i < cages.Length - 1; i++)
				{
					if (cages[i] == CageType.Black)
					{
						cages[i - 1] = CageType.Point;
						cages[i + 1] = CageType.Point;
					}
				}
				if (cages[0] == CageType.Black) cages[1] = CageType.Point;
				if (cages[cages.Length - 1] == CageType.Black) cages[cages.Length - 2] = CageType.Point;
				return cages;
			}

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

						cages[i] = CageType.Point;

						if (tempIndexCounter + i < cages.Length)
						{
							cages[tempIndexCounter + i] = CageType.Point;
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

		// Дорисовывает число если оно одно в линейке
		public CageType[] Method0007_UpDrawOneNumber(CageType[] cages, int[] numbers)
		{
			if (numbers.Length > 1 || cages.All(w => w != CageType.Black))
			{
				return cages;
			}

			int index = 0;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					index = i;
					break;
				}
			}

			for (int i = 0; i < cages.Length; i++)
			{
				if (numbers[0] <= Math.Abs(index - i))
				{
					cages[i] = CageType.Point;
				}
			}
			return cages;
		}

		// рисует точки там, где если будет черная клетка - будет слишком много черных подряд
		public CageType[] Method0008_PickPointsWhereCannotBeBlack(CageType[] cages, int[] numbers)
		{
			var maxInt = numbers.Max();

			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.White)
				{
					CageType[] newCages = new CageType[cages.Length];
					Array.Copy(cages, newCages, cages.Length);
					newCages[i] = CageType.Black;

					var maxLength = MaxValueOnLine(newCages);
					if (maxLength > maxInt)
					{
						cages[i] = CageType.Point;
					}
				}
			}


			return cages;
		}

		// если по краям большие числа иногда можно поставить точки вне их досягаемости
		public CageType[] Method0009_FindMaxValueAndPickPoints(CageType[] cages, int[] numbers)
		{
			var maxOnLine = MaxValueOnLine(cages);

			if (numbers[0] == numbers.Max() && numbers.Where(w => w >= maxOnLine).Count() == 1 && maxOnLine < numbers[0])
			{
				var blackEndIndex = 0;
				var isBeenBlack = false;
				for (int i = 0; i < cages.Length; i++)
				{
					if (cages[i] == CageType.Black)
					{
						isBeenBlack = true;
					}
					if (isBeenBlack && cages[i] != CageType.Black)
					{
						blackEndIndex = i;
						break;
					}
				}
				var startBlackIndex = blackEndIndex - numbers[0];
				for (int i = 0; i < startBlackIndex; i++)
				{
					cages[i] = CageType.Point;
				}
			}
			return cages;
		}

		// если между двумя точками нет черных точек и расстояние мало для помещения числа то ставим точки
		public CageType[] Method0010_FillMinSpacesBetweenPoints(CageType[] cages, int[] numbers)
		{
			var minNumber = numbers.Min();

			int startPoint = -1;
			int endPoint = 0;

			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Point)
				{
					endPoint = i;
					if (i - startPoint > minNumber)
					{

					}
					else
					{
						var isHaveBlack = false;
						for (int j = startPoint + 1; j < endPoint; j++)
						{
							if (cages[i] == CageType.Black)
							{
								isHaveBlack = true;
							}
						}
						if (!isHaveBlack)
						{
							for (int j = startPoint + 1; j < endPoint; j++)
							{
								cages[j] = CageType.Point;
							}
						}
					}
					startPoint = i;
				}
			}
			return cages;
		}

		public CageType[] Method0011_DrawLinesIfItHaveBegin(CageType[] cages, int[] numbers)
		{
			var firstNumber = numbers[0];
			var isHaveBegin = false;
			for (int i = 0; i < firstNumber - 1; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isHaveBegin = true;
				}
			}
			var isWasBegin = false;
			for (int i = 0; i < firstNumber; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isWasBegin = true;
				}
				if (isWasBegin)
				{
					cages[i] = CageType.Black;
				}
			}

			cages = cages.Reverse().ToArray();
			firstNumber = numbers.Last();
			isHaveBegin = false;
			for (int i = 0; i < firstNumber - 1; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isHaveBegin = true;
				}
			}
			isWasBegin = false;
			for (int i = 0; i < firstNumber; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isWasBegin = true;
				}
				if (isWasBegin)
				{
					cages[i] = CageType.Black;
				}
			}


			return cages.Reverse().ToArray();
		}

		public CageType[] Method0012_DrawBeginIfIsItBegin(CageType[] cages, int[] numbers)
		{
			var minNumber = numbers.Min();

			var isPreviousCage = false;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black && isPreviousCage)
				{
					for (int j = i; j < minNumber + i; j++)
					{
						cages[j] = CageType.Black;
					}
				}
				isPreviousCage = cages[i] == CageType.Point;
			}


			cages = cages.Reverse().ToArray();


			isPreviousCage = false;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black && isPreviousCage)
				{
					for (int j = i; j < minNumber + i; j++)
					{
						cages[j] = CageType.Black;
					}
				}
				isPreviousCage = cages[i] == CageType.Point;
			}

			return cages.Reverse().ToArray();
		}


		#region HelpFunctions

		private int MaxValueOnLine(CageType[] cages)
		{
			var isBlack = false;
			var totalCounter = 0;
			var tempCounter = 0;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					tempCounter++;
					isBlack = true;
				}
				else
				{
					if (isBlack)
					{
						totalCounter = Math.Max(totalCounter, tempCounter);
					}
					isBlack = false;
					tempCounter = 0;
				}
			}
			return totalCounter;
		}

		#endregion
	}
}
