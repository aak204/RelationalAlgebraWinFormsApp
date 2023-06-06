using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RelationalAlgebraWinFormsApp
{
    internal class UndoStack
    {
        // Словарь для хранения стеков команд для каждой ячейки в форме.
        // Ключ словаря - это строка в формате "{номер строки}:{название столбца}"
        private Dictionary<string, Stack<ICommand>> _commands = new Dictionary<string, Stack<ICommand>>();

        // Ссылка на главную форму
        public MainForm MainForm
        {
            get => default;
            set
            {
            }
        }

        // Метод для выполнения команды и помещения ее в стек
        public void Execute(int rowIndex, string columnName, ICommand command)
        {
            // Формируем ключ словаря
            var key = $"{rowIndex}:{columnName}";

            // Если в словаре нет стека команд для этой ячейки, создаем его
            if (!_commands.ContainsKey(key))
            {
                _commands[key] = new Stack<ICommand>();
            }

            // Выполняем команду и добавляем ее в стек
            command.Execute(null);
            _commands[key].Push(command);
        }

        // Метод для отмены последней команды в стеке для данной ячейки
        public void Undo(int rowIndex, string columnName)
        {
            // Формируем ключ словаря
            var key = $"{rowIndex}:{columnName}";

            // Если в словаре нет стека команд для этой ячейки, или стек пуст, то ничего не делаем
            if (!_commands.ContainsKey(key) || _commands[key].Count == 0) return;

            // Иначе извлекаем последнюю команду из стека и отменяем ее
            ICommand command = _commands[key].Pop();
            command.Undo();
        }

        // Метод для удаления последней команды из стека для данной ячейки без ее отмены
        public void Pop(int rowIndex, string columnName)
        {
            // Формируем ключ словаря
            var key = $"{rowIndex}:{columnName}";

            // Если в словаре есть стек команд для этой ячейки и он не пуст, извлекаем из него последнюю команду
            if (_commands.ContainsKey(key) && _commands[key].Count > 0)
            {
                _commands[key].Pop();
            }
        }
    }

}
