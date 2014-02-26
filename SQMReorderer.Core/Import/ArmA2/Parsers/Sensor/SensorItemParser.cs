using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Sensor
{
    public class SensorItemParser : ItemParserBase<ResultObjects.Sensor>
    {
        public SensorItemParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            PropertySetters.Add(new IntegerPropertySetter("a", x => ParseResult.A = x));
            PropertySetters.Add(new IntegerPropertySetter("b", x => ParseResult.B = x));
            PropertySetters.Add(new DoublePropertySetter("angle", x => ParseResult.Angle = x));
            PropertySetters.Add(new IntegerPropertySetter("rectangular", x => ParseResult.Rectangular = x));
            PropertySetters.Add(new StringPropertySetter("activationBy", x => ParseResult.ActivationBy = x));
            PropertySetters.Add(new StringPropertySetter("activationType", x => ParseResult.ActivationType = x));
            PropertySetters.Add(new IntegerPropertySetter("repeating", x => ParseResult.Repeating = x));
            PropertySetters.Add(new IntegerPropertySetter("timeoutMin", x => ParseResult.TimeoutMin = x));
            PropertySetters.Add(new IntegerPropertySetter("timeoutMid", x => ParseResult.TimeoutMid = x));
            PropertySetters.Add(new IntegerPropertySetter("timeoutMax", x => ParseResult.TimeoutMax = x));
            PropertySetters.Add(new IntegerPropertySetter("interruptable", x => ParseResult.Interruptable = x));
            PropertySetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            PropertySetters.Add(new StringPropertySetter("age", x => ParseResult.Age = x));
            PropertySetters.Add(new StringPropertySetter("name", x => ParseResult.Name = x));
            PropertySetters.Add(new IntegerPropertySetter("idVehicle", x => ParseResult.IdVehicle = x));
            PropertySetters.Add(new StringPropertySetter("expCond", x => ParseResult.ExpCond = x));
            PropertySetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
            PropertySetters.Add(new StringPropertySetter("expDesactiv", x => ParseResult.ExpDesactiv = x));
            PropertySetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));
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