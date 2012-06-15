using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class VehiclesParserTests
    {
        private VehiclesParser _vehiclesParser;

        [SetUp]
        public void Setup()
        {
            _vehiclesParser = new VehiclesParser();
        }

        [Test]
        public void Expect_is_vehicles_to_be_true_on_correct_vehicles_element_syntax()
        {
            var stream = new SqmStream(new[] { "class Vehicles", "{", "};" });

            var isVehiclesElement = _vehiclesParser.IsVehiclesElement(stream);

            Assert.IsTrue(isVehiclesElement);
        }

        [Test]
        public void Expect_is_vehicles_to_be_false_on_incorrect_vehicles_element_syntax()
        {
            var stream = new SqmStream(new[] { "class Item0", "{", "};" });

            var isVehiclesElement = _vehiclesParser.IsVehiclesElement(stream);

            Assert.IsFalse(isVehiclesElement);
        }

        [Test]
        public void Expect_parse_exception_given_empty_list()
        {
            var inputText = new[]
                {
                    "class Vehicles",
                    "{",
                    "items=0",
                    "};"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            Assert.Throws<SqmParseException>(() => _vehiclesParser.ParseVehicleElement(stream));
        }

        [Test]
        public void Expect_parse_exception_given_incorrect_item_count()
        {
            var inputText = new[]
                {
                    "class Vehicles",
                    "{",
                    "items=0",
                    "class Item0",
                    "{",
                    "side=\"EAST\"",
                    "};",
                    "};"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            Assert.Throws<SqmParseException>(() => _vehiclesParser.ParseVehicleElement(stream));
        }

        [Test]
        public void Expect_parser_to_return_one_item_with_correct_data_given_one_list_item()
        {
            var inputText = new[]
                {
                    "class Vehicles",
                    "{",
                    "items=1",
                    "class Item0",
                    "{",
                    "text=\"TestString\"",
                    "};",
                    "};"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            var items = _vehiclesParser.ParseVehicleElement(stream);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("TestString", items[0].Text);
        }

        [Test]
        public void Expect_parser_to_return_three_items_given_three_list_items()
        {
            var inputText = new[]
                {
                    "class Vehicles",
                    "{",
                    "items=3",
                    "class Item0",
                    "{",
                    "side=\"EAST\";",
                    "};",
                    "class Item1",
                    "{",
                    "side=\"EAST\";",
                    "};",
                    "class Item2",
                    "{",
                    "side=\"EAST\";",
                    "};",
                    "};"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            var items = _vehiclesParser.ParseVehicleElement(stream);

            Assert.AreEqual(3, items.Count);
        }
    }
}
