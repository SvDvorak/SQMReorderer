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
        private readonly List<PropertySetterBase> _propertySetters = new List<PropertySetterBase>();

        private Item _item;

        public ItemParser()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);

            _propertySetters.Add(new VectorPropertySetter("position", x => _item.Position = x));
            _propertySetters.Add(new DoublePropertySetter("azimut", x => _item.Azimut = x));
            _propertySetters.Add(new IntegerPropertySetter("id", x => _item.Id = x));
            _propertySetters.Add(new DoublePropertySetter("skill", x => _item.Skill = x));
            _propertySetters.Add(new StringPropertySetter("side", x => _item.Side = x));
            _propertySetters.Add(new StringPropertySetter("vehicle", x => _item.Vehicle = x));
            _propertySetters.Add(new StringPropertySetter("player", x => _item.Player = x));
            _propertySetters.Add(new IntegerPropertySetter("leader", x => _item.Leader = x));
            _propertySetters.Add(new StringPropertySetter("rank", x => _item.Rank = x));
            _propertySetters.Add(new StringPropertySetter("lock", x => _item.Lock = x));
            _propertySetters.Add(new StringPropertySetter("init", x => _item.Init = x));
            _propertySetters.Add(new StringPropertySetter("text", x => _item.Text = x));
            _propertySetters.Add(new StringPropertySetter("description", x => _item.Description = x));
            _propertySetters.Add(new IntegerListPropertySetter("synchronizations", x => _item.Synchronizations = x));

            _propertySetters.Add(new StringPropertySetter("name", x => _item.Name = x));
            _propertySetters.Add(new StringPropertySetter("markerType", x => _item.MarkerType = x));
            _propertySetters.Add(new StringPropertySetter("type", x => _item.Type = x));
            _propertySetters.Add(new StringPropertySetter("fillName", x => _item.FillName = x));
            _propertySetters.Add(new IntegerPropertySetter("a", x => _item.A = x));
            _propertySetters.Add(new IntegerPropertySetter("b", x => _item.B = x));
            _propertySetters.Add(new IntegerPropertySetter("drawBorder", x => _item.DrawBorder = x));
            _propertySetters.Add(new DoublePropertySetter("angle", x => _item.Angle = x));

            _propertySetters.Add(new StringPropertySetter("activationBy", x => _item.ActivationBy = x));
            _propertySetters.Add(new IntegerPropertySetter("interruptable", x => _item.Interruptable = x));
            _propertySetters.Add(new StringPropertySetter("age", x => _item.Age = x));
            _propertySetters.Add(new StringPropertySetter("expCond", x => _item.ExpCond = x));
            _propertySetters.Add(new StringPropertySetter("expActiv", x => _item.ExpActiv = x));
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Item ParseItemElement(SqmStream stream)
        {
            _item = new Item();

            var vehiclesParser = new ItemListParser("Vehicles");

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while(!stream.IsAtEndOfContext)
            {
                if(vehiclesParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    var items = vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    _item.Items = items;

                    continue;
                }

                Result matchResult = Result.Failure;
                foreach (var propertySetter in _propertySetters)
                {
                    matchResult = propertySetter.SetPropertyIfMatch(stream);

                    if(matchResult == Result.Success)
                    {
                        break;
                    }
                }

                // TODO: HACK! We're currently ignoring the Effects class but should be parsed!
                if(stream.CurrentLine.Contains("Effects"))
                {
                    stream.NextLineInContext();
                    continue;
                }

                if (matchResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                }

                stream.NextLineInContext();
            }

            return _item;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _item.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
