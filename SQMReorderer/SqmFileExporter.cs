using System.IO;
using SQMReorderer.SqmExport;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    public class SqmFileExporter : ISqmFileExporter
    {
        private readonly ISqmElementVisitor _sqmElementVisitor;
        private readonly IStreamWriterFactory _streamWriterFactory;

        public SqmFileExporter(ISqmElementVisitor sqmElementVisitor, IStreamWriterFactory streamWriterFactory)
        {
            _sqmElementVisitor = sqmElementVisitor;
            _streamWriterFactory = streamWriterFactory;
        }

        public void Export(Stream stream, SqmContents contents)
        {
            var contentText = _sqmElementVisitor.Visit("", contents);

            var streamWriter = _streamWriterFactory.Create(stream);

            streamWriter.Write(contentText);
        }
    }
}