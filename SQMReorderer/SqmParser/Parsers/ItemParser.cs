using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.Parsers
{
    public class ItemParser
    {
        private readonly Regex _itemHeaderRegex = new Regex(@"Item(?<number>\d+)", RegexOptions.Compiled);

        private readonly Regex _sideRegex = new Regex(@"side\=""(?<side>\w+)""", RegexOptions.Compiled);
        private readonly Regex _vehicleRegex = new Regex(@"vehicle\=""(?<vehicle>\w+)""", RegexOptions.Compiled);
        private readonly Regex _rankRegex = new Regex(@"rank\=""(?<rank>\w+)""", RegexOptions.Compiled);
        private readonly Regex _textRegex = new Regex(@"text\=""(?<text>\w+)""", RegexOptions.Compiled);
        private readonly Regex _descriptionRegex = new Regex(@"description\=""(?<description>[\w\s]+)""", RegexOptions.Compiled);


        public bool IsItem(string inputRow)
        {
            var match = _itemHeaderRegex.Match(inputRow);

            return match.Success;
        }

        public Item ParseItem(string[] inputText)
        {
            var item = new Item();

            var currentMatch = _itemHeaderRegex.Match(inputText[0]);

            if (currentMatch.Success)
            {
                var numberGroup = currentMatch.Groups["number"];
                item.Number = Convert.ToInt32(numberGroup.Value);
            }

            foreach (var line in inputText)
            {
                currentMatch = _sideRegex.Match(line);
                if (currentMatch.Success)
                {
                    var sideGroup = currentMatch.Groups["side"];
                    item.Side = sideGroup.Value;
                    continue;
                }

                currentMatch = _vehicleRegex.Match(line);
                if(currentMatch.Success)
                {
                    var vehicleGroup = currentMatch.Groups["vehicle"];
                    item.Vehicle = vehicleGroup.Value;
                    continue;
                }

                currentMatch = _rankRegex.Match(line);
                if (currentMatch.Success)
                {
                    var rankGroup = currentMatch.Groups["rank"];
                    item.Rank = rankGroup.Value;
                    continue;
                }

                currentMatch = _textRegex.Match(line);
                if (currentMatch.Success)
                {
                    var textGroup = currentMatch.Groups["text"];
                    item.Text = textGroup.Value;
                    continue;
                }

                currentMatch = _descriptionRegex.Match(line);
                if (currentMatch.Success)
                {
                    var descriptionGroup = currentMatch.Groups["description"];
                    item.Description = descriptionGroup.Value;
                    continue;
                }

                throw new SqmParsingException("Unknown property: " + line);
            }

            return item;
        }
    }
}
