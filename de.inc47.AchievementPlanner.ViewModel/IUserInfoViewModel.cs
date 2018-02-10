namespace de.inc47.AchievementPlanner.ViewModel
{
  public interface IUserInfoViewModel : IViewModelBase
  {
    int GameCount { get; }
    int GamesWithAchievementsCount { get; }
    int GamesWithAchievedAchievementsCount { get; }
    int PlayedGamesWithAchievementsCount { get; }
    int CompleteGamesCount { get; }
    int PossibleAchievementCount { get; }
    int PossibleAchievementOfPlayedGamesCount { get; }
    int PossibleAchievementOfAchievedGamesCount { get; }
    int AchievedAchievementCount { get; }
    string Name { get; set; }
    string Avatar { get; set; }
    double AverageGameCompletionRate { get; }
    double OverallCompletionRate { get; }
  }
}