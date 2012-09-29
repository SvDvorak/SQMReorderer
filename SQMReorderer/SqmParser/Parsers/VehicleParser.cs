using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class VehicleParser : IItemParser<Vehicle>
    {
        private readonly Regex _itemNumberRegex;
        private readonly List<PropertySetterBase> _propertySetters = new List<PropertySetterBase>();

        private Vehicle _vehicle;

        public VehicleParser()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);

            _propertySetters.Add(new VectorPropertySetter("position", x => _vehicle.Position = x));
            _propertySetters.Add(new DoublePropertySetter("azimut", x => _vehicle.Azimut = x));
            _propertySetters.Add(new IntegerPropertySetter("id", x => _vehicle.Id = x));
            _propertySetters.Add(new DoublePropertySetter("skill", x => _vehicle.Skill = x));
            _propertySetters.Add(new StringPropertySetter("side", x => _vehicle.Side = x));
            _propertySetters.Add(new StringPropertySetter("vehicle", x => _vehicle.VehicleName = x));
            _propertySetters.Add(new StringPropertySetter("player", x => _vehicle.Player = x));
            _propertySetters.Add(new IntegerPropertySetter("leader", x => _vehicle.Leader = x));
            _propertySetters.Add(new StringPropertySetter("rank", x => _vehicle.Rank = x));
            _propertySetters.Add(new StringPropertySetter("lock", x => _vehicle.Lock = x));
            _propertySetters.Add(new StringPropertySetter("init", x => _vehicle.Init = x));
            _propertySetters.Add(new StringPropertySetter("text", x => _vehicle.Text = x));
            _propertySetters.Add(new StringPropertySetter("description", x => _vehicle.Description = x));
            _propertySetters.Add(new IntegerListPropertySetter("synchronizations", x => _vehicle.Synchronizations = x));
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Vehicle ParseItemElement(SqmStream stream)
        {
            _vehicle = new Vehicle();

            var vehiclesParser = new ItemListParser<Vehicle>(new VehicleParser(), "Vehicles");

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while(!stream.IsAtEndOfContext)
            {
                if(vehiclesParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    var items = vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    _vehicle.Vehicles = items;

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

                if (matchResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                }

                stream.NextLineInContext();
            }

            return _vehicle;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _vehicle.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
