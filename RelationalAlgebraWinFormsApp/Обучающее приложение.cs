using Newtonsoft.Json;
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
using System.Xml;

namespace RelationalAlgebraWinFormsApp
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Необходимые переменные для приложения
        /// </summary>
        private Table table1;
        private Table table2;
        private string selectedOperation, newColmName;
        private int forms, auto;
        private bool once = true, partiallyFilled = true;
        private readonly UndoStack _undoStack = new UndoStack();
        private readonly UndoStack _undoStack2 = new UndoStack();
        private readonly string[] namesColumns = new string[3] { "ID", "ФИО", "КОМПАНИЯ" };
        public delegate Table OperationDelegate(Table table1, Table table2, int forms, string columnName);
        public OperationSelectionForm operationSelectionForm;
        private Dictionary<string, OperationDelegate> operationDelegates;
        private bool doNotShowSaveMsgBox = false; // Флаг, который хранит, хотим ли мы показывать MessageBox
        private string lastOperation = null; // Хранит последнюю выполненную операцию
        private bool flag = false; // Хранит последнюю выполненную операцию


        /// <summary>
        /// Инициализация главной формы
        /// </summary>
        public MainForm()
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
    { "Union", (table1, table2, forms, columnName) => RelationalOperations.Union(table1, table2)},
    { "Intersection", (table1, table2, forms, columnName) => RelationalOperations.Intersection(table1, table2) },
    { "Difference", (table1, table2, forms, columnName) => RelationalOperations.Difference(table1, table2, forms) },
    { "CartesianProduct", (table1, table2, forms, columnName) => RelationalOperations.CartesianProduct(table1, table2) },
    //{ "Select", (table1, table2, forms, columnName) => RelationalOperations.Select(table1, table2, forms, columnName) },
    //{ "Project", (table1, table2, forms, columnName) => RelationalOperations.Project(table1, table2, forms, columnName) },
    { "Divide", (tables, table2, forms, columnName) => RelationalOperations.Divide(table1, table2) },
    { "InnerJoin", (table1, table2, forms, columnName) => RelationalOperations.InnerJoin(table1, table2, columnName) },
    { "LeftJoin", (table1, table2, forms, columnName) => RelationalOperations.LeftJoin(table1, table2, columnName) },
    { "RightJoin", (table1, table2, forms, columnName) => RelationalOperations.RightJoin(table1, table2, columnName) },
};



        }

        /// <summary>
        /// Функция для автоматического заполнения двух таблиц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillInAutomaticallyButton_Click(object sender, EventArgs e)
        {
            auto++;
            if (auto > 1)
            {
                DialogResult result = MessageBox.Show("Внимание! При повторном заполнении все сбросится до исходного состояния", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

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
            operationSelectionForm = new OperationSelectionForm(table1, table2);
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
        private void DisplayResultTableDataGridView(Table result)
        {
            // Create a new DataGridView
            DataGridView resultDataGridView = new DataGridView();

            // Set the design for DataGridView
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
            resultDataGridView.ColumnHeadersDefaultCellStyle = headerStyle;
            resultDataGridView.DefaultCellStyle = dataGridViewCellStyle;
            resultDataGridView.GridColor = Color.FromArgb(100, 100, 100);
            resultDataGridView.BackgroundColor = Color.FromArgb(40, 40, 40);
            resultDataGridView.BorderStyle = BorderStyle.None;
            resultDataGridView.ReadOnly = true;
            // Set DataGridView properties
            resultDataGridView.Dock = DockStyle.Fill;
            resultDataGridView.AllowUserToAddRows = false;
            resultDataGridView.AllowUserToDeleteRows = false;
            resultDataGridView.AllowUserToResizeRows = false;
            resultDataGridView.RowHeadersVisible = false;
            resultDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resultDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Create columns in the DataGridView
            foreach (var item in result.columnsNames)
            {
                resultDataGridView.Columns.Add(item, item);
            }

            // Add rows to the DataGridView
            foreach (var value in result.data_obj)
            {
                resultDataGridView.Rows.Add(value);
            }

            // Clear the ResultPanel and add the new DataGridView to it
            ResultPanel.Controls.Clear();
            ResultPanel.Controls.Add(resultDataGridView);

            // Set the ResultPanel properties
            ResultPanel.AutoSize = true;
            ResultPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }

        private void ShowResultButton_Click(object sender, EventArgs e)
        {
            if (partiallyFilled)
            {
                MessageBox.Show("Пожалуйста, сначала выберите операцию.");
                return;
            }
            Table result = PerformOperation(selectedOperation);

            if (result != null)
            {
                DisplayResultTableDataGridView(result);
                if (!doNotShowSaveMsgBox) // если флаг не установлен, показываем MessageBox
                {
                    if (selectedOperation == lastOperation)
                    {
                        return;
                    }
                    lastOperation = selectedOperation; // обновляем последнюю выполненную операцию
                    DialogResult dialogResult = MessageBox.Show("Хотите ли вы сохранить результат в файл?", "Сохранение результата", MessageBoxButtons.YesNoCancel);

                    if (dialogResult == DialogResult.Yes)
                    {
                        SaveResultToFile(result);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        doNotShowSaveMsgBox = true; // если пользователь нажимает "Больше не показывать", устанавливаем флаг
                    }
                }
            }
        }

        private void SaveResultToFile(Table result)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text File|*.txt|JSON File|*.json",
                Title = "Сохранить результат"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        SaveResultToTxt(result, saveFileDialog.FileName);
                        break;
                    case 2:
                        SaveResultToJson(result, saveFileDialog.FileName);
                        break;
                }
            }
        }

        private void SaveResultToTxt(Table result, string fileName)
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Записываем имена столбцов
            foreach (var columnName in result.columnsNames)
            {
                stringBuilder.Append($"{columnName}\t");
            }
            stringBuilder.AppendLine();

            // Записываем данные
            foreach (var row in result.data_obj)
            {
                foreach (var cell in row)
                {
                    stringBuilder.Append($"{cell.ToString()}\t");
                }
                stringBuilder.AppendLine();
            }

            File.WriteAllText(fileName, stringBuilder.ToString());
        }

        private void SaveResultToJson(Table result, string fileName)
        {
            string json = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        /// <summary>
        /// Функция сохранения в файл таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Table result = PerformOperation(selectedOperation);

            if (result != null)
            {
                SaveResultToFile(result);
            }
        }

        /// <summary>
        /// Функция для обновления значений в таблицах главной формы
        /// </summary>
        /// <param name="mode"></param>
        private void PopulateDataGridView(Fill mode)
        {
            DataGridViewDesignHelper.ApplyDesign(TabelOne);
            DataGridViewDesignHelper.ApplyDesign(TabelTwo);

            TabelOne.Rows.Clear();
            TabelOne.Columns.Clear();

            foreach (var item in table1.columnsNames)
            {
                TabelOne.Columns.Add(item, item);
            }

            TabelOne.AllowUserToAddRows = false;

            foreach (var value in table1.data_obj)
            {
                TabelOne.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }

            TabelTwo.Rows.Clear();
            TabelTwo.Columns.Clear();

            foreach (var item in table2.columnsNames)
            {
                TabelTwo.Columns.Add(item, item);
            }

            TabelTwo.AllowUserToAddRows = false;
            foreach (var value in table2.data_obj)
            {
                TabelTwo.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }
        }


        /// <summary>
        /// Функция которая возвращает результат операции. Динамическая, так как количество столбцов могут быть разными для операций
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        private Table PerformOperation(string operation)
        {
            if (operationDelegates.ContainsKey(operation))
            {
                
                if (operation == "InnerJoin" || operation == "LeftJoin" || operation == "RightJoin")
                {
                    string columnName = operationSelectionForm.GetSelectedAttribute();
                    if (columnName != null)
                        return operationDelegates[operation](table1, table2, forms, columnName);
                    else
                        return null;
                }
                else
                {
                    string columnName = "";
                    return operationDelegates[operation](table1, table2, forms, columnName);
                }
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

        public static Table InnerJoin(Table table1, Table table2, string columnNameJoin)
        {
            Table result = RelationalOperations.InnerJoin(table1, table2, columnNameJoin);
            return result;
        }

        public static Table LeftJoin(Table table1, Table table2, string columnNameJoin)
        {
            Table result = RelationalOperations.LeftJoin(table1, table2, columnNameJoin);
            return result;
        }

        public static Table RightJoin(Table table1, Table table2, string columnNameJoin)
        {
            Table result = RelationalOperations.RightJoin(table1, table2, columnNameJoin);
            return result;
        }

        public static Table Divide(Table table1, Table table2)
        {
            Table result = RelationalOperations.Divide(table1, table2);
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

                // Retrieve the ID value from the first column
                if (TabelOne.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(TabelOne.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    // Retrieve the values of all other columns
                    List<object> values = new List<object>();
                    for (int i = 1; i < TabelOne.Columns.Count; i++)
                    {
                        values.Add(TabelOne.Rows[e.RowIndex].Cells[i].Value);
                    }

                    // Create an undo command and add it to the stack
                    int rowIndex = e.RowIndex;
                    string columnName = TabelOne.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table1.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(TabelOne, rowIndex, e.ColumnIndex, previousValues, currentValues);
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
                    TabelOne.Rows[e.RowIndex].Cells[0].Value = table1.GetRows()[e.RowIndex][0];
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

                // Retrieve the ID value from the first column
                if (TabelTwo.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(TabelTwo.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    // Retrieve the values of all other columns
                    List<object> values = new List<object>();
                    for (int i = 1; i < TabelTwo.Columns.Count; i++)
                    {
                        values.Add(TabelTwo.Rows[e.RowIndex].Cells[i].Value);
                    }

                    // Create an undo command and add it to the stack
                    int rowIndex = e.RowIndex;
                    string columnName = TabelTwo.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table2.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(TabelTwo, rowIndex, e.ColumnIndex, previousValues, currentValues);
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
                    TabelTwo.Rows[e.RowIndex].Cells[0].Value = table2.GetRows()[e.RowIndex][0];
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
                CopySelectionToClipboard(TabelOne);
            }
            else if (e.Control && e.KeyCode == Keys.V && TabelOne.ReadOnly == false && TabelTwo.ReadOnly == false)
            {
                PasteClipboardValues(TabelOne);
            }
            else if(e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = TabelOne.CurrentCell.RowIndex;
                string columnIndex = TabelOne.Columns[TabelOne.CurrentCell.ColumnIndex].Name;
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
            TabelOne.CurrentCell = TabelOne.Rows[e.RowIndex].Cells[0];
        }

        /// <summary>
        /// Функция добавления строки в таблицу 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            TabelTwo.CurrentCell = TabelTwo.Rows[e.RowIndex].Cells[0];
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
                CopySelectionToClipboard(TabelTwo);
            }
            else if (e.Control && e.KeyCode == Keys.V && TabelOne.ReadOnly == false && TabelTwo.ReadOnly == false)
            {
                PasteClipboardValues(TabelTwo);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = TabelTwo.CurrentCell.RowIndex;
                string columnIndex = TabelTwo.Columns[TabelTwo.CurrentCell.ColumnIndex].Name;
                _undoStack2.Undo(rowIndex, columnIndex);
                _undoStack2.Pop(rowIndex, columnIndex);
            }
        }

        private void леваяТаблицаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!table1.IsEmpty())
            {
                AddColum newColumName = new AddColum(flag = false);
                newColumName.ShowDialog();
                newColmName = newColumName.getColumName();
                string cleanedString = newColmName.Trim().Replace("\n", "");
                if (cleanedString != "")
                {
                    for (int i = 0; i < table1.columnsNames.Length; i++)
                    {
                        if (cleanedString == table1.columnsNames[i])
                        {
                            MessageBox.Show("Столбец с таким названием уже есть !");
                            return;
                        }
                    }
                    table1.AddColumn(cleanedString);
                    PopulateDataGridView(Fill.Manual);
                }
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void праваяТаблицаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!table2.IsEmpty())
            {
                AddColum newColumName = new AddColum(flag = false);
                newColumName.ShowDialog();
                newColmName = newColumName.getColumName();
                string cleanedString = newColmName.Trim().Replace("\n", "");
                if (cleanedString != "")
                {
                    for (int i = 0; i < table2.columnsNames.Length; i++)
                    {
                        if (cleanedString == table2.columnsNames[i])
                        {
                            MessageBox.Show("Столбец с таким названием уже есть !");
                            return;
                        }
                    }
                    table2.AddColumn(cleanedString);
                    PopulateDataGridView(Fill.Manual);
                }
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Обучающее приложение по \"Реляционной алгебре\"\n");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Функция добавления строки для левой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void леваяТаблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!table1.IsEmpty())
            {
                table1.AddRow();
                PopulateDataGridView(Fill.Manual);
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Функция добавления строки для правой таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void праваяТаблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!table2.IsEmpty())
            {
                table2.AddRow();
                PopulateDataGridView(Fill.Manual);
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

enum Fill 
{ 
    Auto,
    Manual
};