using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class SensorParser : IItemParser<Sensor>
    {
        private readonly Regex _itemNumberRegex;
        private readonly List<PropertySetterBase> _propertySetters = new List<PropertySetterBase>();

        private Sensor _sensor;

        public SensorParser()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);

            _propertySetters.Add(new VectorPropertySetter("position", x => _sensor.Position = x));

            _propertySetters.Add(new StringPropertySetter("type", x => _sensor.Type = x));
            _propertySetters.Add(new IntegerPropertySetter("a", x => _sensor.A = x));
            _propertySetters.Add(new IntegerPropertySetter("b", x => _sensor.B = x));

            _propertySetters.Add(new StringPropertySetter("activationBy", x => _sensor.ActivationBy = x));
            _propertySetters.Add(new IntegerPropertySetter("interruptable", x => _sensor.Interruptable = x));
            _propertySetters.Add(new StringPropertySetter("age", x => _sensor.Age = x));
            _propertySetters.Add(new StringPropertySetter("expCond", x => _sensor.ExpCond = x));
            _propertySetters.Add(new StringPropertySetter("expActiv", x => _sensor.ExpActiv = x));
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Sensor ParseItemElement(SqmStream stream)
        {
            _sensor = new Sensor();

            stream.MatchHeader(_itemNumberRegex, SetItemNumber);

            while (!stream.IsAtEndOfContext)
            {
                Result matchResult = Result.Failure;
                foreach (var propertySetter in _propertySetters)
                {
                    matchResult = propertySetter.SetPropertyIfMatch(stream);

                    if (matchResult == Result.Success)
                    {
                        break;
                    }
                }

                // TODO: HACK! We're currently ignoring the Effects class but should be parsed!
                if (stream.CurrentLine.Contains("Effects"))
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

            return _sensor;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _sensor.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
