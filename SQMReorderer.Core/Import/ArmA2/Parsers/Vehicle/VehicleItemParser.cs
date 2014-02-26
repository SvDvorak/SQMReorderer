﻿using System.Collections.Generic;
using SQMReorderer.Core.Import.ArmA2.Parsers.Waypoint;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Vehicle
{
    public class VehicleItemParser : ItemParserBase<ResultObjects.Vehicle>
    {
        public VehicleItemParser()
        {
            var vehiclesParser = new ItemListParser<ResultObjects.Vehicle>(new VehicleItemParserFactory(), "Vehicles");
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Vehicle>>(vehiclesParser, x => ParseResult.Vehicles = x));

            var waypointsParser = new ItemListParser<ResultObjects.Waypoint>(new WaypointItemParserFactory(), "Waypoints");
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Waypoint>>(waypointsParser, x => ParseResult.Waypoints = x));

            PropertySetters.Add(new DoublePropertySetter("presence", x => ParseResult.Presence = x));
            PropertySetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            PropertySetters.Add(new IntegerPropertySetter("placement", x => ParseResult.Placement = x));
            PropertySetters.Add(new DoublePropertySetter("azimut", x => ParseResult.Azimut = x));
            PropertySetters.Add(new StringPropertySetter("special", x => ParseResult.Special = x));
            PropertySetters.Add(new IntegerPropertySetter("id", x => ParseResult.Id = x));
            PropertySetters.Add(new StringPropertySetter("side", x => ParseResult.Side = x));
            PropertySetters.Add(new StringPropertySetter("vehicle", x => ParseResult.VehicleName = x));
            PropertySetters.Add(new StringPropertySetter("player", x => ParseResult.Player = x));
            PropertySetters.Add(new IntegerPropertySetter("leader", x => ParseResult.Leader = x));
            PropertySetters.Add(new StringPropertySetter("rank", x => ParseResult.Rank = x));
            PropertySetters.Add(new DoublePropertySetter("skill", x => ParseResult.Skill = x));
            PropertySetters.Add(new StringPropertySetter("lock", x => ParseResult.Lock = x));
            PropertySetters.Add(new DoublePropertySetter("health", x => ParseResult.Health = x));
            PropertySetters.Add(new DoublePropertySetter("fuel", x => ParseResult.Fuel = x));
            PropertySetters.Add(new DoublePropertySetter("ammo", x => ParseResult.Ammo = x));
            PropertySetters.Add(new StringPropertySetter("text", x => ParseResult.Text = x));
            PropertySetters.Add(new StringPropertySetter("init", x => ParseResult.Init = x));
            PropertySetters.Add(new StringPropertySetter("description", x => ParseResult.Description = x));
            PropertySetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));

            ContextSetters.Add(new MultiLineStringListPropertySetter("markers", x => ParseResult.Markers = x));
        }
    }
}
