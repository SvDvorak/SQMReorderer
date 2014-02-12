using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.Parsers.Intel
{
    public class IntelParser : ParserBase<ResultObjects.Intel>
    {
        private readonly Regex _intelRegex = new Regex(@"class Intel", RegexOptions.Compiled);

        public IntelParser()
        {
            PropertySetters.Add(new StringPropertySetter("briefingName", x => ParseResult.BriefingName = x));
            PropertySetters.Add(new StringPropertySetter("briefingDescription", x => ParseResult.BriefingDescription = x));
            PropertySetters.Add(new DoublePropertySetter("startWeather", x => ParseResult.StartWeather = x));
            PropertySetters.Add(new DoublePropertySetter("forecastWeather", x => ParseResult.ForecastWeather = x));
            PropertySetters.Add(new IntegerPropertySetter("year", x => ParseResult.Year = x));
            PropertySetters.Add(new IntegerPropertySetter("month", x => ParseResult.Month = x));
            PropertySetters.Add(new IntegerPropertySetter("day", x => ParseResult.Day = x));
            PropertySetters.Add(new IntegerPropertySetter("hour", x => ParseResult.Hour = x));
            PropertySetters.Add(new IntegerPropertySetter("minute", x => ParseResult.Minute = x));
        }

        protected override Regex HeaderRegex
        {
            get { return _intelRegex; }
        }
    }
}