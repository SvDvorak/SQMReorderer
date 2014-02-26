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

        public int? IdStatic { get; set; }
        public int? IdObject { get; set; }
        public int? HousePos { get; set; }
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
        public int? TimeoutMin { get; set; }
        public int? TimeoutMid { get; set; }
        public int? TimeoutMax { get; set; }
        public string ShowWp { get; set; }
    }
}