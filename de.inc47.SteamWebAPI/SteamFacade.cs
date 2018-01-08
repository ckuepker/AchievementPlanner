using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.inc47.AchievementPlanner.Configuration;
using de.inc47.AchievementPlanner.Model;
using Steam.Models;
using Steam.Models.SteamCommunity;
using SteamWebAPI2.Interfaces;

namespace de.inc47.SteamWebAPI
{
  public class SteamFacade : ISteamFacade
  {
    private string _apiKey = Configuration.SteamWebApiKey;

    public SteamFacade()
    {
    }

    public HashSet<IAchievement> GetAchievements(uint appId)
    {
      var i = new SteamUserStats(_apiKey);
      //var task = i.GetGlobalAchievementPercentagesForAppAsync(appId);
      var task = i.GetSchemaForGameAsync(appId);
      HashSet<IAchievement> achievements = new HashSet<IAchievement>();
      IReadOnlyCollection<SchemaGameAchievementModel> resultList = task.Result.Data.AvailableGameStats.Achievements;
      foreach (SchemaGameAchievementModel a in resultList)
      {
        IAchievement ach = new Achievement(a.DisplayName, a.Description, a.Icon);
        achievements.Add(ach);
      }
      return achievements;
    }

    public HashSet<IGame> GetGamesOfUser(ulong steamId)
    {
      var i = new PlayerService(_apiKey);
      var task = i.GetOwnedGamesAsync(steamId, true, true);
      
      HashSet<IGame> result = new HashSet<IGame>();
      foreach (OwnedGameModel game in task.Result.Data.OwnedGames)
      {
        IGame g = new Game(game.AppId, game.Name, game.ImgIconUrl, game.PlaytimeForever);
        result.Add(g);
      }
      return result;
    }

    
  }
}

