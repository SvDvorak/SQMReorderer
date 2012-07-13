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
                if(_currentLineNumber >= _inputText.Count() - 1)
                {
                    return true;
                }

                var currentLine = _inputText[_currentLineNumber];
                var isSingleEndBracket = _parsingHelperFunctions.IsLineEndBracket(currentLine) &&
                    !_parsingHelperFunctions.IsLineStartBracket(currentLine);

                return isSingleEndBracket;
            }
        }

        private bool CanStepIntoInnerContext
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

        private bool CanStepOutOfInnerContext
        {
            get
            {
                var currentLine = _inputText[_currentLineNumber];

                return _parsingHelperFunctions.IsLineEndBracket(currentLine);
            }
        }

        public SqmStream(string[] inputText)
        {
            _inputText = inputText;

            _parsingHelperFunctions = new ParsingHelperFunctions();
        }

        public void StepIntoInnerContext()
        {
            if (!CanStepIntoInnerContext)
            {
                throw new SqmParseException("Cant step into context at line " + _currentLineNumber);
            }

            _headerLineNumber = _currentLineNumber;
            _currentLineNumber += 2;
        }

        public void StepIntoOuterContext()
        {
            if(!CanStepOutOfInnerContext)
            {
                throw new SqmParseException("Cant step out of context at line " + _currentLineNumber);
            }

            _currentLineNumber += 1;
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

        private int GetNextLineNumberInContext()
        {
            var lineNumber = _currentLineNumber + 1;
            var endBracketSkipCount = 0;

            while(true)
            {
                if(lineNumber == _inputText.Count())
                {
                    // If we have passed the last line and not found a closing bracket then it must be the outer most
                    // scope which does not have a final bracket. Might cause problems when files have unmatching brackets?
                    return lineNumber - 1;
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
    }
}
