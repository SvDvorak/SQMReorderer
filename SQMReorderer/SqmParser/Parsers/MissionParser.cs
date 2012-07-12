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
                var groups = groupsParser.ParseElementItems(stream);
                stream.StepIntoOuterContext();

                var group = new Group();
                group.Items = groups;

                missionResult.Group.Add(group);
            }

            return missionResult;
        }
    }
}