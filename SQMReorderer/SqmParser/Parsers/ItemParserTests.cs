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

        private string[] completeItemText = new[]
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
        public void Expect_is_item_to_return_true_on_correct_item_syntax()
        {
            var inputText = new[]
                {
                    "class Item0",
                    "{",
                    "}"
                };

            var isItem = _parser.IsItem(inputText[0]);

            Assert.AreEqual(true, isItem);
        }

        [Test]
        public void Expect_parser_to_parse_all_properties()
        {
            var itemResult = _parser.ParseItem(completeItemText);

            Assert.AreEqual(5, itemResult.Number);
            Assert.AreEqual("WEST", itemResult.Side);
            Assert.AreEqual("US_Soldier_TL_EP1", itemResult.Vehicle);
            Assert.AreEqual("CORPORAL", itemResult.Rank);
            Assert.AreEqual("UnitUS_Alpha_FTL", itemResult.Text);
            Assert.AreEqual("US Army Alpha Fireteam Leader", itemResult.Description);
        }
    }
}
