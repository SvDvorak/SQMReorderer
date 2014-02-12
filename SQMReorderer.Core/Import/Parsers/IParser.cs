using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.Parsers
{
    public interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}