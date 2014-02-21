using System.IO;

namespace SQMReorderer.Core.Import.FileVersion
{
    public class StreamReaderAdapter : IStreamReaderAdapter
    {
        private readonly StreamReader _streamReader;

        public StreamReaderAdapter(Stream stream)
        {
            _streamReader = new StreamReader(stream);
        }

        public string ReadLine()
        {
            return _streamReader.ReadLine();
        }
    }
}