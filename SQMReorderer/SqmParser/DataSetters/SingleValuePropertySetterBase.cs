using System;

namespace SQMReorderer.SqmParser.DataSetters
{
    public abstract class SingleValuePropertySetterBase<T> : PropertySetterBase
    {
        protected Action<T> PropertySetter { get; private set; }

        protected SingleValuePropertySetterBase(string propertyName, string valuePattern, Action<T> propertySetter)
            : base(propertyName + @"\=(?<value>" + valuePattern + @")")
        {
            PropertySetter = propertySetter;
        }
    }
}