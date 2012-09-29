using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Parsers;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser
{
    public class SqmParser
    {
        private readonly Regex _versionRegex = new Regex(@"version=(?<version>\d+)", RegexOptions.Compiled);

        private readonly MissionStateParser _missionParser = new MissionStateParser("Mission");
        private readonly MissionStateParser _introParser = new MissionStateParser("Intro");
        private readonly MissionStateParser _outroWinParser = new MissionStateParser("OutroWin");
        private readonly MissionStateParser _outroLooseParser = new MissionStateParser("OutroLoose");

        private ParseResult _parseResult;

        public ParseResult Parse(SqmStream stream)
        {
            _parseResult = new ParseResult();

            while(!stream.IsAtEndOfContext)
            {
                if (_missionParser.IsMissionStateElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _parseResult.Mission = _missionParser.ParseMissionState(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_introParser.IsMissionStateElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _parseResult.Intro = _missionParser.ParseMissionState(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_outroWinParser.IsMissionStateElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _parseResult.OutroWin = _outroWinParser.ParseMissionState(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_outroLooseParser.IsMissionStateElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _parseResult.OutroLose = _outroLooseParser.ParseMissionState(stream);
                    stream.StepIntoOuterContext();
                }
                else if (stream.IsCurrentLineMatch(_versionRegex))
                {
                    stream.MatchCurrentLine(_versionRegex, x => SetVersion(x));
                }

                stream.NextLineInContext();
            }

            return _parseResult;
        }

        private int? SetVersion(Match match)
        {
            var versionGroup = match.Groups["version"];
            return _parseResult.Version = Convert.ToInt32(versionGroup.Value);
        }
    }
}
