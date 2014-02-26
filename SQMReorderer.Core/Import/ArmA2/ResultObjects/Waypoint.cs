using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ArmA2.ResultObjects
{
    public class Waypoint : ItemBase
    {
        public Waypoint()
        {
            Synchronizations = new List<int>();
            Effects = new List<string>();
        }

        public int? Placement { get; set; }
        public int? CompletitionRadius { get; set; }
        public string Type { get; set; }
        public string CombatMode { get; set; }
        public string Formation { get; set; }
        public string Speed { get; set; }
        public string Combat { get; set; }
        public string ExpActiv { get; set; }
        public List<int> Synchronizations { get; set; }
        public List<string> Effects { get; set; }
        public string ShowWp { get; set; }
    }
}