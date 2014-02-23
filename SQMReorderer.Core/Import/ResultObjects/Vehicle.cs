using System.Collections.Generic;

namespace SQMReorderer.Core.Import.ResultObjects
{
    public class Vehicle : ItemBase
    {
        public Vehicle()
        {
            Synchronizations = new List<int>();
            Vehicles = new List<Vehicle>();
        }

        public double? Azimut { get; set; }
        public string Special { get; set; }
        public int? Id { get; set; }
        public string Side { get; set; }
        public string VehicleName { get; set; }
        public string Player { get; set; }
        public int? Leader { get; set; }
        public string Rank { get; set; }
        public string Lock { get; set; }
        public double? Skill { get; set; }
        public double? Health { get; set; }
        public double? Fuel { get; set; }
        public string Text { get; set; }
        public string Init { get; set; }
        public string Description { get; set; }
        public List<int> Synchronizations { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}