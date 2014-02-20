using SQMReorderer.Core.Import.ArmA2.Context;

namespace SQMReorderer.Core.Import.ArmA2.DataSetters
{
    public interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}