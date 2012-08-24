﻿using System;
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

        private readonly List<string> completeSimpleItemText = new List<string>
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

        private readonly List<string> completeComplexItemText = new List<string>
            {
                @"class Item4",
                @"{",
                @"side=""WEST"";",
			    @"class Vehicles",
			    @"{",
				@"items=4;",
				@"class Item0",
				@"{",
				@"text=""UnitUS_Bravo_FTL"";",
				@"};",
				@"class Item1",
				@"{",
				@"text=""UnitUS_Bravo_AR"";",
				@"};",
				@"class Item2",
				@"{",
				@"text=""UnitUS_Bravo_AAR"";",
				@"};",
				@"class Item3",
				@"{",
				@"text=""UnitUS_Bravo_Eng"";",
				@"};",
			    @"};"
            };

        private SqmStream completeSimpleItemStream;
        private SqmStream completeComplexItemStream;


        [SetUp]
        public void Setup()
        {
            _parser = new ItemParser();

            completeSimpleItemStream = new SqmStream(completeSimpleItemText);
            completeComplexItemStream = new SqmStream(completeComplexItemText);
        }

        [Test]
        public void Expect_is_item_to_return_true_on_correct_item_element_syntax()
        {
            var stream = new SqmStream(new List<string> { "class Item0" });

            var isItemElement = _parser.IsItemElement(stream);

            Assert.IsTrue(isItemElement);
        }

        [Test]
        public void Expect_is_item_to_return_false_on_incorrect_item_element_syntax()
        {
            var stream = new SqmStream(new List<string> { "class Markers" });

            var isItemElement = _parser.IsItemElement(stream);

            Assert.IsFalse(isItemElement);
        }

        [Test]
        public void Expect_parser_to_parse_all_properties()
        {
            completeSimpleItemStream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(completeSimpleItemStream);

            Assert.AreEqual(5533.8467, itemResult.Position.X);
            Assert.AreEqual(143.18413, itemResult.Position.Y);
            Assert.AreEqual(6350.1045, itemResult.Position.Z);
            Assert.AreEqual(17.206261, itemResult.Azimut);
            Assert.AreEqual(5, itemResult.Number);
            Assert.AreEqual(4, itemResult.Id);
            Assert.AreEqual("WEST", itemResult.Side);
            Assert.AreEqual("US_Soldier_TL_EP1", itemResult.Vehicle);
            Assert.AreEqual("PLAY CDG", itemResult.Player);
            Assert.AreEqual(1, itemResult.Leader);
            Assert.AreEqual("CORPORAL", itemResult.Rank);
            Assert.AreEqual(0.60000002, itemResult.Skill);
            Assert.AreEqual("UnitUS_Alpha_FTL", itemResult.Text);
            Assert.AreEqual("US Army Alpha Fireteam Leader", itemResult.Description);
            Assert.AreEqual(@"GrpUS_Alpha = group this; nul = [""ftl"",this] execVM ""f\common\folk_assignGear.sqf"";", itemResult.Init);
            Assert.AreEqual(116, itemResult.Synchronizations[0]);
            Assert.AreEqual(117, itemResult.Synchronizations[1]);
        }

        [Test]
        public void Expect_exception_if_property_not_found()
        {
            var inputText = new List<string>
                {
                    "class Item0",
                    "{",
                    "derpderp=\"herpderp\"",
                    "};"
                };

            var stream = new SqmStream(inputText);

            Assert.Throws<SqmParseException>(() => _parser.ParseItemElement(stream));
        }

        [Test]
        public void Expect_parser_to_parse_sub_items()
        {
            var inputText = new List<string>
                {
                    "class Item0",
                    "{",
                    "side=\"WEST\";",
                    "class Vehicles",
                    "{",
                    "items=1;",
                    "class Item0",
                    "{",
                    "text=\"SomeText\";",
                    "};",
                    "};",
                    "};"
                };

            var stream = new SqmStream(inputText);

            stream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(stream);

            Assert.AreEqual("SomeText", itemResult.Items[0].Text);
        }

        // TODO Remove?
        [Test]
        public void Expect_parser_to_parse_complex_item_with_sub_items()
        {
            completeComplexItemStream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(completeComplexItemStream);

            Assert.AreEqual(4, itemResult.Items.Count);
            Assert.AreEqual("UnitUS_Bravo_FTL", itemResult.Items[0].Text);
            Assert.AreEqual("UnitUS_Bravo_Eng", itemResult.Items[3].Text);
        }
    }
}
