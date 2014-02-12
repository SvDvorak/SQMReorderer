using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.DataSetters
{
    public interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}