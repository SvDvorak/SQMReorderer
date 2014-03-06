﻿using System;
using System.Text.RegularExpressions;
using SQMImportExport.Import.ArmA3.ResultObjects;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.HelperFunctions;

namespace SQMImportExport.Import.ArmA3.Parsers
{
    internal class ItemParserBase<TItemType> : ParserBase<TItemType>
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
