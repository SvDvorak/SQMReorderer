using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser
{
    public class SqmStream
    {
        private readonly Regex _nonEmptyLineRegex = new Regex(@"[\w\d]+");

        private readonly string[] _inputText;
        private ParsingHelperFunctions _parsingHelperFunctions;

        private int _headerLineNumber;
        private int _currentLineNumber;

        public bool IsAtEndOfContext
        {
            get
            {
                if(_currentLineNumber == _inputText.Count() - 1)
                {
                    return true;
                }

                return _parsingHelperFunctions.IsLineEndBracket(_inputText[_currentLineNumber]);
            }
        }

        private int GetNextLineNumberInContext()
        {
            var lineNumber = _currentLineNumber + 1;
            var endBracketSkipCount = 0;

            while(true)
            {
                if(lineNumber == _inputText.Count())
                {
                    throw new SqmParseException("Unexpected end of context at line " + lineNumber);
                }

                var currentLine = _inputText[lineNumber];

                if(_parsingHelperFunctions.IsLineStartBracket(currentLine))
                {
                    endBracketSkipCount++;
                }

                if(IsCurrentLineMatch(_nonEmptyLineRegex) && endBracketSkipCount == 0)
                {
                    return lineNumber;
                }

                if (_parsingHelperFunctions.IsLineEndBracket(_inputText[lineNumber]))
                {
                    if(endBracketSkipCount == 0)
                    {
                        return lineNumber - 1;
                    }
                    else
                    {
                        endBracketSkipCount--;
                    }
                }

                lineNumber++;
            }
        }

        public bool CanStepIntoInnerContext
        {
            get
            {
                if (_currentLineNumber == _inputText.Count() - 1)
                {
                    return true;
                }

                var nextLine = _inputText[_currentLineNumber + 1];

                return _parsingHelperFunctions.IsLineStartBracket(nextLine);
            }
        }

        public SqmStream(string[] inputText)
        {
            _inputText = inputText;

            _parsingHelperFunctions = new ParsingHelperFunctions();
        }

        public void StepIntoInnerContext()
        {
            if (CanStepIntoInnerContext)
            {
                _headerLineNumber = _currentLineNumber;
                _currentLineNumber += 2;
            }
        }

        public void StepOutOfInnerContext()
        {

        }

        public bool IsHeaderMatch(Regex headerRegex)
        {
            var match = headerRegex.Match(_inputText[_headerLineNumber]);

            return match.Success;
        }

        public void MatchHeader(Regex headerRegex, Action<Match> matchFoundAction)
        {
            var match = headerRegex.Match(_inputText[_headerLineNumber]);

            if (match.Success)
            {
                matchFoundAction(match);
            }
        }

        public bool IsCurrentLineMatch(Regex currentLineRegex)
        {
            var match = currentLineRegex.Match(_inputText[_currentLineNumber]);

            return match.Success;
        }

        public void MatchCurrentLine(Regex currentLineRegex, Action<Match> matchFoundAction)
        {
            var match = currentLineRegex.Match(_inputText[_currentLineNumber]);

            if (match.Success)
            {
                matchFoundAction(match);
            }
        }

        public void NextLineInContext()
        {
            if(!IsAtEndOfContext)
            {
                _currentLineNumber = GetNextLineNumberInContext();
            }
        }
    }
}
