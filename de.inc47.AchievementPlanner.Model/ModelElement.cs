using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace de.inc47.AchievementPlanner.Model
{
  /// <summary>
  /// Concrete implementation of INotifyPropertyChanged for IModelElement
  /// </summary>
  public abstract class ModelElement : IModelElement
  {
    private bool _dirty = false;
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // TODO Get rid of Dirty
    public bool Dirty
    {
      get { return _dirty; }
      set
      {
        if (value != _dirty)
        {
          _dirty = value;
          OnPropertyChanged("Dirty");
        }
      }
    }

    public virtual bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
    {
      return true;
    }
  }
}