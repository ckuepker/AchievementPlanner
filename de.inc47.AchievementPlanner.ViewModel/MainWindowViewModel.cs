using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using de.inc47.AchievementPlanner.Model;
using de.inc47.SteamWebAPI;
using GalaSoft.MvvmLight.CommandWpf;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class MainWindowViewModel : ViewModelBase
  {
    private ISteamFacade _facade = new SteamFacade();
    private string _steamId;
    private string _status;
    private readonly IStore _store;
    private IUser _user;
    private IGamesViewModel _gamesViewModel;
    private IAchievementsListViewModel _achievementsListViewModel;
    private ITabViewModel _selectedTab;

    public MainWindowViewModel()
    {
      _store = new Store(Configuration.Configuration.StoreLocation);
      User = _store.Load();
    }

    /// <summary>
    /// True if a user has been set from store or loaded via API. Used to display initial profile dialog.
    /// </summary>
    public bool Initialized { get; set; }

    public IUser User
    {
      get { return _user; }
      set
      {
        UserInfo = null;
        _user = value;
        if (_user != null)
        {
          Initialized = true;
          UserInfo = new UserInfoViewModel(_user);
          _gamesViewModel = new GamesViewModel(User.OwnedGames);
          _achievementsListViewModel = new AchievementListViewModel(User.OwnedGames.Select(g => g.Achievements).SelectMany(a => a), GetGameFromAchievement, () => UserInfo);
          Tabs = new ObservableCollection<ITabViewModel>
          {
            new TabViewModel("Games", _gamesViewModel),
            new TabViewModel("Achievements", _achievementsListViewModel)
          };
          SelectedTab = Tabs[0];
          OnPropertyChanged(nameof(UserInfo));
          OnPropertyChanged(nameof(Initialized));
          OnPropertyChanged(nameof(Tabs));
          OnPropertyChanged(nameof(SelectedTab));
        }
        else
        {
          Tabs = null;
          Initialized = false;
          SelectedTab = null;
          OnPropertyChanged(nameof(Initialized));
          OnPropertyChanged(nameof(Tabs));
        }
      }
    }

    public IUserInfoViewModel UserInfo { get; set; }

    public string SteamId
    {
      get { return _steamId; }
      set { _steamId = value; }
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

    public ObservableCollection<ITabViewModel> Tabs
    {
      get;
      private set;
    }

    public ITabViewModel SelectedTab
    {
      get { return _selectedTab; }
      set
      {
        _selectedTab = value;
        OnPropertyChanged(nameof(SelectedTab));
      }
    }

    private IGame GetGameFromAchievement(IAchievement achievement)
    {
      // TODO Optimize by utilizing IDs or Dictionaries
      return User.OwnedGames.Where(g => g.AchievementCount > 0).FirstOrDefault(g => g.Achievements.Contains(achievement));
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
            Status = Status + string.Format("\r\nLoading achievements for '{0}' ({1})... ({2}/{3})", g.Name, g.AppId, i,
                       gameCount);
            // Gets names, icons and global completion
            try
            {
              g.Achievements = _facade.GetAchievements(g.AppId);
              Status = Status + string.Format("\r\n\t-> Found {0} achievements", g.Achievements.Count());
              if (g.Achievements.Any())
              {
                gamesWithAchievements.Add(g);
                // Sets unlock state for current user
                Status = Status + string.Format("\r\nLoading achievement completion for '{0}' ({1})...", g.Name,
                           g.AppId);
                _facade.GetAchievementCompletionStates(numericValue, g);
              }
            }
            catch (Exception ex)
            {
              Status = Status + "\r\nERROR: Could not load achievements: " + ex.Message;
            }
            i++;
          }
          User = new User(numericValue, userInfo.Item1, userInfo.Item2) { OwnedGames = games };
          File.WriteAllText("E:\\data\\dev\\net\\achievement_planner_import.log", Status);
        };
        bw.RunWorkerAsync();
      }
    }

    private RelayCommand _saveCommand;
    public RelayCommand SaveCommand
    {
      get { return _saveCommand ?? (_saveCommand = new RelayCommand(() => { _store.Save(User); }, () => User != null && User.Dirty)); }
    }

    public ICommand UpdateCompletionStatesCommand
    {
      get
      {
        return new RelayCommand(() =>
        {
          using (var bw = new BackgroundWorker())
          {
            bw.DoWork += (sender, args) =>
            {
              Status = "Updating completion states for all games...\r\n";
              int i = 0;
              IList<IGame> gamesToCheck = User.OwnedGames.Where(g => g.Achievements != null && g.Achievements.Any()).ToList();
              int count = gamesToCheck.Count;
              Status = Status + string.Format("{0} games to check\r\n", count);
              foreach (IGame g in gamesToCheck)
              {
                i++;
                Status = Status + string.Format("\t({0}/{1}) Loading Achievements for {2} ...\r\n", i, count, g.Name);
                try
                {
                  _facade.GetAchievementCompletionStates(User.SteamId, g);
                }
                catch (Exception ex)
                {
                  Status = Status + string.Format("\t\tFailed to load completion states: {0}\r\n", ex.Message);
                }
              }
              Status = Status + "Update of completion state completed. You may now save...";
              User.Dirty = true;
              SaveCommand.RaiseCanExecuteChanged();
            };
            bw.RunWorkerAsync();
          }
        }, () => User != null);
      }
    }

    /// <summary>
    /// Updates the list owned games for the user and loads achievements and completion states for any new games.
    /// </summary>
    /// <returns></returns>
    public ICommand UpdateGamesCommand
    {
      get
      {
        return new RelayCommand(() =>
        {
          using (var bw = new BackgroundWorker())
          {
            bw.DoWork += (sender, args) =>
            {
              Status = "Updating list of owned games...";
              IEnumerable<IGame> games = _facade.GetGamesOfUser(User.SteamId);
              HashSet<uint> ownedIds = new HashSet<uint>();
              foreach (uint appId in User.OwnedGames.Select(g => g.AppId))
              {
                ownedIds.Add(appId);
              }
              IList<IGame> newGames = games.Where(g => !ownedIds.Contains(g.AppId)).ToList();
              if (newGames.Count == 0)
              {
                Status += string.Format("\r\nFound no new games", newGames.Count);
                return;
              }
              Status += string.Format("\r\n\tFound {0} new games:", newGames.Count);
              foreach (IGame newGame in newGames)
              {
                Status += string.Format("\r\n\t\t* {0} ({1})", newGame.Name, newGame.AppId);
              }
              int i = 1;
              int newGamesCount = newGames.Count;
              foreach (var game in newGames)
              {
                Status += string.Format("\r\n\rLoading achievements for {0} ({1}/{2})...", game.Name, i, newGamesCount);
                game.Achievements = _facade.GetAchievements(game.AppId);
                Status += string.Format("\r\n\t\tFound {0} achievements", game.Achievements.Count());
                i++;
                if (game.Achievements.Any())
                {
                  Status += "\r\n\t\tLoading completion states...";
                  _facade.GetAchievementCompletionStates(User.SteamId, game);
                }
              }
              User.OwnedGames = User.OwnedGames.Union(newGames);
              Status += "\r\nDone!";
            };
            bw.RunWorkerAsync();
          }
        }, () => User != null);
      }
    }

    public ICommand UpdateAchievementsCommand
    {
      get
      {
        return new RelayCommand(() =>
        {
          using (var bw = new BackgroundWorker())
          {
            bw.DoWork += (sender, args) =>
            {
              Status = "Updating available achievements for all games...";
              int i = 1;
              int gameCount = User.OwnedGames.Count();
              bool changed = false;
              foreach (IGame g in User.OwnedGames)
              {
                Status += string.Format("\r\n\tLoading Achievements for {0} ({1}/{2})...", g.Name, i, gameCount);
                HashSet<IAchievement> achievements = _facade.GetAchievements(g.AppId);
                if (achievements.Any())
                {
                  var knownAchievements = g.Achievements.ToList();
                  achievements.ExceptWith(knownAchievements);
                  if (achievements.Any())
                  {
                    Status += string.Format("\r\n\t\t{0} new achievements found, consider updating completion status!", achievements.Count);
                    g.Achievements = knownAchievements.Union(achievements);
                    _facade.GetAchievementCompletionStates(User.SteamId, g);
                    changed = true;
                  }
                }
                i++;
              }
              if (changed)
              {
                User.Dirty = true;
              }
            };
            bw.RunWorkerAsync();
          }
        }, () => User != null);
      }
    }
  }
}