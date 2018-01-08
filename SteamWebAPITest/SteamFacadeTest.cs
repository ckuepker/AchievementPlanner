using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using de.inc47.AchievementPlanner.Configuration;
using de.inc47.AchievementPlanner.Model;
using de.inc47.SteamWebAPI;

namespace SteamWebAPITest
{
  [TestFixture]
  public class SteamFacadeTest
  {
    private ISteamFacade _sut;

    [SetUp]
    public void Setup()
    {
      _sut = new SteamFacade();
    }


    [Test]
    public void TestGetGamesOfUser()
    {
      ulong steamId = 76561197962198367;
      HashSet<IGame> games = _sut.GetGamesOfUser(steamId);
      CollectionAssert.IsNotEmpty(games);
      Assert.AreEqual(130, games.Count);
      Assert.True(games.FirstOrDefault(g => g.Name == "Divinity: Original Sin 2") != null);
    }

    [Test]
    public void TestGetAchievementsSpaceEngineers()
    {
      uint appId = 244850;
      HashSet<IAchievement> csgoAchievements = _sut.GetAchievements(appId);
      Assert.AreEqual(18, csgoAchievements.Count);
      Assert.AreEqual(1, csgoAchievements.Count(a => a.Name == "Death Wish"));
    }
  }
}
