using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Export;
using SQMImportExport.Import;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class SaveSqmFileTests
    {
        private IStreamFactory _streamFactory;
        private Stream _stream;
        private ISqmFileExporterFactory _exporterFactory;
        private ISqmContentsVisitor _exportVisitor;
        private ISqmContents _sqmContents;

        [SetUp]
        public void Setup()
        {
            _streamFactory = Substitute.For<IStreamFactory>();
            _stream = Substitute.For<Stream>();

            _exporterFactory = Substitute.For<ISqmFileExporterFactory>();
            _exportVisitor = Substitute.For<ISqmContentsVisitor>();

            _sqmContents = Substitute.For<ISqmContents>();
        }

        [Test]
        public void Contents_accept_visitor_created_from_exporter()
        {
            _streamFactory.Create("testFilePath").Returns(_stream);
            _exporterFactory.Create(_stream).Returns(_exportVisitor);

            var sut = new SaveSqmFile(_streamFactory, _exporterFactory);
            sut.Save("testFilePath", _sqmContents);

            _sqmContents.Received().Accept(_exportVisitor);
        }

        [Test]
        public void Closes_stream_after_finishing_export()
        {
            _streamFactory.Create("testFilePath").Returns(_stream);

            var sut = new SaveSqmFile(_streamFactory, _exporterFactory);
            sut.Save("testFilePath", _sqmContents);

            _stream.Received().Close();
        }
    }
}
