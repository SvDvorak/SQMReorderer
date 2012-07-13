using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class MissionParser
    {
        public Mission ParseMission(SqmStream stream)
        {
            var missionResult = new Mission();

            var groupsParser = new ItemListParser();

            if (groupsParser.IsListElement("Groups", stream))
            {
                stream.StepIntoInnerContext();
                missionResult.Groups = groupsParser.ParseElementItems(stream);
                stream.StepIntoOuterContext();
            }

            return missionResult;
        }
    }
}