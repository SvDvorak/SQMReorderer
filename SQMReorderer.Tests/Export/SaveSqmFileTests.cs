using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Common;
using SQMImportExport.Export;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class SaveSqmFileTests
    {
        private IStreamFactory _streamFactory;
        private Stream _stream;
        private ISqmExporter _sqmExporter;
        private SqmContentsBase _sqmContents;

        [SetUp]
        public void Setup()
        {
            _streamFactory = Substitute.For<IStreamFactory>();
            _stream = Substitute.For<Stream>();

            _sqmExporter = Substitute.For<ISqmExporter>();

            _sqmContents = Substitute.For<SqmContentsBase>();
        }

        [Test]
        public void Exports_contents_with_exporter_using_stream()
        {
            _streamFactory.Create("testFilePath").Returns(_stream);

            var sut = new SaveSqmFile(_streamFactory, _sqmExporter);
            sut.Save("testFilePath", _sqmContents);

			_sqmExporter.Received().Export(_stream, _sqmContents);
        }

        [Test]
        public void Closes_stream_after_finishing_export()
        {
            _streamFactory.Create("testFilePath").Returns(_stream);

            var sut = new SaveSqmFile(_streamFactory, _sqmExporter);
            sut.Save("testFilePath", _sqmContents);

            _stream.Received().Close();
        }
    }
}
