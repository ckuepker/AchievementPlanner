using System.Collections.Generic;

namespace de.inc47.AchievementPlanner.Model
{
  public interface IUser
  {
    string Name { get; }
    ulong SteamId { get; }
    string AvatarUrl { get; }
    IEnumerable<IGame> OwnedGames { get; set; }
  }
}
