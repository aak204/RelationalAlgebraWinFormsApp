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
        private readonly Tuple<int, string, string> _previousValues;
        private readonly Tuple<int, string, string> _currentValues;

        public UndoCommand(DataGridView dataGridView, int rowIndex, int columnIndex, Tuple<int, string, string> previousValues, Tuple<int, string, string> currentValues)
        {
            _dataGridView = dataGridView;
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _previousValues = previousValues;
            _currentValues = currentValues;
        }

        public void Execute(object parameter)
        {
            switch (_columnIndex)
            { 
                case 0:
                    _dataGridView[_dataGridView.Columns["id"].Index, _dataGridView.CurrentCell.RowIndex].Value = _currentValues.Item1;
                    break;
                case 1:
                    _dataGridView[_dataGridView.Columns["name"].Index, _dataGridView.CurrentCell.RowIndex].Value = _currentValues.Item2;
                    break;
                case 2:
                    _dataGridView[_dataGridView.Columns["company"].Index, _dataGridView.CurrentCell.RowIndex].Value = _currentValues.Item3;
                    break;
            }
        }

        public void Undo()
        {
            switch (_columnIndex)
            {
                case 0:
                    _dataGridView[_dataGridView.Columns["id"].Index, _rowIndex].Value = _previousValues.Item1;
                    break;
                case 1:
                    _dataGridView[_dataGridView.Columns["name"].Index, _rowIndex].Value = _previousValues.Item2;
                    break;
                case 2:
                    _dataGridView[_dataGridView.Columns["company"].Index, _rowIndex].Value = _previousValues.Item3;
                    break;
            }

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
