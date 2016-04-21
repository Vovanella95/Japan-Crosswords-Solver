using JapanCrossworkSolver;
using JapanCrossworkSolver.Models;
using MvvmCross.Core.ViewModels;

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


		public FirstViewModel()
		{
			_solver = new CrosswordSolver(new int[][] { new[] { 2 }, new[] { 1, 2 }, new[] { 1, 1 }, new[] { 3 }, new[] { 1 } },
										  new int[][] { new[] { 1 }, new[] { 1 }, new[] { 1, 1 }, new[] { 2, 1 }, new[] { 4 } });
			Cages = _solver.ArrayOfCages;

			GridHeight = _solver.Height * 25;
			GridWidth = _solver.Width * 25;
		}

		public IMvxCommand MakeStep
		{
			get
			{
				return new MvxCommand(() =>
				{
					_solver.MakeStep();
					Cages = _solver.ArrayOfCages;
				});
			}
		}
	}
}
