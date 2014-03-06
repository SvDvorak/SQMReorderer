using System.IO;

namespace SQMReorderer.Core.Export
{
    internal interface IStreamFactory
    {
        Stream Create(string filePath);
    }
}