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
        public void Expect_groups_to_be_parsed_in_mission()
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

        [Test]
        public void Expect_vehicles_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    @"class Mission\n",
                    @"{\n",
                    @"class Vehicles\n",
                    @"{\n",
                    @"items=3;\n",
                    @"class Item0\n",
                    @"{\n",
                    @"text=""SupplyTruck"";\n",
                    @"};\n",
                    @"class Item1\n",
                    @"{\n",
                    @"text=""AmmoBox1"";\n",
                    @"};\n",
                    @"class Item2\n",
                    @"{\n",
                    @"text=""AmmoBox2"";\n",
                    @"};\n",
                    @"};\n",
                    @"};\n"
                };

            var stream = new SqmStream(inputText);
            stream.StepIntoInnerContext();

            var missionResult = _missionParser.ParseMission(stream);

            Assert.AreEqual(3, missionResult.Vehicles.Count);

            Assert.AreEqual("SupplyTruck", missionResult.Vehicles[0].Text);
            Assert.AreEqual("AmmoBox1", missionResult.Vehicles[1].Text);
            Assert.AreEqual("AmmoBox2", missionResult.Vehicles[2].Text);
        }

        [Test]
        public void Expect_all_mission_properties_to_be_parsed()
        {
            var inputText = new List<string>
                {
                    @"class Mission\n",
                    @"{\n",
                    @"addOns[]=\n",
                    @"{\n",
                    @"""cacharacters_e"",\n",
                    @"""zargabad"",\n",
                    @"""ca_highcommand"",\n",
                    @"""cacharacters2"",\n",
                    @"""CAWheeled_E""\n",
                    @"};\n",
                    @"addOnsAuto[]=\n",
                    @"{\n",
                    @"""ca_modules_functions"",\n",
                    @"""cacharacters_e"",\n",
                    @"""CAWheeled_E"",\n",
                    @"};\n",
                    @"randomSeed=4931020);\n",
                    @"}\n"
                };

            var stream = new SqmStream(inputText);
            stream.StepIntoInnerContext();

            var missionResult = _missionParser.ParseMission(stream);

            Assert.AreEqual(5, missionResult.AddOns.Count);
            Assert.AreEqual(3, missionResult.AddOnsAuto.Count);

            Assert.AreEqual("cacharacters_e", missionResult.AddOns[0]);
            Assert.AreEqual("zargabad", missionResult.AddOns[1]);
            Assert.AreEqual("ca_highcommand", missionResult.AddOns[2]);
            Assert.AreEqual("cacharacters2", missionResult.AddOns[3]);
            Assert.AreEqual("CAWheeled_E", missionResult.AddOns[4]);
            
            Assert.AreEqual("ca_modules_functions", missionResult.AddOnsAuto[0]);
            Assert.AreEqual("cacharacters_e", missionResult.AddOnsAuto[1]);
            Assert.AreEqual("CAWheeled_E", missionResult.AddOnsAuto[2]);

            Assert.AreEqual(4931020, missionResult.RandomSeed);
        }
    }
}
