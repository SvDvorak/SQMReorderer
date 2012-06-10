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

        private int _itemCount;

        public bool IsVehiclesElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_vehiclesRegex);
        }

        public List<Item> ParseVehicleElement(SqmStream stream)
        {
            _itemCount = 0;

            var itemParser = new ItemParser();
            var itemList = new List<Item>();

            while(!stream.IsAtEndOfContext)
            {
                if(stream.IsCurrentLineMatch(_itemCountRegex))
                {
                    stream.MatchCurrentLine(_itemCountRegex, SetItemCount);
                }
                else if (itemParser.IsItemElement(stream))
                {
                    stream.StepIntoInnerContext();
                    var item = itemParser.ParseItemElement(stream);
                    stream.StepOutOfInnerContext();

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
