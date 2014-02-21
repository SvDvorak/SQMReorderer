using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    public class SqmFileImporterTests
    {
        private SqmFileImporter _sut;
        private IFileVersionRetriever _fileVersionRetriever;
        private Core.Import.ArmA2.ISqmFileImporter _arma2Importer;
        private Core.Import.ArmA3.ISqmFileImporter _arma3Importer;
        private ISqmContentCombiner _contentCombiner;

        [SetUp]
        public void Setup()
        {
            _fileVersionRetriever = Substitute.For<IFileVersionRetriever>();
            _arma2Importer = Substitute.For<Core.Import.ArmA2.ISqmFileImporter>();
            _arma3Importer = Substitute.For<Core.Import.ArmA3.ISqmFileImporter>();
            _contentCombiner = Substitute.For<ISqmContentCombiner>();

            _sut = new SqmFileImporter(_fileVersionRetriever, _contentCombiner, _arma2Importer, _arma3Importer);
        }

        [Test]
        public void Uses_arma_2_parser_when_file_version_indicates_arma_2_version()
        {
            var stream = Substitute.For<Stream>();
            _fileVersionRetriever.GetVersion(stream).Returns(FileVersion.ArmA2);

            var arma2Contents = new Core.Import.ArmA2.ResultObjects.SqmContents();
            var expectedContents = new SqmContents();

            _arma2Importer.Import(stream).Returns(arma2Contents);
            _contentCombiner.Combine(arma2Contents).Returns(expectedContents);

            var sqmContents = _sut.Import(stream);

            Assert.AreEqual(expectedContents, sqmContents);
        }

        [Test]
        public void Uses_arma_3_parser_when_file_version_indicates_arma_3_version()
        {
            var stream = Substitute.For<Stream>();
            _fileVersionRetriever.GetVersion(stream).Returns(FileVersion.ArmA3);

            var arma3Contents = new Core.Import.ArmA3.ResultObjects.SqmContents();
            var expectedContents = new SqmContents();

            _arma3Importer.Import(stream).Returns(arma3Contents);
            _contentCombiner.Combine(arma3Contents).Returns(expectedContents);

            var sqmContents = _sut.Import(stream);

            Assert.AreEqual(expectedContents, sqmContents);
        }
    }
}
