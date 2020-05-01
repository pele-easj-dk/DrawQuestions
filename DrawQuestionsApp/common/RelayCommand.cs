using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrawQuestionsApp.common
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
            return (_canExec == null) ? true : _canExec();
        }

        public void Execute(object parameter)
        {
            _command();
        }

        public event EventHandler CanExecuteChanged;
    }
}
