using System;

namespace SQMReorderer.SqmParser.PropertySetters
{
    public class IntegerPropertySetter : SingleValuePropertySetterBase<int>
    {
        public IntegerPropertySetter(string propertyName, Action<int> propertySetter)
            : base(propertyName, @"\d+", propertySetter)
        {
        }

        protected override void SetPropertyValue(string value)
        {
            PropertySetter(Convert.ToInt32(value));
        }
    }
}