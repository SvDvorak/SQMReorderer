using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.HelperFunctions;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class VehicleParser : ItemParserBase<Vehicle>
    {
        public VehicleParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => Item.Position = x));
            PropertySetters.Add(new DoublePropertySetter("azimut", x => Item.Azimut = x));
            PropertySetters.Add(new IntegerPropertySetter("id", x => Item.Id = x));
            PropertySetters.Add(new StringPropertySetter("side", x => Item.Side = x));
            PropertySetters.Add(new StringPropertySetter("vehicle", x => Item.VehicleName = x));
            PropertySetters.Add(new StringPropertySetter("player", x => Item.Player = x));
            PropertySetters.Add(new IntegerPropertySetter("leader", x => Item.Leader = x));
            PropertySetters.Add(new StringPropertySetter("rank", x => Item.Rank = x));
            PropertySetters.Add(new DoublePropertySetter("skill", x => Item.Skill = x));
            PropertySetters.Add(new StringPropertySetter("lock", x => Item.Lock = x));
            PropertySetters.Add(new StringPropertySetter("text", x => Item.Text = x));
            PropertySetters.Add(new StringPropertySetter("init", x => Item.Init = x));
            PropertySetters.Add(new StringPropertySetter("description", x => Item.Description = x));
            PropertySetters.Add(new IntegerListPropertySetter("synchronizations", x => Item.Synchronizations = x));
        }

        protected override Result CustomParseContext(SqmContext context)
        {
            var childVehiclesParser = new ItemListParser<Vehicle>(new VehicleParser(), "Vehicles");

            if (childVehiclesParser.IsListElement(context))
            {
                var items = childVehiclesParser.ParseElementItems(context);
                Item.Vehicles = items;

                return Result.Success;
            }

            return Result.Failure;
        }
    }
}
