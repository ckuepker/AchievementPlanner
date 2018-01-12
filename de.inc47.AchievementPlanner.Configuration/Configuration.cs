using System;
using System.IO;

namespace de.inc47.AchievementPlanner.Configuration
{
  
  public class Configuration
  {
    private static readonly string _path_to_apikey = "E:\\data\\dev\\net\\steamapikey.txt";
    private static readonly string _path_to_store = "E:\\data\\dev\\net\\achievement_planner.store";

    public static String SteamWebApiKey;
    public static ulong SteamId;

    static Configuration()
    {
      SteamWebApiKey = File.ReadAllText(_path_to_apikey);
      SteamId = Convert.ToUInt64(File.ReadAllText(_path_to_store));
    }
  }
}
