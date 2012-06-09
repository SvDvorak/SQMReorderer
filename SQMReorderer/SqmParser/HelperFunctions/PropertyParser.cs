using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.HelperFunctions
{
    public class PropertyParser
    {
        private Dictionary<string, Regex> _propertyRegexes = new Dictionary<string, Regex>();

        public string ParseStringProperty(string stringProperty, string line, Action<string> propertyRead)
        {
            Regex propertyRegex;

            if(!_propertyRegexes.ContainsKey(stringProperty))
            {
                propertyRegex = new Regex(stringProperty + @"\=""(?<propertyValue>\w+)""");
                _propertyRegexes.Add(stringProperty, propertyRegex);
            }
            else
            {
                propertyRegex = _propertyRegexes[stringProperty];
            }

            var match = propertyRegex.Match(line);

            if(match.Success)
            {
                var matchGroup = match.Groups["propertyValue"];

                return matchGroup.Value;
            }

            return null;
        }
    }
}
