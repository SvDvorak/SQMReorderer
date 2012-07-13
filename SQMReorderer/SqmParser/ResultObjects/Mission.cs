using System.Collections.Generic;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Mission
    {
        public Mission()
        {
            Groups = new List<Item>();
        }

        public List<Item> Groups { get; set; }
    }
}