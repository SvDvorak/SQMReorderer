using System.IO;
using SQMReorderer.Core.SqmExport;
using SQMReorderer.Core.SqmParser.ResultObjects;
using SQMReorderer.Core.Streams;

namespace SQMReorderer.Gui.Dialogs
{
    public class SqmFileExporter : ISqmFileExporter
    {
        private readonly ISqmElementVisitor _sqmElementVisitor;
        private readonly IContextIndenter _contextIndenter;
        private readonly IStreamWriterFactory _streamWriterFactory;

        public SqmFileExporter(ISqmElementVisitor sqmElementVisitor, IContextIndenter contextIndenter, IStreamWriterFactory streamWriterFactory)
        {
            _sqmElementVisitor = sqmElementVisitor;
            _contextIndenter = contextIndenter;
            _streamWriterFactory = streamWriterFactory;
        }

        public void Export(Stream stream, SqmContents contents)
        {
            var contentText = _sqmElementVisitor.Visit("", contents);
            var indentedText = _contextIndenter.Indent(contentText);

            var streamWriter = _streamWriterFactory.Create(stream);

            streamWriter.Write(indentedText);

            streamWriter.Flush();
        }
    }
}