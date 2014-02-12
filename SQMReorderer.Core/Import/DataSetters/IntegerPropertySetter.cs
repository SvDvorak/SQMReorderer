using System;
using SQMReorderer.Core.Import.HelperFunctions;

namespace SQMReorderer.Core.Import.DataSetters
{
    public class IntegerPropertySetter : SingleValuePropertySetterBase<int>
    {
        public IntegerPropertySetter(string propertyName, Action<int> propertySetter)
            : base(propertyName, CommonRegexPatterns.IntegerPattern, propertySetter)
        {
        }

        protected override void SetPropertyValue(string value)
        {
            PropertySetter(Convert.ToInt32(value));
        }
    }
}