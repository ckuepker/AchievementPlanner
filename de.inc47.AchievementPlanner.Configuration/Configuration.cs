using System;
using System.IO;

namespace de.inc47.AchievementPlanner.Configuration
{
  
  public class Configuration
  {
    private static readonly string _path_to_apikey = "E:\\data\\dev\\net\\steamapikey.txt";

    public static String SteamWebApiKey;

    static Configuration()
    {
      SteamWebApiKey = File.ReadAllText(_path_to_apikey);
    }
  }
}
