using JapanCrossworkSolver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Demo.Win8.Converters
{
	public class CageTypeToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if ((CageType)value == CageType.White)
			{
				return "Snow";
			}
			if ((CageType)value == CageType.Black)
			{
				return "#FF1B1B1B";
			}
			return "Gray";

		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
