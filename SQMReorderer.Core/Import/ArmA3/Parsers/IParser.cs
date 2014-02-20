using SQMReorderer.Core.Import.ArmA3.Context;

namespace SQMReorderer.Core.Import.ArmA3.Parsers
{
    public interface IParser<TParseResult>
    {
        bool IsCorrectContext(SqmContext context);
        TParseResult ParseContext(SqmContext context);
    }
}