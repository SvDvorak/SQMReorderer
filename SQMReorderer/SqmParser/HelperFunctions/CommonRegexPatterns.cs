namespace SQMReorderer.SqmParser.HelperFunctions
{
    public static class CommonRegexPatterns
    {
        public static string DoublePattern
        {
            get { return @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?"; }
        }
    }
}
