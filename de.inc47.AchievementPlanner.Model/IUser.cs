using System.Collections.Generic;

namespace de.inc47.AchievementPlanner.Model
{
  public interface IUser : IModelElement
  {
    string Name { get; }
    ulong SteamId { get; }
    string AvatarUrl { get; }
    IEnumerable<IGame> OwnedGames { get; set; }
    int GameCount { get; }
    int GamesWithAchievementsCount { get; }
    int GamesWithAchievedAchievementsCount { get; }
    int PlayedGamesWithAchievementsCount { get; }
    int CompleteGamesCount { get; }
    int PossibleAchievementCount { get; }
    int PossibleAchievementOfPlayedGamesCount { get; }
    int PossibleAchievementOfAchievedGamesCount { get; }
    int AchievedAchievementCount { get; }
  }
}
