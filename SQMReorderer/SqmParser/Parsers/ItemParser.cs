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
        private readonly List<PropertySetterBase> _propertyRegexes = new List<PropertySetterBase>();

        private Item _currentItem;

        public ItemParser()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);

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
            _propertyRegexes.Add(new IntegerListPropertySetter("synchronizations", x => _currentItem.Synchronizations = x));
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Item ParseItemElement(SqmStream stream)
        {
            _currentItem = new Item();

            var vehiclesParser = new ItemListParser("Vehicles");

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while(!stream.IsAtEndOfContext)
            {
                if(vehiclesParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    var items = vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    _currentItem.Items = items;

                    continue;
                }

                Result matchResult = Result.Failure;

                foreach (var propertySetter in _propertyRegexes)
                {
                    matchResult = propertySetter.SetPropertyIfMatch(stream);

                    if(matchResult == Result.Success)
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

            return _currentItem;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _currentItem.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
