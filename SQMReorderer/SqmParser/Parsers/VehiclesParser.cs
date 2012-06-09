using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.Parsers
{
    public class VehiclesParser
    {
        private readonly Regex _vehiclesRegex = new Regex(@"class\s+Vehicles", RegexOptions.Compiled);

        private readonly Regex _itemCountRegex = new Regex(@"items\=(?<itemCount>\d+)", RegexOptions.Compiled);

        private ParsingHelperFunctions _parsingHelperFunctions = new ParsingHelperFunctions();
        private ItemParser _itemParser = new ItemParser();

        public bool IsVehiclesElement(string inputLine)
        {
            var match = _vehiclesRegex.Match(inputLine);

            return match.Success;
        }

        public List<Item> ParseVehicleElement(string[] inputText)
        {
            int itemCount = 0;

            var itemList = new List<Item>();

            foreach (var line in inputText)
            {
                if (_parsingHelperFunctions.IsLineBracket(line))
                {
                    continue;
                }

                var currentMatch = _itemCountRegex.Match(line);
                if (currentMatch.Success)
                {
                    var itemCountGroup = currentMatch.Groups["itemCount"];
                    itemCount = Convert.ToInt32(itemCountGroup.Value);
                    continue;
                }

                if (_itemParser.IsItemElement(line))
                {
                    var item = _itemParser.ParseItemElement(inputText);

                    itemList.Add(item);
                }
            }

            if (itemCount != itemList.Count)
            {
                throw new SqmParseException("Declared item count does not match actual item count.\n" +
                                            "Declared: " + itemCount + "\n" +
                                            "Actual: " + itemList.Count);
            }
            
            if(itemCount == 0)
            {
                throw new SqmParseException("Item list cannot be empty");
            }

            return itemList;
        }
    }
}
