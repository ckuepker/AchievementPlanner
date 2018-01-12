using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.inc47.AchievementPlanner.Model;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ModelTest
{
  public class GameTest
  {
    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 20)]
    [TestCase(2, 40)]
    [TestCase(3, 60)]
    [TestCase(4, 80)]
    [TestCase(5, 100)]
    public void TestCompletionRate(int completedAchievements, double expectedCompletionRate)
    {
      var g = new Game(1, "A game", "", TimeSpan.Zero);
      var ach = new List<IAchievement>(5);
      for (int i = 0; i < 5; i++)
      {
        ach.Add(new Achievement("Achievement " + i, "", ""));
      }
      for (int i = 0; i < completedAchievements; i++)
      {
        ach[i].Completed = true;
      }
      g.Achievements = ach;
      Assert.AreEqual(expectedCompletionRate, g.CompletionRate, 0d);
    }
  }
}
