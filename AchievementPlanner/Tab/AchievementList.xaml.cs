using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using de.inc47.AchievementPlanner.ViewModel;

namespace AchievementPlanner.Tab
{
  /// <summary>
  /// Interaction logic for AchievementList.xaml
  /// </summary>
  public partial class AchievementList : UserControl
  {
    public AchievementList()
    {
      InitializeComponent();
    }

    private void AchievementsCollection_OnFilter(object sender, FilterEventArgs e)
    {
      IAchievementViewModel a = e.Item as IAchievementViewModel;
      if (ShowCompletedCheckBox.IsChecked.HasValue && ShowCompletedCheckBox.IsChecked.Value)
      {
        if (ShowUnplayedCheckBox.IsChecked.HasValue && ShowUnplayedCheckBox.IsChecked.Value)
        {
          e.Accepted = true;
        }
        else
        {
          e.Accepted = a.Completed || a.Game.CompletedAchievementCount > 0;
        }
      }
      else
      {
        if (ShowUnplayedCheckBox.IsChecked.HasValue && ShowUnplayedCheckBox.IsChecked.Value)
        {
          e.Accepted = !a.Completed;
        }
        else
        {
          e.Accepted = !a.Completed && a.Game.CompletedAchievementCount > 0;
        }
      }
    }

    private void FilterCheckboxChanged(object sender, RoutedEventArgs e)
    {
      var achievementsCvs = this.Resources["AchievementsCollection"] as CollectionViewSource;
      if (achievementsCvs != null && achievementsCvs.View != null)
      {
        achievementsCvs.View.Refresh();
      }
    }
  }
}
