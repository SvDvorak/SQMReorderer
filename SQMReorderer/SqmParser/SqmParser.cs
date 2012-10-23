using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
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

        public ParseResult Parse(SqmContext context)
        {
            _parseResult = new ParseResult();

            foreach (var subContext in context.SubContexts)
            {
                if (_missionParser.IsMissionStateElement(subContext))
                {
                    _parseResult.Mission = _missionParser.ParseMissionState(subContext);
                }
                else if (_introParser.IsMissionStateElement(subContext))
                {
                    _parseResult.Intro = _missionParser.ParseMissionState(subContext);
                }
                else if (_outroWinParser.IsMissionStateElement(subContext))
                {
                    _parseResult.OutroWin = _outroWinParser.ParseMissionState(subContext);
                }
                else if (_outroLooseParser.IsMissionStateElement(subContext))
                {
                    _parseResult.OutroLose = _outroLooseParser.ParseMissionState(subContext);
                }
                else
                {
                    throw new SqmParseException("Unknown context: " + subContext.Header);
                }
            }

            foreach (var line in context.Lines)
            {
                if (line.IsMatch(_versionRegex))
                {
                    line.Match(_versionRegex, x => SetVersion(x));
                }
                else
                {
                    throw new SqmParseException("Unknown property: " + line);
                }
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
