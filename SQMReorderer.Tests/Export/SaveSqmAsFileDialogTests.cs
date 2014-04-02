using System.IO;
using NSubstitute;
using NUnit.Framework;
using SQMImportExport.Common;
using SQMImportExport.Export;
using SQMReorderer.Gui.Dialogs;

namespace SQMReorderer.Tests.Export
{
    [TestFixture]
    public class SaveSqmAsFileDialogTests
    {
        private SaveSqmAsFileDialog _saveSqmAsFileDialog;
        private ISaveFileDialogAdapter _saveFileDialogAdapter;
        private ISqmExporter _sqmExporter;

        private SqmContentsBase _sqmContents;
        private MemoryStream _memoryStream;

        [SetUp]
        public void Setup()
        {
            _saveFileDialogAdapter = Substitute.For<ISaveFileDialogAdapter>();
            _sqmExporter = Substitute.For<ISqmExporter>();
            _saveSqmAsFileDialog = new SaveSqmAsFileDialog(_saveFileDialogAdapter, _sqmExporter);

            _sqmContents = Substitute.For<SqmContentsBase>();
            _memoryStream = Substitute.For<MemoryStream>();
            _saveFileDialogAdapter.OpenFile().Returns(_memoryStream);
        }

        [Test]
        public void Shows_save_file_dialog()
        {
            _saveSqmAsFileDialog.ShowDialog(_sqmContents);

            _saveFileDialogAdapter.Received().ShowDialog();
        }

        [Test]
        public void Opens_selected_path_when_user_selects_file_path()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(true);

            _saveSqmAsFileDialog.ShowDialog(_sqmContents);

            _saveFileDialogAdapter.Received().OpenFile();
        }

        [Test]
        public void Exports_sqm_contents_using_stream()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(true);

            _saveSqmAsFileDialog.ShowDialog(_sqmContents);

			_sqmExporter.Received().Export(_memoryStream, _sqmContents);
        }

        [Test]
        public void Closes_stream_after_exporting()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(true);

            _saveSqmAsFileDialog.ShowDialog(_sqmContents);

            _memoryStream.Received().Close();
        }

        [Test]
        public void Does_nothing_if_user_presses_cancel()
        {
            _saveFileDialogAdapter.ShowDialog().Returns(false);

            _saveSqmAsFileDialog.ShowDialog(_sqmContents);

            _saveFileDialogAdapter.DidNotReceive().OpenFile();
        }

        [Test]
        public void Automatically_appends_sqm_file_ending()
        {
            Assert.AreEqual(true, _saveFileDialogAdapter.AddExtension);
            Assert.AreEqual("SQM File (*.sqm)|*.sqm", _saveFileDialogAdapter.Filter);
        }
    }
}