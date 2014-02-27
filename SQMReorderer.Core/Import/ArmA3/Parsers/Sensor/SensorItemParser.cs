using System.Collections.Generic;
using SQMReorderer.Core.Import.DataSetters;
using SQMReorderer.Core.Import.DataSetters.Effects;

namespace SQMReorderer.Core.Import.ArmA3.Parsers.Sensor
{
    public class SensorItemParser : ItemParserBase<ResultObjects.Sensor>
    {
        public SensorItemParser()
        {
            var effectsParser = new EffectsParser();
            ContextSetters.Add(new ContextSetter<List<string>>(effectsParser, x => ParseResult.Effects = x));

            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new DoublePropertySetter("a", x => ParseResult.A = x));
            LineSetters.Add(new DoublePropertySetter("b", x => ParseResult.B = x));
            LineSetters.Add(new StringPropertySetter("activationBy", x => ParseResult.ActivationBy = x));
            LineSetters.Add(new StringPropertySetter("activationType", x => ParseResult.ActivationType = x));
            LineSetters.Add(new IntegerPropertySetter("interruptable", x => ParseResult.Interruptable = x));
            LineSetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            LineSetters.Add(new StringPropertySetter("age", x => ParseResult.Age = x));
            LineSetters.Add(new StringPropertySetter("expCond", x => ParseResult.ExpCond = x));
            LineSetters.Add(new StringPropertySetter("expActiv", x => ParseResult.ExpActiv = x));
        }
    }
}
