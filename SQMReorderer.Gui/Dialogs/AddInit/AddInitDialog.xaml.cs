using System.Windows;
using SQMReorderer.Gui.Command;

namespace SQMReorderer.Gui.Dialogs.AddInit
{
    public partial class AddInitDialog : Window, IAddInitDialog
    {
        public AddInitDialog()
        {
            InitializeComponent();
        }

        public AddInitViewModel ViewModel
        {
            get { return (AddInitViewModel) DataContext; }
            set { DataContext = value; }
        }

        public new AddInitResult ShowDialog()
        {
            ViewModel = new AddInitViewModel();
            ViewModel.OkCommand = new DelegateCommand(OkClose);
            ViewModel.CancelCommand = new DelegateCommand(CancelClose);

            var dialogResult = base.ShowDialog();
            var okHasBeenPressed = dialogResult.HasValue && dialogResult.Value;

            return new AddInitResult()
                {
                    DialogResult = okHasBeenPressed ? Dialogs.DialogResult.Ok : Dialogs.DialogResult.Cancel,
                    InitToAdd = ViewModel.InitToAdd
                };
        }

        private void OkClose()
        {
            DialogResult = true;
            Close();
        }

        private void CancelClose()
        {
            DialogResult = false;
            Close();
        }
    }
}
