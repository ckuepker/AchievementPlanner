using System.Collections.ObjectModel;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public interface IAchievementsListViewModel : IViewModelBase
  {
    ObservableCollection<IAchievementViewModel> Achievements { get; }

    bool IsShowCompleted { get; set; }
    bool IsShowUnplayed { get; set; }
  }
}