using System.Collections.Generic;
using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    internal interface IStreamToStringsReader
    {
        List<string> Read(Stream stream);
    }
}