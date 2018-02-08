using System;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class AchievementViewModel : ViewModelBase, IAchievementViewModel
  {
    private readonly IGame _game;
    private readonly IAchievement _achievement;

    public AchievementViewModel(IAchievement achievement, Func<IAchievement,IGame> getGame)
    {
      _achievement = achievement;
      _game = getGame(_achievement);
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
  }
}