using System;

namespace de.inc47.AchievementPlanner.Model
{
  public class Achievement : ModelElement, IAchievement
  {
    private bool _completed;
    private double _globalCompletionPercentage;

    public Achievement(string name, string description, string iconUrl, string internalShortName = "")
    {
      Name = name;
      Description = description;
      IconUrl = iconUrl;
      Completed = false;
      Unlockable = true;
      InternalShortName = internalShortName;
    }

    public string InternalShortName { get; }
    public string Name { get; }
    public string Description { get; }
    public string IconUrl { get; }

    public bool Completed
    {
      get { return _completed; }
      set
      {
        if (value != _completed)
        {
          _completed = value;
          Dirty = true;
          OnPropertyChanged();
        }
      }
    }

    public bool Unlockable { get; }

    public double GlobalCompletionPercentage
    {
      get { return _globalCompletionPercentage; }
      set
      {
        if (value - _globalCompletionPercentage > Double.Epsilon)
        {
          _globalCompletionPercentage = value;
          Dirty = true;
          OnPropertyChanged();
        }
      }
    }

    public override bool Equals(object obj)
    {
      if (obj == null || !(obj is IAchievement))
      {
        return false;
      }
      IAchievement other = obj as IAchievement;
      return InternalShortName == other.InternalShortName
        && Name == other.Name 
        && Description == other.Description 
        && Unlockable == other.Unlockable 
        && IconUrl == other.IconUrl;
    }

    public override int GetHashCode()
    {
      return string.Format("{0}{1}{2}", Name, Description, IconUrl).GetHashCode();
    }
  }
}
