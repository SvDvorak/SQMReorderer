using SQMReorderer.Core.Import.ArmA2.DataSetters;

namespace SQMReorderer.Core.Import.ArmA2.Parsers.Marker
{
    public class MarkerItemParser : ItemParserBase<ResultObjects.Marker>
    {
        public MarkerItemParser()
        {
            PropertySetters.Add(new VectorPropertySetter("position", x => ParseResult.Position = x));
            PropertySetters.Add(new StringPropertySetter("name", x => ParseResult.Name = x));
            PropertySetters.Add(new StringPropertySetter("text", x => ParseResult.Text = x));
            PropertySetters.Add(new StringPropertySetter("markerType", x => ParseResult.MarkerType = x));
            PropertySetters.Add(new StringPropertySetter("type", x => ParseResult.Type = x));
            PropertySetters.Add(new StringPropertySetter("fillName", x => ParseResult.FillName = x));
            PropertySetters.Add(new IntegerPropertySetter("a", x => ParseResult.A = x));
            PropertySetters.Add(new IntegerPropertySetter("b", x => ParseResult.B = x));
            PropertySetters.Add(new IntegerPropertySetter("drawBorder", x => ParseResult.DrawBorder = x));
            PropertySetters.Add(new DoublePropertySetter("angle", x => ParseResult.Angle = x));
        }
    }
}
