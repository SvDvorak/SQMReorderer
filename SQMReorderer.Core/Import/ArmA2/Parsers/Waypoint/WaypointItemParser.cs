using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Waypoint
{
    public class WaypointItemParser : ItemParserBase<ResultObjects.Waypoint>
    {
        public WaypointItemParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            PropertySetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            PropertySetters.Add(new StringPropertySetter("showWP", x => ParseResult.ShowWp = x));
        }
    }
}
