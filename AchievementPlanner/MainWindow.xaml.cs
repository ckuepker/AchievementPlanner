using System.Windows;
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
  }
}
