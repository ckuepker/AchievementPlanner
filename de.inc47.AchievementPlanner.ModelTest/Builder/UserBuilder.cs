using System.Collections.Generic;
using de.inc47.AchievementPlanner.Model;

namespace de.inc47.AchievementPlanner.ModelTest.Builder
{
  public class UserBuilder
  {
    private IEnumerable<IGame> _games;
    private ulong _steamId;

    public UserBuilder()
    {
      _games = new List<IGame>(0);
      _steamId = 0L;
    }

    public IUser Build()
    {
      var u = new User(_steamId, "", "");
      u.OwnedGames = _games;
      return u;
    }

    public UserBuilder WithGames(IEnumerable<IGame> games)
    {
      _games = games;
      return this;
    }

    public UserBuilder WithSteamId(ulong steamId)
    {
      _steamId = steamId;
      return this;
    }
  }
}
