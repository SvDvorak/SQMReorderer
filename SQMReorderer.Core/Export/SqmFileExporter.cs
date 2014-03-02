using System.IO;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.StreamHelpers;

namespace SQMReorderer.Core.Export
{
    public class SqmFileExporter : ISqmContentsVisitor
    {
        private readonly ArmA2.ISqmElementVisitor _arma2ElementVisitor;
        private readonly ArmA3.ISqmElementVisitor _arma3ElementVisitor;
        private readonly IContextIndenter _contextIndenter;
        private readonly IStreamWriterFactory _streamWriterFactory;

        private readonly Stream _stream;

        public SqmFileExporter(
            Stream stream,
            ArmA2.ISqmElementVisitor arma2ElementVisitor,
            ArmA3.ISqmElementVisitor arma3ElementVisitor, 
            IContextIndenter contextIndenter, 
            IStreamWriterFactory streamWriterFactory)
        {
            _stream = stream;
            _arma2ElementVisitor = arma2ElementVisitor;
            _arma3ElementVisitor = arma3ElementVisitor;
            _contextIndenter = contextIndenter;
            _streamWriterFactory = streamWriterFactory;
        }

        public void Visit(Import.ArmA2.ResultObjects.SqmContents arma2Contents)
        {
            var contentText = _arma2ElementVisitor.Visit("", arma2Contents);
            var indentedText = _contextIndenter.Indent(contentText);

            WriteText(indentedText);
        }

        public void Visit(Import.ArmA3.ResultObjects.SqmContents arma3Contents)
        {
            var contentText = _arma3ElementVisitor.Visit("", arma3Contents);
            var indentedText = _contextIndenter.Indent(contentText);

            WriteText(indentedText);
        }

        private void WriteText(string indentedText)
        {
            var streamWriter = _streamWriterFactory.Create(_stream);

            streamWriter.Write(indentedText);

            streamWriter.Flush();
        }
    }
}