using System.Collections.Generic;
using System.IO;

namespace SQMReorderer
{
    public interface IStreamToStringsReader
    {
        List<string> Read(Stream stream);
    }
}