using System;
using System.Windows.Input;

namespace DrawQuestionDesktopApp.common
{
    public class RelayCommand:ICommand
    {
        private Action _command;
        private Func<bool> _canExec;

        public RelayCommand(Action command):this(command, null)
        {
        }

        public RelayCommand(Action command, Func<bool> canExec)
        {
            _command = command;
            _canExec = canExec;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _command();
        }

        public event EventHandler CanExecuteChanged;
    }
}
