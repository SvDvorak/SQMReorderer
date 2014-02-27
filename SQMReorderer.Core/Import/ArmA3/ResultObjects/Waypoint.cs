using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ArmA3.ResultObjects
{
    public class Waypoint : ItemBase
    {
        public Waypoint()
        {
            Effects = new List<string>();
        }

        public string Type { get; set; }
        public string ExpActiv { get; set; }
        public List<string> Effects { get; set; }
        public string ShowWp { get; set; }
    }
}