using System.Collections.Generic;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmExport
{
    public class SqmElementExportVisitor : ISqmElementVisitor
    {
        public List<string> Visit(Item item)
        {
            var propertyVisitor = new SqmPropertyVisitor();

            var itemRows = new List<string>()
                {
                    "class Item" + item.Number + "\n",
                    "{\n",
                    propertyVisitor.Visit("azimut", item.Azimut),
                    propertyVisitor.Visit("position", item.Position),
                    propertyVisitor.Visit("id", item.Id),
                    propertyVisitor.Visit("side", item.Side),
                    propertyVisitor.Visit("vehicle", item.Vehicle),
                    propertyVisitor.Visit("player", item.Player),
                    propertyVisitor.Visit("leader", item.Leader),
                    propertyVisitor.Visit("rank", item.Rank),
                    propertyVisitor.Visit("skill", item.Skill),
                    propertyVisitor.Visit("text", item.Text),
                    propertyVisitor.Visit("init", item.Init),
                    propertyVisitor.Visit("description", item.Description),
                    propertyVisitor.Visit("synchronizations", item.Synchronizations),
                    propertyVisitor.Visit("name", item.Name),
                    propertyVisitor.Visit("markerType", item.MarkerType),
                    propertyVisitor.Visit("type", item.Type),
                    propertyVisitor.Visit("fillName", item.FillName),
                    propertyVisitor.Visit("a", item.A),
                    propertyVisitor.Visit("b", item.B),
                    propertyVisitor.Visit("drawBorder", item.DrawBorder),
                    propertyVisitor.Visit("angle", item.Angle),
                    propertyVisitor.Visit("activationBy", item.ActivationBy),
                    propertyVisitor.Visit("interruptable", item.Interruptable),
                    propertyVisitor.Visit("age", item.Age),
                    propertyVisitor.Visit("expCond", item.ExpCond),
                    propertyVisitor.Visit("expActiv", item.ExpActiv),
                    "};"
                };

            return itemRows;
        }
    }
}
