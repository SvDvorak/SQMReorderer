using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    [TestFixture]
    public class SqmElementExportVisitorTests
    {
        private SqmElementExportVisitor _exportVisitor;

        [SetUp]
        public void Setup()
        {
            _exportVisitor = new SqmElementExportVisitor();
        }

        [Test]
        public void Expect_empty_string_on_empty_file()
        {
            var parseResult = new ParseResult();

            var exportedParseResult = _exportVisitor.Visit("file", parseResult);

            Assert.AreEqual("", exportedParseResult);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_simple_file()
        {
            var parseResult = new ParseResult();

            parseResult.Version = 11;
            parseResult.Mission = new MissionState();
            parseResult.Intro = new MissionState();
            parseResult.OutroWin = new MissionState();
            parseResult.OutroLose = new MissionState();

            var originalParseResultText = new StringBuilder();

            originalParseResultText.Append("version=11;\n");
            originalParseResultText.Append("class Mission\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");
            originalParseResultText.Append("class Intro\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");
            originalParseResultText.Append("class OutroWin\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");
            originalParseResultText.Append("class OutroLoose\n");
            originalParseResultText.Append("{\n");
            originalParseResultText.Append("};\n");

            var exportedParseResult = _exportVisitor.Visit("file", parseResult);

            Assert.AreEqual(originalParseResultText, exportedParseResult);
        }

        [Test]
        public void Expect_exporter_to_successfully_export_complete_simple_item()
        {
            var originalItemText = new StringBuilder();

            originalItemText.Append("class Item3\n");
            originalItemText.Append("{\n");
            originalItemText.Append("azimut=3.14;\n");
            originalItemText.Append("position[]={10,12,14};\n");
            originalItemText.Append("id=4;\n");
            originalItemText.Append("side=\"GUER\";\n");
            originalItemText.Append("vehicle=\"TK_GUE_Soldier_2_EP1\";\n");
            originalItemText.Append("player=\"PLAY CDG\";\n");
            originalItemText.Append("leader=1;\n");
            originalItemText.Append("rank=\"CORPORAL\";\n");
            originalItemText.Append("skill=0.60000002;\n");
            originalItemText.Append("text=\"UnitGUE_MTR1_AG\";\n");
            originalItemText.Append("init=\"GrpGUE_MTR1 = group this; nul = [\"mtrag\",this] execVM \"f\\common\\folk_assignGear.sqf\";\";\n");
            originalItemText.Append("description=\"TK Local Mortar Team 1 Assistant Gunner\";\n");
            originalItemText.Append("synchronizations[]={1,2,3};\n");
            originalItemText.Append("name=\"mkrInsertion\";\n");
            originalItemText.Append("markerType=\"RECTANGLE\";\n");
            originalItemText.Append("type=\"EMPTY\";\n");
            originalItemText.Append("fillName=\"FDiagonal\";\n");
            originalItemText.Append("a=45;\n");
            originalItemText.Append("b=55;\n");
            originalItemText.Append("drawBorder=1;\n");
            originalItemText.Append("angle=2.42;\n");
            originalItemText.Append("activationBy=\"ANY\";\n");
            originalItemText.Append("interruptable=1;\n");
            originalItemText.Append("age=\"UNKNOWN\";\n");
            originalItemText.Append("expCond=\"checkpoint3NrOfClearedDT == 7\";\n");
            originalItemText.Append("expActiv=\"end = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";\";\n");
            originalItemText.Append("class Effects\n");
            originalItemText.Append("{\n");
            originalItemText.Append("\"blur\"\n");
            originalItemText.Append("};\n");
            originalItemText.Append("};\n");

            var item = new Item();
            item.Number = 3;
            item.Azimut = 3.14;
            item.Position = new Vector(10, 12, 14);
            item.Id = 4;
            item.Side = "GUER";
            item.Vehicle = "TK_GUE_Soldier_2_EP1";
            item.Player = "PLAY CDG";
            item.Leader = 1;
            item.Rank = "CORPORAL";
            item.Lock = "UNLOCKED";
            item.Skill = 0.60000002;
            item.Text = "UnitGUE_MTR1_AG";
            item.Init = "GrpGUE_MTR1 = group this; nul = [\"mtrag\",this] execVM \"f\\common\\folk_assignGear.sqf\";";
            item.Description = "TK Local Mortar Team 1 Assistant Gunner";
            item.Synchronizations = new List<int> { 1, 2, 3 };

            item.Name = "mkrInsertion";
            item.MarkerType = "RECTANGLE";
            item.Type = "EMPTY";
            item.FillName = "FDiagonal";
            item.A = 45;
            item.B = 55;
            item.DrawBorder = 1;
            item.Angle = 2.42;

            item.ActivationBy = "ANY";
            item.Interruptable = 1;
            item.Age = "UNKNOWN";
            item.ExpCond = "checkpoint3NrOfClearedDT == 7";
            item.ExpActiv = "end = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";";

            item.Effects = new List<string>() { "blur" };

            var exportedItem = _exportVisitor.Visit("Item" + item.Number, item);

            Assert.AreEqual(originalItemText.ToString(), exportedItem);
        }
        
        [Test]
        public void Expect_exporter_to_successfully_export_complex_item()
        {
            var originalItemsText = new StringBuilder();
            originalItemsText.Append("class Item3\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("id=4;\n");
            originalItemsText.Append("class Vehicles\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("items=2;\n");
            originalItemsText.Append("class Item4\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("id=5;\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("class Item5\n");
            originalItemsText.Append("{\n");
            originalItemsText.Append("id=6;\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");
            originalItemsText.Append("};\n");

            var exportVisitor = new SqmElementExportVisitor();

            var item1 = new Item();
            item1.Number = 3;
            item1.Id = 4;

            var item1_1 = new Item();
            item1_1.Number = 4;
            item1_1.Id = 5;
            var item1_2 = new Item();
            item1_2.Number = 5;
            item1_2.Id = 6;

            item1.Items.Add(item1_1);
            item1.Items.Add(item1_2);

            var exportedItem = exportVisitor.Visit("Item" + item1.Number, item1);

            var originalItemsString = originalItemsText.ToString();

            Assert.AreEqual(originalItemsString, exportedItem);
        }
    }
}
