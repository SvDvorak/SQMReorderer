using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA3.Parsers.Sensor
{
    public class SensorItemParser : ItemParserBase<ResultObjects.Sensor>
    {
        public SensorItemParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            PropertySetters.Add(new DoublePropertySetter("a", x => ParseResult.A = x));
            PropertySetters.Add(new DoublePropertySetter("b", x => ParseResult.B = x));
            PropertySetters.Add(new StringPropertySetter("activationBy", x => ParseResult.ActivationBy = x));
            PropertySetters.Add(new IntegerPropertySetter("interruptable", x => ParseResult.Interruptable = x));
            PropertySetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            PropertySetters.Add(new StringPropertySetter("age", x => ParseResult.Age = x));
            PropertySetters.Add(new StringPropertySetter("expCond", x => ParseResult.ExpCond = x));
            PropertySetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
        }

        protected override Result CustomParseContext(SqmContext context)
        {
            var parseResult = Result.Failure;

            // TODO: HACK! We're currently ignoring the Effects class but should be parsed!
            if (context.Header.Contains("Effects"))
            {
                parseResult = Result.Success;
            }

            return parseResult;
        }
    }
}
