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
        private readonly ItemListParser _groupsParser = new ItemListParser("Groups");
        private readonly ItemListParser _vehiclesParser = new ItemListParser("Vehicles");
        private readonly ItemListParser _markersParser = new ItemListParser("Markers");
        private readonly ItemListParser _sensorsParser = new ItemListParser("Sensors");

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
                Result matchResult = Result.Failure;

                foreach (var propertySetter in _multiLineStringPropertySetters)
                {
                    matchResult = propertySetter.SetPropertyIfMatch(stream);

                    if (matchResult == Result.Success)
                    {
                        break;
                    }
                }

                if(matchResult == Result.Success)
                {
                    continue;
                }

                _randomSeedPropertySetter.SetPropertyIfMatch(stream);

                if(_intelParser.IsIntelElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Intel = _intelParser.ParseIntel(stream);
                    stream.StepIntoOuterContext();
                }

                if (_groupsParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Groups = _groupsParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    
                    continue;
                }

                if (_vehiclesParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Vehicles = _vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();

                    continue;
                }

                if (_markersParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Markers = _markersParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();

                    continue;
                }

                if (_sensorsParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _missionState.Sensors = _sensorsParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();

                    continue;
                }

                stream.NextLineInContext();
            }

            return _missionState;
        }
    }
}