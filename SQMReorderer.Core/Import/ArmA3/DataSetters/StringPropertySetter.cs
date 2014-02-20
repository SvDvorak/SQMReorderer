using System;

namespace SQMReorderer.Core.Import.ArmA3.DataSetters
{
    public class StringPropertySetter : SingleValuePropertySetterBase<string>
    {
        public StringPropertySetter(string propertyName, Action<string> propertySetter)
            : base(propertyName, @""".+""", propertySetter)
        {
        }

        protected override void SetPropertyValue(string value)
        {
            value = value.Substring(1, value.Length - 2);

            PropertySetter(value.Trim());
        }
    }
}