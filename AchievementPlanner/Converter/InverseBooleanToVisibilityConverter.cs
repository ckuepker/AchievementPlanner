using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AchievementPlanner.Converter
{
  public class InverseBooleanToVisibilityConverter : IValueConverter
  {

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool)
      {
        var b = (bool) value;
        if (b)
        {
          return Visibility.Collapsed;
        }
        return Visibility.Visible;
      }
      throw new ArgumentException("Can only convert bools");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
