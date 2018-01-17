using System.Security.Cryptography;

namespace de.inc47.AchievementPlanner.Model
{
  public class Achievement : ModelElement, IAchievement
  {
    private bool _completed;
    private double _globalCompletionPercentage;

    public Achievement(string name, string description, string iconUrl)
    {
      Name = name;
      Description = description;
      IconUrl = iconUrl;
      Completed = false;
      Unlockable = true;
    }

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
        if (value != _globalCompletionPercentage)
        {
          _globalCompletionPercentage = value;
          Dirty = true;
          OnPropertyChanged();
        }
      }
    }
  }
}
