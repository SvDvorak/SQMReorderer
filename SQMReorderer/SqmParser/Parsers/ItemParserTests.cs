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

        private readonly List<string> completeSimpleGroupItemText = new List<string>
            {
                @"class Item5\n",
                @"{\n",
                @"position[]={5533.8467,143.18413,6350.1045};\n",
                @"azimut=17.206261;\n",
                @"id=4;\n",
                @"side=""WEST"";\n",
                @"vehicle=""US_Soldier_TL_EP1"";\n",
                @"player=""PLAY CDG"";\n",
                @"leader=1;\n",
                @"rank=""CORPORAL"";\n",
                @"lock=""UNLOCKED"";\n",
                @"skill=0.60000002;\n",
                @"text=""UnitUS_Alpha_FTL"";\n",
                @"init=""GrpUS_Alpha = group this; nul = [""ftl"",this] execVM ""f\common\folk_assignGear.sqf"";"";\n",
                @"description=""US Army Alpha Fireteam Leader"";\n",
                @"synchronizations[]={116,117};\n",
                @"};"
            };

        private readonly List<string> completeSimpleMarkerItemText = new List<string>
            {
                @"class Item0\n",
                @"{\n",
                @"position[]={414,16,412};\n",
                @"name=""TargetAreaCenter"";\n",
                @"markerType=""ELLIPSE"";\n",
                @"type=""Empty"";\n",
                @"text=""Destroy equipment"";",
                @"fillName=""Border"";\n",
                @"a=40;\n",
                @"b=30;\n",
                @"drawBorder=1;\n",
                @"};\n"
            };

        private readonly List<string> completeSimpleSensorItemText = new List<string>
            {
                @"class Item0\n",
                @"{\n",
                @"position[]={414,16,413};\n",
                @"a=40;\n",
                @"b=30;\n",
                @"activationBy=""ANY"";\n",
                @"interruptable=1;\n",
                @"type=""SWITCH"";\n",
                @"age=""UNKNOWN"";\n",
                @"expCond=""!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)"";\n",
                @"expActiv=""myEnd = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";"";\n",
                @"class Effects\n",
                @"{\n",
                @"filmgrain,\n",
                @"motionblur,\n",
                @"brown\n",
                @"};\n",
                @"};"
            };

        private readonly List<string> completeComplexGroupItemText = new List<string>
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

        private SqmStream _completeSimpleGroupItemStream;
        private SqmStream _completeSimpleMarkerItemStream;
        private SqmStream _completeSimpleSensorItemStream;
        private SqmStream _completeComplexGroupItemStream;


        [SetUp]
        public void Setup()
        {
            _parser = new ItemParser();

            _completeSimpleGroupItemStream = new SqmStream(completeSimpleGroupItemText);
            _completeSimpleMarkerItemStream = new SqmStream(completeSimpleMarkerItemText);
            _completeSimpleSensorItemStream = new SqmStream(completeSimpleSensorItemText);
            _completeComplexGroupItemStream = new SqmStream(completeComplexGroupItemText);
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
        public void Expect_parser_to_parse_all_group_item_properties()
        {
            _completeSimpleGroupItemStream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(_completeSimpleGroupItemStream);

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
            Assert.AreEqual("UNLOCKED", itemResult.Lock);
            Assert.AreEqual(0.60000002, itemResult.Skill);
            Assert.AreEqual("UnitUS_Alpha_FTL", itemResult.Text);
            Assert.AreEqual("US Army Alpha Fireteam Leader", itemResult.Description);
            Assert.AreEqual(@"GrpUS_Alpha = group this; nul = [""ftl"",this] execVM ""f\common\folk_assignGear.sqf"";", itemResult.Init);
            Assert.AreEqual(116, itemResult.Synchronizations[0]);
            Assert.AreEqual(117, itemResult.Synchronizations[1]);
        }

        [Test]
        public void Expect_parser_to_parse_all_marker_item_properties()
        {
            _completeSimpleMarkerItemStream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(_completeSimpleMarkerItemStream);

            Assert.AreEqual(414, itemResult.Position.X);
            Assert.AreEqual(16, itemResult.Position.Y);
            Assert.AreEqual(412, itemResult.Position.Z);
            Assert.AreEqual(0, itemResult.Number);
            Assert.AreEqual("TargetAreaCenter", itemResult.Name);
            Assert.AreEqual("ELLIPSE", itemResult.MarkerType);
            Assert.AreEqual("Empty", itemResult.Type);
            Assert.AreEqual("Destroy equipment", itemResult.Text);
            Assert.AreEqual("Border", itemResult.FillName);
            Assert.AreEqual(40, itemResult.A);
            Assert.AreEqual(30, itemResult.B);
            Assert.AreEqual(1, itemResult.DrawBorder);
        }

        [Test]
        public void Expect_parser_to_parse_all_sensor_item_properties()
        {
            _completeSimpleSensorItemStream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(_completeSimpleSensorItemStream);

            Assert.AreEqual(414, itemResult.Position.X);
            Assert.AreEqual(16, itemResult.Position.Y);
            Assert.AreEqual(413, itemResult.Position.Z);
            Assert.AreEqual(0, itemResult.Number);
            Assert.AreEqual(40, itemResult.A);
            Assert.AreEqual(30, itemResult.B);
            Assert.AreEqual("ANY", itemResult.ActivationBy);
            Assert.AreEqual(1, itemResult.Interruptable);
            Assert.AreEqual("SWITCH", itemResult.Type);
            Assert.AreEqual("UNKNOWN", itemResult.Age);
            Assert.AreEqual(@"!alive SupplyTruck && ((getDammage AmmoBox1) > 0.5) && ((getDammage AmmoBox2) > 0.5)", itemResult.ExpCond);
            Assert.AreEqual(@"myEnd = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";", itemResult.ExpActiv);
            //Assert.AreEqual(3, itemResult.Effects);
            //Assert.AreEqual("filmgrain", itemResult.Effects[0]);
            //Assert.AreEqual("motionblur", itemResult.Effects[1]);
            //Assert.AreEqual("brown", itemResult.Effects[2]);
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
            _completeComplexGroupItemStream.StepIntoInnerContext();

            var itemResult = _parser.ParseItemElement(_completeComplexGroupItemStream);

            Assert.AreEqual(4, itemResult.Items.Count);
            Assert.AreEqual("UnitUS_Bravo_FTL", itemResult.Items[0].Text);
            Assert.AreEqual("UnitUS_Bravo_Eng", itemResult.Items[3].Text);
        }
    }
}
