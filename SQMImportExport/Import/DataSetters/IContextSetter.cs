using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.DataSetters
{
    internal interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}