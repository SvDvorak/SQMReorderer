using System.Collections.Generic;
using System.IO;

namespace SQMReorderer.Core.Streams
{
    public interface IStreamToStringsReader
    {
        List<string> Read(Stream stream);
    }
}