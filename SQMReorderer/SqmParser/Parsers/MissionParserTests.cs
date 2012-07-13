﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class MissionParserTests
    {
        private MissionParser _missionParser = new MissionParser();

        [Test]
        public void Expect_empty_mission_to_return_empty_result()
        {
            var inputText = new[]
                {
                    "class Mission\n",
                    "{\n",
                    "};\n"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            Mission missionResult = _missionParser.ParseMission(stream);

            Assert.AreEqual(0, missionResult.Groups.Count);
        }

        [Test]
        public void Expect_group_to_be_parsed_in_mission()
        {
            var inputText = new[]
                {
                    "class Mission\n",
                    "{\n",
                    "class Groups\n",
                    "{\n",
                    "items=1;\n",
                    "class Item0\n",
                    "{\n",
                    "side=\"LOGIC\";\n",
                    "};\n",
                    "};\n",
                    "};\n"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            var missionResult = _missionParser.ParseMission(stream);

            Assert.AreEqual(1, missionResult.Groups.Count);
        }
    }
}
