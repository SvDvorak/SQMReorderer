using SQMReorderer.SqmParser.PropertySetters;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer.SqmParser.Parsers
{
    public class MarkerParser : ItemParserBase<Marker>
    {
        public MarkerParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => Item.Position = x));
            PropertySetters.Add(new StringPropertySetter("name", x => Item.Name = x));
            PropertySetters.Add(new StringPropertySetter("text", x => Item.Text = x));
            PropertySetters.Add(new StringPropertySetter("markerType", x => Item.MarkerType = x));
            PropertySetters.Add(new StringPropertySetter("type", x => Item.Type = x));
            PropertySetters.Add(new StringPropertySetter("fillName", x => Item.FillName = x));
            PropertySetters.Add(new IntegerPropertySetter("a", x => Item.A = x));
            PropertySetters.Add(new IntegerPropertySetter("b", x => Item.B = x));
            PropertySetters.Add(new IntegerPropertySetter("drawBorder", x => Item.DrawBorder = x));
            PropertySetters.Add(new DoublePropertySetter("angle", x => Item.Angle = x));
        }
    }
}
