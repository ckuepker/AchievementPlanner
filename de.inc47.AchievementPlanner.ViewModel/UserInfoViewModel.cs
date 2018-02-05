using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class UserInfoViewModel : ViewModelBase
  {
    private readonly IUser _user;

    private Dictionary<string, HashSet<string>> _propertyMap = new Dictionary<string, HashSet<string>>
    {
      {
        "OwnedGames", new HashSet<string>
        {
          "GameCount",
          "GamesWithAchievementsCount",
          "GamesWithAchievedAchievementsCount",
          "PlayedGamesWithAchievementsCount",
          "CompleteGamesCount",
          "PossibleAchievementCount",
          "PossibleAchievementOfPlayedGamesCount",
          "PossibleAchievementOfAchievedGamesCount",
          "AchievedAchievementCount",
          "AverageGameCompletionRate",
          "OverallCompletionRate"
        }
      },
    };

    public UserInfoViewModel(IUser user)
    {
      if (user == null)
      {
        throw new ArgumentException("User may not be null");
      }
      _user = user;
      Name = _user.Name;
      Avatar = _user.AvatarUrl;
      _user.PropertyChanged += UserOnPropertyChanged;
    }

    private void UserOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
    {
      string sourceProperty = propertyChangedEventArgs.PropertyName;
      if (_propertyMap.ContainsKey(propertyChangedEventArgs.PropertyName))
      {
        foreach (string targetProperty in _propertyMap[sourceProperty])
        {
          OnPropertyChanged(targetProperty);
        }
      }
    }

    public int GameCount
    {
      get { return _user.OwnedGames.Count(); }
    }

    public int GamesWithAchievementsCount
    {
      get { return _user.OwnedGames.Count(g => g.Achievements.Any()); }
    }

    public int GamesWithAchievedAchievementsCount
    {
      get { return _user.OwnedGames.Count(g => g.Achievements.Any(a => a.Completed)); }
    }

    public int PlayedGamesWithAchievementsCount
    {
      get { return _user.OwnedGames.Count(g => g.Playtime > TimeSpan.Zero && g.Achievements.Any()); }
    }

    public int CompleteGamesCount
    {
      get { return _user.OwnedGames.Count(g => g.Achievements.Any() && g.Achievements.All(a => a.Completed)); }
    }

    public int PossibleAchievementCount
    {
      get { return _user.OwnedGames.Sum(g => g.Achievements.Count()); }
    }

    public int PossibleAchievementOfPlayedGamesCount
    {
      get { return _user.OwnedGames.Where(g => g.Playtime > TimeSpan.Zero).Sum(g => g.AchievementCount); }
    }

    public int PossibleAchievementOfAchievedGamesCount
    {
      get { return _user.OwnedGames.Where(g => g.Achievements.Any(a => a.Completed)).Sum(g => g.AchievementCount); }
    }

    public int AchievedAchievementCount
    {
      get { return _user.OwnedGames.Where(g => g.CompletedAchievementCount > 0).Sum(g => g.CompletedAchievementCount); }
    }

    public string Name { get; set; }
    public string Avatar { get; set; }

    public double AverageGameCompletionRate
    {
      get
      {
        //double z = (double) AchievedAchievementCount; // 1773
        //double n = (double) PossibleAchievementOfAchievedGamesCount;

        // Indeed steam does an average of completion rates instead of calculating the ratio of achieved/achievable achievements
        double n = (double) GamesWithAchievedAchievementsCount;
        double z = (double) _user.OwnedGames.Where(g => g.CompletedAchievementCount > 0).Sum(g => g.CompletionRate);
        double r = z / n;
        return r;
      }
    }

    public double OverallCompletionRate
    {
      get
      {
        double z = (double)AchievedAchievementCount; // 1773
        double n = (double)PossibleAchievementOfAchievedGamesCount;
        double r = z / n;
        return r;
      }
    }
  }
}