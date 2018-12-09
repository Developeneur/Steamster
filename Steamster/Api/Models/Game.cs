using System;
using System.Collections.Generic;
using System.Text;

namespace Steamster.Api.Api.Models
{
    public class Game
    {
        public string gameName { get; set; }
        public string gameVersion { get; set; }
        public AvailableGameStat availableGameStats { get; set; }
    }

    public class AvailableGameStat
    {
        public List<Achievement> achievements { get; set; }
        public List<Stat> stats { get; set; }
    }

    public class Achievement
    {
        public string name { get; set; }
        public int defaultvalue { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public bool hidden { get; set; }

    }

    public class Stat
    {
        public string name { get; set; }
        public long defaultvalue{ get; set; }
        public string displayName{ get; set; }
    }
}
