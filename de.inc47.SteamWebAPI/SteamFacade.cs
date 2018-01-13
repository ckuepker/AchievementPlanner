using System;
using System.Collections.Generic;
using System.Linq;
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
      var response = task.Result.Data;
      if (response.AvailableGameStats == null || response.AvailableGameStats.Achievements == null)
      {
        return new HashSet<IAchievement>();
      }
      IReadOnlyCollection<SchemaGameAchievementModel> resultList = response.AvailableGameStats.Achievements;
      foreach (SchemaGameAchievementModel a in resultList)
      {
        IAchievement ach = new Achievement(a.DisplayName, a.Description, a.Icon);
        achievements.Add(ach);
      }
      return achievements;
    }

    public IEnumerable<IGame> GetGamesOfUser(ulong steamId)
    {
      var i = new PlayerService(_apiKey);
      var task = i.GetOwnedGamesAsync(steamId, true, true);
      
      return task.Result.Data.OwnedGames.Select(og => new Game(og.AppId,og.Name,og.ImgIconUrl,og.PlaytimeForever));
    }

    public Tuple<string, string> GetUserInfo(ulong steamId)
    {
      var i = new SteamUser(_apiKey);
      var t = i.GetPlayerSummaryAsync(steamId);
      PlayerSummaryModel m = t.Result.Data;
      return new Tuple<string, string>(m.Nickname, m.AvatarFullUrl);
    }

    public void GetAchievementCompletionStates(ulong steamId, IGame game)
    {
      var i = new SteamUserStats(_apiKey);
      var t = i.GetPlayerAchievementsAsync(game.AppId, steamId);
      var result = t.Result.Data;
      foreach (var playerAchievementModel in result.Achievements.Where(a => a.Achieved == 1))
      {
        var a = game.Achievements.First(ach => ach.Name == playerAchievementModel.Name);
        a.Completed = true;
      }
    }
  }
}

