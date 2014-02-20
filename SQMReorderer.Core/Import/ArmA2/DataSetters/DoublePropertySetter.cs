using System;
using System.Globalization;
using SQMReorderer.Core.Import.ArmA2.HelperFunctions;

namespace SQMReorderer.Core.Import.ArmA2.DataSetters
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