using System.Collections.Generic;
using NUnit.Framework;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    [TestFixture]
    public class SqmElementExportVisitorTests
    {
        [Test]
        public void Expect_exporter_to_successfully_export_complete_simple_item()
        {
            var originalItemStrings = new List<string>
                {
                    "class Item3\n",
				    "{\n",
                    "azimut=3.14;\n",
                    "position[]={10,12,14};\n",
					"id=4;\n",
					"side=\"GUER\";\n",
					"vehicle=\"TK_GUE_Soldier_2_EP1\";\n",
                    "player=\"PLAY CDG\";\n",
					"leader=1;\n",
					"rank=\"CORPORAL\";\n",
                    "skill=0.60000002;\n",
                    "text=\"UnitGUE_MTR1_AG\";\n",
					"init=\"GrpGUE_MTR1 = group this; nul = [\"mtrag\",this] execVM \"f\\common\\folk_assignGear.sqf\";\";\n",
                    "description=\"TK Local Mortar Team 1 Assistant Gunner\";\n",
                    "synchronizations[]={1,2,3};\n",
                    "name=\"mkrInsertion\";\n",
                    "markerType=\"RECTANGLE\";\n",
                    "type=\"EMPTY\";\n",
                    "fillName=\"FDiagonal\";\n",
                    "a=45;\n",
                    "b=55;\n",
                    "drawBorder=1;\n",
                    "angle=2.42;\n",
                    "activationBy=\"ANY\";\n",
                    "interruptable=1;\n",
                    "age=\"UNKNOWN\";\n",
                    "expCond=\"checkpoint3NrOfClearedDT == 7\";\n",
                    "expActiv=\"end = [1] execVM \"f\\server\\f_mpEndBroadcast.sqf\";\";\n",
                    "};"
                };

            var exportVisitor = new SqmElementExportVisitor();

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
            item.Synchronizations = new List<int> {1, 2, 3};

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

            var exportedItemStrings = exportVisitor.Visit(item);

            var stringListComparer = new StringListComparer();

            var comparisonResult = stringListComparer.Compare(originalItemStrings, exportedItemStrings);

            if(comparisonResult.IsSame != true)
            {
                Assert.Fail("Imported and exported strings are not the same. Error on row " + comparisonResult.ErrorRowNumber + ": \"" +
                    comparisonResult.ErrorRowInList1 + "\" vs \"" + comparisonResult.ErrorRowInList2 + "\"");
            }
        }
    }
}
