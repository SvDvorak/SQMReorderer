using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class MarkerParser : IItemParser<Marker>
    {
        private readonly Regex _itemNumberRegex;
        private readonly List<PropertySetterBase> _propertySetters = new List<PropertySetterBase>();

        private Marker _marker;

        public MarkerParser()
        {
            _itemNumberRegex = new Regex(@"class Item(?<number>" + CommonRegexPatterns.IntegerPattern + @")", RegexOptions.Compiled);

            _propertySetters.Add(new VectorPropertySetter("position", x => _marker.Position = x));
            _propertySetters.Add(new StringPropertySetter("text", x => _marker.Text = x));

            _propertySetters.Add(new StringPropertySetter("name", x => _marker.Name = x));
            _propertySetters.Add(new StringPropertySetter("markerType", x => _marker.MarkerType = x));
            _propertySetters.Add(new StringPropertySetter("type", x => _marker.Type = x));
            _propertySetters.Add(new StringPropertySetter("fillName", x => _marker.FillName = x));
            _propertySetters.Add(new IntegerPropertySetter("a", x => _marker.A = x));
            _propertySetters.Add(new IntegerPropertySetter("b", x => _marker.B = x));
            _propertySetters.Add(new IntegerPropertySetter("drawBorder", x => _marker.DrawBorder = x));
            _propertySetters.Add(new DoublePropertySetter("angle", x => _marker.Angle = x));
        }

        public bool IsItemElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_itemNumberRegex);
        }

        public Marker ParseItemElement(SqmStream stream)
        {
            _marker = new Marker();

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

                if (matchResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                }

                stream.NextLineInContext();
            }

            return _marker;
        }

        private void SetItemNumber(Match match)
        {
            var numberGroup = match.Groups["number"];
            _marker.Number = Convert.ToInt32(numberGroup.Value);
        }
    }
}
