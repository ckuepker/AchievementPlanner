using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.inc47.AchievementPlanner.Model
{
  public interface IAchievement
  {
    string Name { get; }

    string Description { get; }
    string IconUrl { get; }
  }
}
