using JapanCrossworkSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
			Methods.Add(Method0003);
			Methods.Add(Method0004);
			Methods.Add(Method0005);
			Methods.Add(Method0006);
			Methods.Add(Method0007);
			Methods.Add(Method0008);
			Methods.Add(Method0009);
			Methods.Add(Method0010);
			Methods.Add(Method0011);
			Methods.Add(Method0012);
			Methods.Add(Method0013);
			//Methods.Add(Method0014);
		}

		public void MakeStep()
		{
			var a = TopNumbers.Sum(w => w.Sum());
			var b = LeftNumbers.Sum(w => w.Sum());

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

			if (row.All(w => w != CageType.White))
			{
				return;
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

			if (column.All(w => w != CageType.White))
			{
				return;
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
			if (cages.All(w => w != CageType.White))
			{
				return method(cages, numbers);
			}

			int startIndex = 0;
			while (cages[startIndex] != CageType.White)
			{
				startIndex++;
			}
			if (startIndex > 0 && cages[startIndex - 1] == CageType.Black)
			{
				while (startIndex > 0 && cages[startIndex - 1] == CageType.Black)
				{
					startIndex--;
				}
			}

			int startNumbers = 0;
			bool ifBlack = false;
			for (int i = 0; i < startIndex; i++)
			{
				if (cages[i] == CageType.Black)
				{
					if (!ifBlack)
					{
						startNumbers++;
					}
					ifBlack = true;
				}
				if (cages[i] == CageType.Point)
				{
					ifBlack = false;
				}
			}

			cages = cages.Reverse().ToArray();

			int endIndex = 0;
			while (cages[endIndex] != CageType.White)
			{
				endIndex++;
			}
			if (endIndex > 0 && cages[endIndex - 1] == CageType.Black)
			{
				while (endIndex > 0 && cages[endIndex - 1] == CageType.Black)
				{
					endIndex--;
				}
			}

			int endNumbers = 0;
			ifBlack = false;
			for (int i = 0; i < endIndex; i++)
			{
				if (cages[i] == CageType.Black)
				{
					if (!ifBlack)
					{
						endNumbers++;
					}
					ifBlack = true;
				}
				if (cages[i] == CageType.Point)
				{
					ifBlack = false;
				}
			}

			cages = cages.Reverse().ToArray();

			var newCages = cages.Skip(startIndex).Take(cages.Length - (startIndex + endIndex));
			var newNumbers = numbers.Skip(startNumbers).Take(numbers.Length - (startNumbers + endNumbers));


			var result = method(newCages.ToArray(), newNumbers.ToArray());
			result = method(result.Reverse().ToArray(), newNumbers.Reverse().ToArray()).Reverse().ToArray();

			for (int i = 0; i < cages.Length - (endIndex + startIndex); i++)
			{
				cages[i + startIndex] = result[i];
			}

			return cages;
		}


		// Самый простой метод считает справа и слева числа и заполняет
		public CageType[] Method0001(CageType[] cages, int[] numbers)
		{
			CageType[] oldCages = new CageType[cages.Length];
			Array.Copy(cages, oldCages, cages.Length);

			if (numbers.Length == 1 && (cages.Length < numbers[0]))
			{
				CompareCages(oldCages, cages, "1", numbers);
				return cages;
			}


			var l = numbers.Sum() + numbers.Length - 1;
			var L = cages.Length;
			var n = L - l;

			int counter = 0;
			foreach (var number in numbers)
			{
				if (number > n)
				{
					int skipedIndex = counter + n;
					if (skipedIndex < 0)
					{
						CompareCages(oldCages, cages, "1", numbers);
						return cages;
					}
					for (int i = 0; i >= 0 && i < number - n; i++)
					{
						cages[skipedIndex] = CageType.Black;
						skipedIndex++;
					}

				}
				counter += (1 + number);
			}
			CompareCages(oldCages, cages, "1", numbers);
			return cages;
		}

		// дорисовывает черные клетки по крафм
		public CageType[] Method0002(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var number = numbers.First();

			bool isHaveBlack = false;
			for (int i = 0; i < number; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isHaveBlack = true;
				}
				if (isHaveBlack)
				{
					cages[i] = CageType.Black;
				}
			}
			return cages;
		}

		// если число черных равно числу чисел то все белые - точки
		public CageType[] Method0003(CageType[] cages, int[] numbers)
		{
			var blackCount = cages.Where(w => w == CageType.Black).Count();
			var numSum = numbers.Sum();
			if (numSum == blackCount)
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

		// ставит точки по бокам большого единственного в строке числа
		public CageType[] Method0004(CageType[] cages, int[] numbers)
		{
			if (numbers.Length != 1 || cages.Where(w => w == CageType.Black).Count() == 0)
			{
				return cages;
			}
			var number = numbers[0];

			int firstBlackIndex = -1;
			int lastBlackIndex = 0;

			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					lastBlackIndex = i;
					if (firstBlackIndex == -1)
					{
						firstBlackIndex = i;
					}
				}
			}


			var startPoint = lastBlackIndex - number + 1;
			for (int i = 0; i < startPoint; i++)
			{
				cages[i] = CageType.Point;
			}

			var endPoint = firstBlackIndex + number;
			for (int i = endPoint; i < cages.Length; i++)
			{
				cages[i] = CageType.Point;
			}
			return cages;
		}

		// если в строке одно число разбито - объединяет клетки
		public CageType[] Method0005(CageType[] cages, int[] numbers)
		{
			if (numbers.Length != 1 || cages.Where(w => w == CageType.Black).Count() == 0)
			{
				return cages;
			}
			var number = numbers[0];

			int firstBlackIndex = -1;
			int lastBlackIndex = 0;

			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					lastBlackIndex = i;
					if (firstBlackIndex == -1)
					{
						firstBlackIndex = i;
					}
				}
			}

			for (int i = firstBlackIndex + 1; i <= lastBlackIndex; i++)
			{
				cages[i] = CageType.Black;
			}
			return cages;
		}

		// ставит точки после чисел по краям
		public CageType[] Method0006(CageType[] cages, int[] numbers)
		{
			if (numbers.Length == 0)
			{
				return cages;
			}

			if (numbers[0] < cages.Length && cages[0] == CageType.Black)
			{
				cages[numbers[0]] = CageType.Point;
			}
			return cages;
		}

		// если есть точка+черная, то дорисовывает ее до минимального числа
		public CageType[] Method0007(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var minNumber = numbers.Min();
			for (int i = 0; i < cages.Length - 1; i++)
			{
				if (cages[i] == CageType.Point && cages[i + 1] == CageType.Black)
				{
					for (int j = 0; j < minNumber; j++)
					{
						cages[i + j + 1] = CageType.Black;
					}
				}
			}
			return cages;
		}

		// если между точками нельзя вместить клетки - то там клетки
		public CageType[] Method0008(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var minNumber = numbers.Min();
			if (minNumber == 2)
			{

			}

			if (minNumber < 2) return cages;

			var tempCounter = 0;
			var isWhite = false;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] != CageType.Point)
				{
					isWhite = true;
					tempCounter++;
					continue;
				}
				if (cages[i] == CageType.Point)
				{
					if (isWhite && tempCounter < minNumber)
					{
						for (int j = 0; j < tempCounter; j++)
						{
							cages[i - j - 1] = CageType.Point;
						}
					}
					isWhite = false;
					tempCounter = 0;
				}
			}

			return cages;
		}

		// если в строке максимальное число черных подряд - по бокам ставятся точки
		public CageType[] Method0009(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var max = numbers.Max();

			var isBlask = false;
			var tempCounter = 0;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isBlask = true;
					tempCounter++;
				}
				if (cages[i] != CageType.Black)
				{
					if (isBlask && tempCounter == max)
					{
						cages[i] = CageType.Point;
						if (i - max - 1 >= 0)
						{
							cages[i - max - 1] = CageType.Point;
						}
					}
					isBlask = false;
					tempCounter = 0;
				}
			}
			return cages;
		}

		// решает сложную задачу по краям (возможны ошибки)
		public CageType[] Method0010(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var firstPoint = -1;
			var isBeenBlack = false;
			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.Black)
				{
					isBeenBlack = true;
				}
				if (cages[i] == CageType.Point)
				{
					firstPoint = i;
					break;
				}
			}

			var minNumber = firstPoint / 2 + 1;
			if (firstPoint == -1 || numbers[0] < minNumber || !isBeenBlack) return cages;

			var endIndex = numbers[0];
			var startIndex = firstPoint - numbers[0];
			for (int i = startIndex; i < endIndex; i++)
			{
				cages[i] = CageType.Black;
			}
			return cages;
		}

		// расставляет зачатки черных по краям
		public CageType[] Method0011(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var number = numbers[0];
			for (int i = 0; i < number; i++)
			{
				if (cages[i] == CageType.Black)
				{
					while (i < cages.Length && cages[i] == CageType.Black)
					{
						var index = i - number;
						if (index >= 0)
						{
							cages[index] = CageType.Point;
						}
						i++;
					}
					break;
				}
			}
			return cages;
		}

		// ставит точку если точка, фиг знает как объяснить
		public CageType[] Method0012(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;
			if (cages.Length > numbers[0] && cages[numbers[0]] == CageType.Black)
			{
				cages[0] = CageType.Point;
			}
			return cages;
		}

		// если добавление черной клетки даст число клеток больше числа - то там точка
		public CageType[] Method0013(CageType[] cages, int[] numbers)
		{
			if (cages.Length == 0 || numbers.Length == 0) return cages;

			var maxNum = numbers.Max();

			for (int i = 0; i < cages.Length; i++)
			{
				if (cages[i] == CageType.White)
				{
					cages[i] = CageType.Black;
					var maxCages = MaxValueOnLine(cages);
					if (maxCages > maxNum)
					{
						cages[i] = CageType.Point;
					}
					else
					{
						cages[i] = CageType.White;
					}
				}
			}
			return cages;
		}

		// метод типа как первый, но учитывает точки ( возможны ошибки, я хз)
		public CageType[] Method0014(CageType[] cages, int[] numbers)
		{
			if (numbers.Length == 4 && numbers[0] == 5)
			{

			}

			var indexes = new int[numbers.Length, 2];


			var numCounter = 0;
			for (int i = 0; i < cages.Length && numCounter < numbers.Length; i++)
			{
				if (cages[i] != CageType.Point)
				{
					var isPlacing = true;
					for (int j = 0; j < numbers[numCounter]; j++)
					{
						if (i + j < cages.Length && cages[i + j] == CageType.Point)
						{
							isPlacing = false;
						}
					}
					if (isPlacing)
					{
						indexes[numCounter, 1] = i + numbers[numCounter] - 1;
						i += numbers[numCounter];
						numCounter++;
					}
				}
			}

			numbers = numbers.Reverse().ToArray();

			numCounter = 0;
			for (int i = cages.Length - 1; i >= 0 && numCounter < numbers.Length; i--)
			{
				if (cages[i] != CageType.Point)
				{
					var isPlacing = true;
					for (int j = 0; j < numbers[numCounter]; j++)
					{
						if (cages[i - j] == CageType.Point)
						{
							isPlacing = false;
						}
					}
					if (isPlacing)
					{
						indexes[indexes.GetLength(0) - numCounter - 1, 0] = i - numbers[numCounter] + 1;
						i -= numbers[numCounter];
						numCounter++;
					}
				}
			}


			for (int i = 0; i < indexes.GetLength(0); i++)
			{
				for (int j = indexes[i, 0]; j <= indexes[i, 1]; j++)
				{
					if (j < cages.Length)
					{
						cages[j] = CageType.Black;
					}
				}
			}

			return cages;
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

		private void CompareCages(CageType[] oldCages, CageType[] newCages, string method, int[] numbers)
		{
			for (int i = 0; i < oldCages.Length; i++)
			{
				if ((oldCages[i] == CageType.Black && newCages[i] == CageType.Point) || oldCages[i] == CageType.Point && newCages[i] == CageType.Black)
				{
					//throw new Exception(method);
				}
			}
		}
		#endregion
	}
}
