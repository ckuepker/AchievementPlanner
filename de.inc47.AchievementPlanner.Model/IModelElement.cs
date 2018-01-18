using System.ComponentModel;
using System.Windows;

namespace de.inc47.AchievementPlanner.Model
{
  /// <summary>
  /// Base interface for all model types
  /// </summary>
  public interface IModelElement : INotifyPropertyChanged, IWeakEventListener
  {
    bool Dirty { get; set; }
  }
}
