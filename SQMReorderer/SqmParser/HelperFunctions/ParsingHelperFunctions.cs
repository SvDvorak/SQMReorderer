using System;
using System.Collections.Generic;
using System.Linq;
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

        public BracketPositionResult GetNextBracketsPositions(string[] inputText, int currentRow)
        {
            var result = new BracketPositionResult();
            result.Success = false;

            var foundStartBracket = false;

            for (int row = currentRow; row < inputText.Count(); row++)
            {
                var currentLine = inputText[row];

                var startBracketMatch = startBracketRegex.Match(currentLine);

                if(startBracketMatch.Success)
                {
                    result.StartBracketPosition = row;

                    foundStartBracket = true;
                }

                var endBracketMatch = endBracketRegex.Match(currentLine);

                if (endBracketMatch.Success)
                {
                    if(foundStartBracket)
                    {
                        result.EndBracketPosition = row;

                        result.Success = true;

                        return result;
                    }
                }
            }

            return result;
        }
    }
}
