using System;
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
    private ulong _steamId = 76561197962198367;
    private uint _appid = 244850;

    [SetUp]
    public void Setup()
    {
      _sut = new SteamFacade();
    }


    [Test]
    public void TestGetGamesOfUser()
    {
      IEnumerable<uint> games = _sut.GetGamesOfUser(_steamId).Select(g => g.AppId).ToList();
      CollectionAssert.IsNotEmpty(games);
      Assert.AreEqual(134, games.Count());
      CollectionAssert.Contains(games, 730, "'CS GO' should be in games list");
      CollectionAssert.Contains(games, 245550, "'Free To Play' should be in games list");
      CollectionAssert.Contains(games, 221910, "'Stanley Parable' should be in games list");
    }

    [Test]
    [TestCase("Space Engineers", 244850, 27, "Death Wish")]
    [TestCase("Stanley Parable", 221910, 10, "Unachievable")]
    [TestCase("Free To Play", 245550, 5, "Missing")]
    [TestCase("CS GO", 730, 167, "HE Grenade Expert")]
    [TestCase("CSS", 240, 147, "Expert Marksman")]
    public void TestGetAchievements(string gameName, int appId, int expectedCount, string exemplaryAchievementName)
    {
      HashSet<IAchievement> achievements = _sut.GetAchievements((uint)appId);
      Assert.AreEqual(expectedCount, achievements.Count);
      Assert.AreEqual(1, achievements.Count(a => a.Name == exemplaryAchievementName));
      Assert.True(achievements.All(a => a.GlobalCompletionPercentage > 0d));
      Assert.True(achievements.All(a => a.GlobalCompletionPercentage <= 100d));
    }

    [Test]
    public void TestGetUserInfo()
    {
      Tuple<string, string> info = _sut.GetUserInfo(_steamId);
      Assert.AreEqual("Contra 0x2F", info.Item1);
      Assert.AreEqual("https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/2a/2ad0555225c30d3d506aa9d2e51e356817ba6be9_medium.jpg", info.Item2);
    }

    [Test]
    public void TestGetAchievementsCompletionState()
    {
      IGame se = new Game(_appid, "Space Engineers", "", TimeSpan.Zero);
      se.Achievements = _sut.GetAchievements(_appid);
      _sut.GetAchievementCompletionStates(_steamId, se);
      Assert.False(se.Achievements.First(a => a.Name == "Master Engineer").Completed);
      Assert.True(se.Achievements.Where(a => a.Name != "Master Engineer").All(a => a.Completed));
    }
  }
}
