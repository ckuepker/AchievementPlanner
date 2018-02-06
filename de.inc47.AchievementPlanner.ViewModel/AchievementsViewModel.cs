using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementsViewModel : ViewModelBase, IAchievementsViewModel
  {
    private readonly ObservableCollection<IAchievement> _achievements;

    public AchievementsViewModel(IEnumerable<IAchievement> achievements)
    {
      _achievements = new ObservableCollection<IAchievement>(achievements.OrderBy(a => a.Name));
    }

    public ObservableCollection<IAchievement> Achievements
    {
      get { return _achievements; }
    }
  }
}