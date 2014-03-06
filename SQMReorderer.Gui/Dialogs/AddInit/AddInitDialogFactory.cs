namespace SQMReorderer.Gui.Dialogs.AddInit
{
    public class AddInitDialogFactory : IAddInitDialogFactory
    {
        public IAddInitDialog Create()
        {
            return new AddInitDialog();
        }
    }
}