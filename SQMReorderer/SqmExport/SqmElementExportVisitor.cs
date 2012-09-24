using System.Collections.Generic;
using System.Text;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    public class SqmElementExportVisitor : ISqmElementVisitor
    {
        private SqmPropertyVisitor _propertyVisitor = new SqmPropertyVisitor();

        public string Visit(string elementName, ParseResult parseResult)
        {
            var fileString = new StringBuilder();

            fileString.Append(_propertyVisitor.Visit("version", parseResult.Version));

            fileString.Append(Visit("Mission", parseResult.Mission));
            fileString.Append(Visit("Intro", parseResult.Intro));
            fileString.Append(Visit("OutroWin", parseResult.OutroWin));
            fileString.Append(Visit("OutroLoose", parseResult.OutroLose));

            return fileString.ToString();
        }

        public string Visit(string elementName, MissionState mission)
        {
            if(mission == null)
            {
                return "";
            }

            var missionString = new StringBuilder();

            missionString.Append("class ");
            missionString.Append(elementName);
            missionString.Append("\n");
            missionString.Append("{\n");

            missionString.Append("};\n");

            return missionString.ToString();
        }

        public string Visit(string elementName, Item item)
        {
            var itemString = new StringBuilder();

            itemString.Append("class " + elementName + "\n");
            itemString.Append("{\n");
            itemString.Append(_propertyVisitor.Visit("azimut", item.Azimut));
            itemString.Append(_propertyVisitor.Visit("position", item.Position));
            itemString.Append(_propertyVisitor.Visit("id", item.Id));
            itemString.Append(_propertyVisitor.Visit("side", item.Side));
            itemString.Append(_propertyVisitor.Visit("vehicle", item.Vehicle));
            itemString.Append(_propertyVisitor.Visit("player", item.Player));
            itemString.Append(_propertyVisitor.Visit("leader", item.Leader));
            itemString.Append(_propertyVisitor.Visit("rank", item.Rank));
            itemString.Append(_propertyVisitor.Visit("skill", item.Skill));
            itemString.Append(_propertyVisitor.Visit("text", item.Text));
            itemString.Append(_propertyVisitor.Visit("init", item.Init));
            itemString.Append(_propertyVisitor.Visit("description", item.Description));
            itemString.Append(_propertyVisitor.Visit("synchronizations", item.Synchronizations));
            itemString.Append(_propertyVisitor.Visit("name", item.Name));
            itemString.Append(_propertyVisitor.Visit("markerType", item.MarkerType));
            itemString.Append(_propertyVisitor.Visit("type", item.Type));
            itemString.Append(_propertyVisitor.Visit("fillName", item.FillName));
            itemString.Append(_propertyVisitor.Visit("a", item.A));
            itemString.Append(_propertyVisitor.Visit("b", item.B));
            itemString.Append(_propertyVisitor.Visit("drawBorder", item.DrawBorder));
            itemString.Append(_propertyVisitor.Visit("angle", item.Angle));
            itemString.Append(_propertyVisitor.Visit("activationBy", item.ActivationBy));
            itemString.Append(_propertyVisitor.Visit("interruptable", item.Interruptable));
            itemString.Append(_propertyVisitor.Visit("age", item.Age));
            itemString.Append(_propertyVisitor.Visit("expCond", item.ExpCond));
            itemString.Append(_propertyVisitor.Visit("expActiv", item.ExpActiv));
            itemString.Append(_propertyVisitor.Visit("Effects", item.Effects));

            if(item.Items.Count > 0)
            {
                itemString.Append("class Vehicles\n");
                itemString.Append("{\n");
                itemString.Append(_propertyVisitor.Visit("items", item.Items.Count));

                foreach (var subItem in item.Items)
                {
                    itemString.Append(Visit("Item" + subItem.Number, subItem));
                }

                itemString.Append("};\n");
            }

            itemString.Append("};\n");


            return itemString.ToString();
        }
    }
}
