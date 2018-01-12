using System.Security.Cryptography;

namespace de.inc47.AchievementPlanner.Model
{
  public class Achievement : IAchievement
  {
    public Achievement(string name, string description, string iconUrl)
    {
      Name = name;
      Description = description;
      IconUrl = iconUrl;
      Completed = false;
      Unlockable = true;
    }

    public string Name { get; set; }
    public string Description { get; }
    public string IconUrl { get; }
    public bool Completed { get; set; }
    public bool Unlockable { get; set; }
    public double GlobalCompletionPercentage { get; set; }
  }
}
