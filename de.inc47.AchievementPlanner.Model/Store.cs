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
      try
      {
        string serializedUser = File.ReadAllText(_location);
        IUser u =  JsonConvert.DeserializeObject<User>(serializedUser, new JsonSerializerSettings()
        {
          // Allows deserialization of IEnumerable<IGame/IAchievement>
          TypeNameHandling = TypeNameHandling.Objects
        });
        u.Dirty = false;
        return u;
      }
      catch (FileNotFoundException ex)
      {
        return null;
      }
    }

    public void Save(IUser u)
    {
      var serializedUser = JsonConvert.SerializeObject(u, new JsonSerializerSettings()
      {
        Formatting = Formatting.Indented,
        // Set attributes so that IEnumerable<IGame/IAchievement> can be deserialized
        TypeNameHandling = TypeNameHandling.Objects,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
      });
      File.WriteAllText(_location, serializedUser);
      u.Dirty = false;
    }
  }
}