using SQMReorderer.Core.Import.ArmA3.Context;

namespace SQMReorderer.Core.Import.ArmA3.DataSetters
{
    public interface IContextSetter
    {
        Result SetContextIfMatch(SqmContext context);
    }
}