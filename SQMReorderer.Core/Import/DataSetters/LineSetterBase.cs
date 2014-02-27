using System.Text.RegularExpressions;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.DataSetters
{
    public abstract class LineSetterBase : ILineSetter
    {
        private readonly Regex _lineRegex;

        protected LineSetterBase(string linePattern)
        {
            _lineRegex = new Regex(@"^\s*" + linePattern);
        }

        public Result SetValueIfLineMatches(SqmLine line)
        {
            return line.Match(_lineRegex, SetValue);
        }

        private void SetValue(Match match)
        {
            SetValue(match.Groups["value"].Value);
        }

        protected abstract void SetValue(string value);
    }
}