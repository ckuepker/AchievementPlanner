using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Extension;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ModelTest
{
  public class AchievementTest
  {
    [Test]
    public void TestNotifiesOnCompletedChange()
    {
      var sut = new Achievement("Name", "", "");
      sut.ShouldNotifyOn(a => a.Completed).When(a => a.Completed = true);
      Assert.True(sut.Dirty);
    }

    [Test]
    public void TestNotifiesOnCompletionPercentageChange()
    {
      var sut = new Achievement("Name", "", "");
      sut.ShouldNotifyOn(a => a.GlobalCompletionPercentage).When(a => a.GlobalCompletionPercentage = 50.0);
      Assert.True(sut.Dirty);
    }

    [Test]
    public void TestDirtyChangesOnCompleteChange()
    {
      var sut = new Achievement("", "", "");
      sut.ShouldNotifyOn(a => a.Dirty).When(a => a.Completed = true);
    }
  }
}
