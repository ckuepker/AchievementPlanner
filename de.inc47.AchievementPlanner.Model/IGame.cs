using System;
using System.Collections.Generic;

namespace de.inc47.AchievementPlanner.Model
{
  public interface IGame : IModelElement
  {
    uint AppId { get; }
    string Name { get; }
    string IconUrl { get; }
    TimeSpan Playtime { get; }
    /// <summary>
    /// Contains all achievements known for this game or an empty collection if none are available.
    /// </summary>
    IEnumerable<IAchievement> Achievements { get; set; }
    double CompletionRate { get; }
    int CompletedAchievementCount { get; }
    int AchievementCount { get; }
  }
}
