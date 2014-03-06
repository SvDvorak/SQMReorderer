namespace SQMImportExport.Import.ArmA3.Parsers.Waypoint
{
    internal class WaypointItemParserFactory : IItemParserFactory<ResultObjects.Waypoint>
    {
        public IParser<ResultObjects.Waypoint> CreateParser()
        {
            return new WaypointItemParser();
        }
    }
}