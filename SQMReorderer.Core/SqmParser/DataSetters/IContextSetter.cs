using SQMReorderer.Core.SqmParser.Context;

namespace SQMReorderer.Core.SqmParser.DataSetters
{
    public interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}