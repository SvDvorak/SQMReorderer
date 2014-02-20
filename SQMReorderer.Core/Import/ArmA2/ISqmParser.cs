using SQMReorderer.Core.Import.ArmA2.Context;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA2
{
    public interface ISqmParser
    {
        SqmContents ParseContext(SqmContext context);
    }
}