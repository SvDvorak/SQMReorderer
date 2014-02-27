using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.Core.Import.ArmA3.Parsers.Waypoint;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class WaypointItemParserTests
    {
        private WaypointItemParser _sut;
        private SqmContextCreator _contextCreator;

        private readonly List<string> completeWaypointText = new List<string>
            {
                "class Item0",
                "{",
                "position[]={4083.6555,25.784687,11750.772};",
                "type=\"DISMISS\";",
                "expActiv=\"op_h1;\";",
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
            Assert.AreEqual("DISMISS", waypoint.Type);
            Assert.AreEqual("op_h1;", waypoint.ExpActiv);
            Assert.AreEqual("NEVER", waypoint.ShowWp);
        }

        [Test]
        public void Effects_are_parsed_in_waypoint()
        {
            var context = _contextCreator.CreateContext(new List<string>
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