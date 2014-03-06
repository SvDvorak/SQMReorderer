using SQMReorderer.Gui.Command;

namespace SQMReorderer.Gui.Dialogs.AddInit
{
    public class AddInitViewModel
    {
        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public string InitToAdd { get; set; }
    }
}