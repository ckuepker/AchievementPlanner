using System;
using System.Globalization;
using System.Windows.Data;

namespace AchievementPlanner.Converter
{
  internal class CompletionStateToOpacityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool completed = (bool) value;
      if (completed)
      {
        return 1d;
      }
      return 0.3;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
