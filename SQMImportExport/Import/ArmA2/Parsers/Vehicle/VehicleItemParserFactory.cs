namespace SQMReorderer.Core.Import.ArmA2.Parsers.Vehicle
{
    internal class VehicleItemParserFactory : IItemParserFactory<ResultObjects.Vehicle>
    {
        public IParser<ResultObjects.Vehicle> CreateParser()
        {
            return new VehicleItemParser();
        }
    }
}