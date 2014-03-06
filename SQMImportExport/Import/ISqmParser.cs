using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import
{
    internal interface ISqmParser
    {
        ISqmContents ParseContext(SqmContext context);
    }
}