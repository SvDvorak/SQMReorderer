using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.PropertySetters
{
    class MultiLineStringListPropertySetter
    {
        private readonly Regex _propertyNameRegex;
        private readonly Regex _listStringRegex;

        private readonly Action<List<string>> _propertySetter;

        public MultiLineStringListPropertySetter(string propertyName, Action<List<string>> propertySetter)
        {
            _propertyNameRegex = new Regex(propertyName + @"\[\]\=");
            _listStringRegex = new Regex(@"[\d\w_]+");

            _propertySetter = propertySetter;
        }

        public Result SetPropertyIfMatch(SqmStream stream)
        {
            var isCurrentLineCorrectProperty = stream.IsCurrentLineMatch(_propertyNameRegex);

            if(isCurrentLineCorrectProperty)
            {
                var propertyStrings = new List<string>();

                stream.StepIntoInnerContext();

                while (!stream.IsAtEndOfContext)
                {
                    stream.MatchCurrentLine(_listStringRegex, x => propertyStrings.Add(x.Value));
                    stream.NextLineInContext();
                }

                _propertySetter(propertyStrings);

                stream.StepIntoOuterContext();

                return Result.Success;
            }
            else
            {
                return Result.Failure;
            }
        }
    }
}
