using System;
using System.Globalization;
using SQMReorderer.SqmParser.HelperFunctions;

namespace SQMReorderer.SqmParser.PropertySetters
{
    public class DoublePropertySetter : SingleValuePropertySetterBase<double>
    {
        private readonly NumberFormatInfo _doubleFormatInfo;

        public DoublePropertySetter(string propertyName, Action<double> propertySetter)
            : base(propertyName, CommonRegexPatterns.DoublePattern, propertySetter)
        {
            _doubleFormatInfo = new NumberFormatInfo();
            _doubleFormatInfo.CurrencyDecimalSeparator = ".";
        }

        protected override void SetPropertyValue(string value)
        {
            PropertySetter(double.Parse(value, _doubleFormatInfo));
        }
    }
}