using System;
using System.Collections.Generic;
using System.Linq;
using SQMReorderer.Core.Import.HelperFunctions;

namespace SQMReorderer.Core.Import.DataSetters
{
    internal class IntegerListPropertySetter : MultiValuePropertySetterBase<List<int>>
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
