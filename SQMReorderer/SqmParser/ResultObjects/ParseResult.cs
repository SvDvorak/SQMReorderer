using System.Collections.Generic;
using SQMReorderer.SqmParser.Parsers;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser
{
    public class ParseResult
    {
        public int Version { get; set; }

        public Mission Mission { get; set; }
    }
}