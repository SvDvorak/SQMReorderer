namespace SQMReorderer.Core.Import.ArmA2.Parsers.Sensor
{
    public class SensorItemParserFactory : IItemParserFactory<ResultObjects.Sensor>
    {
        public IParser<ResultObjects.Sensor> CreateParser()
        {
            return new SensorItemParser();
        }
    }
}