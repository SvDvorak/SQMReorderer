using System;
using System.Globalization;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class IntelParser
    {
        private const string DoublePattern = @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?";
        private readonly NumberFormatInfo _doubleFormatInfo;

        private readonly Regex _intelRegex = new Regex(@"class Intel", RegexOptions.Compiled);

        private readonly Regex _briefingNameRegex;
        private readonly Regex _briefingDescriptionRegex;
        private readonly Regex _startWeatherRegex;
        private readonly Regex _forecastWeatherRegex;
        private readonly Regex _yearRegex;
        private readonly Regex _monthRegex;
        private readonly Regex _dayRegex;
        private readonly Regex _hourRegex;
        private readonly Regex _minuteRegex;

        private Intel _intel;

        public IntelParser()
        {
            _doubleFormatInfo = new NumberFormatInfo();
            _doubleFormatInfo.CurrencyDecimalSeparator = ".";

            _briefingNameRegex = new Regex(@"briefingName\=""(?<briefingName>.+)""");
            _briefingDescriptionRegex = new Regex(@"briefingDescription\=""(?<briefingDescription>.+)""");
            _startWeatherRegex = new Regex(@"startWeather\=(?<startWeather>" + DoublePattern + @")");
            _forecastWeatherRegex = new Regex(@"forecastWeather\=(?<forecastWeather>" + DoublePattern + @")");
            _yearRegex = new Regex(@"year\=(?<year>\d+)");
            _monthRegex = new Regex(@"month\=(?<month>\d+)");
            _dayRegex = new Regex(@"day\=(?<day>\d+)");
            _hourRegex = new Regex(@"hour\=(?<hour>\d+)");
            _minuteRegex = new Regex(@"minute\=(?<minute>\d+)");
        }

        public bool IsIntelElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_intelRegex);
        }

        public Intel ParseIntel(SqmStream stream)
        {
            _intel = new Intel();

            while(!stream.IsAtEndOfContext)
            {
                if(stream.IsCurrentLineMatch(_briefingNameRegex))
                {
                    stream.MatchCurrentLine(_briefingNameRegex, SetBriefingName);
                }
                else if (stream.IsCurrentLineMatch(_briefingDescriptionRegex))
                {
                    stream.MatchCurrentLine(_briefingDescriptionRegex, SetBriefingDescription);
                }
                else if (stream.IsCurrentLineMatch(_startWeatherRegex))
                {
                    stream.MatchCurrentLine(_startWeatherRegex, SetStartWeather);
                }
                else if (stream.IsCurrentLineMatch(_forecastWeatherRegex))
                {
                    stream.MatchCurrentLine(_forecastWeatherRegex, SetForecastWeather);
                }
                else if (stream.IsCurrentLineMatch(_yearRegex))
                {
                    stream.MatchCurrentLine(_yearRegex, SetYear);
                }
                else if (stream.IsCurrentLineMatch(_monthRegex))
                {
                    stream.MatchCurrentLine(_monthRegex, SetMonth);
                }
                else if (stream.IsCurrentLineMatch(_dayRegex))
                {
                    stream.MatchCurrentLine(_dayRegex, SetDay);
                }
                else if (stream.IsCurrentLineMatch(_hourRegex))
                {
                    stream.MatchCurrentLine(_hourRegex, SetHour);
                }
                else if (stream.IsCurrentLineMatch(_minuteRegex))
                {
                    stream.MatchCurrentLine(_minuteRegex, SetMinute);
                }
                else
                {
                    throw new SqmParseException("Unknown property: " + stream.CurrentLine);
                }

                stream.NextLineInContext();
            }

            return _intel;
        }

        private void SetBriefingName(Match match)
        {
            var briefingNameGroup = match.Groups["briefingName"];
            _intel.BriefingName = briefingNameGroup.Value;
        }

        private void SetBriefingDescription(Match match)
        {
            var briefingDescriptionGroup = match.Groups["briefingDescription"];
            _intel.BriefingDescription = briefingDescriptionGroup.Value;
        }

        private void SetStartWeather(Match match)
        {
            var startWeatherGroup = match.Groups["startWeather"];
            _intel.StartWeather = double.Parse(startWeatherGroup.Value, _doubleFormatInfo);
        }

        private void SetForecastWeather(Match match)
        {
            var forecastWeatherGroup = match.Groups["forecastWeather"];
            _intel.ForecastWeather = double.Parse(forecastWeatherGroup.Value, _doubleFormatInfo);
        }

        private void SetYear(Match match)
        {
            var yearGroup = match.Groups["year"];
            _intel.Year = Convert.ToInt32(yearGroup.Value);
        }

        private void SetMonth(Match match)
        {
            var monthGroup = match.Groups["month"];
            _intel.Month = Convert.ToInt32(monthGroup.Value);
        }

        private void SetDay(Match match)
        {
            var dayGroup = match.Groups["day"];
            _intel.Day = Convert.ToInt32(dayGroup.Value);
        }

        private void SetHour(Match match)
        {
            var hourGroup = match.Groups["hour"];
            _intel.Hour = Convert.ToInt32(hourGroup.Value);
        }

        private void SetMinute(Match match)
        {
            var minuteGroup = match.Groups["minute"];
            _intel.Minute = Convert.ToInt32(minuteGroup.Value);
        }
    }
}