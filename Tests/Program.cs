using JapanCrossworkSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			var a = new CrosswordSolver(new int[][] { new[] { 1, 2, 3 }, new[] { 4, 3, 1 }, new[] { 6, 6, 6 } }, new int[][] { new[] { 1, 3 }, new[] { 5, 2 }, new[] { 1 } });


			var c = a.ArrayOfCages;
		}
	}
}
