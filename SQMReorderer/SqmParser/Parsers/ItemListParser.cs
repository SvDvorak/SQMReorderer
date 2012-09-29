using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.Parsers
{
    public class ItemListParser<TItemType>
    {
        private readonly Regex _classRegex = new Regex(@"class\s+(?<class>\w+)", RegexOptions.Compiled);
        private readonly Regex _itemCountRegex = new Regex(@"items\=(?<itemCount>\d+)", RegexOptions.Compiled);

        private readonly IItemParser<TItemType> _itemParser;
        private readonly string _listTypeName;

        private int _itemCount;

        public ItemListParser(IItemParser<TItemType> itemParser, string listTypeName)
        {
            _itemParser = itemParser;
            _listTypeName = listTypeName;
        }

        public bool IsListElement(SqmStream stream)
        {
            bool isCorrectListElement = false;

            stream.MatchCurrentLine(_classRegex, match =>
                {
                    isCorrectListElement = match.Groups["class"].Value == _listTypeName;
                });

            return isCorrectListElement;
        }

        public List<TItemType> ParseElementItems(SqmStream stream)
        {
            _itemCount = 0;

            var itemList = new List<TItemType>();

            while(!stream.IsAtEndOfContext)
            {
                if(stream.IsCurrentLineMatch(_itemCountRegex))
                {
                    stream.MatchCurrentLine(_itemCountRegex, SetItemCount);
                }
                else if (_itemParser.IsItemElement(stream))
                {
                    stream.StepIntoInnerContext();
                    var item = _itemParser.ParseItemElement(stream);
                    stream.StepIntoOuterContext();

                    itemList.Add(item);
                }

                stream.NextLineInContext();
            }

            if (_itemCount != itemList.Count)
            {
                throw new SqmParseException("Declared item count does not match actual item count.\n" +
                                            "Declared: " + _itemCount + "\n" +
                                            "Actual: " + itemList.Count);
            }
            
            if(_itemCount == 0)
            {
                throw new SqmParseException("Item list cannot be empty");
            }

            return itemList;
        }

        private void SetItemCount(Match match)
        {
            var itemCountGroup = match.Groups["itemCount"];
            _itemCount = Convert.ToInt32(itemCountGroup.Value);
        }
    }
}
