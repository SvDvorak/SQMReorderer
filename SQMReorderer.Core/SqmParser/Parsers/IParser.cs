using SQMReorderer.Core.SqmParser.Context;

namespace SQMReorderer.Core.SqmParser.Parsers
{
    public interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}