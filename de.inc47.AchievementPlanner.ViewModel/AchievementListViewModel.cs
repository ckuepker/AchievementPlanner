using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementListViewModel : ViewModelBase, IAchievementsListViewModel
  {
    private readonly ObservableCollection<IAchievementViewModel> _achievements;

    public AchievementListViewModel(IEnumerable<IAchievement> achievements, Func<IAchievement,IGame> getGame)
    {
      _achievements = new ObservableCollection<IAchievementViewModel>(achievements.OrderBy(a => a.GlobalCompletionPercentage).Select(a => new AchievementViewModel(a, getGame)));
    }

    public ObservableCollection<IAchievementViewModel> Achievements
    {
      get { return _achievements; }
    }
  }
}