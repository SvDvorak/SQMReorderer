using System.Collections.Generic;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Mission
    {
        public Mission()
        {
            AddOns = new List<string>();
            AddOnsAuto = new List<string>();

            Groups = new List<Item>();
            Vehicles = new List<Item>();
        }

        public List<string> AddOns { get; set; }
        public List<string> AddOnsAuto { get; set; }

        public int? RandomSeed { get; set; }

        public Intel Intel { get; set; }

        public List<Item> Groups { get; set; }
        public List<Item> Vehicles { get; set; }
    }
}