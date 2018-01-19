using System.Collections.Generic;

namespace de.inc47.AchievementPlanner.Model
{
  public interface IUser : IModelElement
  {
    string Name { get; }
    ulong SteamId { get; }
    string AvatarUrl { get; }
    IEnumerable<IGame> OwnedGames { get; set; }
    uint GameCount { get; }
    uint GamesWithAchievementsCount { get; }
    uint GamesWithAchievedAchievementsCount { get; }
    uint PlayedGamesWithAchievementsCount { get; }
    uint CompleteGamesCount { get; }
  }
}
