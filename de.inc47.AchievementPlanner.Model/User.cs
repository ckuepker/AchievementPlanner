using System.Collections.Generic;

namespace de.inc47.AchievementPlanner.Model
{
  public class User : ModelElement, IUser
  {
    private IEnumerable<IGame> _ownedGames;

    /// <summary>
    /// Creates a user without associated games.
    /// </summary>
    /// <param name="steamId"></param>
    /// <param name="name"></param>
    /// <param name="avatarUrl"></param>
    /// <param name="games"></param>
    public User(ulong steamId, string name, string avatarUrl)
    {
      SteamId = steamId;
      Name = name;
      AvatarUrl = avatarUrl;
      Dirty = true;
    }

    public string Name { get; }
    public ulong SteamId { get; }
    public string AvatarUrl { get; }

    public IEnumerable<IGame> OwnedGames
    {
      get { return _ownedGames; }
      set
      {
        _ownedGames = value;
        Dirty = true;
        OnPropertyChanged("OwnedGames");
      }
    }
  }
}