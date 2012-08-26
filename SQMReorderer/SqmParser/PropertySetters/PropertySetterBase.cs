using System.Text.RegularExpressions;

namespace SQMReorderer.SqmParser.PropertySetters
{
    public abstract class PropertySetterBase
    {
        private readonly Regex _propertyRegex;

        protected PropertySetterBase(string propertyPattern)
        {
            _propertyRegex = new Regex(propertyPattern);
        }

        public Result SetPropertyIfMatch(SqmStream stream)
        {
            return stream.MatchCurrentLine(_propertyRegex, SetPropertyValue);
        }

        private void SetPropertyValue(Match match)
        {
            SetPropertyValue(match.Groups["value"].Value);
        }

        protected abstract void SetPropertyValue(string value);
    }
}