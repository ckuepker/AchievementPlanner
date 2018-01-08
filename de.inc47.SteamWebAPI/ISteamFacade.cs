using System.Collections.Generic;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.SteamWebAPI
{
  public interface ISteamFacade
  {
    HashSet<IAchievement> GetAchievements(uint appId);
    HashSet<IGame> GetGamesOfUser(ulong steamId);
  }
}
