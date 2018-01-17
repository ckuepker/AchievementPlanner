using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace de.inc47.AchievementPlanner.Model
{
  public class Game : ModelElement, IGame
  {
    private string _iconUrl;
    private IEnumerable<IAchievement> _achievements;

    /// <summary>
    /// Creates a game info without associated achievements
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <param name="iconUrl"></param>
    /// <param name="playtime"></param>
    public Game(uint appId, string name, string iconUrl, TimeSpan playtime)
    {
      AppId = appId;
      Name = name;
      IconUrl = iconUrl;
      Playtime = playtime;
    }

    /// <summary>
    /// Creates a game with associated achievements. Constructor is required for automated deserialization.
    /// </summary>
    /// <param name="appId"></param>
    /// <param name="name"></param>
    /// <param name="iconUrl"></param>
    /// <param name="playtime"></param>
    /// <param name="achievements"></param>
    [JsonConstructor]
    public Game(uint appId, string name, string iconUrl, TimeSpan playtime, IEnumerable<Achievement> achievements)
    {
      AppId = appId;
      Name = name;
      IconUrl = iconUrl;
      Playtime = playtime;
      Achievements = achievements;
    }

    public uint AppId { get; }
    public string Name { get; }

    public string IconUrl
    {
      get { return _iconUrl; }
      private set
      {
        if (!string.IsNullOrEmpty(value) && !value.EndsWith(".jpg") && !value.StartsWith("http://"))
        {
          _iconUrl = string.Format("http://media.steampowered.com/steamcommunity/public/images/apps/{0}/{1}.jpg", AppId,
            value);
        }
        else
        {
          _iconUrl = value;
        }
      }
    }

    public TimeSpan Playtime { get; }

    public IEnumerable<IAchievement> Achievements
    {
      get { return _achievements; }
      set
      {
        if (_achievements != value)
        {
          Dirty = true;
          if (_achievements != null && _achievements.Any())
          {
            foreach (var achievement in _achievements)
            {
              PropertyChangedEventManager.RemoveListener(achievement, this, string.Empty);
            }
          }
          _achievements = value;
          if (_achievements != null && _achievements.Any())
          {
            foreach (var achievement in _achievements)
            {
              PropertyChangedEventManager.AddListener(achievement, this, string.Empty);
            }
          }
        }
      }
    }

    public double CompletionRate
    {
      get
      {
        double completed = CompletedAchievementCount;
        double all = AchievementCount;
        return completed / all;
      }
    }

    public int CompletedAchievementCount
    {
      get { return Achievements != null ? Achievements.Count(a => a.Completed) : 0; }
    }

    public int AchievementCount
    {
      get { return Achievements != null ? Achievements.Count() : 0; }
    }

    public override bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
    {
      string propertyName = ((PropertyChangedEventArgs) e).PropertyName;
      switch (propertyName)
      {
        case "Dirty":
          Dirty = true;
          break;
        case "Completed":
          Dirty = true;
          OnPropertyChanged("CompletedAchievementCount");
          OnPropertyChanged("CompletionRate");
          break;
      }
      return base.ReceiveWeakEvent(managerType, sender, e);
    }
  }
}