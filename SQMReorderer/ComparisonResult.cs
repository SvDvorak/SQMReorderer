namespace SQMReorderer
{
    public class ComparisonResult
    {
        public bool IsSame { get; set; }
        public int ErrorRowNumber { get; set; }
        public string ErrorRowInList1 { get; set; }
        public string ErrorRowInList2 { get; set; }
    }
}