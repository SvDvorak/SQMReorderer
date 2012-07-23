using System;
using System.Collections.Generic;
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

        public ParseResult Parse(List<string> inputText)
        {
            _parseResult = new ParseResult();

            var stream = new SqmStream(inputText);

            if(stream.IsAtEndOfContext)
            {
                return _parseResult;
            }
            while(!stream.IsAtEndOfContext)
            {
                if (stream.IsCurrentLineMatch(_missionRegex))
                {
                    stream.StepIntoInnerContext();
                    _parseResult.Mission = _missionParser.ParseMission(stream);
                    stream.StepIntoOuterContext();

                    continue;
                }

                if (stream.IsCurrentLineMatch(_versionRegex))
                {
                    stream.MatchCurrentLine(_versionRegex,
                                            x => SetVersion(x));
                }

                stream.NextLineInContext();
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
