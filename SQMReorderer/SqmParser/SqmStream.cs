using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser
{
    public class SqmStream
    {
        private readonly string[] _inputText;
        private ParsingHelperFunctions _parsingHelperFunctions;

        public int HeaderLineNumber { get; private set; }
        public int CurrentLineNumber { get; private set; }

        public bool IsAtEndOfContext
        {
            get
            {
                if(CurrentLineNumber >= _inputText.Count() - 1)
                {
                    return true;
                }

                var nextLine = _inputText[CurrentLineNumber + 1];

                return _parsingHelperFunctions.IsLineEndBracket(nextLine);
            }
        }

        public bool IsAtStartOfContext
        {
            get
            {
                if (CurrentLineNumber == 0)
                {
                    return true;
                }

                var nextLine = _inputText[CurrentLineNumber - 1];

                return _parsingHelperFunctions.IsLineStartBracket(nextLine);
            }
        }

        public bool CanStepIntoNextContext { get; set; }

        public SqmStream(string[] inputText)
        {
            _inputText = inputText;

            _parsingHelperFunctions = new ParsingHelperFunctions();

            StepIntoInnerContext();
        }

        public void StepIntoInnerContext()
        {
            var currentLine = _inputText[CurrentLineNumber];

            while (!_parsingHelperFunctions.IsLineStartBracket(currentLine))
            {
                CurrentLineNumber++;
                currentLine = _inputText[CurrentLineNumber];
            }

            CurrentLineNumber++;
        }

        public bool IsHeaderMatch(Regex headerRegex)
        {
            var match = headerRegex.Match(_inputText[HeaderLineNumber]);

            return match.Success;
        }

        public void MatchHeader(Regex headerRegex, Action<Match> matchFoundAction)
        {
            var match = headerRegex.Match(_inputText[HeaderLineNumber]);

            if (match.Success)
            {
                matchFoundAction(match);
            }
        }

        public bool IsCurrentLineMatch(Regex currentLineRegex)
        {
            var match = currentLineRegex.Match(_inputText[CurrentLineNumber]);

            return match.Success;
        }

        public void MatchCurrentLine(Regex currentLineRegex, Action<Match> matchFoundAction)
        {
            var match = currentLineRegex.Match(_inputText[CurrentLineNumber]);

            if (match.Success)
            {
                matchFoundAction(match);
            }
        }

        public void NextLineInContext()
        {
            if(!IsAtEndOfContext)
            {
                CurrentLineNumber++;
            }
        }
    }
}
