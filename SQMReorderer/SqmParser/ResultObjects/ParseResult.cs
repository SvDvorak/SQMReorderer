using System.Collections.Generic;
using SQMReorderer.SqmParser.Parsers;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser
{
    public class ParseResult
    {
        public int Version { get; set; }

        public Intel Intel { get; set; }

        public List<Item> Groups { get; set; }
    }
}