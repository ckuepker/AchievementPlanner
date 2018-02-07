using System;
using System.Collections.Generic;
using System.Linq;
using de.inc47.AchievementPlanner.Configuration;
using de.inc47.AchievementPlanner.Model;
using Steam.Models;
using Steam.Models.SteamCommunity;
using Steam.Models.SteamPlayer;
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
      var achievementsTask = i.GetSchemaForGameAsync(appId);
      HashSet<IAchievement> achievements = new HashSet<IAchievement>();
      var response = achievementsTask.Result.Data;
      if (response.AvailableGameStats == null || response.AvailableGameStats.Achievements == null)
      {
        return new HashSet<IAchievement>();
      }
      IReadOnlyCollection<SchemaGameAchievementModel> resultList = response.AvailableGameStats.Achievements;
      foreach (SchemaGameAchievementModel a in resultList)
      {
        IAchievement ach = new Achievement(a.DisplayName, a.Description, a.Icon, a.Name);
        achievements.Add(ach);
      }
      var globalPercentageTask = i.GetGlobalAchievementPercentagesForAppAsync(appId);
      foreach (GlobalAchievementPercentageModel globalAchievementPercentageModel in globalPercentageTask.Result.Data)
      {
        IAchievement achievement = achievements.FirstOrDefault(a => a.InternalShortName == globalAchievementPercentageModel.Name);
        if (achievement != null)
        {
          achievement.GlobalCompletionPercentage = globalAchievementPercentageModel.Percent;
        }
      }
      return achievements;
    }

    public IEnumerable<IGame> GetGamesOfUser(ulong steamId)
    {
      var i = new PlayerService(_apiKey);
      var task = i.GetOwnedGamesAsync(steamId, includeAppInfo: true, includeFreeGames: true);

      IList<Game> games = task.Result.Data.OwnedGames.Select(og => new Game(og.AppId, og.Name, og.ImgIconUrl, og.PlaytimeForever)).ToList();
      games.Add(new Game(245550, "Free To Play", "", TimeSpan.FromMinutes(90)));
      return games;
    }

    public Tuple<string, string> GetUserInfo(ulong steamId)
    {
      var i = new SteamUser(_apiKey);
      var t = i.GetPlayerSummaryAsync(steamId);
      PlayerSummaryModel m = t.Result.Data;
      return new Tuple<string, string>(m.Nickname, m.AvatarMediumUrl);
    }

    public void GetAchievementCompletionStates(ulong steamId, IGame game)
    {
      var i = new SteamUserStats(_apiKey);
      var t = i.GetPlayerAchievementsAsync(game.AppId, steamId);
      var result = t.Result.Data;
      foreach (PlayerAchievementModel playerAchievementModel in result.Achievements.Where(a => a.Achieved == 1))
      {
        var a = game.Achievements.First(ach => ach.Name == playerAchievementModel.Name);
        a.Completed = true;
      }
    }
  }
}

