namespace de.inc47.AchievementPlanner.ViewModel
{
  public interface IAchievementViewModel : IViewModelBase
  {
    string IconUrl { get; }
    string Name { get; }
    string Description { get; }
    bool Completed { get; }
    string GameIconUrl { get; }
    double GlobalCompletionPercentage { get; }
    double CompletionRateIncrement { get; }
    double AverageCompletionRateIncrement { get; }
  }
}