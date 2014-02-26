using System.Collections.Generic;
using SQMReorderer.Core.Import.ArmA2.Parsers.Effects;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Waypoint
{
    public class WaypointItemParser : ItemParserBase<ResultObjects.Waypoint>
    {
        public WaypointItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            PropertySetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            PropertySetters.Add(new IntegerPropertySetter("placement", x => ParseResult.Placement = x));
            PropertySetters.Add(new IntegerPropertySetter("completitionRadius", x => ParseResult.CompletitionRadius = x));
            PropertySetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            PropertySetters.Add(new StringPropertySetter("combatMode", x => ParseResult.CombatMode = x));
            PropertySetters.Add(new StringPropertySetter("formation", x => ParseResult.Formation = x));
            PropertySetters.Add(new StringPropertySetter("speed", x => ParseResult.Speed = x));
            PropertySetters.Add(new StringPropertySetter("combat", x => ParseResult.Combat = x));
            PropertySetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
            PropertySetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));
            PropertySetters.Add(new StringPropertySetter("showWP", x => ParseResult.ShowWp = x));
        }
    }
}
