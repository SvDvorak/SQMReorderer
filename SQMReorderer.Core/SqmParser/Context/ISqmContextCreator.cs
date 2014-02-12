using System.Collections.Generic;

namespace SQMReorderer.Core.SqmParser.Context
{
    public interface ISqmContextCreator
    {
        SqmContext CreateRootContext(List<string> contextText);
        SqmContext CreateContext(List<string> contextText);
    }
}