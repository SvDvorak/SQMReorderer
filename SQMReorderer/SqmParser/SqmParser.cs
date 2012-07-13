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

        private ParseResult _parseResult;

        public ParseResult Parse(String[] inputText)
        {
            _parseResult = new ParseResult();

            var stream = new SqmStream(inputText);

            while(!stream.IsAtEndOfContext)
            {
                if (stream.IsCurrentLineMatch(_versionRegex))
                {
                    stream.MatchCurrentLine(_versionRegex,
                        x => SetVersion(x));

                    stream.NextLineInContext();
                }

                if (stream.IsCurrentLineMatch(_missionRegex))
                {
                    stream.StepIntoInnerContext();
                    _parseResult.Mission = _missionParser.ParseMission(stream);
                    stream.StepIntoOuterContext();
                }
            }

            return _parseResult;
        }

        private int SetVersion(Match match)
        {
            var versionGroup = match.Groups["version"];
            return _parseResult.Version = Convert.ToInt32(versionGroup.Value);
        }
    }
}
