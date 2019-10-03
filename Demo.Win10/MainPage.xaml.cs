using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using JapanCrossworkSolver;
using JapanCrossworkSolver.Models;
using Caliburn.Micro;

namespace Demo.Win10
{
	public class CageNumber : PropertyChangedBase
	{
		private string _number;
		public string Number
		{
			get => _number;
			set
			{
				_number = value;
				NotifyOfPropertyChange(nameof(Number));
			}
		}

		public static CageNumber From(int number)
		{
			return new CageNumber
			{
				Number = number.ToString()
			};
		}
	}

	public sealed partial class MainPage
	{
		private CrosswordSolver _solver;

		public CageNumber[][] TopItems;
		public CageNumber[][] LeftItems;

		public MainPage()
		{
			InitializeComponent();

			// Elephant
			TopItems = new[] { new[] { CageNumber.From(3) }, new[] { CageNumber.From(3) }, new[] { CageNumber.From(9) }, new[] { CageNumber.From(10) }, new[] { CageNumber.From(7) }, new[] { CageNumber.From(3), CageNumber.From(3) }, new[] { CageNumber.From(2), CageNumber.From(2), CageNumber.From(2) }, new[] { CageNumber.From(7), CageNumber.From(2) }, new[] { CageNumber.From(6), CageNumber.From(5) }, new[] { CageNumber.From(7), CageNumber.From(4) }, new[] { CageNumber.From(4), CageNumber.From(2) }, new[] { CageNumber.From(8) }, new[] { CageNumber.From(2) }, new[] { CageNumber.From(1) } };
			LeftItems = new[] { new[] { CageNumber.From(3) }, new[] { CageNumber.From(5) }, new[] { CageNumber.From(9) }, new[] { CageNumber.From(4), CageNumber.From(5) }, new[] { CageNumber.From(5), CageNumber.From(5) }, new[] { CageNumber.From(4), CageNumber.From(4), CageNumber.From(1) }, new[] { CageNumber.From(5), CageNumber.From(2), CageNumber.From(3) }, new[] { CageNumber.From(1), CageNumber.From(4), CageNumber.From(1), CageNumber.From(2) }, new[] { CageNumber.From(1), CageNumber.From(8), CageNumber.From(1) }, new[] { CageNumber.From(2), CageNumber.From(5), CageNumber.From(2) }, new[] { CageNumber.From(2), CageNumber.From(2), CageNumber.From(2) }, new[] { CageNumber.From(2), CageNumber.From(2) } };

			// Terapod
			//var topNumbers = new int[][] { new[] { 5 }, new[] { 1, 4 }, new[] { 1, 2 }, new[] { 2, 1, 1 }, new[] { 2, 4 }, new[] { 1, 6 }, new[] { 1, 1, 8 }, new[] { 3, 11 }, new[] { 17 }, new[] { 18 }, new[] { 17 }, new[] { 3, 11 }, new[] { 1, 1, 8 }, new[] { 6 }, new[] { 4 }, new[] { 6 }, new[] { 7 }, new[] { 3 }, new[] { 1 }, };
			//var leftNumbers = new int[][] { new[] { 1 }, new[] { 3 }, new[] { 3 }, new[] { 5 }, new[] { 5 }, new[] { 7 }, new[] { 3 }, new[] { 4, 7, 1 }, new[] { 1, 2, 5, 3 }, new[] { 1, 2, 5, 2 }, new[] { 1, 1, 7, 2 }, new[] { 2, 9, 2 }, new[] { 1, 13 }, new[] { 1, 13 }, new[] { 16 }, new[] { 1, 12 }, new[] { 9 }, new[] { 7 }, };

			//Fan
			//var topNumbers = new int[][] { new[] { 4 }, new[] { 6 }, new[] { 8 }, new[] { 8 }, new[] { 2, 8, 2 }, new[] { 6, 6, 3 }, new[] { 6, 3, 4, 3 }, new[] { 6, 1, 1, 2, 3 }, new[] { 6, 5, 6 }, new[] { 6, 5, 7 }, new[] { 7, 3, 2, 3 }, new[] { 7, 4, 3 }, new[] { 5, 8, 3 }, new[] { 3, 8, 2 }, new[] { 8 }, new[] { 8 }, new[] { 6 }, new[] { 3 }, };
			//var leftNumbers = new int[][] { new[] { 5 }, new[] { 8 }, new[] { 9 }, new[] { 10 }, new[] { 10 }, new[] { 8 }, new[] { 2, 2 }, new[] { 3 }, new[] { 1, 3, 4 }, new[] { 3, 1, 3, 5 }, new[] { 4, 1, 3, 6 }, new[] { 6, 3, 7 }, new[] { 7, 8 }, new[] { 8, 7 }, new[] { 8, 6 }, new[] { 6, 1, 4 }, new[] { 4, 2 }, new[] { 2 }, new[] { 2 }, new[] { 8 }, new[] { 10 }, new[] { 10 }, };

			//Duck
			//var topNumbers = new int[][] { new[] { 2, 2 }, new[] { 2, 2 }, new[] { 3, 5 }, new[] { 5, 2, 2 }, new[] { 2, 5, 1 }, new[] { 1, 2, 1, 1 }, new[] { 1, 1, 2, 2, 1 }, new[] { 1, 6, 1 }, new[] { 2, 2, 3, 1 }, new[] { 1, 2, 1, 1, 2 }, new[] { 3, 1, 2 }, new[] { 1, 1, 4 }, new[] { 4, 2 }, };
			//var leftNumbers = new int[][] { new[] { 5 }, new[] { 2, 4 }, new[] { 1, 1, 1 }, new[] { 1, 3, 2 }, new[] { 5, 2 }, new[] { 5, 2 }, new[] { 4 }, new[] { 2, 2, 2 }, new[] { 1, 4, 1 }, new[] { 1, 4, 1 }, new[] { 1, 4, 2 }, new[] { 1, 1 }, new[] { 4, 4 }, new[] { 13 }, };

			//Cube
			//var topNumbers = new int[][] { new[] { 12 }, new[] { 2, 1 }, new[] { 3, 2, 2, 2, 1 }, new[] { 2, 1, 2, 2, 2, 1 }, new[] { 2, 1, 1 }, new[] { 3, 1 }, new[] { 3, 1 }, new[] { 4, 1 }, new[] { 4, 2, 2, 2, 1 }, new[] { 2, 1, 2, 2, 2, 1 }, new[] { 2, 1, 1 }, new[] { 14 }, new[] { 9, 2 }, new[] { 13 }, new[] { 2, 8 }, new[] { 11 } };
			//var leftNumbers = new int[][] { new[] { 2, 4, 3 }, new[] { 14 }, new[] { 2, 4, 3, 1 }, new[] { 14, 1 }, new[] { 1, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 2, 2, 5 }, new[] { 1, 1, 3 }, new[] { 1, 2, 2, 1, 2 }, new[] { 1, 2, 2, 3 }, new[] { 1, 2 }, new[] { 12 }, };


			//Cow
			//var topNumbers = new [] { new[] { 2, 3 }, new[] { 1, 4, 2 }, new[] { 3, 1, 4 }, new[] { 1, 1, 4 }, new[] { 1, 1, 1, 2 }, new[] { 1, 1, 2 }, new[] { 3, 1 }, new[] { 1, 1, 1, 1 }, new[] { 3, 3, 1 }, new[] { 1, 4 }, };
			//var leftNumbers = new [] { new[] { 3, 3 }, new[] { 1, 1, 1, 1 }, new[] { 1, 5, 1 }, new[] { 1, 2 }, new[] { 2, 1, 1 }, new[] { 1, 2 }, new[] { 5, 2 }, new[] { 1, 2, 1, 2 }, new[] { 6, 1, 1 }, new[] { 4, 1, 2 }, };

			//Spider
			//var topNumbers = new int[][] { new[] { 4 }, new[] { 2 }, new[] { 2 }, new[] { 7, 1, 5 }, new[] { 2, 2, 3 }, new[] { 3, 1, 1, 2 }, new[] { 5, 2, 2, 5 }, new[] { 2, 6, 7 }, new[] { 2, 1, 13 }, new[] { 14 }, new[] { 2, 1, 13 }, new[] { 2, 6, 7 }, new[] { 5, 2, 2, 5 }, new[] { 3, 1, 1, 2 }, new[] { 2, 2, 3 }, new[] { 7, 1, 5 }, new[] { 2 }, new[] { 2 }, new[] { 4 }, };
			//var leftNumbers = new int[][] { new[] { 1, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 2, 1 }, new[] { 1, 2, 1, 1, 2, 1 }, new[] { 1, 2, 1, 2, 1 }, new[] { 2, 5, 2 }, new[] { 11 }, new[] { 7 }, new[] { 2, 5, 2 }, new[] { 3, 7, 3 }, new[] { 2, 2, 3, 2, 2 }, new[] { 2, 2, 5, 2, 2 }, new[] { 1, 1, 7, 1, 1 }, new[] { 1, 2, 7, 2, 1 }, new[] { 1, 1, 7, 1, 1 }, new[] { 1, 7, 1 }, new[] { 1, 7, 1 }, new[] { 1, 5, 1 }, };

			// Pig
			//TopNumbers = new int[][] { new[] { 3, 3 }, new[] { 1, 4, 1, 1 }, new[] { 1, 1, 1 }, new[] { 2, 1, 1, 1, 1 }, new[] { 1, 1, 1 }, new[] { 1, 1, 4 }, new[] { 2, 1 }, new[] { 1, 1 }, new[] { 1, 2, 2 }, new[] { 4, 4 }, };
			//LeftNumbers = new int[][] { new[] { 4, 4 }, new[] { 1, 4, 1 }, new[] { 2, 1 }, new[] { 1, 1, 1, 2 }, new[] { 1, 1 }, new[] { 4, 1 }, new[] { 1, 1, 1 }, new[] { 2, 1, 1, 1 }, new[] { 1, 1, 2 }, new[] { 8 }, };

			// Tirex
			//var topNumbers = new [] { new[] { 8, 6 }, new[] { 6, 1, 3 }, new[] { 5, 1, 1, 2 }, new[] { 4, 2, 1, 2 }, new[] { 4, 1, 1 }, new[] { 3, 1, 1 }, new[] { 3, 2, 1 }, new[] { 3, 2, 1 }, new[] { 3, 1, 1 }, new[] { 3, 3, 1 }, new[] { 2, 4, 2 }, new[] { 2, 5, 2 }, new[] { 2, 5, 3 }, new[] { 1, 5, 3 }, new[] { 2, 2 }, new[] { 3, 2 }, new[] { 6, 1, 1 }, new[] { 4, 1, 1 }, new[] { 1, 2 }, new[] { 6, 1, 3 }, new[] { 1, 4, 6 }, new[] { 1, 5 }, new[] { 3, 3, 5 }, };
			//var leftNumbers = new [] { new[] { 14, 2 }, new[] { 13, 1, 1 }, new[] { 10, 2, 2, 1 }, new[] { 5, 3, 3, 1 }, new[] { 3, 6, 2, 2 }, new[] { 2, 6, 2, 2 }, new[] { 1, 1, 2, 6, 2, 2, 1 }, new[] { 1, 2, 2, 4, 1, 2, 1 }, new[] { 1 }, new[] { 1, 2, 6 }, new[] { 2, 1, 1, 1, 7, 3 }, new[] { 1, 1, 1, 1, 1, 5, 3 }, new[] { 2, 4 }, new[] { 4, 5 }, new[] { 6, 7 }, };

			//TopNumbers = new int[][] { new[] { 5, 3 }, new[] { 2, 1, 2, 1 }, new[] { 2, 3, 1, 1 }, new[] { 2, 1, 1, 1, 3 }, new[] { 1, 1, 5, 2 }, new[] { 1, 1, 1, 3, 1 }, new[] { 1, 1, 1, 2 }, new[] { 2, 5, 3 }, new[] { 2, 1, 1 }, new[] { 2, 2, 2, 2 }, new[] { 5, 3, 1 }, new[] { 1, 1, 1, 1 }, new[] { 3, 1 }, new[] { 1, 2, 4 }, new[] { 3, 4 }, };
			//LeftNumbers = new int[][] { new[] { 5 }, new[] { 2, 2 }, new[] { 2, 2, 1 }, new[] { 2, 1, 1, 1, 2, 1, 1 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 1, 3, 1, 1, 1, 1 }, new[] { 1, 1, 2, 2, 1 }, new[] { 2, 3, 1, 2, 2 }, new[] { 1, 2, 1 }, new[] { 4, 1, 1, 1 }, new[] { 2, 3, 3, 1 }, new[] { 1, 4, 1, 2, 1 }, new[] { 2, 1, 1, 2, 1 }, new[] { 1, 2, 1, 1 }, new[] { 3, 5 }, };

			//Elc
			//var topNumbers = new [] { new[] { 2 }, new[] { 2 }, new[] { 4 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 5 }, new[] { 1, 3 }, new[] { 9 }, new[] { 1, 12 }, new[] { 3, 7 }, new[] { 2, 11 }, new[] { 4, 6 }, new[] { 2, 5 }, new[] { 2, 6 }, new[] { 10 }, new[] { 6 }, new[] { 10 }, new[] { 4 }, };
			//var leftNumbers = new [] { new[] { 1, 1 }, new[] { 1, 1, 1, 1, 1, 1 }, new[] { 5, 5 }, new[] { 5, 5 }, new[] { 3 }, new[] { 2, 2 }, new[] { 8 }, new[] { 14 }, new[] { 2, 12 }, new[] { 11 }, new[] { 11 }, new[] { 11 }, new[] { 5, 4 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, };

			// Note
			//var topNumbers = new [] { new[] { 2 }, new[] { 4 }, new[] { 5 }, new[] { 4 }, new[] { 12 }, new[] { 3 }, new[] { 4 }, new[] { 3, 2 }, new[] { 3, 4 }, new[] { 3, 5 }, new[] { 3, 4 }, new[] { 12 }, };
			//var leftNumbers = new [] { new[] { 1 }, new[] { 3 }, new[] { 6 }, new[] { 8 }, new[] { 5, 1 }, new[] { 3, 1 }, new[] { 1, 1 }, new[] { 1, 1 }, new[] { 1, 1, 1 }, new[] { 1, 4 }, new[] { 1, 5 }, new[] { 1, 1, 5 }, new[] { 4, 3 }, new[] { 5 }, new[] { 5 }, new[] { 3 }, };

			//Coffee
			//var topNumbers = new[] { new[] { 8, 1, 7 }, new[] { 6, 2, 5 }, new[] { 5, 2, 3 }, new[] { 4, 2, 2 }, new[] { 3, 1, 4 }, new[] { 2, 4, 1 }, new[] { 2, 4 }, new[] { 1, 6 }, new[] { 1, 3 }, new[] { 1, 3 }, new[] { 3 }, new[] { 5 }, new[] { 5 }, new[] { 5 }, new[] { 3 }, new[] { 1, 3 }, new[] { 1, 3 }, new[] { 1, 6 }, new[] { 2, 4 }, new[] { 2, 4, 1 }, new[] { 3, 4, 1 }, new[] { 4, 2, 2 }, new[] { 5, 2, 3 }, new[] { 6, 2, 5 }, new[] { 8, 1, 7 } };
			//var leftNumbers = new[] { new[] { 10, 10 }, new[] { 7, 7 }, new[] { 5, 5 }, new[] { 4, 4 }, new[] { 3, 3 }, new[] { 2, 2 }, new[] { 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 4, 4 }, new[] { 8, 8 }, new[] { 8, 8 }, new[] { 6, 1, 1, 6 }, new[] { 11 }, new[] { 1, 7, 1 }, new[] { 1, 5, 1 }, new[] { 2, 3, 2 }, new[] { 2, 1, 2 }, new[] { 3, 3 }, new[] { 4, 4 }, new[] { 6, 6 } };

			//Blade
			//var topNumbers = new[] { new[] { 4 }, new[] { 6 }, new[] { 7, 1 }, new[] { 4, 3, 1 }, new[] { 4, 3, 2 }, new[] { 3, 3, 2 }, new[] { 2, 3, 3, 3 }, new[] { 2, 3, 4, 3 }, new[] { 1, 4, 3, 3 }, new[] { 1, 4, 3, 3 }, new[] { 29 }, new[] { 8, 3, 3, 2 }, new[] { 8, 3, 3, 3 }, new[] { 3, 19 }, new[] { 2, 1, 3, 3, 3 }, new[] { 1, 4, 3, 4 }, new[] { 2, 3, 3, 4 }, new[] { 2, 4, 3, 4 }, new[] { 5, 3, 4 }, new[] { 3, 2, 3, 3 }, new[] { 1, 1, 1, 3, 3 }, new[] { 3, 1, 3, 3 }, new[] { 2, 4, 3 }, new[] { 7 }, new[] { 5 }, };
			//var leftNumbers = new[] { new[] { 4 }, new[] { 5 }, new[] { 4 }, new[] { 3 }, new[] { 3 }, new[] { 3 }, new[] { 3 }, new[] { 12 }, new[] { 2,1,1,2 }, new[] { 1,1 }, new[] { 1,1,4 }, new[] { 1,1,5,2 }, new[] { 15 }, new[] { 15,1 }, new[] { 14,2 }, new[] { 10,1,3 }, new[] { 5,1,1 }, new[] { 4,1,4 }, new[] { 3,4,10 }, new[] { 11,11 }, new[] { 10,1,8 }, new[] { 5,6,3 }, new[] { 10,2 }, new[] { 17 }, new[] { 4,1,10 }, new[] { 3,1,2,8 }, new[] { 3,1,1 }, new[] { 3,3 }, new[] { 3,2 }, new[] { 1 }, };

			//TODO
			//var topNumbers = new int[][] { new[] { 30 }, new[] { 19, 8 }, new[] { 19, 4 }, new[] { 7, 10 }, new[] { 4, 15 }, new[] { 2, 17 }, new[] { 1, 5, 17 }, new[] { 16 }, new[] { 3, 2, 1, 3 }, new[] { 3, 1, 1, 1, 1, 10 }, new[] { 3, 1, 1, 1, 1, 10 }, new[] { 3, 1, 1, 2, 1, 9 }, new[] { 3, 3, 1, 2, 7 }, new[] { 3, 2, 1, 1, 1, 1, 6 }, new[] { 3, 4, 1, 6, 4, 5 }, new[] { 3, 1, 1, 1, 1, 1, 2, 4 }, new[] { 3, 2, 1, 1, 2, 2, 1, 4 }, new[] { 3, 1, 1, 1, 1, 1, 2, 4 }, new[] { 3, 1, 2, 1, 2, 4 }, new[] { 6, 1, 2, 3, 3 }, new[] { 3, 1, 1, 1, 3, 1 }, new[] { 5, 1, 1, 2, 2 }, new[] { 8, 1, 1, 2, 1, 2 }, new[] { 6, 1, 1, 2, 2, 2 }, new[] { 8, 1, 1, 1, 2, 3, 2 }, new[] { 8, 2, 1, 2, 7 }, new[] { 6, 6, 2, 8 }, new[] { 7, 3, 9 }, new[] { 4, 3, 3, 10 }, new[] { 3, 1, 2, 2, 10 }, new[] { 1, 2, 7, 12 }, new[] { 3, 6, 11 }, new[] { 1, 6, 10 }, new[] { 2, 3, 9 }, new[] { 3, 3, 8 }, new[] { 3, 2, 7 }, new[] { 3, 2, 6 }, new[] { 3, 3, 4 }, new[] { 3, 2, 2 }, new[] { 3, 3 }, };
			//var leftNumbers = new int[][] { new[] { 7, 2 }, new[] { 6, 9, 2, 2 }, new[] { 5, 12, 1, 3, 2 }, new[] { 5, 13, 1, 4, 2 }, new[] { 4, 3, 1, 6, 1 }, new[] { 4, 2, 5, 1, 9 }, new[] { 4, 2, 3, 1, 1, 8 }, new[] { 3, 2, 1, 2, 1, 8 }, new[] { 3, 2, 4, 6, 3 }, new[] { 3, 3, 4, 2, 1 }, new[] { 3, 1, 14, 1, 1, 1, 1 }, new[] { 3, 2, 1, 2, 1, 2 }, new[] { 8, 4, 5, 2, 1, 3 }, new[] { 8, 2, 1, 2, 1, 6 }, new[] { 10, 1, 4, 9 }, new[] { 8, 1, 5, 5 }, new[] { 11, 1, 4, 2, 3, 4 }, new[] { 9, 2, 2, 3, 3, 2 }, new[] { 12, 3, 3, 2, 2, 3, 1 }, new[] { 1, 4, 1, 1, 2, 4 }, new[] { 1, 4, 2, 8, 1, 5, 4 }, new[] { 1, 4, 3, 1, 1, 1, 2, 7, 3 }, new[] { 2, 3, 3, 1, 2, 2, 9, 1 }, new[] { 2, 3, 4, 2, 2, 2, 11 }, new[] { 2, 3, 5, 3, 2, 13 }, new[] { 3, 2, 6, 2, 14 }, new[] { 3, 2, 12, 16 }, new[] { 3, 2, 12, 13 }, new[] { 3, 1, 10, 18 }, new[] { 2, 9, 19 }, };

			//Cat (cat??)
			var topItems = new[] { new[] { 10, 8 }, new[] { 11, 3, 5 }, new[] { 12, 3, 2, 3, 2 }, new[] { 12, 4, 4, 2, 3 }, new[] { 10, 1, 3, 4, 2, 3 }, new[] { 8, 1, 3, 4, 2, 4 }, new[] { 7, 2, 3, 2, 2, 4 }, new[] { 5, 3, 3, 3, 5 }, new[] { 2, 5, 7, 5 }, new[] { 7, 4, 2, 2 }, new[] { 10, 2, 1, 1, 1 }, new[] { 11, 1, 1, 3, 1 }, new[] { 11, 1, 1, 3, 1 }, new[] { 11, 1, 1, 3, 1 }, new[] { 10, 2, 1, 1, 1 }, new[] { 7, 4, 2, 2 }, new[] { 2, 5, 7, 5 }, new[] { 4, 3, 3, 3, 5 }, new[] { 7, 2, 3, 2, 2, 4 }, new[] { 8, 1, 3, 4, 2, 4 }, new[] { 10, 1, 3, 4, 2, 3 }, new[] { 12, 4, 4, 2, 3 }, new[] { 12, 3, 2, 2, 2 }, new[] { 12, 3, 5 }, new[] { 10, 8 }, };
			var leftItems = new[] { new[] { 1, 1 }, new[] { 3, 3 }, new[] { 4, 5 }, new[] { 6, 6 }, new[] { 7, 6 }, new[] { 8, 8 }, new[] { 8, 8 }, new[] { 9, 9 }, new[] { 9, 9 }, new[] { 7, 7 }, new[] { 5, 9, 5 }, new[] { 4, 13, 4 }, new[] { 3, 17, 3 }, new[] { 2, 1, 11, 1, 2 }, new[] { 1, 5, 9, 5, 1 }, new[] { 7, 7, 7 }, new[] { 7, 7, 7 }, new[] { 2, 2, 5, 2, 2 }, new[] { 1, 3, 1, 5, 1, 3, 1 }, new[] { 1, 5, 1, 5, 1, 5, 1 }, new[] { 1, 5, 2, 3, 2, 5, 1 }, new[] { 1, 3, 2, 2, 3, 1 }, new[] { 3, 4, 4, 2 }, new[] { 25 }, new[] { 8, 8 }, new[] { 1, 7, 1 }, new[] { 9, 9 }, new[] { 7, 3, 7 }, new[] { 6, 5, 6 }, new[] { 4, 3, 4 }, new[] { 3, 3 }, new[] { 7 }, };

			TopItems = TransformToCagesArray(topItems);
			LeftItems = TransformToCagesArray(leftItems);

			TopItems = new CageNumber[20][];
			for (int i = 0; i < 20; i++)
			{
				TopItems[i] = new CageNumber[10];
				for (int j = 0; j < 10; j++)
				{
					TopItems[i][j] = new CageNumber();
				}
			}

			LeftItems = new CageNumber[20][];
			for (int i = 0; i < 20; i++)
			{
				LeftItems[i] = new CageNumber[10];

				for (int j = 0; j < 10; j++)
				{
					LeftItems[i][j] = new CageNumber();
				}
			}

			//_solver = new CrosswordSolver(topItems, leftItems);
			//DrawCages();
		}

		private int[][] TransformToInt32Array(CageNumber[][] cageNumbers)
		{
			var result = new List<int[]>();
			foreach (var cageNumber in cageNumbers)
			{
				result.Add(cageNumber.Where(w=> !string.IsNullOrEmpty(w.Number)).Select(w => Convert.ToInt32(w.Number)).ToArray());
			}

			return result.ToArray();
		}

		private CageNumber[][] TransformToCagesArray(int[][] numbers)
		{
			var result = new List<CageNumber[]>();
			foreach (var number in numbers)
			{
				result.Add(number.Select(w => CageNumber.From(w)).ToArray());
			}

			return result.ToArray();
		}

		private void DrawCages()
		{
			ContentGrid.Children.Clear();
			for (int i = 0; i < _solver.Cages.GetLength(0); i++)
			{
				for (int j = 0; j < _solver.Cages.GetLength(1); j++)
				{
					var cage = CreateGrid(i, j, _solver.Cages[i, j]);
					ContentGrid.Children.Add(cage);
				}
			}
		}

		private Grid CreateGrid(int i, int j, CageType type)
		{
			var grid = new Grid();
			grid.Width = 24;
			grid.Height = 24;
			grid.HorizontalAlignment = HorizontalAlignment.Left;
			grid.VerticalAlignment = VerticalAlignment.Top;

			switch (type)
			{
				case CageType.Black:
					grid.Background = new SolidColorBrush(Colors.Black);
					break;
				case CageType.White:
					grid.Background = new SolidColorBrush(Colors.White);
					break;
				case CageType.Point:
					grid.Background = new SolidColorBrush(Colors.Silver);
					break;
			}

			grid.Margin = new Thickness(24 * j, 24 * i, 0, 0);

			return grid;
		}

		private void OnCompleteButtonClick(object sender, RoutedEventArgs e)
		{

			_solver = new CrosswordSolver(TransformToInt32Array(TopItems), TransformToInt32Array(LeftItems));

			for (int i = 0; i < 50; i++)
			{
				_solver.MakeStep();
			}
			
			DrawCages();
		}

		private void OnBeforeTextChanged(TextBox sender, TextBoxTextChangingEventArgs args)
		{
			sender.Text = new string(sender.Text.Where(char.IsDigit).ToArray());
		}
	}
}
