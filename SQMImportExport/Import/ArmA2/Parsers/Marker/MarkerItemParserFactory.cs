namespace SQMImportExport.Import.ArmA2.Parsers.Marker
{
    internal class MarkerItemParserFactory : IItemParserFactory<ResultObjects.Marker>
    {
        public IParser<ResultObjects.Marker> CreateParser()
        {
            return new MarkerItemParser();
        }
    }
}