﻿using System.Collections.Generic;
using SQMImportExport.Import.ArmA3.Parsers.Waypoint;
using SQMImportExport.Import.DataSetters;

namespace SQMImportExport.Import.ArmA3.Parsers.Vehicle
{
    internal class VehicleItemParser : ItemParserBase<ResultObjects.Vehicle>
    {
        public VehicleItemParser()
        {
            var vehiclesParser = new ItemListParser<ResultObjects.Vehicle>(new VehicleItemParserFactory(), "Vehicles");
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Vehicle>>(vehiclesParser, x => ParseResult.Vehicles = x));

            var waypointsParser = new ItemListParser<ResultObjects.Waypoint>(new WaypointItemParserFactory(), "Waypoints");
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Waypoint>>(waypointsParser, x => ParseResult.Waypoints = x));

            LineSetters.Add(new DoublePropertySetter("presence", x => ParseResult.Presence = x));
            LineSetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            LineSetters.Add(new IntegerPropertySetter("placement", x => ParseResult.Placement = x));
            LineSetters.Add(new DoublePropertySetter("azimut", x => ParseResult.Azimut = x));
            LineSetters.Add(new IntegerPropertySetter("id", x => ParseResult.Id = x));
            LineSetters.Add(new StringPropertySetter("side", x => ParseResult.Side = x));
            LineSetters.Add(new StringPropertySetter("vehicle", x => ParseResult.VehicleName = x));
            LineSetters.Add(new StringPropertySetter("player", x => ParseResult.Player = x));
            LineSetters.Add(new IntegerPropertySetter("leader", x => ParseResult.Leader = x));
            LineSetters.Add(new StringPropertySetter("rank", x => ParseResult.Rank = x));
            LineSetters.Add(new DoublePropertySetter("skill", x => ParseResult.Skill = x));
            LineSetters.Add(new StringPropertySetter("lock", x => ParseResult.Lock = x));
            LineSetters.Add(new DoublePropertySetter("health", x => ParseResult.Health = x));
            LineSetters.Add(new DoublePropertySetter("ammo", x => ParseResult.Ammo = x));
            LineSetters.Add(new StringPropertySetter("text", x => ParseResult.Text = x));
            LineSetters.Add(new StringPropertySetter("init", x => ParseResult.Init = x));
            LineSetters.Add(new StringPropertySetter("description", x => ParseResult.Description = x));
            LineSetters.Add(new IntegerListPropertySetter("synchronizations", x => ParseResult.Synchronizations = x));
        }
    }
}
