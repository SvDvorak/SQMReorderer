﻿namespace SQMImportExport.Import.HelperFunctions
{
    public static class CommonRegexPatterns
    {
        public static string DoublePattern
        {
            get { return @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?"; }
        }

        public static string IntegerPattern
        {
            get { return @"-?\d+"; }
        }

        public static string NonSpacedTextPattern
        {
            get { return @"[\d\w_]+"; }
        }
    }
}
