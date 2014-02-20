using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.ArmA2
{
    public interface ISqmParser
    {
        SqmContents ParseContext(SqmContext context);
    }
}