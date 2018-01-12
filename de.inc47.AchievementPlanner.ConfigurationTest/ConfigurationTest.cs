using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ConfigurationTest
{
  public class ConfigurationTest
  {
    [Test]
    public void TestGetSteamId()
    {
      Assert.AreEqual(76561197962198367, Configuration.Configuration.SteamId);
    }

    [Test]
    public void TestGetApiKey()
    {
      Assert.False(string.IsNullOrEmpty(Configuration.Configuration.SteamWebApiKey));
    }
  }
}
