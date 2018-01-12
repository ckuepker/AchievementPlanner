namespace de.inc47.AchievementPlanner.Model
{
  /// <summary>
  /// Stores user data with games and achievements in a file system location, using JSON de/serialization
  /// </summary>
  public interface IStore
  {
    /// <summary>
    /// Deserializes the store from the default file system location and returns a corresponding IUser instance
    /// </summary>
    /// <returns></returns>
    IUser Load();
    /// <summary>
    /// Serializes the given user to JSON, including games, achievements and completions, and writes to the default file system location.
    /// </summary>
    /// <param name="u"></param>
    void Save(IUser u);
  }
}
