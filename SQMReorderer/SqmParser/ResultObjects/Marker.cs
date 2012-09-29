namespace SQMReorderer.SqmParser.ResultObjects
{
    public class Marker : ItemBase
    {
        public string Text { get; set; }
        public Vector Position { get; set; }

        public string Name { get; set; }
        public string MarkerType { get; set; }
        public string Type { get; set; }
        public string FillName { get; set; }
        public int? A { get; set; }
        public int? B { get; set; }
        public int? DrawBorder { get; set; }
        public double? Angle { get; set; }
    }
}