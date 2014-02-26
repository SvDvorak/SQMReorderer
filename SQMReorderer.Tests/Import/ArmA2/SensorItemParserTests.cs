﻿using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA2.Parsers.Sensor;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Tests.Import.ArmA2
{
    [TestFixture]
    public class SensorItemParserTests
    {
        private SensorItemParser _parser;

        private readonly List<string> completeSimpleSensorItemText = new List<string>
            {
                "class Item0\n",
                "{\n",
                "position[]={414,16,413};\n",
                "a=40;\n",
                "b=30;\n",
                "angle=20.8573;\n",
                "rectangular=1;\n",
                "activationBy=\"ANY\";\n",
                "activationType=\"NOT PRESENT\";\n",
                "repeating=1\n",
                "timeoutMin=30;\n",
                "timeoutMid=31;\n",
                "timeoutMax=32;\n",
                "interruptable=1;\n",
                "type=\"SWITCH\";\n",
                "age=\"UNKNOWN\";\n",
                "name=\"END\";\n",
                "idVehicle=795;",
                "expCond=\"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)\";\n",
                "expActiv=\"myEnd = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";\";\n",
                "expDesactiv=\"a whole bunch of text\";\n",
                "synchronizations[]={0,1};",
                "class Effects\n",
                "{\n",
                "filmgrain,\n",
                "motionblur,\n",
                "brown\n",
                "};\n",
                "};"
            };

        private SqmContext _completeSimpleSensorItemContext;

        [SetUp]
        public void Setup()
        {
            _parser = new SensorItemParser();

            var contextCreator = new SqmContextCreator();

            _completeSimpleSensorItemContext = contextCreator.CreateContext(completeSimpleSensorItemText);
        }

        [Test]
        public void Expect_parser_to_parse_all_sensor_item_properties()
        {
            var sensorResult = _parser.ParseContext(_completeSimpleSensorItemContext);

            Assert.AreEqual(0, sensorResult.Number);
            Assert.AreEqual(414, sensorResult.Position.X);
            Assert.AreEqual(16, sensorResult.Position.Y);
            Assert.AreEqual(413, sensorResult.Position.Z);
            Assert.AreEqual(40, sensorResult.A);
            Assert.AreEqual(30, sensorResult.B);
            Assert.AreEqual(20.8573, sensorResult.Angle);
            Assert.AreEqual(1, sensorResult.Rectangular);
            Assert.AreEqual("ANY", sensorResult.ActivationBy);
            Assert.AreEqual("NOT PRESENT", sensorResult.ActivationType);
            Assert.AreEqual(1, sensorResult.Repeating);
            Assert.AreEqual(30, sensorResult.TimeoutMin);
            Assert.AreEqual(31, sensorResult.TimeoutMid);
            Assert.AreEqual(32, sensorResult.TimeoutMax);
            Assert.AreEqual(1, sensorResult.Interruptable);
            Assert.AreEqual("SWITCH", sensorResult.Type);
            Assert.AreEqual("UNKNOWN", sensorResult.Age);
            Assert.AreEqual("END", sensorResult.Name);
            Assert.AreEqual(795, sensorResult.IdVehicle);
            Assert.AreEqual(@"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)",
                sensorResult.ExpCond);
            Assert.AreEqual(@"myEnd = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";", sensorResult.ExpActiv);
            Assert.AreEqual(@"a whole bunch of text", sensorResult.ExpDesactiv);
            Assert.AreEqual(0, sensorResult.Synchronizations[0]);
            Assert.AreEqual(1, sensorResult.Synchronizations[1]);
            //Assert.AreEqual(3, itemResult.Effects);
            //Assert.AreEqual("filmgrain", itemResult.Effects[0]);
            //Assert.AreEqual("motionblur", itemResult.Effects[1]);
            //Assert.AreEqual("brown", itemResult.Effects[2]);
        }
    }
}