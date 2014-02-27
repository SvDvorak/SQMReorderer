﻿using System.Collections.Generic;
using SQMReorderer.Core.Import.DataSetters;
using SQMReorderer.Core.Import.DataSetters.Effects;

namespace SQMReorderer.Core.Import.ArmA3.Parsers.Waypoint
{
    public class WaypointItemParser : ItemParserBase<ResultObjects.Waypoint>
    {
        public WaypointItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
            LineSetters.Add(new StringPropertySetter("showWP", x => ParseResult.ShowWp = x));
        }
    }
}