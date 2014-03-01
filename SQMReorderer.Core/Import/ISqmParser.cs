using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Core.Import
{
    public interface ISqmParser
    {
        ISqmContents ParseContext(SqmContext context);
    }
}