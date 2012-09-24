using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    interface ISqmElementVisitor
    {
        string Visit(string elementName, ParseResult parseResult);
        string Visit(string elementName, MissionState mission);
        string Visit(string elementName, Item item);
    }
}
