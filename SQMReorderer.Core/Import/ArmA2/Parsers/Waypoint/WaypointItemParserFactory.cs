namespace SQMReorderer.Core.Import.ArmA2.Parsers.Waypoint
{
    public class WaypointItemParserFactory : IItemParserFactory<ResultObjects.Waypoint>
    {
        public IParser<ResultObjects.Waypoint> CreateParser()
        {
            return new WaypointItemParser();
        }
    }
}