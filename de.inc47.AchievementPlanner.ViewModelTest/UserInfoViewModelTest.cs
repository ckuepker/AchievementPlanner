using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;
using de.inc47.AchievementPlanner.ModelTest.Builder;
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
      IUserInfoViewModel sut = new UserInfoViewModel(userMock.Object);
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

    [Test]
    public void TestAverageGameCompletionRate()
    {
      var g1 = new GameBuilder().WithAchievements(5, 2).Build();
      var g2 = new GameBuilder().WithAchievements(10, 10).Build();
      var u = new UserBuilder().WithGames(new List<IGame> {g1, g2}).Build();
      IUserInfoViewModel sut = new UserInfoViewModel(u);

      Assert.AreEqual(0.7, sut.AverageGameCompletionRate, 0d);

      sut.ShouldNotifyOn(vm => vm.AverageGameCompletionRate).When(vm => g1.Achievements.Last().Completed = true);
      Assert.AreEqual(0.8, sut.AverageGameCompletionRate);
    }

    [Test]
    public void TestOverallCompletionRate()
    {
      var g1 = new GameBuilder().WithAchievements(5, 2).Build();
      var g2 = new GameBuilder().WithAchievements(10, 10).Build();
      var u = new UserBuilder().WithGames(new List<IGame> { g1, g2 }).Build();
      IUserInfoViewModel sut = new UserInfoViewModel(u);

      Assert.AreEqual(0.8, sut.OverallCompletionRate, 0d);

      sut.ShouldNotifyOn(vm => vm.OverallCompletionRate).When(vm => g1.Achievements.Last().Completed = true);
      Assert.AreEqual(0.86, sut.OverallCompletionRate, 0.01d);
    }
  }
}
