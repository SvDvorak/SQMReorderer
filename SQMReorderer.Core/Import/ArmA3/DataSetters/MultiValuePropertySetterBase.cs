using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SQMReorderer.Core.Import.ArmA3.DataSetters
{
    public abstract class MultiValuePropertySetterBase<T> : PropertySetterBase
    {
        protected Action<T> PropertySetter { get; private set; }

        private Regex _valueRegex;

        protected MultiValuePropertySetterBase(string propertyName, string valuePattern, Action<T> propertySetter)
            : base(propertyName + @"\[\]\=\{(?<value>(" + valuePattern + @",?)*)\}")
        {
            PropertySetter = propertySetter;

            _valueRegex = new Regex(valuePattern);
        }

        protected override void SetPropertyValue(string value)
        {
            var match = _valueRegex.Match(value);

            var values = new List<string>();

            while(match.Success)
            {
                values.Add(match.Value);

                match = match.NextMatch();
            }

            SetPropertyValues(values);
        }

        protected abstract void SetPropertyValues(List<string> values);
    }
}