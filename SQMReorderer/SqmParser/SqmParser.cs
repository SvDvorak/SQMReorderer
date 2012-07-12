using System;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Parsers;

namespace SQMReorderer.SqmParser
{
    public class SqmParser
    {
        private readonly Regex _versionRegex = new Regex(@"version=(?<version>\d+)", RegexOptions.Compiled);
        private readonly Regex _missionRegex = new Regex(@"class\s+Mission", RegexOptions.Compiled);

        private readonly MissionParser _missionParser = new MissionParser();

        public ParseResult Parse(String[] inputText)
        {
            var parseResult = new ParseResult();

            var stream = new SqmStream(inputText);

            while(!stream.IsAtEndOfContext)
            {
                if (stream.IsCurrentLineMatch(_versionRegex))
                {
                    stream.MatchCurrentLine(_versionRegex,
                        x => parseResult.Version = Convert.ToInt32(x.Value));

                    stream.NextLineInContext();
                }

                if (stream.IsCurrentLineMatch(_missionRegex))
                {
                    stream.StepIntoInnerContext();
                    _missionParser.ParseMission(stream);
                    stream.StepIntoOuterContext();
                }
            }

            return parseResult;
        }
    }
}
