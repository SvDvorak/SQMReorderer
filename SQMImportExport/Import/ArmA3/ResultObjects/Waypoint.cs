using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ArmA3.ResultObjects
{
    public class Waypoint : ItemBase
    {
        public Waypoint()
        {
            Effects = new List<string>();
        }

        public int? Placement { get; set; }
        public int? CompletitionRadius { get; set; }
        public string Type { get; set; }
        public string ExpActiv { get; set; }
        public List<string> Effects { get; set; }
        public int? TimeoutMin { get; set; }
        public int? TimeoutMid { get; set; }
        public int? TimeoutMax { get; set; }
        public string ShowWp { get; set; }
    }
}