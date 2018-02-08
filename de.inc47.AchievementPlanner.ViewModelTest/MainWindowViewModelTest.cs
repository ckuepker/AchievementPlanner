using System.Linq;
using de.inc47.AchievementPlanner.ModelTest.Builder;
using de.inc47.AchievementPlanner.ModelTest.Extension;
using de.inc47.AchievementPlanner.ViewModel;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ViewModelTest
{
  public class MainWindowViewModelTest
  {
    [Test]
    public void TestTabs()
    {
      var sut = new MainWindowViewModel();
      sut.User = null;
      Assert.Null(sut.User);
      CollectionAssert.IsEmpty(sut.Tabs);

      sut.ShouldNotifyOn(vm => vm.Tabs).When(vm => sut.User = new UserBuilder().Build());
      Assert.NotNull(sut.User);
      Assert.NotNull(sut.Tabs);
      Assert.AreEqual(2, sut.Tabs.Count);
      Assert.True(sut.Tabs.Count(t => t.Header == "Games" && t.Content is IGamesViewModel) == 1);
      Assert.True(sut.Tabs.Count(t => t.Header == "Achievements" && t.Content is IAchievementsListViewModel) == 1);
    }
  }
}
