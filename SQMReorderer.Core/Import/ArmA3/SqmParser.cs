﻿using System;
using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.ArmA3.DataSetters;
using SQMReorderer.Core.Import.ArmA3.Parsers;
using SQMReorderer.Core.Import.ArmA3.Parsers.MissionState;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA3
{
    public class SqmParser : ParserBase<SqmContents>, ISqmParser
    {
        public SqmParser()
        {
            var missionParser = new MissionStateParser("Mission");
            var introParser = new MissionStateParser("Intro");
            var outroWinParser = new MissionStateParser("OutroWin");
            var outroLooseParser = new MissionStateParser("OutroLoose");

            ContextSetters.Add(new ContextSetter<MissionState>(missionParser, x => ParseResult.Mission = x));
            ContextSetters.Add(new ContextSetter<MissionState>(introParser, x => ParseResult.Intro = x));
            ContextSetters.Add(new ContextSetter<MissionState>(outroWinParser, x => ParseResult.OutroWin = x));
            ContextSetters.Add(new ContextSetter<MissionState>(outroLooseParser, x => ParseResult.OutroLose = x));

            PropertySetters.Add(new IntegerPropertySetter("version", x => ParseResult.Version = x));
        }

        protected override Regex HeaderRegex
        {
            get { throw new NotImplementedException(); }
        }
    }
}