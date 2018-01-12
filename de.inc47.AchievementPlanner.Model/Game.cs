using System;
using System.Collections.Generic;
using System.Linq;

namespace de.inc47.AchievementPlanner.Model
{
  public class Game : IGame
  {
    public Game(uint appId, string name, string iconUrl, TimeSpan playtime)
    {
      AppId = appId;
      Name = name;
      IconUrl = iconUrl;
      Playtime = playtime;
    }

    public uint AppId { get; }
    public string Name { get; }
    public string IconUrl { get; }
    public TimeSpan Playtime { get; }
    public IEnumerable<IAchievement> Achievements { get; set; }

    public double CompletionRate
    {
      get
      {
        double completed = Achievements.Count(a => a.Completed);
        double all = Achievements.Count();
        return 100 * completed / all;
      }
    }
  }
}