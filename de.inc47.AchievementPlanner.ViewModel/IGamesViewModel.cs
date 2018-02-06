using System.Collections.ObjectModel;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public interface IGamesViewModel : IViewModelBase
  {
    ObservableCollection<IGame> Games { get; }
  }
}
