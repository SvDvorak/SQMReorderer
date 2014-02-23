using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMReorderer.Core.Import;
using SQMReorderer.Core.Import.ResultObjects;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Tests.Import
{
    [TestFixture]
    internal class OpenSqmFileDialogTests
    {
        private IOpenFileDialogAdapter _openFileDialogAdapter;
        private ISqmFileImporter _sqmFileImporter;
        private IMessageBoxPresenter _messageBoxPresenter;

        private SqmContents _expectedContents;
        private MemoryStream _memoryStream;
        private OpenSqmFileDialog _openSqmFileDialog;

        [SetUp]
        public void Setup()
        {
            _openFileDialogAdapter = Substitute.For<IOpenFileDialogAdapter>();
            _sqmFileImporter = Substitute.For<ISqmFileImporter>();
            _messageBoxPresenter = Substitute.For<IMessageBoxPresenter>();

            _openSqmFileDialog = new OpenSqmFileDialog(_openFileDialogAdapter, _sqmFileImporter, _messageBoxPresenter);

            _memoryStream = Substitute.For<MemoryStream>();
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
        public void Opens_selected_path_when_user_selects_file_path()
        {
            _openFileDialogAdapter.ShowDialog().Returns(true);

            _openSqmFileDialog.ShowDialog();

            _openFileDialogAdapter.Received().OpenFile();
        }

        [Test]
        public void Returns_parsed_file_result_from_sqm_importer()
        {
            _openFileDialogAdapter.ShowDialog().Returns(true);

            var actualContents = _openSqmFileDialog.ShowDialog();

            Assert.AreEqual(_expectedContents, actualContents);
        }

        [Test]
        public void Closes_stream_after_file_is_imported()
        {
            _openFileDialogAdapter.ShowDialog().Returns(true);

            _openSqmFileDialog.ShowDialog();

            _memoryStream.Received().Close();
        }

        [Test]
        public void Does_nothing_if_user_presses_cancel()
        {
            _openFileDialogAdapter.ShowDialog().Returns(false);

            var sqmContents = _openSqmFileDialog.ShowDialog();

            _openFileDialogAdapter.DidNotReceive().OpenFile();
            Assert.AreEqual(null, sqmContents);
        }

        [Test]
        public void Filters_on_only_opening_sqm_files()
        {
            Assert.AreEqual("SQM Files (*.sqm)|*.sqm", _openFileDialogAdapter.Filter);
        }

        [Test]
        public void Shows_error_message_and_returns_null_when_sqm_import_fails()
        {
            _openFileDialogAdapter.ShowDialog().Returns(true);
            _sqmFileImporter.Import(Arg.Any<Stream>()).Returns(x => { throw new SqmParseException("Parse error"); });

            var sqmContents = _openSqmFileDialog.ShowDialog();

            _messageBoxPresenter.Received().ShowError("Unable to read file: Parse error");
            Assert.IsNull(sqmContents);
        }
    }
}