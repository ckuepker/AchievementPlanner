using System.Collections.Generic;

namespace de.inc47.AchievementPlanner.Model
{
  public class User : IUser
  {
    public User(ulong steamId, string name, string avatarUrl)
    {
      SteamId = steamId;
      Name = name;
      AvatarUrl = avatarUrl;
    }

    public string Name { get; }
    public ulong SteamId { get; }
    public string AvatarUrl { get; }
    public IEnumerable<IGame> OwnedGames { get; set; }
  }
}