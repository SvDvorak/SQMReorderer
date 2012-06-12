using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.Parsers
{
    public class ItemParser
    {
        private readonly Regex _itemNumberRegex = new Regex(@"class Item(?<number>\d+)", RegexOptions.Compiled);

        private readonly Regex _idRegex = new Regex(@"id\=""(?<id>\d+)""", RegexOptions.Compiled);
        private readonly Regex _sideRegex = new Regex(@"side\=""(?<side>\w+)""", RegexOptions.Compiled);
        private readonly Regex _vehicleRegex = new Regex(@"vehicle\=""(?<vehicle>\w+)""", RegexOptions.Compiled);
        private readonly Regex _rankRegex = new Regex(@"rank\=""(?<rank>\w+)""", RegexOptions.Compiled);
        private readonly Regex _textRegex = new Regex(@"text\=""(?<text>\w+)""", RegexOptions.Compiled);
        private readonly Regex _descriptionRegex = new Regex(@"description\=""(?<description>[\w\s]+)""", RegexOptions.Compiled);

        private Item _currentItem;

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Item ParseItemElement(SqmStream stream)
        {
            _currentItem = new Item();

            var vehiclesParser = new VehiclesParser();

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while(!stream.IsAtEndOfContext)
            {
                if(vehiclesParser.IsVehiclesElement(stream))
                {
                    stream.StepIntoInnerContext();
                    var items = vehiclesParser.ParseVehicleElement(stream);
                    _currentItem.Items = items;
                }
                else if (stream.IsCurrentLineMatch(_idRegex))
                {
                    stream.MatchCurrentLine(_idRegex, SetId);
                }
                else if (stream.IsCurrentLineMatch(_sideRegex))
                {
                    stream.MatchCurrentLine(_sideRegex, SetSide);
                }
                else if(stream.IsCurrentLineMatch(_vehicleRegex))
                {
                    stream.MatchCurrentLine(_vehicleRegex, SetVehicle);
                }
                else if (stream.IsCurrentLineMatch(_rankRegex))
                {
                    stream.MatchCurrentLine(_rankRegex, SetRank);
                }
                else if (stream.IsCurrentLineMatch(_textRegex))
                {
                    stream.MatchCurrentLine(_textRegex, SetText);
                }
                else if (stream.IsCurrentLineMatch(_descriptionRegex))
                {
                    stream.MatchCurrentLine(_descriptionRegex, SetDescription);
                }

                stream.NextLineInContext();

                //throw new SqmParseException("Unknown property: " + line);
            }

            return _currentItem;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _currentItem.Number = Convert.ToInt32(numberGroup.Value);
        }

        private void SetId(Match match)
        {
            var idGroup = match.Groups["id"];
            _currentItem.Id = Convert.ToInt32(idGroup.Value);
        }

        private void SetSide(Match match)
        {
            var sideGroup = match.Groups["side"];
            _currentItem.Side = sideGroup.Value;
        }

        private void SetVehicle(Match match)
        {
            var vehicleGroup = match.Groups["vehicle"];
            _currentItem.Vehicle = vehicleGroup.Value;
        }

        private void SetRank(Match match)
        {
            var rankGroup = match.Groups["rank"];
            _currentItem.Rank = rankGroup.Value;
        }

        private void SetText(Match match)
        {
            var textGroup = match.Groups["text"];
            _currentItem.Text = textGroup.Value;
        }

        private void SetDescription(Match match)
        {
            var descriptionGroup = match.Groups["description"];
            _currentItem.Description = descriptionGroup.Value;
        }
    }
}
