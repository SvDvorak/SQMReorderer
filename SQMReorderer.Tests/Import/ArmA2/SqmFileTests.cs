using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.Import.FileVersion;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Core.StreamHelpers;

namespace SQMReorderer.Tests.Import.ArmA2
{
    [Category("AcceptanceTest")]
    [TestFixture]
    public class SqmFileTests
    {
        private const string _testExportPath = "test_arma2_export.sqm";

        [Test]
        public void Expect_SqmParser_to_successfully_parse_testFile()
        {
            CleanupPreviousTest();
            var importStream = GetImportStream();
            var importResults = ImportStream(importStream);
            importStream.Seek(0, SeekOrigin.Begin);

            ExportContents(importResults);

            var verifyExportStream = GetExportedFileStream();
            Assert.AreEqual(CombineToSingleString(importStream), CombineToSingleString(verifyExportStream));

            verifyExportStream.Close();
        }

        private void CleanupPreviousTest()
        {
            if (File.Exists(_testExportPath))
            {
                File.Delete(_testExportPath);
            }
        }

        private Stream GetImportStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetName().Name + "." + "Import.ArmA2." + "mission.sqm";
            var importStream = assembly.GetManifestResourceStream(resourcePath);

            return importStream;
        }

        private SqmContents ImportStream(Stream importStream)
        {
            var streamToStringsReader = new StreamToStringsReader();
            var sqmContextCreator = new SqmContextCreator();
            var sqmFileImporter = new SqmFileImporter(new FileVersionRetriever(new StreamReaderFactory()),
                new SqmContentCombiner(),
                new Core.Import.ArmA2.SqmFileImporter(streamToStringsReader, sqmContextCreator,
                    new Core.Import.ArmA2.SqmParser()),
                new Core.Import.ArmA3.SqmFileImporter(streamToStringsReader, sqmContextCreator,
                    new Core.Import.ArmA3.SqmParser()));

            var importResults = sqmFileImporter.Import(importStream);

            return importResults;
        }

        private void ExportContents(SqmContents importResults)
        {
            var sqmFileExporter = new SqmFileExporter(new SqmElementExportVisitor(), new ContextIndenter(),
                new StreamWriterFactory());
            var exportStream = new FileStream(_testExportPath, FileMode.OpenOrCreate);
            sqmFileExporter.Export(exportStream, importResults);
            exportStream.Close();
        }

        private FileStream GetExportedFileStream()
        {
            var verifyExportStream = new FileStream(_testExportPath, FileMode.Open);

            return verifyExportStream;
        }

        private string CombineToSingleString(Stream fileStream)
        {
            var fileContents = new StreamToStringsReader().Read(fileStream);
            var testFileStringBuilder = new StringBuilder();

            foreach (var row in fileContents)
            {
                testFileStringBuilder.Append(row + "\n");
            }

            return testFileStringBuilder.ToString();
        }
    }
}