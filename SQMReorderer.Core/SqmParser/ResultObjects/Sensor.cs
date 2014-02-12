namespace SQMReorderer.Core.SqmParser.ResultObjects
{
    public class Sensor : ItemBase
    {
        public int? A { get; set; }
        public int? B { get; set; }
        public string Type { get; set; }
        public string ActivationBy { get; set; }
        public int? Interruptable { get; set; }
        public string Age { get; set; }
        public string ExpCond { get; set; }
        public string ExpActiv { get; set; }
    }
}
