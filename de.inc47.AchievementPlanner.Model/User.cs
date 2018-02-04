using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
        if (_ownedGames != null && _ownedGames.Any())
        {
          foreach (IGame game in _ownedGames)
          {
            PropertyChangedEventManager.RemoveListener(game, this, string.Empty);
          }
        }
        _ownedGames = value;
        if (_ownedGames != null && _ownedGames.Any())
        {
          foreach (IGame game in _ownedGames)
          {
            PropertyChangedEventManager.AddListener(game, this, string.Empty);
          }
        }
        Dirty = true;
        OnPropertyChanged("OwnedGames");
      }
    }

    public override bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
    {
      var pcea = e as PropertyChangedEventArgs;
      if (pcea != null)
      {
        string propertyName = pcea.PropertyName;
        if (propertyName == "CompletionRate")
        {
          OnPropertyChanged("OwnedGames");
        }
      }
      return base.ReceiveWeakEvent(managerType, sender, e);
    }
  }
}