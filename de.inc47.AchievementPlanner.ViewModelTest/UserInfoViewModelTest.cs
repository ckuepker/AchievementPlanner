using System.ComponentModel;
using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Extension;
using de.inc47.AchievementPlanner.ViewModel;
using Moq;
using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ViewModelTest
{
  public class UserInfoViewModelTest
  {
    [Test]
    public void TestNotifiesOnOwnedGamesChange()
    {
      var userMock = new Mock<IUser>();
      var sut = new UserInfoViewModel(userMock.Object);
      sut.ShouldNotifyOn(vm => vm.GameCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.GamesWithAchievementsCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.GamesWithAchievedAchievementsCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.PlayedGamesWithAchievementsCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.CompleteGamesCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.PossibleAchievementCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.PossibleAchievementOfPlayedGamesCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.PossibleAchievementOfAchievedGamesCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
      sut.ShouldNotifyOn(vm => vm.AchievedAchievementCount).When(vm => userMock.Raise(u => u.PropertyChanged += null, new PropertyChangedEventArgs("OwnedGames")));
    }
  }
}
