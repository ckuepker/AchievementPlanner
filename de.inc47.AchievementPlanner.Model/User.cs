using System.Collections.Generic;
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
  }
}