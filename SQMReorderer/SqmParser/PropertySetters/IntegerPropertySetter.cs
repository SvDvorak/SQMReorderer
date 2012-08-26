using System;
using SQMReorderer.SqmParser.HelperFunctions;

namespace SQMReorderer.SqmParser.PropertySetters
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