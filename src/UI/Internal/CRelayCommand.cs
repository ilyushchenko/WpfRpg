using System;
using System.Windows.Input;

namespace UI.Internal
{
    public class CRelayCommand : ICommand
    {
        private readonly Action<Object> _execute;
        private readonly Func<Object, Boolean> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public CRelayCommand(Action<Object> execute, Func<Object, Boolean> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(Object parameter)
        {
            _execute(parameter);
        }
    }
}