using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class MissionParser
    {
        private readonly ItemListParser _groupsParser = new ItemListParser("Groups");
        private readonly ItemListParser _vehiclesParser = new ItemListParser("Vehicles");
        private readonly IntelParser _intelParser = new IntelParser();

        private readonly List<MultiLineStringListPropertySetter> _multiLineStringPropertySetters = new List<MultiLineStringListPropertySetter>();

        private readonly IntegerPropertySetter _randomSeedPropertySetter;

        private Mission _mission;

        public MissionParser()
        {
            _multiLineStringPropertySetters.Add(new MultiLineStringListPropertySetter("addOns", x => _mission.AddOns = x));
            _multiLineStringPropertySetters.Add(new MultiLineStringListPropertySetter("addOnsAuto", x => _mission.AddOnsAuto = x));

            _randomSeedPropertySetter = new IntegerPropertySetter("randomSeed", x => _mission.RandomSeed = x);
        }

        public Mission ParseMission(SqmStream stream)
        {
            _mission = new Mission();

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
                    _mission.Intel = _intelParser.ParseIntel(stream);
                    stream.StepIntoOuterContext();
                }

                if (_groupsParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _mission.Groups = _groupsParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    
                    continue;
                }

                if (_vehiclesParser.IsListElement(stream))
                {
                    stream.StepIntoInnerContext();
                    _mission.Vehicles = _vehiclesParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();

                    continue;
                }

                stream.NextLineInContext();
            }

            return _mission;
        }
    }
}