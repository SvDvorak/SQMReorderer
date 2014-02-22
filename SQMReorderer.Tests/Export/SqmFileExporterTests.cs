using System;
using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import.FileVersion;
using SQMReorderer.Core.Import.ResultObjects;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class SqmFileExporterTests
    {
        [Test]
        public void Throws_sqm_export_exception_when_version_is_not_arma_2_or_arma_3()
        {
            var fileVersionRetriever = Substitute.For<IFileVersionRetriever>();
            fileVersionRetriever.GetVersion(Arg.Any<int>()).Returns(x => { throw new Exception(); });
            var sqmFileExporter = new SqmFileExporter(null, null, fileVersionRetriever);
            var sqmContents = new SqmContents() { Version = 0 };

            Assert.Throws<SqmExportException>(() => sqmFileExporter.Export(null, sqmContents));
        }

        [Test]
        public void Uses_arma_2_exporter_when_version_is_arma_2()
        {
            var arma2Exporter = Substitute.For<Core.Export.ArmA2.ISqmFileExporter>();
            var fileVersionRetriever = Substitute.For<IFileVersionRetriever>();
            fileVersionRetriever.GetVersion(11).Returns(FileVersion.ArmA2);

            var sqmFileExporter = new SqmFileExporter(arma2Exporter, null, fileVersionRetriever);

            var sqmContents = new SqmContents { Version = 11 };
            var stream = Substitute.For<Stream>();
            sqmFileExporter.Export(stream, sqmContents);

            arma2Exporter.Received().Export(stream, sqmContents);
        }

        [Test]
        public void Uses_arma_3_exporter_when_version_is_arma_3()
        {
            var arma3Exporter = Substitute.For<Core.Export.ArmA3.ISqmFileExporter>();
            var fileVersionRetriever = Substitute.For<IFileVersionRetriever>();
            fileVersionRetriever.GetVersion(12).Returns(FileVersion.ArmA3);

            var sqmFileExporter = new SqmFileExporter(null, arma3Exporter, fileVersionRetriever);

            var sqmContents = new SqmContents { Version = 12 };
            var stream = Substitute.For<Stream>();
            sqmFileExporter.Export(stream, sqmContents);

            arma3Exporter.Received().Export(stream, sqmContents);
        }
    }
}
