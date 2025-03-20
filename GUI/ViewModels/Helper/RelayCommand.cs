using System;
using System.Windows.Input;

namespace GUI.ViewModels.Helper
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        // Constructor yêu cầu hành động thực thi và có thể có điều kiện thực thi (canExecute)
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Xác định xem command có thể được thực thi hay không.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Thực thi hành động được truyền vào.
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // Sự kiện này thông báo cho hệ thống khi điều kiện thực thi thay đổi.
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
