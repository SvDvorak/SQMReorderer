using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool IsMissionStateElement(SqmStream stream)
        {
            return stream.IsCurrentLineMatch(_missionStateHeaderRegex);
        }

        public MissionState ParseMissionState(SqmStream stream)
        {
            _missionState = new MissionState();

            while (!stream.IsAtEndOfContext)
            {
                foreach (var propertySetter in _multiLineStringPropertySetters)
                {
                    propertySetter.SetPropertyIfMatch(stream);
                }

                _randomSeedPropertySetter.SetPropertyIfMatch(stream);

                if(_intelParser.IsIntelElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Intel = _intelParser.ParseIntel(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_groupsParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Groups = _groupsParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_vehiclesParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Vehicles = _vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_markersParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Markers = _markersParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                }
                else if (_sensorsParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Sensors = _sensorsParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                }

                stream.NextLineInContext();
            }

            return _missionState;
        }
    }
}