using System;
using System.Windows.Input;

namespace LeetPhotos.Core.Utils
{
	public class LeetCommand : ICommand
    {
        private Action _action;

        public event EventHandler CanExecuteChanged;

		public LeetCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }

	public class LeetCommand<T> : ICommand
    {
        private Action<T> _action;

		public LeetCommand(Action<T> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is T)
                return true;

            return false;
        }


        public void Execute(object parameter)
        {

            _action((T)parameter);
        }
    }
}
