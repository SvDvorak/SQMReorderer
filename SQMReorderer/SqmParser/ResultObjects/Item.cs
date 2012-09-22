using System.Collections.Generic;
using SQMReorderer.SqmParser.Parsers;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Item
    {
        public Item()
        {
            Synchronizations = new List<int>();
            Items = new List<Item>();
        }

        public int Number { get; set; }

        public double? Azimut { get; set; }
        public Vector Position { get; set; }
        public int? Id { get; set; }
        public string Side { get; set; }
        public string Vehicle { get; set; }
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
        public int Interruptable { get; set; }
        public string Age { get; set; }
        public string ExpCond { get; set; }
        public string ExpActiv { get; set; }

        public List<string> Effects { get; set; }

        public List<Item> Items { get; set; }
    }
}