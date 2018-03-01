using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementListViewModel : ViewModelBase, IAchievementsListViewModel
  {

    public AchievementListViewModel(IEnumerable<IAchievement> achievements, Func<IAchievement, IGame> getGame, Func<IUserInfoViewModel> getUserInfo)
    {
      Achievements = new ObservableCollection<IAchievementViewModel>(achievements.Select(a => new AchievementViewModel(a, getGame, getUserInfo)));
    }

    public ObservableCollection<IAchievementViewModel> Achievements { get; set; }
  }
}