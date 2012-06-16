using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQMReorderer.SqmParser.Parsers
{
    public class GroupsParser
    {
        public Group ParseGroupElement(SqmStream stream)
        {
            throw new NotImplementedException();
        }
    }

    public class Group
    {
        public List<Item> Items { get; set; }
    }
}
