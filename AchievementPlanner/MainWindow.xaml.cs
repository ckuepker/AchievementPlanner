using System.Windows;
using System.Windows.Controls;
using de.inc47.AchievementPlanner.ViewModel;

namespace AchievementPlanner
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new MainWindowViewModel();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      var mwvm = DataContext as MainWindowViewModel;
      mwvm.LoadFromApi();
    }

    private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
    {
      LogTextBox.ScrollToLine(LogTextBox.LineCount-1);
    }
  }
}
