using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using de.inc47.AchievementPlanner.Model;
using de.inc47.SteamWebAPI;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class MainWindowViewModel : ViewModelBase
  {
    ISteamFacade _facade = new SteamFacade();
    private string _steamId;
    private IEnumerable<IGame> _games = new List<IGame>();
    private string _status;
    private IStore _store;

    public MainWindowViewModel()
    {
      _store = new Store(Configuration.Configuration.StoreLocation);
      User = _store.Load();
      if (User != null)
      {
        Initialized = true;
        Games = User.OwnedGames;
      }
      else
      {
        Initialized = false;
      }
    }

    /// <summary>
    /// True if a user has been set from store or loaded via API. Used to display initial profile dialog.
    /// </summary>
    public bool Initialized { get; set; }
    public IUser User { get; set; }

    public string SteamId
    {
      get { return _steamId; }
      set { _steamId = value; }
    }

    public IEnumerable<IGame> Games
    {
      get { return _games; }
      set { _games = value; }
    }

    public string Status
    {
      get { return _status; }
      set
      {
        _status = value;
        OnPropertyChanged("Status");
      }
    }

    public void LoadFromApi()
    {
      ulong numericValue = Convert.ToUInt64(SteamId);
      if (numericValue > 0)
      {
        BackgroundWorker bw = new BackgroundWorker();
        bw.DoWork += (sender, args) =>
        {
          Status = "Loading user...";
          Tuple<string, string> userInfo = _facade.GetUserInfo(numericValue);
          Status = Status + "\r\nLoading games...";
          List<IGame> games = _facade.GetGamesOfUser(numericValue).ToList();
          Status = Status + string.Format("\r\n\t-> Loaded {0} games", games.Count);
          List<IGame> gamesWithAchievements = new List<IGame>(games.Count);
          int i = 0;
          int gameCount = games.Count;
          foreach (IGame g in games)
          {
            Status = Status + string.Format("\r\nLoading achievements for '{0}' ({1})... ({2}/{3})", g.Name, g.AppId, i, gameCount);
            // Gets names, icons and global completion
            g.Achievements = _facade.GetAchievements(g.AppId);
            Status = Status + string.Format("\r\n\t-> Found {0} achievements", g.Achievements.Count());
            if (g.Achievements.Any())
            {
              gamesWithAchievements.Add(g);
              // Sets unlock state for current user
              Status = Status + string.Format("\r\nLoading achievement completion for '{0}' ({1})...", g.Name, g.AppId);
              _facade.GetAchievementCompletionStates(numericValue, g);
            }
            i++;
          }
          User = new User(numericValue, userInfo.Item1, userInfo.Item2) { OwnedGames = games };
          Games = gamesWithAchievements;
          Initialized = true;
          OnPropertyChanged("Initialized");
          OnPropertyChanged("User");
          OnPropertyChanged("Games");
          Status = Status + "\r\nStoring Result...";
          _store.Save(User);
        };
        bw.RunWorkerAsync();
      }
    }
  }
}
