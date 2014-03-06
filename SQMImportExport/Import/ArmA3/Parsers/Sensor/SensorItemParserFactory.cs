namespace SQMReorderer.Core.Import.ArmA3.Parsers.Sensor
{
    internal class SensorItemParserFactory : IItemParserFactory<ResultObjects.Sensor>
    {
        public IParser<ResultObjects.Sensor> CreateParser()
        {
            return new SensorItemParser();
        }
    }
}