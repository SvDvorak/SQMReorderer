using SQMReorderer.Core.SqmParser.Context;
using SQMReorderer.Core.SqmParser.ResultObjects;

namespace SQMReorderer.Core.SqmParser
{
    public interface ISqmParser
    {
        SqmContents ParseContext(SqmContext context);
    }
}