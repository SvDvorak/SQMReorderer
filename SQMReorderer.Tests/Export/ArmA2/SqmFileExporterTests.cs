using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Export.ArmA2;
using SQMReorderer.Core.Import.ArmA2.ResultObjects;
using SQMReorderer.Core.StreamHelpers;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class SqmFileExporterTests
    {
        private SqmFileExporter _exporter;
        private ISqmElementVisitor _sqmElementVisitor;
        private IStreamWriterFactory _streamWriterFactory;
        private IContextIndenter _contextIndenter;

        [SetUp]
        public void Setup()
        {
            _sqmElementVisitor = Substitute.For<ISqmElementVisitor>();
            _streamWriterFactory = Substitute.For<IStreamWriterFactory>();
            _contextIndenter = Substitute.For<IContextIndenter>();
            _exporter = new SqmFileExporter(_sqmElementVisitor, _contextIndenter, _streamWriterFactory);
        }

        [Test]
        public void Uses_sqm_element_visitor_to_convert_contents_to_string()
        {
            var contents = new SqmContents();
            _exporter.Export(new MemoryStream(), contents);

            _sqmElementVisitor.Received().Visit("", contents);
        }

        [Test]
        public void Writes_converted_and_indented_string_to_stream_using_stream_writer()
        {
            var contents = new SqmContents();
            var stream = Substitute.For<Stream>();
            var streamWriter = Substitute.For<IStreamWriterAdapter>();
            const string convertedString = "Text!";
            const string indentedString = "Indented Text!";

            _sqmElementVisitor.Visit("", contents).Returns(convertedString);
            _contextIndenter.Indent(convertedString).Returns(indentedString);
            _streamWriterFactory.Create(stream).Returns(streamWriter);

            _exporter.Export(stream, contents);

            streamWriter.Received().Write(indentedString);
        }

        [Test]
        public void Flushes_stream_writer_when_finished()
        {
            var contents = new SqmContents();
            var stream = Substitute.For<Stream>();
            var streamWriter = Substitute.For<IStreamWriterAdapter>();

            _streamWriterFactory.Create(stream).Returns(streamWriter);

            _exporter.Export(stream, contents);

            streamWriter.Received().Flush();
        }
    }
}