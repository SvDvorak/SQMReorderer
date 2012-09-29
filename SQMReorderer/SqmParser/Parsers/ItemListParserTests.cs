using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class ItemListParserTests
    {
        private ItemListParser<Vehicle> _itemListParser;

        [SetUp]
        public void Setup()
        {
            _itemListParser = new ItemListParser<Vehicle>(new VehicleParser(), "Vehicles");
        }

        [Test]
        public void Expect_is_list_element_to_be_true_on_correct_element_syntax()
        {
            var stream = new SqmStream(new List<string> { "class Vehicles", "{", "};" });

            var isVehiclesElement = _itemListParser.IsListElement(stream);

            Assert.IsTrue(isVehiclesElement);
        }

        [Test]
        public void Expect_is_list_element_to_be_false_on_incorrect_element_syntax()
        {
            var stream = new SqmStream(new List<string> { "class Item0", "{", "};" });

            var isVehiclesElement = _itemListParser.IsListElement(stream);

            Assert.IsFalse(isVehiclesElement);
        }

        [Test]
        public void Expect_parse_exception_given_empty_list()
        {
            var inputText = new List<string>
                {
                    "class Vehicles",
                    "{",
                    "items=0",
                    "};"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            Assert.Throws<SqmParseException>(() => _itemListParser.ParseElementItems(stream));
        }

        [Test]
        public void Expect_parse_exception_given_incorrect_item_count()
        {
            var inputText = new List<string>
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

            Assert.Throws<SqmParseException>(() => _itemListParser.ParseElementItems(stream));
        }

        [Test]
        public void Expect_parser_to_return_one_item_with_correct_data_given_one_list_item()
        {
            var inputText = new List<string>
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

            var items = _itemListParser.ParseElementItems(stream);

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("TestString", items[0].Text);
        }

        [Test]
        public void Expect_parser_to_return_three_items_given_three_list_items()
        {
            var inputText = new List<string>
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

            var items = _itemListParser.ParseElementItems(stream);

            Assert.AreEqual(3, items.Count);
        }
    }
}
