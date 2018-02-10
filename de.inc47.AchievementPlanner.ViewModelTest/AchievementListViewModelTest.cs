using System.Collections.Generic;
using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Extension;
using de.inc47.AchievementPlanner.ViewModel;
using Moq;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ViewModelTest
{
  public class AchievementListViewModelTest
  {
    [Test]
    public void TestIsShowCompleted()
    {
      IAchievementsListViewModel sut = new AchievementListViewModel(new List<IAchievement>(), achievement => new Mock<IGame>().Object, () => new Mock<IUserInfoViewModel>().Object);
      Assert.True(sut.IsShowCompleted);
      sut.ShouldNotifyOn(alvm => alvm.IsShowCompleted).When(alvm => alvm.IsShowCompleted = false);
      Assert.False(sut.IsShowCompleted);
    }

    [Test]
    public void TestIsShowUnplayed()
    {
      IAchievementsListViewModel sut = new AchievementListViewModel(new List<IAchievement>(), achievement => new Mock<IGame>().Object, () => new Mock<IUserInfoViewModel>().Object);
      Assert.False(sut.IsShowUnplayed);
      sut.ShouldNotifyOn(alvm => alvm.IsShowUnplayed).When(alvm => alvm.IsShowUnplayed = true);
      Assert.True(sut.IsShowUnplayed);
    }
  }
}
