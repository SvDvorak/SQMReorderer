using System.Collections.Generic;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Vehicle : ItemBase
    {
        public Vehicle()
        {
            Synchronizations = new List<int>();
            Vehicles = new List<Vehicle>();
        }

        public double? Azimut { get; set; }
        public Vector Position { get; set; }
        public int? Id { get; set; }
        public string Side { get; set; }
        public string VehicleName { get; set; }
        public string Player { get; set; }
        public int? Leader { get; set; }
        public string Rank { get; set; }
        public string Lock { get; set; }
        public double? Skill { get; set; }
        public string Text { get; set; }
        public string Init { get; set; }
        public string Description { get; set; }
        public List<int> Synchronizations { get; set; }

        public string Name { get; set; }
        public string MarkerType { get; set; }
        public string Type { get; set; }
        public string FillName { get; set; }
        public int? A { get; set; }
        public int? B { get; set; }
        public int? DrawBorder { get; set; }
        public double? Angle { get; set; }

        public string ActivationBy { get; set; }
        public int? Interruptable { get; set; }
        public string Age { get; set; }
        public string ExpCond { get; set; }
        public string ExpActiv { get; set; }

        //public List<string> Effects { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}