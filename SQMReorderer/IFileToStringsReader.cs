using System.Collections.Generic;
using System.IO;

namespace SQMReorderer
{
    public interface IFileToStringsReader
    {
        List<string> Read(string fileName);
        List<string> Read(Stream stream);
    }
}