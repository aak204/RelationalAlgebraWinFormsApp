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
        private readonly DataGridView _dataGridView;
        private readonly int _rowIndex;
        private readonly int _columnIndex;
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

        public void Execute(object parameter)
        {
            var columnName = _dataGridView.Columns[_columnIndex].Name;
            var columnIndex = _dataGridView.Columns[columnName].Index;
            _dataGridView[columnIndex, _dataGridView.CurrentCell.RowIndex].Value = _currentValues[_columnIndex];
        }

        public void Undo()
        {
            var columnName = _dataGridView.Columns[_columnIndex].Name;
            var columnIndex = _dataGridView.Columns[columnName].Index;
            _dataGridView[columnIndex, _rowIndex].Value = _previousValues[_columnIndex];
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
