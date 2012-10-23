using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class SensorParser : ItemParserBase<Sensor>
    {
        public SensorParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => Item.Position = x));
            PropertySetters.Add(new IntegerPropertySetter("a", x => Item.A = x));
            PropertySetters.Add(new IntegerPropertySetter("b", x => Item.B = x));
            PropertySetters.Add(new StringPropertySetter("activationBy", x => Item.ActivationBy = x));
            PropertySetters.Add(new IntegerPropertySetter("interruptable", x => Item.Interruptable = x));
            PropertySetters.Add(new StringPropertySetter("type", x => Item.Type = x));
            PropertySetters.Add(new StringPropertySetter("age", x => Item.Age = x));
            PropertySetters.Add(new StringPropertySetter("expCond", x => Item.ExpCond = x));
            PropertySetters.Add(new StringPropertySetter("expActiv", x => Item.ExpActiv = x));
        }

        protected override Result CustomParseContext(SqmContext context)
        {
            var parseResult = Result.Success;

            // TODO: HACK! We're currently ignoring the Effects class but should be parsed!
            //if (stream.CurrentLine.Contains("Effects"))
            //{
            //    parseResult = Result.Failure;
            //}

            return parseResult;
        }
    }
}
