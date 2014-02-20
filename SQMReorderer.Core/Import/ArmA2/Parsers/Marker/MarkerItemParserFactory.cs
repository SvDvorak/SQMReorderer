namespace SQMReorderer.Core.Import.ArmA2.Parsers.Marker
{
    public class MarkerItemParserFactory : IItemParserFactory<ResultObjects.Marker>
    {
        public IParser<ResultObjects.Marker> CreateParser()
        {
            return new MarkerItemParser();
        }
    }
}