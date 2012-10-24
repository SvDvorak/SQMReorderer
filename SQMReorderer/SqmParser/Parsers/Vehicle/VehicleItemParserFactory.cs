namespace SQMReorderer.SqmParser.Parsers.Vehicle
{
    public class VehicleItemParserFactory : IItemParserFactory<ResultObjects.Vehicle>
    {
        public IParser<ResultObjects.Vehicle> CreateParser()
        {
            return new VehicleItemParser();
        }
    }
}