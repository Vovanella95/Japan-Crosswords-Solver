using JapanCrossworkSolver;
using JapanCrossworkSolver.Models;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewModels
{
	public class FirstViewModel : MvxViewModel
	{
		private CrosswordSolver _solver;

		private CageType[] _cages;
		public CageType[] Cages
		{
			get
			{
				return _cages;
			}
			set
			{
				SetProperty(ref _cages, value);
			}
		}

		private double _gridWidth;
		public double GridWidth
		{
			get
			{
				return _gridWidth;
			}
			set
			{
				SetProperty(ref _gridWidth, value);
			}
		}

		private double _gridHeight;
		public double GridHeight
		{
			get
			{
				return _gridHeight;
			}
			set
			{
				SetProperty(ref _gridHeight, value);
			}
		}

		private int[][] _topNumbers;
		public int[][] TopNumbers
		{
			get
			{
				return _topNumbers;
			}
			set
			{
				SetProperty(ref _topNumbers, value);
			}
		}

		private int[][] _leftNumbers;
		public int[][] LeftNumbers
		{
			get
			{
				return _leftNumbers;
			}
			set
			{
				SetProperty(ref _leftNumbers, value);
			}
		}


		public FirstViewModel()
		{

			// Elephant
			//TopNumbers = new int[][] { new[] { 3 }, new[] { 3 }, new[] { 9 }, new[] { 10 }, new[] { 7 }, new[] { 3, 3 }, new[] { 2, 2, 2 }, new[] { 7, 2 }, new[] { 6, 5 }, new[] { 7, 4 }, new[] { 4, 2 }, new[] { 8 }, new[] { 2 }, new[] { 1 }, };
			//LeftNumbers = new int[][] { new[] { 3 }, new[] { 5 }, new[] { 9 }, new[] { 4, 5 }, new[] { 5, 5 }, new[] { 4, 4, 1 }, new[] { 5, 2, 3 }, new[] { 1, 4, 1, 2 }, new[] { 1, 8, 1 }, new[] { 2, 5, 2 }, new[] { 2, 2, 2 }, new[] { 2, 2 }, };

			// Terapod
			//TopNumbers = new int[][] { new[] { 5 }, new[] { 1, 4 }, new[] { 1, 2 }, new[] { 2, 1, 1 }, new[] { 2, 4 }, new[] { 1, 6 }, new[] { 1, 1, 8 }, new[] { 3, 11 }, new[] { 17 }, new[] { 18 }, new[] { 17 }, new[] { 3, 11 }, new[] { 1, 1, 8 }, new[] { 6 }, new[] { 4 }, new[] { 6 }, new[] { 7 }, new[] { 3 }, new[] { 1 }, };
			//LeftNumbers = new int[][] { new[] { 1 }, new[] { 3 }, new[] { 3 }, new[] { 5 }, new[] { 5 }, new[] { 7 }, new[] { 3 }, new[] { 4, 7, 1 }, new[] { 1, 2, 5, 3 }, new[] { 1, 2, 5, 2 }, new[] { 1, 1, 7, 2 }, new[] { 2, 9, 2 }, new[] { 1, 13 }, new[] { 1, 13 }, new[] { 16 }, new[] { 1, 12 }, new[] { 9 }, new[] { 7 }, };

			//Fan
			//TopNumbers = new int[][] { new[] { 4 }, new[] { 6 }, new[] { 8 }, new[] { 8 }, new[] { 2, 8, 2 }, new[] { 6, 6, 3 }, new[] { 6, 3, 4, 3 }, new[] { 6, 1, 1, 2, 3 }, new[] { 6, 5, 6 }, new[] { 6, 5, 7 }, new[] { 7, 3, 2, 3 }, new[] { 7, 4, 3 }, new[] { 5, 8, 3 }, new[] { 3, 8, 2 }, new[] { 8 }, new[] { 8 }, new[] { 6 }, new[] { 3 }, };
			//LeftNumbers = new int[][] { new[] { 5 }, new[] { 8 }, new[] { 9 }, new[] { 10 }, new[] { 10 }, new[] { 8 }, new[] { 2, 2 }, new[] { 3 }, new[] { 1, 3, 4 }, new[] { 3, 1, 3, 5 }, new[] { 4, 1, 3, 6 }, new[] { 6, 3, 7 }, new[] { 7, 8 }, new[] { 8, 7 }, new[] { 8, 6 }, new[] { 6, 1, 4 }, new[] { 4, 2 }, new[] { 2 }, new[] { 2 }, new[] { 8 }, new[] { 10 }, new[] { 10 }, };

			//Duck
			//TopNumbers = new int[][] { new[] { 2, 2 }, new[] { 2, 2 }, new[] { 3, 5 }, new[] { 5, 2, 2 }, new[] { 2, 5, 1 }, new[] { 1, 2, 1, 1 }, new[] { 1, 1, 2, 2, 1 }, new[] { 1, 6, 1 }, new[] { 2, 2, 3, 1 }, new[] { 1, 2, 1, 1, 2 }, new[] { 3, 1, 2 }, new[] { 1, 1, 4 }, new[] { 4, 2 }, };
			//LeftNumbers = new int[][] { new[] { 5 }, new[] { 2, 4 }, new[] { 1, 1, 1 }, new[] { 1, 3, 2 }, new[] { 5, 2 }, new[] { 5, 2 }, new[] { 4 }, new[] { 2, 2, 2 }, new[] { 1, 4, 1 }, new[] { 1, 4, 1 }, new[] { 1, 4, 2 }, new[] { 1, 1 }, new[] { 4, 4 }, new[] { 13 }, };

			//Cube
			//TopNumbers = new int[][] { new[] { 12 }, new[] { 2, 1 }, new[] { 3, 2, 2, 2, 1 }, new[] { 2, 1, 2, 2, 2, 1 }, new[] { 2, 1, 1 }, new[] { 3, 1 }, new[] { 3, 1 }, new[] { 4, 1 }, new[] { 4, 2, 2, 2, 1 }, new[] { 2, 1, 2, 2, 2, 1 }, new[] { 2, 1, 1 }, new[] { 14 }, new[] { 9, 2 }, new[] { 13 }, new[] { 2, 8 }, new[] { 11 } };
			//LeftNumbers = new int[][] { new[] { 2, 4, 3 }, new[] { 14 }, new[] { 2, 4, 3, 1 }, new[] { 14, 1 }, new[] { 1, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 1, 3 }, new[] { 1, 2, 2, 1, 2 }, new[] { 1, 2, 2, 3 }, new[] { 1, 2 }, new[] { 12 }, };


			//Cow
			//TopNumbers = new int[][] { new[] { 2, 3 }, new[] { 1, 4, 2 }, new[] { 3, 1, 4 }, new[] { 1, 1, 4 }, new[] { 1, 1, 1, 2 }, new[] { 1, 1, 2 }, new[] { 3, 1 }, new[] { 1, 1, 1, 1 }, new[] { 3, 3, 1 }, new[] { 1, 4 }, };
			//LeftNumbers = new int[][] { new[] { 3, 3 }, new[] { 1, 1, 1, 1 }, new[] { 1, 5, 1 }, new[] { 1, 2 }, new[] { 2, 1, 1 }, new[] { 1, 2 }, new[] { 5, 2 }, new[] { 1, 2, 1, 2 }, new[] { 6, 1, 1 }, new[] { 4, 1, 2 }, };

			//Spider
			//TopNumbers = new int[][] { new[] { 4 }, new[] { 2 }, new[] { 2 }, new[] { 7, 1, 5 }, new[] { 2, 2, 3 }, new[] { 3, 1, 1, 2 }, new[] { 5, 2, 2, 5 }, new[] { 2, 6, 7 }, new[] { 2, 1, 13 }, new[] { 14 }, new[] { 2, 1, 13 }, new[] { 2, 6, 7 }, new[] { 5, 2, 2, 5 }, new[] { 3, 1, 1, 2 }, new[] { 2, 2, 3 }, new[] { 7, 1, 5 }, new[] { 2 }, new[] { 2 }, new[] { 4 }, };
			//LeftNumbers = new int[][] { new[] { 1, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 1, 1, 2, 1 }, new[] { 1, 2, 1, 2, 1 }, new[] { 2, 5, 2 }, new[] { 11 }, new[] { 7 }, new[] { 2, 5, 2 }, new[] { 3, 7, 3 }, new[] { 2, 2, 3, 2, 2 }, new[] { 2, 2, 5, 2, 2 }, new[] { 1, 1, 7, 1, 1 }, new[] { 1, 2, 7, 2, 1 }, new[] { 1, 1, 7, 1, 1 }, new[] { 1, 7, 1 }, new[] { 1, 7, 1 }, new[] { 1, 5, 1 }, };

			// Pig
			//TopNumbers = new int[][] { new[] { 3, 3 }, new[] { 1, 4, 1, 1 }, new[] { 1, 1, 1 }, new[] { 2, 1, 1, 1, 1 }, new[] { 1, 1, 1 }, new[] { 1, 1, 4 }, new[] { 2, 1 }, new[] { 1, 1 }, new[] { 1, 2, 2 }, new[] { 4, 4 }, };
			//LeftNumbers = new int[][] { new[] { 4, 4 }, new[] { 1, 4, 1 }, new[] { 2, 1 }, new[] { 1, 1, 1, 2 }, new[] { 1, 1 }, new[] { 4, 1 }, new[] { 1, 1, 1 }, new[] { 2, 1, 1, 1 }, new[] { 1, 1, 2 }, new[] { 8 }, };

			// Tirex
			//TopNumbers = new int[][] { new[] { 8, 6 }, new[] { 6, 1, 3 }, new[] { 5, 1, 1, 2 }, new[] { 4, 2, 1, 2 }, new[] { 4, 1, 1 }, new[] { 3, 1, 1 }, new[] { 3, 2, 1 }, new[] { 3, 2, 1 }, new[] { 3, 1, 1 }, new[] { 3, 3, 1 }, new[] { 2, 4, 2 }, new[] { 2, 5, 2 }, new[] { 2, 5, 3 }, new[] { 1, 5, 3 }, new[] { 2, 2 }, new[] { 3, 2 }, new[] { 6, 1, 1 }, new[] { 4, 1, 1 }, new[] { 1, 2 }, new[] { 6, 1, 3 }, new[] { 1, 4, 6 }, new[] { 1, 5 }, new[] { 3, 3, 5 }, };
			//LeftNumbers = new int[][] { new[] { 14, 2 }, new[] { 13, 1, 1 }, new[] { 10, 2, 2, 1 }, new[] { 5, 3, 3, 1 }, new[] { 3, 6, 2, 2 }, new[] { 2, 6, 2, 2 }, new[] { 1, 1, 2, 6, 2, 2, 1 }, new[] { 1, 2, 2, 4, 1, 2, 1 }, new[] { 1 }, new[] { 1, 2, 6 }, new[] { 2, 1, 1, 1, 7, 3 }, new[] { 1, 1, 1, 1, 1, 5, 3 }, new[] { 2, 4 }, new[] { 4, 5 }, new[] { 6, 7 }, };

			//TopNumbers = new int[][] { new[] { 5, 3 }, new[] { 2, 1, 2, 1 }, new[] { 2, 3, 1, 1 }, new[] { 2, 1, 1, 1, 3 }, new[] { 1, 1, 5, 2 }, new[] { 1, 1, 1, 3, 1 }, new[] { 1, 1, 1, 2 }, new[] { 2, 5, 3 }, new[] { 2, 1, 1 }, new[] { 2, 2, 2, 2 }, new[] { 5, 3, 1 }, new[] { 1, 1, 1, 1 }, new[] { 3, 1 }, new[] { 1, 2, 4 }, new[] { 3, 4 }, };
			//LeftNumbers = new int[][] { new[] { 5 }, new[] { 2, 2 }, new[] { 2, 2, 1 }, new[] { 2, 1, 1, 1, 2, 1, 1 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 1, 1, 1, 1 }, new[] { 1, 1, 2, 2, 1 }, new[] { 2, 3, 1, 2, 2 }, new[] { 1, 2, 1 }, new[] { 4, 1, 1, 1 }, new[] { 2, 3, 3, 1 }, new[] { 1, 4, 1, 2, 1 }, new[] { 2, 1, 1, 2, 1 }, new[] { 1, 2, 1, 1 }, new[] { 3, 5 }, };

			//Elc
			//TopNumbers = new int[][] { new[] { 2 }, new[] { 2 }, new[] { 4 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 5 }, new[] { 1, 3 }, new[] { 9 }, new[] { 1, 12 }, new[] { 3, 7 }, new[] { 2, 11 }, new[] { 4, 6 }, new[] { 2, 5 }, new[] { 2, 6 }, new[] { 10 }, new[] { 6 }, new[] { 10 }, new[] { 4 }, };
			//LeftNumbers = new int[][] { new[] { 1, 1 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 5, 5 }, new[] { 5, 5 }, new[] { 3 }, new[] { 2, 2 }, new[] { 8 }, new[] { 14 }, new[] { 2, 12 }, new[] { 11 }, new[] { 11 }, new[] { 11 }, new[] { 5, 4 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, };

			// Note
			//TopNumbers = new int[][] { new[] { 2 }, new[] { 4 }, new[] { 5 }, new[] { 4 }, new[] { 12 }, new[] { 3 }, new[] { 4 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 3, 5 }, new[] { 3, 4 }, new[] { 12 }, };
			//LeftNumbers = new int[][] { new[] { 1 }, new[] { 3 }, new[] { 6 }, new[] { 8 }, new[] { 5, 1 }, new[] { 3, 1 }, new[] { 1, 1 }, new[] { 1, 1 }, new[] { 1, 1, 1 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 1, 1, 5 }, new[] { 4, 3 }, new[] { 5 }, new[] { 5 }, new[] { 3 }, };


			//TopNumbers = new int[][] { new[] { 8, 1, 7 }, new[] { 6, 2, 5 }, new[] { 5, 2, 3 }, new[] { 4, 2, 2 }, new[] { 3, 1, 4 }, new[] { 2, 4, 1 }, new[] { 2, 4 }, new[] { 1, 6 }, new[] { 1, 3 }, new[] { 1, 3 }, new[] { 3 }, new[] { 5 }, new[] { 5 }, new[] { 5 }, new[] { 3 }, new[] { 1, 3 }, new[] { 1, 3 }, new[] { 1, 6 }, new[] { 2, 4 }, new[] { 2, 4, 1 }, new[] { 3, 4, 1 }, new[] { 4, 2, 2 }, new[] { 5, 2, 3 }, new[] { 6, 2, 5 }, new[] { 8, 1, 7 }, };
			//LeftNumbers = new int[][] { new[] { 10, 10 }, new[] { 7, 7 }, new[] { 5, 5 }, new[] { 4, 4 }, new[] { 3, 3 }, new[] { 2, 2 }, new[] { 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 4, 4 }, new[] { 8, 8 }, new[] { 8, 8 }, new[] { 6, 1, 1, 6 }, new[] { 11 }, new[] { 1, 7, 1 }, new[] { 1, 5, 1 }, new[] { 2, 3, 2 }, new[] { 2, 1, 2 }, new[] { 3, 3 }, new[] { 4, 4 }, new[] { 6, 6 }, };


			//Blade
			//TopNumbers = new int[][] { new[] { 4 }, new[] { 6 }, new[] { 7, 1 }, new[] { 4, 3, 1 }, new[] { 4, 3, 2 }, new[] { 3, 3, 2 }, new[] { 2, 3, 3, 3 }, new[] { 2, 3, 4, 3 }, new[] { 1, 4, 3, 3 }, new[] { 1, 4, 3, 3 }, new[] { 29 }, new[] { 8, 3, 3, 2 }, new[] { 8, 3, 3, 3 }, new[] { 3, 19 }, new[] { 2, 1, 3, 3, 3 }, new[] { 1, 4, 3, 4 }, new[] { 2, 3, 3, 4 }, new[] { 2, 4, 3, 4 }, new[] { 5, 3, 4 }, new[] { 3, 2, 3, 3 }, new[] { 1, 1, 1, 3, 3 }, new[] { 3, 1, 3, 3 }, new[] { 2, 4, 3 }, new[] { 7 }, new[] { 5 }, };
			//LeftNumbers = new int[][] { new[] { 4 }, new[] { 5 }, new[] { 4 }, new[] { 3 }, new[] { 3 }, new[] { 3 }, new[] { 3 }, new[] { 12 }, new[] { 2,1,1,2 }, new[] { 1,1 }, new[] { 1,1,4 }, new[] { 1,1,5,2 }, new[] { 15 }, new[] { 15,1 }, new[] { 14,2 }, new[] { 10,1,3 }, new[] { 5,1,1 }, new[] { 4,1,4 }, new[] { 3,4,10 }, new[] { 11,11 }, new[] { 10,1,8 }, new[] { 5,6,3 }, new[] { 10,2 }, new[] { 17 }, new[] { 4,1,10 }, new[] { 3,1,2,8 }, new[] { 3,1,1 }, new[] { 3,3 }, new[] { 3,2 }, new[] { 1 }, };



			//TopNumbers = new int[][] { new[] { 30 }, new[] { 19, 8 }, new[] { 19, 4 }, new[] { 7, 10 }, new[] { 4, 15 }, new[] { 2, 17 }, new[] { 1, 5, 17 }, new[] { 16 }, new[] { 3, 2, 1, 3 }, new[] { 3, 1, 1, 1, 1, 10 }, new[] { 3, 1, 1, 1, 1, 10 }, new[] { 3, 1, 1, 2, 1, 9 }, new[] { 3, 3, 1, 2, 7 }, new[] { 3, 2, 1, 1, 1, 1, 6 }, new[] { 3, 4, 1, 6, 4, 5 }, new[] { 3, 1, 1, 1, 1, 1, 2, 4 }, new[] { 3, 2, 1, 1, 2, 2, 1, 4 }, new[] { 3, 1, 1, 1, 1, 1, 2, 4 }, new[] { 3, 1, 2, 1, 2, 4 }, new[] { 6, 1, 2, 3, 3 }, new[] { 3, 1, 1, 1, 3, 1 }, new[] { 5, 1, 1, 2, 2 }, new[] { 8, 1, 1, 2, 1, 2 }, new[] { 6, 1, 1, 2, 2, 2 }, new[] { 8, 1, 1, 1, 2, 3, 2 }, new[] { 8, 2, 1, 2, 7 }, new[] { 6, 6, 2, 8 }, new[] { 7, 3, 9 }, new[] { 4, 3, 3, 10 }, new[] { 3, 1, 2, 2, 10 }, new[] { 1, 2, 7, 12 }, new[] { 3, 6, 11 }, new[] { 1, 6, 10 }, new[] { 2, 3, 9 }, new[] { 3, 3, 8 }, new[] { 3, 2, 7 }, new[] { 3, 2, 6 }, new[] { 3, 3, 4 }, new[] { 3, 2, 2 }, new[] { 3, 3 }, };
			//LeftNumbers = new int[][] { new[] { 7, 2 }, new[] { 6, 9, 2, 2 }, new[] { 5, 12, 1, 3, 2 }, new[] { 5, 13, 1, 4, 2 }, new[] { 4, 3, 1, 6, 1 }, new[] { 4, 2, 5, 1, 9 }, new[] { 4, 2, 3, 1, 1, 8 }, new[] { 3, 2, 1, 2, 1, 8 }, new[] { 3, 2, 4, 6, 3 }, new[] { 3, 3, 4, 2, 1 }, new[] { 3, 1, 14, 1, 1, 1, 1 }, new[] { 3, 2, 1, 2, 1, 2 }, new[] { 8, 4, 5, 2, 1, 3 }, new[] { 8, 2, 1, 2, 1, 6 }, new[] { 10, 1, 4, 9 }, new[] { 8, 1, 5, 5 }, new[] { 11, 1, 4, 2, 3, 4 }, new[] { 9, 2, 2, 3, 3, 2 }, new[] { 12, 3, 3, 2, 2, 3, 1 }, new[] { 1, 4, 1, 1, 2, 4 }, new[] { 1, 4, 2, 8, 1, 5, 4 }, new[] { 1, 4, 3, 1, 1, 1, 2, 7, 3 }, new[] { 2, 3, 3, 1, 2, 2, 9, 1 }, new[] { 2, 3, 4, 2, 2, 2, 11 }, new[] { 2, 3, 5, 3, 2, 13 }, new[] { 3, 2, 6, 2, 14 }, new[] { 3, 2, 12, 16 }, new[] { 3, 2, 12, 13 }, new[] { 3, 1, 10, 18 }, new[] { 2, 9, 19 }, };

			//Cat (cat??)
			TopNumbers = new int[][] { new[] { 10, 8 }, new[] { 11, 3, 5 }, new[] { 12, 3, 2, 3, 2 }, new[] { 12, 4, 4, 2, 3 }, new[] { 10, 1, 3, 4, 2, 3 }, new[] { 8, 1, 3, 4, 2, 4 }, new[] { 7, 2, 3, 2, 2, 4 }, new[] { 5, 3, 3, 3, 5 }, new[] { 2, 5, 7, 5 }, new[] { 7, 4, 2, 2 }, new[] { 10, 2, 1, 1, 1 }, new[] { 11, 1, 1, 3, 1 }, new[] { 11, 1, 1, 3, 1 }, new[] { 11, 1, 1, 3, 1 }, new[] { 10, 2, 1, 1, 1 }, new[] { 7, 4, 2, 2 }, new[] { 2, 5, 7, 5 }, new[] { 4, 3, 3, 3, 5 }, new[] { 7, 2, 3, 2, 2, 4 }, new[] { 8, 1, 3, 4, 2, 4 }, new[] { 10, 1, 3, 4, 2, 3 }, new[] { 12, 4, 4, 2, 3 }, new[] { 12, 3, 2, 2, 2 }, new[] { 12, 3, 5 }, new[] { 10, 8 }, };
			LeftNumbers = new int[][] { new[] { 1, 1 }, new[] { 3, 3 }, new[] { 4, 5 }, new[] { 6, 6 }, new[] { 7, 6 }, new[] { 8, 8 }, new[] { 8, 8 }, new[] { 9, 9 }, new[] { 9, 9 }, new[] { 7, 7 }, new[] { 5, 9, 5 }, new[] { 4, 13, 4 }, new[] { 3, 17, 3 }, new[] { 2, 1, 11, 1, 2 }, new[] { 1, 5, 9, 5, 1 }, new[] { 7, 7, 7 }, new[] { 7, 7, 7 }, new[] { 2, 2, 5, 2, 2 }, new[] { 1, 3, 1, 5, 1, 3, 1 }, new[] { 1, 5, 1, 5, 1, 5, 1 }, new[] { 1, 5, 2, 3, 2, 5, 1 }, new[] { 1, 3, 2, 2, 3, 1 }, new[] { 3, 4, 4, 2 }, new[] { 25 }, new[] { 8, 8 }, new[] { 1, 7, 1 }, new[] { 9, 9 }, new[] { 7, 3, 7 }, new[] { 6, 5, 6 }, new[] { 4, 3, 4 }, new[] { 3, 3 }, new[] { 7 }, };

			_solver = new CrosswordSolver(TopNumbers, LeftNumbers);

			Cages = _solver.ArrayOfCages;

			GridHeight = _solver.Height * 21 - 5;
			GridWidth = _solver.Width * 20;
		}

		public IMvxCommand MakeStep
		{
			get
			{
				return new MvxCommand(MakeStepAsync);
			}
		}

		private async void MakeStepAsync()
		{
			await MakeStepTask();
		}
		private Task MakeStepTask()
		{
			return Task.Run(() =>
			{

				_solver.MakeStep();

				Cages = _solver.ArrayOfCages;
			});
		}
	}
}