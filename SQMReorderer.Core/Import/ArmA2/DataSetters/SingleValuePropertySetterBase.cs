using System;

namespace SQMReorderer.Core.Import.ArmA2.DataSetters
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