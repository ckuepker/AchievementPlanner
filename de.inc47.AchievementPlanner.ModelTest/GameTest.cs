using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Extension;
using Moq;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ModelTest
{
  public class GameTest
  {
    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 0.20)]
    [TestCase(2, 0.40)]
    [TestCase(3, 0.60)]
    [TestCase(4, 0.80)]
    [TestCase(5, 1.0)]
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

    [Test]
    public void TestCompletedAchievementCountNotifiesOnAchievementCompletedChanges()
    {
      var sut = new Game(0, "", "", TimeSpan.Zero);
      var achievementMock = new Mock<IAchievement>();
      sut.Achievements = new List<IAchievement>
      {
        achievementMock.Object 
      };

      sut.Dirty = false;
      sut.ShouldNotifyOn(g => g.CompletedAchievementCount).When(g => achievementMock.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs("Completed")));
      Assert.True(sut.Dirty);
    }

    [Test]
    public void TestDirtyNotifiesOnAchievementCompletedChanges()
    {
      var sut = new Game(0, "", "", TimeSpan.Zero);
      var achievementMock = new Mock<IAchievement>();
      sut.Achievements = new List<IAchievement>
      {
        achievementMock.Object
      };

      sut.Dirty = false;
      sut.ShouldNotifyOn(g => g.Dirty).When(g => achievementMock.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs("Completed")));
      Assert.True(sut.Dirty);
    }

    [Test]
    public void TestCompletionRateNotifiesOnAchievementCompletedChanges()
    {
      var sut = new Game(0, "", "", TimeSpan.Zero);
      var achievementMock = new Mock<IAchievement>();
      sut.Achievements = new List<IAchievement>
      {
        achievementMock.Object
      };

      sut.Dirty = false;
      sut.ShouldNotifyOn(g => g.CompletionRate).When(g => achievementMock.Raise(a => a.PropertyChanged += null, new PropertyChangedEventArgs("Completed")));
      Assert.True(sut.Dirty);
    }
  }
}
