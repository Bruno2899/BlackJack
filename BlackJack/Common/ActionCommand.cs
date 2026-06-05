using System;
using System.Windows.Input;

namespace BlackJack.Common
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> handlerExecute;

        private readonly Func<object, bool> handlerCanExecute;

        public ActionCommand(
            Action<object> execute,
            Func<object, bool> canExecute)
        {
            this.handlerExecute = execute
                ?? throw new ArgumentNullException(
                    "Execute cannot be null");

            this.handlerCanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            this.handlerExecute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (this.handlerCanExecute == null)
            {
                return true;
            }

            return this.handlerCanExecute(parameter);
        }
    }
}