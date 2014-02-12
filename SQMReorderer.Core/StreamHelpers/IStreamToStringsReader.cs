using System.Collections.Generic;
using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    public interface IStreamToStringsReader
    {
        List<string> Read(Stream stream);
    }
}