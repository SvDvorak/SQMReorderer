using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.SqmExport;
using SQMReorderer.SqmParser.ResultObjects;

namespace SQMReorderer
{
    [TestFixture]
    public class SqmFileExporterTests
    {
        private ISqmElementVisitor _sqmElementVisitor;
        private SqmFileExporter _exporter;
        private IStreamWriterFactory _streamWriterFactory;

        [SetUp]
        public void Setup()
        {
            _sqmElementVisitor = Substitute.For<ISqmElementVisitor>();
            _streamWriterFactory = Substitute.For<IStreamWriterFactory>();
            _exporter = new SqmFileExporter(_sqmElementVisitor, _streamWriterFactory);
        }

        [Test]
        public void Uses_sqm_element_visitor_to_convert_contents_to_string()
        {
            var contents = new SqmContents();
            _exporter.Export(new MemoryStream(), contents);

            _sqmElementVisitor.Received().Visit("", contents);
        }

        [Test]
        public void Writes_converted_string_to_stream_using_stream_writer()
        {
            var contents = new SqmContents();
            var stream = Substitute.For<Stream>();
            var streamWriter = Substitute.For<IStreamWriterAdapter>();
            const string convertedString = "Text!";

            _sqmElementVisitor.Visit("", contents).Returns(convertedString);
            _streamWriterFactory.Create(stream).Returns(streamWriter);

            _exporter.Export(stream, contents);

            streamWriter.Received().Write(convertedString);
        }
    }
}