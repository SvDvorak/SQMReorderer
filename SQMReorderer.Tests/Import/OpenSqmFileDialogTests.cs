using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.SqmParser.ResultObjects;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    internal class OpenSqmFileDialogTests
    {
        private IOpenFileDialogAdapter _openFileDialogAdapter;
        private ISqmFileImporter _sqmFileImporter;

        private SqmContents _expectedContents;
        private MemoryStream _memoryStream;
        private OpenSqmFileDialog _openSqmFileDialog;

        [SetUp]
        public void Setup()
        {
            _openFileDialogAdapter = Substitute.For<IOpenFileDialogAdapter>();
            _sqmFileImporter = Substitute.For<ISqmFileImporter>();

            _openSqmFileDialog = new OpenSqmFileDialog(_openFileDialogAdapter, _sqmFileImporter);

            _memoryStream = new MemoryStream();
            _openFileDialogAdapter.OpenFile().Returns(_memoryStream);

            _expectedContents = new SqmContents();
            _sqmFileImporter.Import(_memoryStream).Returns(_expectedContents);
        }

        [Test]
        public void Shows_open_file_dialog()
        {
            _openSqmFileDialog.ShowDialog();

            _openFileDialogAdapter.Received().ShowDialog();
        }

        [Test]
        public void Opens_selected_path()
        {
            _openSqmFileDialog.ShowDialog();

            _openFileDialogAdapter.Received().OpenFile();
        }

        [Test]
        public void Returns_parsed_file_result_from_sqm_importer()
        {
            var actualContents = _sqmFileImporter.Import(_memoryStream);

            Assert.AreEqual(_expectedContents, actualContents);
        }
    }
}