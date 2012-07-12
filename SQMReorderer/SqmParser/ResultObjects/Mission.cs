using System.Collections.Generic;

namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Mission
    {
        public Mission()
        {
            Group = new List<Group>();
        }

        public List<Group> Group { get; set; }
    }
}