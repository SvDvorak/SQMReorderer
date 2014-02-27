using System;
using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.HelperFunctions;

namespace SQMReorderer.Core.Import.DataSetters
{
    public class StringListPropertySetter : MultiValuePropertySetterBase<List<string>>
    {
        public StringListPropertySetter(string propertyName, Action<List<string>> propertySetter)
            : base(propertyName, "\"" + CommonRegexPatterns.NonSpacedTextPattern + "\"", propertySetter)
        {
        }

        protected override void SetPropertyValues(List<string> values)
        {
            PropertySetter(values.Select(x => x.TrimStart('"').TrimEnd('"')).ToList());
        }
    }
}