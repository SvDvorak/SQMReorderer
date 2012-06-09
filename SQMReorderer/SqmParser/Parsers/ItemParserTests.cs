using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SQMReorderer.SqmParser.Parsers
{
    [TestFixture]
    public class ItemParserTests
    {
        private ItemParser _parser;

        private string[] completeSimpleItemText = new[]
            {
                "class Item5",
                "{",
                @"position[]={5533.8467,143.18413,6350.1045};",
                @"azimut=17.206261;",
                @"id=4;",
                @"side=""WEST"";",
                @"vehicle=""US_Soldier_TL_EP1"";",
                @"player=""PLAY CDG"";",
                @"leader=1;",
                @"rank=""CORPORAL"";",
                @"skill=0.60000002;",
                @"text=""UnitUS_Alpha_FTL"";",
                @"init=""GrpUS_Alpha = group this; nul = [""ftl"",this] execVM ""f\common\folk_assignGear.sqf"";"";",
                @"description=""US Army Alpha Fireteam Leader"";",
                @"synchronizations[]={116,117};",
                "};"
            };

        [SetUp]
        public void Setup()
        {
            _parser = new ItemParser();
        }

        [Test]
        public void Expect_is_item_to_return_true_on_correct_item_element_syntax()
        {
            var isItemElement = _parser.IsItemElement("class Item0");

            Assert.IsTrue(isItemElement);
        }

        [Test]
        public void Expect_is_item_to_return_false_on_incorrect_item_element_syntax()
        {
            var isItemElement = _parser.IsItemElement("class Markers");

            Assert.IsFalse(isItemElement);
        }

        [Test]
        public void Expect_parser_to_parse_all_properties()
        {
            var itemResult = _parser.ParseItemElement(completeSimpleItemText);

            Assert.AreEqual(5, itemResult.Number);
            Assert.AreEqual("WEST", itemResult.Side);
            Assert.AreEqual("US_Soldier_TL_EP1", itemResult.Vehicle);
            Assert.AreEqual("CORPORAL", itemResult.Rank);
            Assert.AreEqual("UnitUS_Alpha_FTL", itemResult.Text);
            Assert.AreEqual("US Army Alpha Fireteam Leader", itemResult.Description);
            // TODO: Add rest of existing properties
        }

        // TODO: Reimplement
        [Test]
        [Ignore]
        public void Expect_exception_if_property_not_found()
        {
            var inputText = new[]
                              {
                                  "class Item0",
                                  "{",
                                  "derpderp=\"herpderp\"",
                                  "};"
                              };

            Assert.Throws<SqmParseException>(() => _parser.ParseItemElement(inputText));
        }

        [Test]
        public void Expect_parser_to_parse_sub_items()
        {
            var inputText = new[]
                              {
                                  "class Item0",
                                  "{",
                                  "side=\"WEST\";",
                                  "class Vehicles",
                                  "{",
                                  "items=4;",
                                  "class Item0",
                                  "{",
                                  "text=\"SomeText\";",
                                  "};",
                                  "};",
                                  "};"
                              };

            var itemResult = _parser.ParseItemElement(inputText);

            Assert.AreEqual("SomeText", itemResult.Items[0].Text);
        }
    }
}
