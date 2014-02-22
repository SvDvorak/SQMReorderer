using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.Import.ArmA3;
using SQMReorderer.Core.Import.ArmA3.ResultObjects;
using SQMReorderer.Core.Import.Context;
using SQMReorderer.Core.StreamHelpers;

namespace SQMReorderer.Tests.Import.ArmA3
{
    [TestFixture]
    public class SqmFileTests
    {
        [Test]
        public void Expect_SqmParser_to_successfully_parse_testFile()
        {
            var fileContent = GetSqmFileContents();

            var parseResult = ParseContents(fileContent);

            var exportedTestFile = ExportContents(parseResult);

            Assert.AreEqual(CombineToSingleString(fileContent), exportedTestFile);
        }

        private static string ExportContents(SqmContents parseResult)
        {
            var exportVisitor = new SqmElementExportVisitor();
            var exportedTestFile = exportVisitor.Visit("file", parseResult);
            var indentedText = new ContextIndenter().Indent(exportedTestFile);

            return indentedText;
        }

        private static List<string> GetSqmFileContents()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetName().Name + "." + "Import.ArmA3." + "mission.sqm";
            var fileStream = assembly.GetManifestResourceStream(resourcePath);

            var fileContent = new StreamToStringsReader().Read(fileStream);

            return fileContent;
        }

        private static SqmContents ParseContents(List<string> fileContent)
        {
            var parser = new SqmParser();
            var contextCreator = new SqmContextCreator();

            var context = contextCreator.CreateRootContext(fileContent);
            var parseResult = parser.ParseContext(context);

            return parseResult;
        }

        private string CombineToSingleString(IEnumerable<string> fileContent)
        {
            var testFileStringBuilder = new StringBuilder();

            foreach (var row in fileContent)
            {
                testFileStringBuilder.Append(row + "\n");
            }

            return testFileStringBuilder.ToString();
        }
    }
}
