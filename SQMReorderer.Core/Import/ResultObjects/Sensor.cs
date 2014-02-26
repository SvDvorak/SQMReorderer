using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ResultObjects
{
    public class Sensor : ItemBase
    {
        public Sensor()
        {
            Synchronizations = new List<int>();
        }

        public int? A { get; set; }
        public int? B { get; set; }
        public double? Angle { get; set; }
        public int? Rectangular { get; set; }
        public string Type { get; set; }
        public string ActivationBy { get; set; }
        public string ActivationType { get; set; }
        public int? Repeating { get; set; }
        public int? TimeoutMin { get; set; }
        public int? TimeoutMid { get; set; }
        public int? TimeoutMax { get; set; }
        public int? Interruptable { get; set; }
        public string Age { get; set; }
        public int? IdVehicle { get; set; }
        public string ExpCond { get; set; }
        public string ExpActiv { get; set; }
        public string ExpDesactiv { get; set; }
        public List<int> Synchronizations { get; set; }
    }
}
