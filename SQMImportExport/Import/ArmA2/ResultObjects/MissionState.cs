using System.Collections.Generic;

namespace SQMImportExport.Import.ArmA2.ResultObjects
{
    public class MissionState
    {
        public MissionState()
        {
            AddOns = new List<string>();
            AddOnsAuto = new List<string>();

            Groups = new List<Vehicle>();
            Vehicles = new List<Vehicle>();
            Markers = new List<Marker>();
            Sensors = new List<Sensor>();
        }

        public List<string> AddOns { get; set; }
        public List<string> AddOnsAuto { get; set; }

        public int? RandomSeed { get; set; }

        public Intel Intel { get; set; }

        public List<Vehicle> Groups { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Marker> Markers { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}