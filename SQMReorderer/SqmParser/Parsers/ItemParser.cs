using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class ItemParser
    {
        private readonly Regex _itemNumberRegex;

        private readonly Regex _synchronizationHeaderRegex;
        private readonly Regex _synchronizationItemRegex;

        private Item _currentItem;
        private readonly List<PropertySetterBase> _propertyRegexes = new List<PropertySetterBase>();

        public ItemParser()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>\d+)", RegexOptions.Compiled);

            _propertyRegexes.Add(new VectorPropertySetter("position", x => _currentItem.Position = x));
            _propertyRegexes.Add(new DoublePropertySetter("azimut", x => _currentItem.Azimut = x));
            _propertyRegexes.Add(new IntegerPropertySetter("id", x => _currentItem.Id = x));
            _propertyRegexes.Add(new DoublePropertySetter("skill", x => _currentItem.Skill = x));
            _propertyRegexes.Add(new StringPropertySetter("side", x => _currentItem.Side = x));
            _propertyRegexes.Add(new StringPropertySetter("vehicle", x => _currentItem.Vehicle = x));
            _propertyRegexes.Add(new StringPropertySetter("player", x => _currentItem.Player = x));
            _propertyRegexes.Add(new IntegerPropertySetter("leader", x => _currentItem.Leader = x));
            _propertyRegexes.Add(new StringPropertySetter("rank", x => _currentItem.Rank = x));
            _propertyRegexes.Add(new StringPropertySetter("init", x => _currentItem.Init = x));
            _propertyRegexes.Add(new StringPropertySetter("text", x => _currentItem.Text = x));
            _propertyRegexes.Add(new StringPropertySetter("description", x => _currentItem.Description = x));

            _synchronizationHeaderRegex = new Regex(@"synchronizations\[\]\=", RegexOptions.Compiled);
            _synchronizationItemRegex = new Regex(@"(?<synchronization>\d+)", RegexOptions.Compiled);
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Item ParseItemElement(SqmStream stream)
        {
            _currentItem = new Item();

            var vehiclesParser = new ItemListParser();

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while(!stream.IsAtEndOfContext)
            {
                if(vehiclesParser.IsListElement("Vehicles", stream))
                {
                    stream.StepIntoInnerContext();
                    var items = vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    _currentItem.Items = items;

                    continue;
                }

                foreach (var propertySetter in _propertyRegexes)
                {
                    Result matchResult = propertySetter.SetPropertyIfMatch(stream);

                    if(matchResult == Result.Success)
                    {
                        break;
                    }
                }

                //if (matchResult == Result.Failure)
                //{
                //    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                //}

                if (stream.IsCurrentLineMatch(_synchronizationHeaderRegex))
                {
                    stream.MatchCurrentLine(_synchronizationItemRegex, SetSynchronizations);
                }
                //else
                //{
                //    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                //}

                stream.NextLineInContext();
            }

            return _currentItem;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _currentItem.Number = Convert.ToInt32(numberGroup.Value);
        }

        private void SetSynchronizations(Match match)
        {
            while(match.Success)
            {
                var synchronizationNumber = Convert.ToInt32(match.Value);
                _currentItem.Synchronizations.Add(synchronizationNumber);

                match = match.NextMatch();
            }
        }
    }
}
