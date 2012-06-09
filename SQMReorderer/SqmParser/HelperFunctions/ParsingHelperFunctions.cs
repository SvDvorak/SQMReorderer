using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser
{
    public class ParsingHelperFunctions
    {
        private readonly Regex startBracketRegex = new Regex(@"\s*\{\s*", RegexOptions.Compiled);
        private readonly Regex endBracketRegex = new Regex(@"\s*\}\s*", RegexOptions.Compiled);

        public BracketPositionResult GetNextBracketsPositions(string[] inputText, int currentPosition)
        {
            var result = new BracketPositionResult();
            result.Success = false;

            var foundStartBracket = false;

            for (int position = currentPosition; position < inputText.Count(); position++)
            {
                var currentLine = inputText[position];

                var startBracketMatch = startBracketRegex.Match(currentLine);

                if(startBracketMatch.Success)
                {
                    result.StartBracketPosition = position;

                    foundStartBracket = true;
                }

                var endBracketMatch = endBracketRegex.Match(currentLine);

                if (endBracketMatch.Success)
                {
                    if(foundStartBracket)
                    {
                        result.EndBracketPosition = position;

                        result.Success = true;

                        return result;
                    }
                }
            }

            return result;
        }
    }
}
