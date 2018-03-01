using System.Collections.ObjectModel;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public interface IAchievementsListViewModel : IViewModelBase
  {
    ObservableCollection<IAchievementViewModel> Achievements { get; }
  }
}