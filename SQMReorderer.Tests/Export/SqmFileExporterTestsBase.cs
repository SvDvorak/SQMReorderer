using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Export;
using SQMReorderer.Core.StreamHelpers;

namespace SQMReorderer.Tests.Export
{
    public class SqmFileExporterTestsBase
    {
        internal SqmFileExporter Exporter;
        internal Core.Export.ArmA2.ISqmElementVisitor Arma2Visitor;
        internal Core.Export.ArmA3.ISqmElementVisitor Arma3Visitor;
        internal IStreamWriterAdapter StreamWriter;
        internal IContextIndenter ContextIndenter;

        [SetUp]
        public void Setup()
        {
            Arma2Visitor = Substitute.For<Core.Export.ArmA2.ISqmElementVisitor>();
            Arma3Visitor = Substitute.For<Core.Export.ArmA3.ISqmElementVisitor>();
            StreamWriter = Substitute.For<IStreamWriterAdapter>();
            ContextIndenter = Substitute.For<IContextIndenter>();
            Exporter = new SqmFileExporter(StreamWriter, Arma2Visitor, Arma3Visitor, ContextIndenter);
        }
    }
}