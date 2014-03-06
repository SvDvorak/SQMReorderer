using System.Globalization;

namespace SQMReorderer.Core
{
    public static class DoubleExtensions
    {
        public static string ToStringInvariant(this double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
