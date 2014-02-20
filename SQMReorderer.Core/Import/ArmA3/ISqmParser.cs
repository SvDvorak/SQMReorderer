using SQMReorderer.Core.Import.ArmA3.Context;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;

namespace SQMReorderer.Core.Import.ArmA3
{
    public interface ISqmParser
    {
        SqmContents ParseContext(SqmContext context);
    }
}