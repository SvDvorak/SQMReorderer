using SQMReorderer.Core.Import.ArmA2.Context;

namespace SQMReorderer.Core.Import.ArmA2.Parsers
{
    public interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}