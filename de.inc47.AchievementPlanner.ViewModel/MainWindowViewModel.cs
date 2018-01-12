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

    public MainWindowViewModel()
    {
      SteamId = "76561197962198367";
    }
    public IUser User { get; set; }

    public string SteamId
    {
      get { return _steamId; }
      set
      {
        _steamId = value;
        ulong numericValue = Convert.ToUInt64(_steamId);
        if (numericValue > 0)
        {
          BackgroundWorker bw = new BackgroundWorker();
          bw.DoWork += (sender, args) =>
          {
            Status = "Loading user...";
            Tuple<string, string> userInfo = _facade.GetUserInfo(numericValue);
            Status = "Loading games...";
            List<IGame> games = _facade.GetGamesOfUser(numericValue).ToList();
            List<IGame> gamesWithAchievements = new List<IGame>(games.Count);
            foreach (IGame g in games)
            {
              Status = string.Format("Loading achievements for '{0}' ({1})...", g.Name, g.AppId);
              // Gets names, icons and global completion
              g.Achievements = _facade.GetAchievements(g.AppId);
              if (g.Achievements.Any())
              {
                gamesWithAchievements.Add(g);
                // Sets unlock state for current user
                Status = string.Format("Loading achievement completion for '{0}' ({1})...", g.Name, g.AppId);
                _facade.GetAchievementCompletionStates(numericValue, g);
              }
            }
            User = new User(numericValue, userInfo.Item1, userInfo.Item2) {OwnedGames = games};
            Games = gamesWithAchievements;
            OnPropertyChanged("User");
            OnPropertyChanged("Games");
          };
          bw.RunWorkerAsync();
        }
      }
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
  }
}
