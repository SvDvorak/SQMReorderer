using System.Collections.Generic;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Group
    {
        public Group()
        {
            Items = new List<Item>();
        }

        public List<Item> Items { get; set; }
    }
}