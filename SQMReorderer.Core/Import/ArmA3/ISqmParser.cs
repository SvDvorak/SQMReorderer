using SQMReorderer.Core.Import.ArmA3.ResultObjects;
using SQMReorderer.Core.Import.Context;

namespace SQMReorderer.Core.Import.ArmA3
{
    public interface ISqmParser
    {
        SqmContents ParseContext(SqmContext context);
    }
}