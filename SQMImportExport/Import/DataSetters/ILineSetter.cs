using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.DataSetters
{
    internal interface ILineSetter
    {
        Result SetValueIfLineMatches(SqmLine line);
    }
}