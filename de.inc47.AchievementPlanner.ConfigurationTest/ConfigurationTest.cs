using NUnit.Framework;

namespace de.inc47.AchievementPlanner.ConfigurationTest
{
  public class ConfigurationTest
  {

    [Test]
    public void TestGetApiKey()
    {
      Assert.False(string.IsNullOrEmpty(Configuration.Configuration.SteamWebApiKey));
    }
  }
}
