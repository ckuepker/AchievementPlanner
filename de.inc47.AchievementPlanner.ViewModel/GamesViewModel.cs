using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class GamesViewModel : ViewModelBase, IGamesViewModel
  {
    private readonly ObservableCollection<IGame> _games;

    public GamesViewModel(IEnumerable<IGame> games)
    {
      _games = new ObservableCollection<IGame>(games.Where(g => g.CompletedAchievementCount > 0).OrderBy(g => g.CompletionRate));
    }

    public ObservableCollection<IGame> Games
    {
      get { return _games; }
    }
  }
}