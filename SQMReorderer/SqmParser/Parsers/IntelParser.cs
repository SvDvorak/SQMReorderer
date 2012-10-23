using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class IntelParser
    {
        private readonly Regex _intelRegex = new Regex(@"class Intel", RegexOptions.Compiled);

        private readonly List<PropertySetterBase> _propertyRegexes = new List<PropertySetterBase>();

        private Intel _intel;

        public IntelParser()
        {
            _propertyRegexes.Add(new StringPropertySetter("briefingName", x => _intel.BriefingName = x));
            _propertyRegexes.Add(new StringPropertySetter("briefingDescription", x => _intel.BriefingDescription = x));
            _propertyRegexes.Add(new DoublePropertySetter("startWeather", x => _intel.StartWeather = x));
            _propertyRegexes.Add(new DoublePropertySetter("forecastWeather", x => _intel.ForecastWeather = x));
            _propertyRegexes.Add(new IntegerPropertySetter("year", x => _intel.Year = x));
            _propertyRegexes.Add(new IntegerPropertySetter("month", x => _intel.Month = x));
            _propertyRegexes.Add(new IntegerPropertySetter("day", x => _intel.Day = x));
            _propertyRegexes.Add(new IntegerPropertySetter("hour", x => _intel.Hour = x));
            _propertyRegexes.Add(new IntegerPropertySetter("minute", x => _intel.Minute = x));
        }

        public bool IsIntelElement(SqmContext context)
        {
            return context.IsHeaderMatch(_intelRegex);
        }

        public Intel ParseIntel(SqmContext context)
        {
            _intel = new Intel();

            foreach(var line in context.Lines)
            {
                Result matchResult = Result.Failure;

                foreach (var propertySetter in _propertyRegexes)
                {
                    matchResult = propertySetter.SetPropertyIfMatch(line);

                    if (matchResult == Result.Success)
                    {
                        break;
                    }
                }

                if (matchResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + line);
                }
            }

            return _intel;
        }
    }
}