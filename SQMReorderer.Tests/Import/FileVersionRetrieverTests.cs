using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Import;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class FileVersionRetrieverTests
    {
        private FileVersionRetriever _sut;
        private Stream _stream;
        private IStreamReaderFactory _streamReaderFactory;

        [SetUp]
        public void Setup()
        {
            _stream = Substitute.For<Stream>();
            _streamReaderFactory = Substitute.For<IStreamReaderFactory>();
            _sut = new FileVersionRetriever(_streamReaderFactory);
        }

        [Test]
        public void Throws_exception_when_stream_is_empty()
        {
            Assert.Throws<SqmParseException>(() => _sut.GetVersion(_stream));
        }

        [Test]
        public void Throws_exception_when_first_line_in_stream_does_not_contain_version()
        {
            var streamReader = Substitute.For<IStreamReaderAdapter>();
            streamReader.ReadLine().Returns("blagh blagh");
            _streamReaderFactory.Create(_stream).Returns(streamReader);

            Assert.Throws<SqmParseException>(() => _sut.GetVersion(_stream));
        }

        [Test]
        public void Arma_2_version_is_returned_when_version_is_11()
        {
            var streamReader = Substitute.For<IStreamReaderAdapter>();
            streamReader.ReadLine().Returns("version=11;");
            _streamReaderFactory.Create(_stream).Returns(streamReader);

            var fileVersion = _sut.GetVersion(_stream);

            Assert.AreEqual(FileVersion.ArmA2, fileVersion);
        }

        [Test]
        public void Arma_3_version_is_returned_when_version_is_12()
        {
            var streamReader = Substitute.For<IStreamReaderAdapter>();
            streamReader.ReadLine().Returns("version=12;");
            _streamReaderFactory.Create(_stream).Returns(streamReader);

            var fileVersion = _sut.GetVersion(_stream);

            Assert.AreEqual(FileVersion.ArmA3, fileVersion);
        }
    }
}
