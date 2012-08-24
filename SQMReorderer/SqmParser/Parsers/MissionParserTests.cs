using System;
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
            var inputText = new List<string>
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
            var inputText = new List<string>
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

        [Test]
        public void Expect_groups_to_be_parsed_irregardless_of_mission_content_order()
        {
            var inputText = new List<string>
                {
                    "class Mission\n",
                    "{\n",
                    "addOns[]=\n",
                    "{\n",
                    "\"zargabad\",\n",
                    "};\n",
                    "class Groups\n",
                    "{\n",
                    "items=1;\n",
                    "class Item0\n",
                    "{\n",
                    "side=\"LOGIC\";\n",
                    "};\n",
                    "};\n",
                    "randomSeed=4931020;\n",
                    "};\n"
                };

            var stream = new SqmStream(inputText);
            stream.StepIntoInnerContext();

            var missionResult = _missionParser.ParseMission(stream);

            Assert.AreEqual(1, missionResult.Groups.Count);
        }

        [Test]
        public void Expect_intel_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    @"class Mission\n",
                    @"{\n",
                    @"class Intel\n",
                    @"{\n",
                    @"};\n",
                    @"};\n"
                };

            var stream = new SqmStream(inputText);
            stream.StepIntoInnerContext();

            var missionResult = _missionParser.ParseMission(stream);

            Assert.IsNotNull(missionResult.Intel);
        }
    }
}
