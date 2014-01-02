using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser
{
    public interface ISqmParser
    {
        SqmContents ParseContext(SqmContext context);
    }
}