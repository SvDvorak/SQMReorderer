using SQMReorderer.SqmParser.Context;

namespace SQMReorderer.SqmParser.Parsers
{
    public interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}