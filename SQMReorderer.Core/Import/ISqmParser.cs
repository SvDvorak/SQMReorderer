using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import
{
    public interface ISqmParser
    {
        ISqmContents ParseContext(SqmContext context);
    }
}