using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import
{
    public interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}