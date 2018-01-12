using System.IO;
using Newtonsoft.Json;

namespace de.inc47.AchievementPlanner.Model
{
  /// <summary>
  /// Concrete implementation of IStore
  /// </summary>
  public class Store : IStore
  {
    private readonly string _location;

    /// <summary>
    /// Creates a store which saves and loads to/from the given location
    /// </summary>
    /// <param name="location"></param>
    public Store(string location)
    {
      _location = location;
    }

    public IUser Load()
    {
      string serializedUser = File.ReadAllText(_location);
      return JsonConvert.DeserializeObject<User>(serializedUser, new JsonSerializerSettings()
      {
        // Allows deserialization of IEnumerable<IGame/IAchievement>
        TypeNameHandling = TypeNameHandling.Objects
      });
    }

    public void Save(IUser u)
    {
      var serializedUser = JsonConvert.SerializeObject(u, new JsonSerializerSettings()
      {
        // Set attributes so that IEnumerable<IGame/IAchievement> can be deserialized
        TypeNameHandling = TypeNameHandling.Objects,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
      });
      File.WriteAllText(_location, serializedUser);
    }
  }
}