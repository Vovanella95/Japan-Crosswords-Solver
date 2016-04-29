using JapanCrossworkSolver;
using JapanCrossworkSolver.Models;
using MvvmCross.Core.ViewModels;
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
			TopNumbers = new int[][] { new[] { 4 }, new[] { 2, 2, 1 }, new[] { 1, 2, 1, 1 }, new[] { 1, 1, 1, 1, 1 }, new[] { 1, 1, 1, 1, 1 }, new[] { 1, 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, new[] { 1, 1, 5 }, new[] { 3, 1 }, };
			LeftNumbers = new int[][] { new[] { 7 }, new[] { 1,1 }, new[] { 1,1 }, new[] { 8 }, new[] { 1,1 }, new[] { 9 }, new[] { 1,1 }, new[] { 1,3,1 }, new[] { 1,2 }, new[] { 8 }, };

			_solver = new CrosswordSolver(TopNumbers, LeftNumbers);

			Cages = _solver.ArrayOfCages;

			GridHeight = _solver.Height * 21;
			GridWidth = _solver.Width * 21;
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