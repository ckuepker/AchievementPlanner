using System;

namespace de.inc47.AchievementPlanner.Model
{
  public interface IGame
  {
    ulong AppId { get; }
    string Name { get; }
    string IconUrl { get; }
    TimeSpan Playtime { get; }
  }
}
