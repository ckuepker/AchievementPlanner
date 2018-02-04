using System.Collections.Generic;
using System.Linq;
using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Builder;
using de.inc47.AchievementPlanner.ModelTest.Extension;
using Moq;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ModelTest
{
  public class UserTest
  {
    private ulong _steamId = 76561197962198367;

    [Test]
    public void TestDirtyIsTrueByDefault()
    {
      var sut = new User(0, "", "");
      Assert.True(sut.Dirty);
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public void TestDirtyNotifiesOnSet(bool initial)
    {
      var sut = new User(0, "", "");
      sut.Dirty = initial;
      sut.ShouldNotifyOn(u => u.Dirty).When(u => u.Dirty = !initial);
    }

    [Test]
    [TestCase(false)]
    [TestCase(true)]
    public void TestDirtyNotifiesOnUnchangingSet(bool initial)
    {
      var sut = new User(0, "", "");
      sut.Dirty = initial;
      sut.ShouldNotNotifyOn(u => u.Dirty).When(u => u.Dirty = initial);
    }

    [Test]
    public void TestNotifiesOnOwnedGamesChange()
    {
      var sut = new User(0, "", "");
      sut.Dirty = false;
      Assert.False(sut.Dirty);
      sut.ShouldNotifyOn(u => u.Dirty).When(u => u.OwnedGames = new List<IGame>());
      Assert.True(sut.Dirty);
    }

    [Test]
    public void TestNotifiesOnAchievementChange()
    {
      var sut = new User(0, "", "");
      var game = new GameBuilder().WithAchievements(5, 0).Build();
      sut.OwnedGames = new List<IGame> { game };

      sut.ShouldNotifyOn(u => u.OwnedGames).When(u => game.Achievements.Last().Completed = true);
    }
  }
}
