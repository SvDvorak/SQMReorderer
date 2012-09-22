using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    public class SqmElementExportVisitor : ISqmElementVisitor
    {
        public List<string> Visit(Item item)
        {
            var itemRows = new List<string>()
                {
                    @"class Item" + item.Number + "\n",
                    @"{\n",
                    @"azimut=" + item.Azimut + ";\n",
                    @"position[]={" + item.Position.X + "," + item.Position.Y + "," + item.Position.Z + "};\n",
                    @"id=" + item.Id + ";\n",
                    @"side=""" + item.Side + @""";\n",
                    @"vehicle=""" + item.Vehicle + @""";\n",
                    @"player=""" + item.Player + @""";\n",
                    @"leader=" + item.Leader + ";\n",
                    @"rank=""" + item.Rank + @""";\n",
                    @"skill=" + item.Skill + ";\n",
                    @"text=""" + item.Text + @""";\n",
                    @"init=""" + item.Init + @""";\n",
                    @"description=""" + item.Description + @""";\n",
                    @"synchronizations[]={1,2,3};\n", derp derp
                    @"name=""mkrInsertion"";\n",
                    @"markerType=""RECTANGLE"";\n",
                    @"type=""EMPTY"";\n",
                    @"fillName=""FDiagonal"";\n",
                    @"a=45;\n",
                    @"b=55;\n",
                    @"drawBorder=1;\n",
                    @"angle=2.42;\n",
                    @"activationBy=""ANY"";\n",
                    @"interruptable=1;\n",
                    @"age=""UNKNOWN"";\n",
                    @"expCond=""checkpoint3NrOfClearedDT == 7"";\n",
                    @"expActiv=""end = [1] execVM ""f\server\f_mpEndBroadcast.sqf"";"";\n",
                    @"};"
                };
        }
    }
}
