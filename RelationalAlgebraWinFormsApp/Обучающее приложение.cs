using System;
using System.Collections;
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
        private string selectedOperation, newColmName;
        private int forms;
        private bool once = true, partiallyFilled = true;
        private readonly UndoStack _undoStack = new UndoStack();
        private readonly UndoStack _undoStack2 = new UndoStack();
        private readonly string[] namesColumns = new string[3] { "ID", "ФИО", "КОМПАНИЯ" };
        public delegate Table OperationDelegate(Table table1, Table table2, int forms);

        private Dictionary<string, OperationDelegate> operationDelegates;


        /// <summary>
        /// Инициализация главной формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            table1 = new Table(namesColumns);
            table2 = new Table(namesColumns);
            selectedOperation = "";
            PopulateDataGridView(Fill.Auto);

            // Form properties
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Font = new Font("Segoe UI", 9);
            this.ForeColor = Color.Black;

            // Инициализируем делегат
            operationDelegates = new Dictionary<string, OperationDelegate> {
        { "Union", (table1, table2, forms) => RelationalOperations.Union(table1, table2)},
        { "Intersection", (table1, table2, forms) => RelationalOperations.Intersection(table1, table2) },
        { "Difference", (table1, table2, forms) => RelationalOperations.Difference(table1, table2, forms) },
        { "CartesianProduct", (table1, table2, forms) => RelationalOperations.CartesianProduct(table1, table2) } };

        }

        /// <summary>
        /// Функция для автоматического заполнения двух таблиц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillInAutomaticallyButton_Click(object sender, EventArgs e)
        {
            table1 = new Table(namesColumns);
            table1.FillInAutomatically();
            table2 = new Table(namesColumns);
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
                добавитьToolStripMenuItem.Visible = true;
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
            if (operationSelectionForm.GetForms() != "")
                forms = Convert.ToInt32(operationSelectionForm.GetForms());
        }

        /// <summary>
        /// Кнопка для отображения результата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowResultButton_Click(object sender, EventArgs e)
        {
            Table result = PerformOperation(selectedOperation);
            if (partiallyFilled)
            {
                MessageBox.Show("Пожалуйста, сначала выберите операцию.");
                return;
            }
            ResultForm resultForm = new ResultForm(result);
            resultForm.ShowDialog();
        }

        /// <summary>
        /// Функция сохранения в файл таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table result = PerformOperation(selectedOperation);
           
        }

        /// <summary>
        /// Функция для обновления значений в таблицах главной формы
        /// </summary>
        /// <param name="mode"></param>
        private void PopulateDataGridView(Fill mode)
        {
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 255, 255),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9),
                SelectionBackColor = Color.FromArgb(220, 235, 252),
                SelectionForeColor = Color.Black
            };

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle;
            dataGridView1.GridColor = Color.FromArgb(100, 100, 100);
            dataGridView1.BackgroundColor = Color.FromArgb(40, 40, 40);
            dataGridView1.BorderStyle = BorderStyle.None;


            dataGridView2.ColumnHeadersDefaultCellStyle = headerStyle;
            dataGridView2.DefaultCellStyle = dataGridViewCellStyle;
            dataGridView2.GridColor = Color.FromArgb(100, 100, 100);
            dataGridView2.BackgroundColor = Color.FromArgb(40, 40, 40);
            dataGridView2.BorderStyle = BorderStyle.None;


            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            foreach (var item in table1.columsNames)
            {
                dataGridView1.Columns.Add(item, item);
            }

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

            foreach (var value in table1.data_obj)
            {
                dataGridView1.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            foreach (var item in table2.columsNames)
            {
                dataGridView2.Columns.Add(item, item);
            }

            dataGridView2.AllowUserToAddRows = false;
            foreach (var value in table2.data_obj)
            {
                dataGridView2.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }

        }


        /// <summary>
        /// Динамическая функция которая возвращает результат операции. Динамическая, так как количество столбцов могут быть разными для операций
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        private Table PerformOperation(string operation)
        {
            if (operationDelegates.ContainsKey(operation))
            {
                return operationDelegates[operation](table1, table2, forms);
            }
            else
            {
                MessageBox.Show($"Неизвестная операция: {operation}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Table Union(Table table1, Table table2)
        {
            Table result = RelationalOperations.Union(table1, table2);
            return result;
        }

        public static Table Intersection(Table table1, Table table2)
        {
            Table result = RelationalOperations.Intersection(table1, table2);
            return result;
        }

        public static Table Difference(Table table1, Table table2, int forms)
        {
            Table result = RelationalOperations.Difference(table1, table2, forms);
            return result;
        }

        public static Table CartesianProduct(Table table1, Table table2)
        {
            Table result = RelationalOperations.CartesianProduct(table1, table2);
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

                // Retrieve the ID value from the first column
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    // Retrieve the values of all other columns
                    List<object> values = new List<object>();
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                    {
                        values.Add(dataGridView1.Rows[e.RowIndex].Cells[i].Value);
                    }

                    // Create an undo command and add it to the stack
                    int rowIndex = e.RowIndex;
                    string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table1.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(dataGridView1, rowIndex, e.ColumnIndex, previousValues, currentValues);
                    _undoStack.Execute(rowIndex, columnName, command);

                    // Update the corresponding row in the table
                    object[] rowValues = new object[values.Count + 1];
                    rowValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        rowValues[i + 1] = values[i];
                    }
                    table1.RewriteRow(rowValues, e.RowIndex);
                }
                else
                {
                    MessageBox.Show("Неверное значение ID!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = table1.GetRows()[e.RowIndex][0];
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

                // Retrieve the ID value from the first column
                if (dataGridView2.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    // Retrieve the values of all other columns
                    List<object> values = new List<object>();
                    for (int i = 1; i < dataGridView2.Columns.Count; i++)
                    {
                        values.Add(dataGridView2.Rows[e.RowIndex].Cells[i].Value);
                    }

                    // Create an undo command and add it to the stack
                    int rowIndex = e.RowIndex;
                    string columnName = dataGridView2.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table2.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(dataGridView2, rowIndex, e.ColumnIndex, previousValues, currentValues);
                    _undoStack2.Execute(rowIndex, columnName, command);

                    // Update the corresponding row in the table
                    object[] rowValues = new object[values.Count + 1];
                    rowValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        rowValues[i + 1] = values[i];
                    }
                    table2.RewriteRow(rowValues, e.RowIndex);
                }
                else
                {
                    MessageBox.Show("Неверное значение ID!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView2.Rows[e.RowIndex].Cells[0].Value = table2.GetRows()[e.RowIndex][0];
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
                string columnIndex = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
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
                string columnIndex = dataGridView2.Columns[dataGridView2.CurrentCell.ColumnIndex].Name;
                _undoStack2.Undo(rowIndex, columnIndex);
                _undoStack2.Pop(rowIndex, columnIndex);
            }
        }

        private void леваяТаблицаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddColum newColumName = new AddColum();
            newColumName.ShowDialog();
            newColmName = newColumName.getColumName();
            string cleanedString = newColmName.Trim().Replace("\n", "");
            if (cleanedString != "")
            {
                for (int i = 0; i < table1.columsNames.Length; i++)
                {
                    if (cleanedString == table1.columsNames[i])
                    {
                        MessageBox.Show("Столбец с таким названием уже есть !");
                        return;
                    }
                }
                table1.AddColumn(cleanedString);
                PopulateDataGridView(Fill.Manual);
            }
        }

        private void праваяТаблицаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddColum newColumName = new AddColum();
            newColumName.ShowDialog();
            newColmName = newColumName.getColumName();
            string cleanedString = newColmName.Trim().Replace("\n", "");
            if (cleanedString != "")
            {
                for (int i = 0; i < table2.columsNames.Length; i++)
                {
                    if (cleanedString == table2.columsNames[i])
                    {
                        MessageBox.Show("Столбец с таким названием уже есть !");
                        return;
                    }
                }
                table2.AddColumn(cleanedString);
                PopulateDataGridView(Fill.Manual);
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
