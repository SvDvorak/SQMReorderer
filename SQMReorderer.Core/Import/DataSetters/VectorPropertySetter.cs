using System;
using System.Collections.Generic;
using System.Globalization;
using SQMReorderer.Core.Import.HelperFunctions;

namespace SQMReorderer.Core.Import.DataSetters
{
    public class VectorPropertySetter : MultiValuePropertySetterBase<Vector>
    {
        private readonly NumberFormatInfo _doubleFormatInfo;

        public VectorPropertySetter(string propertyName, Action<Vector> propertySetter)
            : base(propertyName, CommonRegexPatterns.DoublePattern, propertySetter)
        {
            _doubleFormatInfo = new NumberFormatInfo();
            _doubleFormatInfo.CurrencyDecimalSeparator = ".";
        }

        protected override void SetPropertyValues(List<string> values)
        {
            var xPos = double.Parse(values[0], _doubleFormatInfo);
            var yPos = double.Parse(values[1], _doubleFormatInfo);
            var zPos = double.Parse(values[2], _doubleFormatInfo);

            PropertySetter(new Vector(xPos, yPos, zPos));
        }
    }
}
