using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.DataSetters
{
    public interface ILineSetter
    {
        Result SetValueIfLineMatches(SqmLine line);
    }
}