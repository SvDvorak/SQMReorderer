using System.Windows;

namespace SQMReorderer.Gui.Dialogs
{
    public class MessageBoxPresenter : IMessageBoxPresenter
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}