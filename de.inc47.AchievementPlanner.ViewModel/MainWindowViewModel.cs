using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using de.inc47.AchievementPlanner.Model;
using de.inc47.SteamWebAPI;

namespace de.inc47.AchievementPlanner.ViewModel
{
  public class MainWindowViewModel : ViewModelBase
  {
    ISteamFacade _facade = new SteamFacade();

    public MainWindowViewModel()
    {
      Games = new ObservableCollection<IGame>();
      BackgroundWorker bw = new BackgroundWorker();
      bw.DoWork += (sender, args) =>
      {
        Games = _facade.GetGamesOfUser(76561197962198367);
        OnPropertyChanged("Games");
      };
      bw.RunWorkerAsync();
    }
    public IEnumerable<IGame> Games { get; set; }
  }
}
