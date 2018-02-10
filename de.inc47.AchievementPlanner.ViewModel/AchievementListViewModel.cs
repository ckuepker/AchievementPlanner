using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementListViewModel : ViewModelBase, IAchievementsListViewModel
  {
    private ObservableCollection<IAchievementViewModel> _achievements;
    private bool _isShowCompleted;
    private bool _isShowUnplayed;
    private readonly IEnumerable<IAchievementViewModel> _completeAchievementViewModels;

    public AchievementListViewModel(IEnumerable<IAchievement> achievements, Func<IAchievement, IGame> getGame, Func<IUserInfoViewModel> getUserInfo)
    {
      // TODO Store in user settings
      _isShowCompleted = true;
      _isShowUnplayed = false;
      _completeAchievementViewModels = new List<IAchievementViewModel>(achievements.Select(a => new AchievementViewModel(a, getGame, getUserInfo)));
      _achievements = new ObservableCollection<IAchievementViewModel>(_completeAchievementViewModels.Where(a => a.Game.CompletedAchievementCount > 0).OrderBy(a => a.Weight).Reverse());
    }

    public ObservableCollection<IAchievementViewModel> Achievements
    {
      get { return _achievements; }
      set { _achievements = value; }
    }

    public bool IsShowCompleted
    {
      get { return _isShowCompleted; }
      set
      {
        if (_isShowCompleted != value)
        {
          _isShowCompleted = value;
          var completed = _completeAchievementViewModels.Where(a => a.Completed);
          foreach (IAchievementViewModel avm in completed)
          {
            if (value)
            {
              Achievements.Insert(FindInsertIndex(avm, Achievements, a => a.Weight), avm);
            }
            else
            {
              Achievements.Remove(avm);
            }
          }
          OnPropertyChanged(nameof(IsShowCompleted));
        }
      }
    }

    public bool IsShowUnplayed
    {
      get { return _isShowUnplayed; }
      set
      {
        if (_isShowUnplayed != value)
        {
          var unplayed = _completeAchievementViewModels.Where(a => a.Game.AchievementCount > 0 && a.Game.CompletedAchievementCount == 0);
          foreach (IAchievementViewModel avm in unplayed)
          {
            if (value)
            {
              Achievements.Insert(FindInsertIndex(avm, Achievements, a => a.Weight), avm);
            }
            else
            {
              Achievements.Remove(avm);
            }
          }
          _isShowUnplayed = value;
          OnPropertyChanged(nameof(IsShowUnplayed));
        }
      }
    }

    private int FindInsertIndex(IAchievementViewModel avm, ObservableCollection<IAchievementViewModel> targetCollection, Func<IAchievementViewModel, double> sortingKeyFunc)
    {
      double compareKeyValue = sortingKeyFunc(avm);
      for (int i = 0; i < targetCollection.Count; i++)
      {
        var candidate = targetCollection[i];
        if (sortingKeyFunc(candidate) < compareKeyValue)
        {
          return i == 0 ? 0 : i - 1;
        }
      }
      return targetCollection.Count;
    }
  }
}