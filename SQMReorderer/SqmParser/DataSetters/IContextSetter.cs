using SQMReorderer.SqmParser.Context;

namespace SQMReorderer.SqmParser.DataSetters
{
    public interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}