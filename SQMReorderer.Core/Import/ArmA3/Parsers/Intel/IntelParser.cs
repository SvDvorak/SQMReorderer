using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA3.Parsers.Intel
{
    public class IntelParser : ParserBase<ResultObjects.Intel>
    {
        private readonly Regex _intelRegex = new Regex(@"class Intel", RegexOptions.Compiled);

        public IntelParser()
        {
            PropertySetters.Add(new StringPropertySetter("briefingName", x => ParseResult.BriefingName = x));
            PropertySetters.Add(new StringPropertySetter("overviewText", x => ParseResult.OverviewText = x));
            PropertySetters.Add(new DoublePropertySetter("timeOfChanges", x => ParseResult.TimeOfChanges = x));
            PropertySetters.Add(new DoublePropertySetter("startWeather", x => ParseResult.StartWeather = x));
            PropertySetters.Add(new DoublePropertySetter("startWind", x => ParseResult.StartWind = x));
            PropertySetters.Add(new DoublePropertySetter("startWaves", x => ParseResult.StartWaves = x));
            PropertySetters.Add(new DoublePropertySetter("forecastWeather", x => ParseResult.ForecastWeather = x));
            PropertySetters.Add(new DoublePropertySetter("forecastWind", x => ParseResult.ForecastWind = x));
            PropertySetters.Add(new DoublePropertySetter("forecastWaves", x => ParseResult.ForecastWaves = x));
            PropertySetters.Add(new DoublePropertySetter("forecastLightnings", x => ParseResult.ForecastLightnings = x));
            PropertySetters.Add(new IntegerPropertySetter("rainForced", x => ParseResult.RainForced = x));
            PropertySetters.Add(new IntegerPropertySetter("lightningsForced", x => ParseResult.LightningsForced = x));
            PropertySetters.Add(new IntegerPropertySetter("wavesForced", x => ParseResult.WavesForced = x));
            PropertySetters.Add(new IntegerPropertySetter("windForced", x => ParseResult.WindForced = x));
            PropertySetters.Add(new IntegerPropertySetter("year", x => ParseResult.Year = x));
            PropertySetters.Add(new IntegerPropertySetter("month", x => ParseResult.Month = x));
            PropertySetters.Add(new IntegerPropertySetter("day", x => ParseResult.Day = x));
            PropertySetters.Add(new IntegerPropertySetter("hour", x => ParseResult.Hour = x));
            PropertySetters.Add(new IntegerPropertySetter("minute", x => ParseResult.Minute = x));
            PropertySetters.Add(new DoublePropertySetter("startFogBase", x => ParseResult.StartFogBase = x));
            PropertySetters.Add(new DoublePropertySetter("forecastFogBase", x => ParseResult.ForecastFogBase = x));
            PropertySetters.Add(new DoublePropertySetter("startFogDecay", x => ParseResult.StartFogDecay = x));
            PropertySetters.Add(new DoublePropertySetter("forecastFogDecay", x => ParseResult.ForecastFogDecay = x));
        }

        protected override Regex HeaderRegex
        {
            get { return _intelRegex; }
        }
    }
}