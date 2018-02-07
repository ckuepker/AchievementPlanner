namespace de.inc47.AchievementPlanner.Model
{
  public interface IAchievement : IModelElement
  {
    string InternalShortName { get; }
    string Name { get; }
    string Description { get; }
    string IconUrl { get; }
    bool Completed { get; set; }
    bool Unlockable { get; }
    double GlobalCompletionPercentage { get; set; }
  }
}
