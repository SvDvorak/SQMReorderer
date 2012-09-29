using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class ItemParserBase<TItemType> : IItemParser<TItemType>
        where TItemType : ItemBase, new ()
    {
        private readonly Regex _itemNumberRegex;

        public TItemType Item { get; private set; }
        public List<PropertySetterBase> PropertySetters { get; private set; }

        public ItemParserBase()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);

            PropertySetters = new List<PropertySetterBase>();
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public TItemType ParseItemElement(SqmStream stream)
        {
            Item = new TItemType();

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while (!stream.IsAtEndOfContext)
            {
                CustomParseElement(stream);

                Result matchResult = Result.Failure;
                foreach (var propertySetter in PropertySetters)
                {
                    matchResult = propertySetter.SetPropertyIfMatch(stream);

                    if (matchResult == Result.Success)
                    {
                        break;
                    }
                }

                if (matchResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                }

                stream.NextLineInContext();
            }

            return Item;
        }

        protected virtual Result CustomParseElement(SqmStream stream)
        {
            return Result.Failure;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            Item.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
