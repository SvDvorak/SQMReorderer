﻿using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA2.Parsers.Waypoint;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Tests.Import.ArmA2
{
    [TestFixture]
    public class WaypointItemParserTests
    {
        private WaypointItemParser _sut;
        private SqmContextCreator _contextCreator;

        private readonly List<string> completeWaypointText = new List<string>()
            {
                "class Item0",
                "{",
                "position[]={4083.6555,25.784687,11750.772};",
                "idStatic=70594;",
                "idObject=-166;",
                "placement=100;",
                "completitionRadius=150;",
                "type=\"DISMISS\";",
                "combatMode=\"RED\";",
                "formation=\"FILE\";",
                "speed=\"LIMITED\";",
                "combat=\"SAFE\";",
                "expActiv=\"op_h1;\";",
                "synchronizations[]={3,2,1};",
                "showWP=\"NEVER\";",
                "};"
            };

        [SetUp]
        public void Setup()
        {
            _sut = new WaypointItemParser();
            _contextCreator = new SqmContextCreator();
        }

        [Test]
        public void Context_is_correct_when_passed_waypoint_item()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Item0", "{\n", "};\n" });

            var isItemElement = _sut.IsCorrectContext(context);

            Assert.IsTrue(isItemElement);
        }

        [Test]
        public void Context_is_incorrect_when_passed_something_other_than_waypoint()
        {
            var context = _contextCreator.CreateContext(new List<string> { "class Sensors", "{\n", "};\n" });

            var isItemElement = _sut.IsCorrectContext(context);

            Assert.IsFalse(isItemElement);
        }

        [Test]
        public void All_waypoint_properties_are_parsed_from_waypoint()
        {
            var context = _contextCreator.CreateContext(completeWaypointText);

            var waypoint = _sut.ParseContext(context);

            Assert.AreEqual(4083.6555, waypoint.Position.X);
            Assert.AreEqual(25.784687, waypoint.Position.Y);
            Assert.AreEqual(11750.772, waypoint.Position.Z);
            Assert.AreEqual(70594, waypoint.IdStatic);
            Assert.AreEqual(-166, waypoint.IdObject);
            Assert.AreEqual(100, waypoint.Placement);
            Assert.AreEqual(150, waypoint.CompletitionRadius);
            Assert.AreEqual("DISMISS", waypoint.Type);
            Assert.AreEqual("RED", waypoint.CombatMode);
            Assert.AreEqual("FILE", waypoint.Formation);
            Assert.AreEqual("LIMITED", waypoint.Speed);
            Assert.AreEqual("SAFE", waypoint.Combat);
            Assert.AreEqual("op_h1;", waypoint.ExpActiv);
            Assert.AreEqual(3, waypoint.Synchronizations[0]);
            Assert.AreEqual(2, waypoint.Synchronizations[1]);
            Assert.AreEqual(1, waypoint.Synchronizations[2]);
            Assert.AreEqual("NEVER", waypoint.ShowWp);
        }

        [Test]
        public void Effects_are_parsed_in_waypoint()
        {
            var context = _contextCreator.CreateContext(new List<string>()
                {
                    "class Item0",
                    "{\n",
                    "class Effects\n",
                    "{\n",
                    "};\n",
                    "};\n"
                });

            var waypoint = _sut.ParseContext(context);

            Assert.IsEmpty(waypoint.Effects);
        }
    }
}
