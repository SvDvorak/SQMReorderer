using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Context;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class MissionStateParser
    {
        private readonly IntelParser _intelParser = new IntelParser();
        private readonly ItemListParser<Vehicle> _groupsParser = new ItemListParser<Vehicle>(new VehicleParser(), "Groups");
        private readonly ItemListParser<Vehicle> _vehiclesParser = new ItemListParser<Vehicle>(new VehicleParser(), "Vehicles");
        private readonly ItemListParser<Marker> _markersParser = new ItemListParser<Marker>(new MarkerParser(), "Markers");
        private readonly ItemListParser<Sensor> _sensorsParser = new ItemListParser<Sensor>(new SensorParser(), "Sensors");

        private readonly Regex _missionStateHeaderRegex;

        private readonly List<MultiLineStringListPropertySetter> _multiLineStringPropertySetters = new List<MultiLineStringListPropertySetter>();

        private readonly IntegerPropertySetter _randomSeedPropertySetter;

        private MissionState _missionState;

        public MissionStateParser(string missionStateHeader)
        {
            _missionStateHeaderRegex = new Regex(@"class\s+" + missionStateHeader, RegexOptions.Compiled);

            _multiLineStringPropertySetters.Add(new MultiLineStringListPropertySetter("addOns", x => _missionState.AddOns = x));
            _multiLineStringPropertySetters.Add(new MultiLineStringListPropertySetter("addOnsAuto", x => _missionState.AddOnsAuto = x));

            _randomSeedPropertySetter = new IntegerPropertySetter("randomSeed", x => _missionState.RandomSeed = x);
        }

        public bool IsMissionStateElement(SqmContext context)
        {
            return context.IsHeaderMatch(_missionStateHeaderRegex);
        }

        public MissionState ParseMissionState(SqmContext context)
        {
            _missionState = new MissionState();

            foreach (var subContext in context.SubContexts)
            {
                var setResult = Result.Failure;

                foreach (var propertySetter in _multiLineStringPropertySetters)
                {
                    setResult = propertySetter.SetPropertyIfMatch(subContext);

                    if(setResult == Result.Success)
                    {
                        break;
                    }
                }

                if(setResult == Result.Success)
                {
                    continue;
                }

                if (_intelParser.IsIntelElement(subContext))
                {
                    _missionState.Intel = _intelParser.ParseIntel(subContext);
                }
                else if (_groupsParser.IsListElement(subContext))
                {
                    _missionState.Groups = _groupsParser.ParseElementItems(subContext);
                }
                else if (_vehiclesParser.IsListElement(subContext))
                {
                    _missionState.Vehicles = _vehiclesParser.ParseElementItems(subContext);
                }
                else if (_markersParser.IsListElement(subContext))
                {
                    _missionState.Markers = _markersParser.ParseElementItems(subContext);
                }
                else if (_sensorsParser.IsListElement(subContext))
                {
                    _missionState.Sensors = _sensorsParser.ParseElementItems(subContext);
                }
                else
                {
                    throw new SqmParseException("Unknown context: " + subContext.Header);
                }
            }

            foreach (var line in context.Lines)
            {
                var parseResult = _randomSeedPropertySetter.SetPropertyIfMatch(line);

                if (parseResult == Result.Failure)
                {
                    throw new SqmParseException("Unknown property: " + line);
                }
            }

            return _missionState;
        }
    }
}