using System.Collections.Generic;
using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.ArmA3.Parsers.Intel;
using SQMReorderer.Core.Import.ArmA3.Parsers.Marker;
using SQMReorderer.Core.Import.ArmA3.Parsers.Sensor;
using SQMReorderer.Core.Import.ArmA3.Parsers.Vehicle;
using SQMReorderer.Core.Import.DataSetters;

namespace SQMReorderer.Core.Import.ArmA3.Parsers.MissionState
{
    internal class MissionStateParser : ParserBase<ResultObjects.MissionState>
    {
        private readonly Regex _missionStateHeaderRegex;

        public MissionStateParser(string missionStateHeader)
        {
            _missionStateHeaderRegex = new Regex(@"class\s+" + missionStateHeader, RegexOptions.Compiled);

            var groupsParser = new ItemListParser<ResultObjects.Vehicle>(new VehicleItemParserFactory(), "Groups");
            var vehiclesParser = new ItemListParser<ResultObjects.Vehicle>(new VehicleItemParserFactory(), "Vehicles");
            var markersParser = new ItemListParser<ResultObjects.Marker>(new MarkerItemParserFactory(), "Markers");
            var sensorsParser = new ItemListParser<ResultObjects.Sensor>(new SensorItemParserFactory(), "Sensors");

            ContextSetters.Add(new ContextSetter<ResultObjects.Intel>(new IntelParser(), x => ParseResult.Intel = x));
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Vehicle>>(groupsParser, x => ParseResult.Groups = x));
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Vehicle>>(vehiclesParser, x => ParseResult.Vehicles = x));
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Marker>>(markersParser, x => ParseResult.Markers = x));
            ContextSetters.Add(new ContextSetter<List<ResultObjects.Sensor>>(sensorsParser, x => ParseResult.Sensors = x));

            ContextSetters.Add(new MultiLineStringListPropertySetter("addOns", x => ParseResult.AddOns = x));
            ContextSetters.Add(new MultiLineStringListPropertySetter("addOnsAuto", x => ParseResult.AddOnsAuto = x));

            LineSetters.Add(new IntegerPropertySetter("randomSeed", x => ParseResult.RandomSeed = x));
        }

        protected override Regex HeaderRegex
        {
            get { return _missionStateHeaderRegex; }
        }
    }
}