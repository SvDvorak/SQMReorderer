using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public interface IItemParser<ItemType>
    {
        bool IsItemElement(SqmStream stream);
        ItemType ParseItemElement(SqmStream stream);
    }
}