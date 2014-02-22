﻿using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.FileVersion;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class FileVersionRetrieverTests
    {
        private FileVersionRetriever _sut;
        private Stream _stream;
        private IStreamReaderFactory _streamReaderFactory;
        private IStreamReaderAdapter _streamReader;

        [SetUp]
        public void Setup()
        {
            _stream = Substitute.For<Stream>();
            _streamReader = Substitute.For<IStreamReaderAdapter>();
            _streamReaderFactory = Substitute.For<IStreamReaderFactory>();
            _streamReaderFactory.Create(_stream).Returns(_streamReader);
            _sut = new FileVersionRetriever(_streamReaderFactory);
        }

        [Test]
        public void Throws_exception_when_stream_is_empty()
        {
            Assert.Throws<SqmVersionException>(() => _sut.GetVersion(_stream));
        }

        [Test]
        public void Throws_exception_when_first_line_in_stream_does_not_contain_version()
        {
            _streamReader.ReadLine().Returns("blagh blagh");

            Assert.Throws<SqmVersionException>(() => _sut.GetVersion(_stream));
        }

        [Test]
        public void Throws_exception_when_file_version_is_unknown()
        {
            Assert.Throws<SqmVersionException>(() => _sut.GetVersion(0));
        }

        [Test]
        public void Arma_2_version_is_returned_when_version_is_11()
        {
            _streamReader.ReadLine().Returns("version=11;");

            var intFileVersion = _sut.GetVersion(11);
            var streamFileVersion = _sut.GetVersion(_stream);

            Assert.AreEqual(FileVersion.ArmA2, intFileVersion);
            Assert.AreEqual(FileVersion.ArmA2, streamFileVersion);
        }

        [Test]
        public void Arma_3_version_is_returned_when_version_is_12()
        {
            _streamReader.ReadLine().Returns("version=12;");

            var intFileVersion = _sut.GetVersion(12);
            var streamFileVersion = _sut.GetVersion(_stream);

            Assert.AreEqual(FileVersion.ArmA3, intFileVersion);
            Assert.AreEqual(FileVersion.ArmA3, streamFileVersion);
        }

        [Test]
        public void Resets_stream_after_reading_version()
        {
            _streamReader.ReadLine().Returns("version=12;");

            _sut.GetVersion(_stream);

            _stream.Received().Seek(0, SeekOrigin.Begin);
        }
    }
}
