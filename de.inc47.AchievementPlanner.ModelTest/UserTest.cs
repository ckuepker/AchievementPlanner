using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Extension;
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
  }
}
