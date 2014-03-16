﻿using System;
using System.Text.RegularExpressions;
using SQMImportExport.Import.ArmA2.Parsers.MissionState;
using SQMImportExport.Import.ArmA2.ResultObjects;
using SQMImportExport.Import.Context;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA2
{
    internal class SqmParser : ParserBase<SqmContents>, ISqmParser
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

            LineSetters.Add(new IntegerPropertySetter("version", x => ParseResult.Version = x));
        }

        protected override Regex HeaderRegex
        {
            get { throw new NotImplementedException(); }
        }

        public new ISqmContents ParseContext(SqmContext context)
        {
            return base.ParseContext(context);
        }
    }
}