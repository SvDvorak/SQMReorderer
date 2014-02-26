using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ResultObjects
{
    public class Waypoint : ItemBase
    {
        public Waypoint()
        {
            Synchronizations = new List<int>();
        }

        public int? Placement { get; set; }
        public int? CompletitionRadius { get; set; }
        public string Type { get; set; }
        public string CombatMode { get; set; }
        public string Formation { get; set; }
        public string Speed { get; set; }
        public string Combat { get; set; }
        public List<int> Synchronizations { get; set; }
        public string ShowWp { get; set; }
    }
}