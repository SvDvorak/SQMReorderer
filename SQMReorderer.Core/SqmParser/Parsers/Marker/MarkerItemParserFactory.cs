namespace SQMReorderer.Core.SqmParser.Parsers.Marker
{
    public class MarkerItemParserFactory : IItemParserFactory<ResultObjects.Marker>
    {
        public IParser<ResultObjects.Marker> CreateParser()
        {
            return new MarkerItemParser();
        }
    }
}