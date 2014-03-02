using System;
using System.IO;

namespace SQMReorderer.Core.StreamHelpers
{
    public class StreamWriterAdapter : IStreamWriterAdapter, IDisposable
    {
        private readonly StreamWriter _streamWriter;

        public StreamWriterAdapter(Stream stream)
        {
            _streamWriter = new StreamWriter(stream);
        }

        public void Write(string text)
        {
            _streamWriter.Write(text);
        }

        public void Flush()
        {
            _streamWriter.Flush();
        }

        public void Dispose()
        {
            _streamWriter.Dispose();
        }
    }
}