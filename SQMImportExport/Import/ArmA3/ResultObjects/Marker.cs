namespace SQMImportExport.Import.ArmA3.ResultObjects
{
    public class Marker : ItemBase
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string MarkerType { get; set; }
        public string Type { get; set; }
        public string ColorName { get; set; }
        public string FillName { get; set; }
        public double? A { get; set; }
        public double? B { get; set; }
        public int? DrawBorder { get; set; }
        public double? Angle { get; set; }
    }
}