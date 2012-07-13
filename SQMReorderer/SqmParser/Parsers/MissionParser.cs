using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class MissionParser
    {
        private ItemListParser _groupsParser = new ItemListParser();

        public Mission ParseMission(SqmStream stream)
        {
            var missionResult = new Mission();

            while (!stream.IsAtEndOfContext)
            {
                if (_groupsParser.IsListElement("Groups", stream))
                {
                    stream.StepIntoInnerContext();
                    missionResult.Groups = _groupsParser.ParseElementItems(stream);
                    stream.StepIntoOuterContext();
                    
                    continue;
                }

                stream.NextLineInContext();
            }

            return missionResult;
        }
    }
}