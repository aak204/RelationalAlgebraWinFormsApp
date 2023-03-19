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
        private Dictionary<Tuple<int, int>, Stack<ICommand>> _commands = new Dictionary<Tuple<int, int>, Stack<ICommand>>();


        public void Execute(int rowIndex, int columnIndex, ICommand command)
        {
            var key = Tuple.Create(rowIndex, columnIndex);
            if (!_commands.ContainsKey(key))
            {
                _commands[key] = new Stack<ICommand>();
            }

            command.Execute(null);
            _commands[key].Push(command);
        }

        public void Undo(int rowIndex, int columnIndex)
        {
            var key = Tuple.Create(rowIndex, columnIndex);
            if (!_commands.ContainsKey(key) || _commands[key].Count == 0) return;

            ICommand command = _commands[key].Pop();
            command.Undo();
        }

        public void Pop(int rowIndex, int columnIndex)
        {
            var key = Tuple.Create(rowIndex, columnIndex);
            if (_commands.ContainsKey(key) && _commands[key].Count > 0)
            {
                _commands[key].Pop();
            }
        }
    }
}
