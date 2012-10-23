using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;

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

        public bool IsListElement(SqmContext context)
        {
            bool isCorrectListElement = false;

            context.MatchHeader(_classRegex, match =>
                {
                    isCorrectListElement = match.Groups["class"].Value == _listTypeName;
                });

            return isCorrectListElement;
        }

        public List<TItemType> ParseElementItems(SqmContext context)
        {
            _itemCount = 0;

            var itemList = new List<TItemType>();

            foreach (var line in context.Lines)
            {
                if (line.IsMatch(_itemCountRegex))
                {
                    line.Match(_itemCountRegex, SetItemCount);
                }
            }

            foreach (var subContext in context.SubContexts)
            {
                if (_itemParser.IsItemContext(subContext))
                {
                    var item = _itemParser.ParseItemContext(subContext);

                    itemList.Add(item);
                }
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
