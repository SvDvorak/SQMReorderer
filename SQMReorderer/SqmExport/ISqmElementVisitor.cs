using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    interface ISqmElementVisitor
    {
        List<string> Visit(Item item);
        //void Visit(Item item);
    }
}
