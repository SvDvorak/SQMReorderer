using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.ArmA3.Context;

namespace SQMReorderer.Core.Import.ArmA3.DataSetters
{
    public abstract class PropertySetterBase
    {
        private readonly Regex _propertyRegex;

        protected PropertySetterBase(string propertyPattern)
        {
            _propertyRegex = new Regex(propertyPattern);
        }

        public Result SetPropertyIfMatch(SqmLine line)
        {
            return line.Match(_propertyRegex, SetPropertyValue);
        }

        private void SetPropertyValue(Match match)
        {
            SetPropertyValue(match.Groups["value"].Value);
        }

        protected abstract void SetPropertyValue(string value);
    }
}