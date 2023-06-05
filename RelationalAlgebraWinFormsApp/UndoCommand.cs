using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace RelationalAlgebraWinFormsApp
{
    internal class UndoCommand : ICommand
    {
        // Ссылка на объект DataGridView, в котором выполняется команда
        private readonly DataGridView _dataGridView;

        // Индексы строки и столбца, для которых создается команда
        private readonly int _rowIndex;
        private readonly int _columnIndex;

        // Массивы значений ячеек до и после выполнения команды
        private readonly object[] _previousValues;
        private readonly object[] _currentValues;

        public UndoCommand(DataGridView dataGridView, int rowIndex, int columnIndex, object[] previousValues, object[] currentValues)
        {
            _dataGridView = dataGridView;
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _previousValues = previousValues;
            _currentValues = currentValues;
        }

        // Ссылка на главную форму
        public MainForm MainForm
        {
            get => default;
            set
            {
            }
        }

        // Ссылка на стек команд для выполнения и отмены
        internal UndoStack UndoStack
        {
            get => default;
            set
            {
            }
        }

        // Метод для выполнения команды. Изменяет значение текущей ячейки на _currentValues[_columnIndex]
        public void Execute(object parameter)
        {
            var columnName = _dataGridView.Columns[_columnIndex].Name;
            var columnIndex = _dataGridView.Columns[columnName].Index;
            _dataGridView[columnIndex, _dataGridView.CurrentCell.RowIndex].Value = _currentValues[_columnIndex];
        }

        // Метод для отмены команды. Изменяет значение ячейки на _previousValues[_columnIndex]
        public void Undo()
        {
            var columnName = _dataGridView.Columns[_columnIndex].Name;
            var columnIndex = _dataGridView.Columns[columnName].Index;
            _dataGridView[columnIndex, _rowIndex].Value = _previousValues[_columnIndex];
        }

        // Проверяет, может ли команда быть выполнена. В данном случае всегда возвращает true.
        public bool CanExecute(object parameter)
        {
            return true;
        }

        // Событие, вызываемое при изменении возможности выполнения команды
        public event EventHandler CanExecuteChanged;
    }
}
