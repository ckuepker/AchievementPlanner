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

    public AchievementListViewModel(IEnumerable<IAchievement> achievements, Func<IAchievement,IGame> getGame, Func<UserInfoViewModel> getUserInfo)
    {
      _achievements = new ObservableCollection<IAchievementViewModel>(achievements.Select(a => new AchievementViewModel(a, getGame, getUserInfo)).OrderBy(a => a.Weight).Reverse());
    }

    public ObservableCollection<IAchievementViewModel> Achievements
    {
      get { return _achievements; }
    }
  }
}