﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace de.inc47.AchievementPlanner.Model
{
  public class User : ModelElement, IUser
  {
    /// <summary>
    /// Creates a user without associated games.
    /// </summary>
    /// <param name="steamId"></param>
    /// <param name="name"></param>
    /// <param name="avatarUrl"></param>
    /// <param name="games"></param>
    public User(ulong steamId, string name, string avatarUrl)
    {
      SteamId = steamId;
      Name = name;
      AvatarUrl = avatarUrl;
      Dirty = true;
    }

    /// <summary>
    /// Creates a user with associated games. This constructor is required for automated deserialization.
    /// </summary>
    /// <param name="steamId"></param>
    /// <param name="name"></param>
    /// <param name="avatarUrl"></param>
    /// <param name="games"></param>
    [JsonConstructor]
    public User(ulong steamId, string name, string avatarUrl, IEnumerable<Game> games)
    {
      SteamId = steamId;
      Name = name;
      AvatarUrl = avatarUrl;
      OwnedGames = games;
    }

    public string Name { get; }
    public ulong SteamId { get; }
    public string AvatarUrl { get; }
    public IEnumerable<IGame> OwnedGames { get; set; }

    public int GameCount
    {
      get
      {
        return OwnedGames.Count();
      }
    }

    public int GamesWithAchievementsCount
    {
      get
      {
        return OwnedGames.Count(g => g.AchievementCount > 0);
      }
    }

    public int GamesWithAchievedAchievementsCount
    {
      get
      {
        return OwnedGames.Count(g => g.AchievementCount > 0 && g.Achievements.Any(a => a.Completed));
      }
    }

    public int PlayedGamesWithAchievementsCount
    {
      get
      {
        return OwnedGames.Count(g => g.Playtime > TimeSpan.Zero && g.AchievementCount > 0);
      }
    }

    public int CompleteGamesCount
    {
      get
      {
        return OwnedGames.Count(g => g.AchievementCount > 0 && g.Achievements.All(a => a.Completed));
      }
    }

    public int PossibleAchievementCount
    {
      get
      {
        return OwnedGames.Sum(g => g.AchievementCount);
      }
    }

    public int PossibleAchievementOfPlayedGamesCount
    {
      get
      {
        return OwnedGames.Where(g => g.AchievementCount > 0 && g.Playtime > TimeSpan.Zero).Sum(g => g.AchievementCount);
      }
    }

    public int PossibleAchievementOfAchievedGamesCount
    {
      get
      {
        return OwnedGames.Where(g => g.AchievementCount > 0 && g.Achievements.Any(a => a.Completed)).Sum(g => g.AchievementCount);
      }
    }

    public int AchievedAchievementCount
    {
      get
      {
        return OwnedGames.Where(g => g.CompletedAchievementCount > 0).Sum(g => g.CompletedAchievementCount);
      }
    }
  }
}