using System;

namespace SQMReorderer.Core.Import.DataSetters
{
    internal abstract class SingleValuePropertySetterBase<T> : LineSetterBase
    {
        protected Action<T> PropertySetter { get; private set; }

        protected SingleValuePropertySetterBase(string propertyName, string valuePattern, Action<T> propertySetter)
            : base(propertyName + @"\=(?<value>" + valuePattern + @")")
        {
            PropertySetter = propertySetter;
        }
    }
}