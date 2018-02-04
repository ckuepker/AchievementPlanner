using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ModelTest.Builder
{
  public class GameBuilder
  {
    private IEnumerable<IAchievement> _achievements;

    public GameBuilder()
    {
      _achievements = new List<IAchievement>(0);
    }

    public IGame Build()
    {
      var g = new Game(0, "", "", TimeSpan.Zero);
      g.Achievements = _achievements;
      return g;
    }

    public GameBuilder WithAchievements(uint count, uint completed)
    {
      var achievements = new List<IAchievement>((int)count);
      for (int i = 0; i < count; i++)
      {
        var achievement = new Achievement("", "", "");
        if (i < completed)
        {
          achievement.Completed = true;
        }
        achievements.Add(achievement);
      }
      _achievements = achievements;
      return this;
    }
  }
}
