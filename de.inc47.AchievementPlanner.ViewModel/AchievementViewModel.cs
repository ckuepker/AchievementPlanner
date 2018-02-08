using System;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementViewModel : ViewModelBase, IAchievementViewModel
  {
    private readonly IGame _game;
    private readonly IAchievement _achievement;
    private readonly Func<UserInfoViewModel> _getUserInfo;

    public AchievementViewModel(IAchievement achievement, Func<IAchievement,IGame> getGame, Func<UserInfoViewModel> getUserInfo)
    {
      _achievement = achievement;
      _game = getGame(_achievement);
      _getUserInfo = getUserInfo;
      IconUrl = _achievement.IconUrl;
      Name = _achievement.Name;
      Description = _achievement.Description;
      Completed = _achievement.Completed;
      GameIconUrl = _game != null ? _game.IconUrl : string.Empty;
      GlobalCompletionPercentage = achievement.GlobalCompletionPercentage;
    }

    public string IconUrl { get; }
    public string Name { get; }
    public string Description { get; }
    public bool Completed { get; }
    public string GameIconUrl { get; }
    public double GlobalCompletionPercentage { get; }

    public double CompletionRateIncrement
    {
      get { return Completed ? 0d : 1d / (double) _game.AchievementCount; }
    }

    public double AverageCompletionRateIncrement
    {
      get { return CompletionRateIncrement / (double) _getUserInfo().GamesWithAchievedAchievementsCount; }
    }
  }
}