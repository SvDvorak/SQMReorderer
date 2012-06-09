using System;
using System.Text.RegularExpressions;
using SQMReorderer.SqmParser.Parsers;

namespace SQMReorderer.SqmParser
{
    public class SqmParser
    {
        private readonly Regex versionRegex = new Regex(@"version=(?<version>\d+)", RegexOptions.Compiled);
        private readonly Regex classRegex = new Regex(@"class\s(?<class>\w+)", RegexOptions.Compiled);

        private ClassParser _classParser = new ClassParser();

        public ParseResult Parse(String[] inputText)
        {
            var parseResult = new ParseResult();

            for (int position = 0; position < inputText.Length; position++)
            {
                var currentLine = inputText[position];

                var versionMatch = versionRegex.Match(currentLine);

                if (versionMatch.Success)
                {
                    var versionGroup = versionMatch.Groups["version"];

                    parseResult.Version = Convert.ToInt32(versionGroup.Value);
                }

                var classMatch = classRegex.Match(currentLine);

                if (versionMatch.Success)
                {
                    var className = classMatch.Groups["class"].Value;

                    _classParser.ParseClass(className, inputText);
                }
            }

            return parseResult;
        }
    }
}
