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

namespace SQMReorderer.Tests.Import
{
    [Category("AcceptanceTest")]
    [TestFixture]
    public class SqmFileTests
    {
        private string _armaVersion;

        [TestCase(2)]
        [TestCase(3)]
        [Test]
        public void Expect_SqmParser_to_successfully_parse_testFile(int armaVersion)
        {
            _armaVersion = armaVersion.ToString();
            CleanupPreviousTest();
            var importStream = GetImportStream();
            var importResults = Import(importStream);
            importStream.Seek(0, SeekOrigin.Begin);

            Export(importResults, GetTestExportPath());

            var verifyExportStream = GetExportedFileStream(GetTestExportPath());
            Assert.AreEqual(CombineToSingleString(importStream), CombineToSingleString(verifyExportStream));

            verifyExportStream.Close();
        }

        private void CleanupPreviousTest()
        {
            var testExportPath = GetTestExportPath();

            if (File.Exists(testExportPath))
            {
                File.Delete(testExportPath);
            }
        }

        private string GetTestExportPath()
        {
            return "test_arma" + _armaVersion + "_export.sqm";
        }

        private Stream GetImportStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetName().Name + ".Import.ArmA" + _armaVersion + ".mission.sqm";
            var importStream = assembly.GetManifestResourceStream(resourcePath);

            return importStream;
        }

        private ISqmContents Import(Stream importStream)
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

        private void Export(ISqmContents importResults, string path)
        {
            var contextIndenter = new ContextIndenter();
            var streamWriterFactory = new StreamWriterFactory();
            var sqmFileExporter = new SqmFileExporter(
                new Core.Export.ArmA2.SqmFileExporter(new Core.Export.ArmA2.SqmElementExportVisitor(), contextIndenter, streamWriterFactory), 
                new Core.Export.ArmA3.SqmFileExporter(new Core.Export.ArmA3.SqmElementExportVisitor(), contextIndenter, streamWriterFactory),
                new FileVersionRetriever(new StreamReaderFactory()));
            var exportStream = new FileStream(path, FileMode.OpenOrCreate);
            //sqmFileExporter.Export(exportStream, importResults);
            exportStream.Close();
        }

        private FileStream GetExportedFileStream(string path)
        {
            var verifyExportStream = new FileStream(path, FileMode.Open);

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