using System;
using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.SqmParser.HelperFunctions;

namespace SQMReorderer.Core.SqmParser.DataSetters
{
    public class IntegerListPropertySetter : MultiValuePropertySetterBase<List<int>>
    {
        public IntegerListPropertySetter(string propertyName, Action<List<int>> propertySetter)
            : base(propertyName, CommonRegexPatterns.IntegerPattern, propertySetter)
        {
        }

        protected override void SetPropertyValues(List<string> values)
        {
            PropertySetter(values.Select(x => Convert.ToInt32(x)).ToList());
        }
    }
}
