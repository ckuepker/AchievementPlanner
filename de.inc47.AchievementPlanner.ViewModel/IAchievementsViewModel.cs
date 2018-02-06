using System.Collections.ObjectModel;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public interface IAchievementsViewModel : IViewModelBase
  {
    ObservableCollection<IAchievement> Achievements { get; }
  }
}