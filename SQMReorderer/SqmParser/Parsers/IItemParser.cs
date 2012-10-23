using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public interface IItemParser<ItemType>
    {
        bool IsItemContext(SqmContext context);
        ItemType ParseItemContext(SqmContext context);
    }
}