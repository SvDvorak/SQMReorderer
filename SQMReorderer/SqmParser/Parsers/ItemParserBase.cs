using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
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

        public bool IsItemContext(SqmContext context)
        {
            return context.IsHeaderMatch(_itemNumberRegex);
        }

        public TItemType ParseItemContext(SqmContext context)
        {
            Item = new TItemType();

            SetItemNumber(context.Header);

            foreach (var subContext in context.SubContexts)
            {
                var parseResult = CustomParseContext(subContext);

                if (parseResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown context: " + subContext.Header);
                }
            }

            foreach (var line in context.Lines)
            {
                var parseResult = new Result();

                foreach (var propertySetter in PropertySetters)
                {
                    parseResult = propertySetter.SetPropertyIfMatch(line);

                    if (parseResult == Result.Success)
                    {
                        break;
                    }
                }

                if (parseResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + line);
                }
            }

            return Item;
        }

        protected virtual Result CustomParseContext(SqmContext context)
        {
            return Result.Failure;
        }

        private void SetItemNumber(string itemHeader)
        {
            var itemNumberMatch = _itemNumberRegex.Match(itemHeader);
            var numberGroup = itemNumberMatch.Groups["number"];
            Item.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
