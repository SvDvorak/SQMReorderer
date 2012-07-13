using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser
{
    public class ParsingHelperFunctions
    {
        private readonly Regex startBracketRegex = new Regex(@"\{", RegexOptions.Compiled);
        private readonly Regex endBracketRegex = new Regex(@"\}", RegexOptions.Compiled);

        public bool IsLineStartBracket(string line)
        {
            var startBracketMatch = startBracketRegex.Match(line);

            return startBracketMatch.Success;
        }

        public bool IsLineEndBracket(string line)
        {
            var endBracketMatch = endBracketRegex.Match(line);

            return endBracketMatch.Success;
        }
    }
}
