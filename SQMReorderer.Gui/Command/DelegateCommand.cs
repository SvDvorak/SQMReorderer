using System;
using System.Windows.Input;

namespace SQMReorderer.Gui.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _commandAction;

        public DelegateCommand(Action commandAction)
        {
            if(commandAction == null)
            {
                throw new ActionCanNotBeNullException();
            }

            _commandAction = commandAction;
        }

        public void Execute(object parameter = null)
        {
            _commandAction.Invoke();
        }

        public bool CanExecute(object parameter = null)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}