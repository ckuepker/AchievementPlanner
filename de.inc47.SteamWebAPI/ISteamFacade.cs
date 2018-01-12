using System;
using System.Collections.Generic;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.SteamWebAPI
{
  public interface ISteamFacade
  {
    /// <summary>
    /// Retrieves all achievements for a game from the Steam web API or an empty collection if none are available.
    /// </summary>
    /// <param name="appId"></param>
    /// <returns></returns>
    HashSet<IAchievement> GetAchievements(uint appId);
    IEnumerable<IGame> GetGamesOfUser(ulong steamId);
    Tuple<string, string> GetUserInfo(ulong steamId);
    void GetAchievementCompletionStates(ulong steamId, IGame game);
  }
}
