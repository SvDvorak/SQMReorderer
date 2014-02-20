﻿using System;
using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.ArmA2.Context;
using SQMReorderer.Core.Import.ArmA2.HelperFunctions;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA2.Parsers
{
    public class ItemParserBase<TItemType> : ParserBase<TItemType>
        where TItemType : ItemBase, new ()
    {
        private readonly Regex _itemNumberRegex;

        public ItemParserBase()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);
        }

        protected override Regex HeaderRegex
        {
            get { return _itemNumberRegex; }
        }

        public override TItemType ParseContext(SqmContext context)
        {
            base.ParseContext(context);

            SetItemNumber(context.Header);

            return ParseResult;
        }

        private void SetItemNumber(string itemHeader)
        {
            var itemNumberMatch = _itemNumberRegex.Match(itemHeader);
            var numberGroup = itemNumberMatch.Groups["number"];
            ParseResult.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}