using System;

namespace de.inc47.AchievementPlanner.Model
{
  public class Game : IGame
  {
    public Game(ulong appId, string name, string iconUrl, TimeSpan playtime)
    {
      AppId = appId;
      Name = name;
      IconUrl = iconUrl;
      Playtime = playtime;
    }

    public ulong AppId { get; }
    public string Name { get; }
    public string IconUrl { get; }
    public TimeSpan Playtime { get; }
  }
}