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
        private Dictionary<string, Stack<ICommand>> _commands = new Dictionary<string, Stack<ICommand>>();


        public void Execute(int rowIndex, string columnName, ICommand command)
        {
            var key = $"{rowIndex}:{columnName}";
            if (!_commands.ContainsKey(key))
            {
                _commands[key] = new Stack<ICommand>();
            }

            command.Execute(null);
            _commands[key].Push(command);
        }

        public void Undo(int rowIndex, string columnName)
        {
            var key = $"{rowIndex}:{columnName}";
            if (!_commands.ContainsKey(key) || _commands[key].Count == 0) return;

            ICommand command = _commands[key].Pop();
            command.Undo();
        }

        public void Pop(int rowIndex, string columnName)
        {
            var key = $"{rowIndex}:{columnName}";
            if (_commands.ContainsKey(key) && _commands[key].Count > 0)
            {
                _commands[key].Pop();
            }
        }
    }
}
