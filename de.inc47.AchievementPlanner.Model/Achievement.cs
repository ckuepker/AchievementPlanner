namespace de.inc47.AchievementPlanner.Model
{
  public class Achievement : IAchievement
  {
    public Achievement(string name, string description, string iconUrl)
    {
      Name = name;
      Description = description;
      IconUrl = iconUrl;
    }

    public string Name { get; set; }
    public string Description { get; }
    public string IconUrl { get; }
  }
}
