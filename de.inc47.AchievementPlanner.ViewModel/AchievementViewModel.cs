using System;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementViewModel : ViewModelBase, IAchievementViewModel
  {
    private readonly IAchievement _achievement;
    private readonly Func<IUserInfoViewModel> _getUserInfo;

    public AchievementViewModel(IAchievement achievement, Func<IAchievement, IGame> getGame, Func<IUserInfoViewModel> getUserInfo)
    {
      _achievement = achievement;
      Game = getGame(_achievement);
      _getUserInfo = getUserInfo;
      IconUrl = _achievement.IconUrl;
      Name = _achievement.Name;
      Description = _achievement.Description;
      Completed = _achievement.Completed;
      GameIconUrl = Game != null ? Game.IconUrl : string.Empty;
      GlobalCompletionPercentage = achievement.GlobalCompletionPercentage;
      CompletionRateIncrement = Completed ? 0d : 1d / (double)Game.AchievementCount;
      AverageCompletionRateIncrement = CompletionRateIncrement / (double)_getUserInfo().GamesWithAchievedAchievementsCount;
    }

    public IGame Game { get; }
    public string IconUrl { get; }
    public string Name { get; }
    public string Description { get; }
    public bool Completed { get; }
    public string GameIconUrl { get; }
    public double GlobalCompletionPercentage { get; }
    public double CompletionRateIncrement { get; }
    public double AverageCompletionRateIncrement { get; }
  }
}