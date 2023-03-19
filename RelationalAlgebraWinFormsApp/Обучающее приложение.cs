using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Необходимые переменные для приложения
        /// </summary>
        private Table table1;
        private Table table2;
        private string selectedOperation;
        private int count = 0;
        private bool flag = true, once = true, partiallyFilled = true;
        private readonly UndoStack _undoStack = new UndoStack();
        private readonly UndoStack _undoStack2 = new UndoStack();

        /// <summary>
        /// Инициализация главной формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            table1 = new Table();
            table2 = new Table();
            selectedOperation = "";
            PopulateDataGridView(Fill.Auto);
        }

        /// <summary>
        /// Функция для автоматического заполнения двух таблиц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillInAutomaticallyButton_Click(object sender, EventArgs e)
        {
            table1 = new Table();
            table1.FillInAutomatically();
            table2 = new Table();
            table2.FillInAutomatically();
            PopulateDataGridView(Fill.Auto);
        }

        /// <summary>
        /// Функция, которая активирует ручной режим редактирования
        /// Если не была нажата кнопка автоматического заполнения, то заполнится пустыми строками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillInManuallyButton_Click(object sender, EventArgs e)
        {
            if (once)
            {
                MessageBox.Show("Активирован ручной режим. Теперь доступно редактирование полей и добавление записей в меню.");
                добавитьСтрокуToolStripMenuItem.Visible = true;
                dataGridView1.ReadOnly = false;
                dataGridView2.ReadOnly = false;
                once = false;
                FillInManuallyButton.Enabled = false;
            }
            if (table1.IsEmpty() && table2.IsEmpty())
            {
                table1.FillInManual();
                table2.FillInManual();
            }

            PopulateDataGridView(Fill.Manual);
        }

        /// <summary>
        /// Кнопка для выбора операции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOperationButton_Click(object sender, EventArgs e)
        {
            partiallyFilled = false;
            if (table1.IsEmpty() && table2.IsEmpty())
                partiallyFilled = true;


            if (partiallyFilled)
            {
                MessageBox.Show("Таблицы не созданы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OperationSelectionForm operationSelectionForm = new OperationSelectionForm();
            operationSelectionForm.ShowDialog();
            selectedOperation = operationSelectionForm.GetSelectedOperation();
        }

        /// <summary>
        /// Кнопка для отображения результата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowResultButton_Click(object sender, EventArgs e)
        {
            dynamic result = PerformOperation(selectedOperation);
            if (partiallyFilled)
            {
                MessageBox.Show("Пожалуйста, сначала выберите операцию.");
                return;
            }
            if (selectedOperation == "CartesianProduct")
            {
                List<string> columnNames = new List<string> { "ID_R1", "ФИО_R1", "КОМПАНИЯY_R1", "ID_R2", "ФИО_R2", "КОМПАНИЯ_R2" };
                DataTable dataTable = ConvertToDataTable(result, columnNames);
                ResultForm resultForm = new ResultForm(dataTable, columnNames);
                resultForm.ShowDialog();
            }
            else
            {
                List<string> columnNames = new List<string> { "ID", "ФИО", "КОМПАНИЯ" };
                DataTable dataTable = ConvertToDataTable(result, columnNames);
                ResultForm resultForm = new ResultForm(dataTable, columnNames);
                resultForm.ShowDialog();
            }

        }

        /// <summary>
        /// Функция сохранения в файл таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Tuple<int, string, string>> result = PerformOperation(selectedOperation);
            if (partiallyFilled)
            {
                MessageBox.Show("Пожалуйста, сначала выберите операцию.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    file.WriteLine("ID,ФИО,КОМПАНИЯ");
                    foreach (Tuple<int, string, string> row in result)
                    {
                        file.WriteLine("{0},{1},{2}", row.Item1, row.Item2, row.Item3);
                    }
                }
            }
        }

        /// <summary>
        /// Функция для обновления значений в таблицах главной формы
        /// </summary>
        /// <param name="mode"></param>
        private void PopulateDataGridView(Fill mode)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("NAME", "ФИО");
            dataGridView1.Columns.Add("COMPANY", "КОМПАНИЯ");
            if (mode == Fill.Auto && once)
            {
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
            }
            else
            {
                dataGridView1.ReadOnly = false;
                dataGridView2.ReadOnly = false;
            }
            dataGridView1.AllowUserToAddRows = false;
            foreach (Tuple<int, string, string> row in table1.GetRows())
            {
                dataGridView1.Rows.Add(row.Item1, row.Item2, row.Item3);
            }

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("NAME", "ФИО");
            dataGridView2.Columns.Add("COMPANY", "КОМПАНИЯ");
            dataGridView2.AllowUserToAddRows = false;
            foreach (Tuple<int, string, string> row in table2.GetRows())
            {
                dataGridView2.Rows.Add(row.Item1, row.Item2, row.Item3);
            }
        }

        /// <summary>
        /// Шаблонная функция, которая преабразует tuples в DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tuples"></param>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        private DataTable ConvertToDataTable<T>(List<T> tuples, List<string> columnNames)
        {
            DataTable dataTable = new DataTable();
            int numberOfColumns = columnNames.Count;

            for (int i = 0; i < numberOfColumns; i++)
            {
                dataTable.Columns.Add(columnNames[i]);
            }

            foreach (var tuple in tuples)
            {
                var row = dataTable.NewRow();
                object[] values = tuple.GetType().GetProperties().Select(p => p.GetValue(tuple)).ToArray();
                row.ItemArray = values;
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// Динамическая функция которая возвращает результат операции. Динамическая, так как количество столбцов могут быть разными для операций
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        private dynamic PerformOperation(string operation)
        {
            dynamic result = null;
            switch (operation)
            {
                case "Union":
                    result = RelationalOperations.Union(table1.GetRows(), table2.GetRows());
                    break;
                case "Intersection":
                    result = RelationalOperations.Intersection(table1.GetRows(), table2.GetRows());
                    break;
                case "Difference":
                    result = RelationalOperations.Difference(table1.GetRows(), table2.GetRows());
                    break;
                case "CartesianProduct":
                    result = RelationalOperations.CartesianProduct(table1.GetRows(), table2.GetRows());
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Функция нужна для отслеживания изменений в ячейках таблицы 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id;
                string name;
                string company;
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    name = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    company = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();


                    int index = table1.CheckId(id);
                    if (index >= 0 && e.ColumnIndex == 0)
                    {
                        count++;
                        // If the ID already exists, restore the previous value in the cell
                        if (count == 1 && !flag)
                        {
                            MessageBox.Show("ID уже существует!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = table1.GetRows()[table1.GetId(e.RowIndex)].Item1;
                        flag = false;
                    }
                    else
                    {
                        count = 0;
                        // Отмена операции
                        int rowIndex = e.RowIndex;
                        int columnIndex = e.ColumnIndex;
                        Tuple<int, string, string> previousValues = table1.GetRows()[e.RowIndex];
                        Tuple<int, string, string> currentValues = new Tuple<int, string, string>(id, name, company);
                        var command = new UndoCommand(dataGridView1, rowIndex, columnIndex, previousValues, currentValues);
                        _undoStack.Execute(rowIndex, columnIndex, command);

                        // If the ID doesn't exist, add the new row to the table
                        table1.RewriteRow(id, name, company, e.RowIndex);
                    }
                }
                else
                {
                    flag = true;
                    MessageBox.Show("Неверное значение ID!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // If the ID value is invalid, restore the previous value in the cell
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = table1.GetRows()[e.RowIndex].Item1;
                    return;
                }
            }
        }

        /// <summary>
        /// Функция нужна для отслеживания изменений в ячейках таблицы 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id;
                string name;
                string company;
                if (dataGridView2.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    // If the ID value is valid, retrieve the new name and company values
                    name = dataGridView2.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    company = dataGridView2.Rows[e.RowIndex].Cells[2].Value?.ToString();

                    int index = table2.CheckId(id);
                    if (index >= 0 && e.ColumnIndex == 0)
                    {
                        count++;
                        // If the ID already exists, restore the previous value in the cell
                        if (count == 1 && !flag)
                        {
                            MessageBox.Show("ID уже существует!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        dataGridView2.Rows[e.RowIndex].Cells[0].Value = table2.GetRows()[table2.GetId(e.RowIndex)].Item1;
                        flag = false;
                    }
                    else
                    {
                        count = 0;
                        // Отмена операции
                        int rowIndex = e.RowIndex;
                        int columnIndex = e.ColumnIndex;
                        Tuple<int, string, string> previousValues = table2.GetRows()[e.RowIndex];
                        Tuple<int, string, string> currentValues = new Tuple<int, string, string>(id, name, company);
                        var command = new UndoCommand(dataGridView2, rowIndex, columnIndex, previousValues, currentValues);
                        _undoStack2.Execute(rowIndex, columnIndex, command);

                        // If the ID doesn't exist, add the new row to the table
                        table2.RewriteRow(id, name, company, e.RowIndex);
                    }
                }
                else
                {
                    flag = true;
                    // If the ID value is invalid, restore the previous value in the cell
                    MessageBox.Show("Неверное значение ID!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView2.Rows[e.RowIndex].Cells[0].Value = table2.GetRows()[e.RowIndex].Item1;
                    return;

                }
            }
        }

        /// <summary>
        /// Функция нужна для копирования, вставки и отмены дейсвия в таблице 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectionToClipboard(dataGridView1);
            }
            else if (e.Control && e.KeyCode == Keys.V && dataGridView1.ReadOnly == false && dataGridView2.ReadOnly == false)
            {
                PasteClipboardValues(dataGridView1);
            }
            else if(e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                _undoStack.Undo(rowIndex, columnIndex);
                _undoStack.Pop(rowIndex, columnIndex);
            }
        }

        /// <summary>
        /// Вспопогательная функция, копирует значения из ячейки таблицы в буфер обмена
        /// </summary>
        /// <param name="dataGridView"></param>
        private void CopySelectionToClipboard(DataGridView dataGridView)
        {
            DataObject dataObject = dataGridView.GetClipboardContent();
            if (dataObject != null)
            {
                Clipboard.SetDataObject(dataObject);
            }
        }

        /// <summary>
        /// Вспопогательная функция, вставляет значения из буфера обмена в ячейку таблицы
        /// </summary>
        /// <param name="dataGridView"></param>
        private void PasteClipboardValues(DataGridView dataGridView)
        {
            string clipboardData = Clipboard.GetText();
            string[] lines = clipboardData.Split('\n');

            int row = dataGridView.SelectedCells[0].RowIndex;
            int col = dataGridView.SelectedCells[0].ColumnIndex;

            for (int i = 0; i < lines.Length; i++)
            {
                if (row + i >= dataGridView.RowCount)
                {
                    break;
                }

                string[] cells = lines[i].Split('\t');
                for (int j = 0; j < cells.Length; j++)
                {
                    if (col + j >= dataGridView.ColumnCount)
                    {
                        break;
                    }

                    dataGridView[col + j, row + i].Value = cells[j];
                }
            }
        }

        /// <summary>
        /// Функция добавления строки в таблицу 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[0];
        }

        /// <summary>
        /// Функция добавления строки в таблицу 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[0];
        }

        /// <summary>
        /// Функция нужна для копирования, вставки и отмены дейсвия в таблице 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectionToClipboard(dataGridView2);
            }
            else if (e.Control && e.KeyCode == Keys.V && dataGridView1.ReadOnly == false && dataGridView2.ReadOnly == false)
            {
                PasteClipboardValues(dataGridView2);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                int columnIndex = dataGridView2.CurrentCell.ColumnIndex;
                _undoStack.Undo(rowIndex, columnIndex);
                _undoStack.Pop(rowIndex, columnIndex);
            }
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Обучающее приложение по \"Реляционной алгебре\"\nID - первинчый ключ. Для редактирования таблицы нужно активировать режим \"Ручное заполнение\"");
        }

        /// <summary>
        /// Функция добавления строки для левой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void леваяТаблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table1.AddRow();
            PopulateDataGridView(Fill.Manual);
        }

        /// <summary>
        /// Функция добавления строки для правой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void праваяТаблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            table2.AddRow();
            PopulateDataGridView(Fill.Manual);
        }
    }
}

enum Fill 
{ 
    Auto,
    Manual
};
