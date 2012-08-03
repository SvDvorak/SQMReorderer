using System.Collections.Generic;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Item
    {
        public Item()
        {
            Items = new List<Item>();
        }

        public int Number { get; set; }

        public int? Id { get; set; }
        public string Side { get; set; }
        public string Vehicle { get; set; }
        public string Rank { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public List<Item> Items { get; set; }
    }
}