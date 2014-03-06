using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ArmA3.ResultObjects
{
    public class Sensor : ItemBase
    {
        public Sensor()
        {
            Effects = new List<string>();
        }
        
        public double? A { get; set; }
        public double? B { get; set; }
        public string Type { get; set; }
        public string ActivationBy { get; set; }
        public string ActivationType { get; set; }
        public int? Interruptable { get; set; }
        public string Age { get; set; }
        public string ExpCond { get; set; }
        public string ExpActiv { get; set; }
        public List<string> Effects { get; set; }
    }
}
