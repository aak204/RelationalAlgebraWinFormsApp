using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        private Table table3;
        private Table result;
        private List<int> checkBoxOrder;
        private string[] AttributeNames;
        private static Dictionary<int, Table> tableMapping;
        private string selectedOperation, newColmName, form1, form2, columnName, ColNameSelect, Operator, Condition;
        private int auto;
        private bool once = true, partiallyFilled = true, lastOperationSuccess;
        private readonly UndoStack _undoStack = new UndoStack();
        private readonly UndoStack _undoStack2 = new UndoStack();
        private readonly UndoStack _undoStack3 = new UndoStack();
        private readonly string[] namesColumns = new string[3] { "ID", "ФИО", "Компания" };
        private readonly string[] namesColumns2 = new string[3] { "ID_Проекта", "Название_проекта", "Компания" };
        private readonly string[] namesColumns3 = new string[3] { "ID_клиента", "Имя_Клиента", "Компания" };
        public delegate Table OperationDelegate(Table table1, Table table2, Table table3, string form1, string form2, string columnName, string[] AttrNames, string ColNameSelect, string Operator, string Condition);
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
            table2 = new Table(namesColumns2);
            table3 = new Table(namesColumns3);
            selectedOperation = "";
            PopulateDataGridView(Fill.Auto);

            // Form properties
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Font = new Font("Segoe UI", 9);
            this.ForeColor = Color.Black;

            // Инициализируем делегат
            operationDelegates = new Dictionary<string, OperationDelegate> {
    { "Union", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? Union(table1, table2) :
        form1 == "B" && form2 == "C" ? Union(table2, table3) :
        Union(table1, table3)},
    { "Intersection", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? Intersection(table1, table2) :
        form1 == "B" && form2 == "C" ? Intersection(table2, table3) :
        Intersection(table1, table3)},
    { "Difference", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? Difference(table1, table2) :
        form1 == "B" && form2 == "C" ? Difference(table2, table3) :
        Difference(table1, table3)},
    { "CartesianProduct", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? CartesianProduct(table1, table2) :
        form1 == "B" && form2 == "C" ? CartesianProduct(table2, table3) :
        CartesianProduct(table1, table3)},
    { "Divide", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? Divide(table1, table2) :
        form1 == "B" && form2 == "C" ? Divide(table2, table3) :
        Divide(table1, table3)},
    { "InnerJoin", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? InnerJoin(table1, table2, columnName) :
        form1 == "B" && form2 == "C" ? InnerJoin(table2, table3, columnName) :
        InnerJoin(table1, table3, columnName)},
    { "LeftJoin", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? LeftJoin(table1, table2, columnName) :
        form1 == "B" && form2 == "C" ? LeftJoin(table2, table3, columnName) :
        LeftJoin(table1, table3, columnName)},
    { "RightJoin", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? RightJoin(table1, table2, columnName) :
        form1 == "B" && form2 == "C" ? RightJoin(table2, table3, columnName) :
        RightJoin(table1, table3, columnName)},
    { "FullJoin", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" && form2 == "B" ? FullJoin(table1, table2, columnName) :
        form1 == "B" && form2 == "C" ? FullJoin(table2, table3, columnName) :
        FullJoin(table1, table3, columnName)},
    { "Projection", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" ? Projection(table1, AttrNames) :
        form1 == "B" ? Projection(table2, AttrNames) :
        Projection(table3, AttrNames)},
    { "Select", (table1, table2, table3, form1, form2, columnName, AttrNames, ColNameSelect, Operator, Condition) =>
        form1 == "A" ? Select(table1, ColNameSelect, Operator, Condition) :
        form1 == "B" ? Select(table2, ColNameSelect, Operator, Condition) :
        Select(table3, ColNameSelect, Operator, Condition)},
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
            table2 = new Table(namesColumns2);
            table2.FillInAutomatically2();
            table3 = new Table(namesColumns3);
            table3.FillInAutomatically3();
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
            if (table1.IsEmpty() && table2.IsEmpty() && table3.IsEmpty())
            {
                table1.FillInManual();
                table2.FillInManual();
                table3.FillInManual();
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
            operationSelectionForm = new OperationSelectionForm(table1, table2, table3, this);
            operationSelectionForm.ShowDialog();
        }

        /// <summary>
        /// Кнопка для отображения результата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayResultTableDataGridView(Table result)
        {
            DataGridView resultDataGridView = new DataGridView();

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

            resultDataGridView.Dock = DockStyle.Fill;
            resultDataGridView.AllowUserToAddRows = false;
            resultDataGridView.AllowUserToDeleteRows = false;
            resultDataGridView.AllowUserToResizeRows = false;
            resultDataGridView.RowHeadersVisible = false;
            resultDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resultDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            resultDataGridView.ScrollBars = ScrollBars.Both;

            foreach (DataGridViewColumn column in resultDataGridView.Columns)
            {
                column.MinimumWidth = 300; // Минимальная ширина
            }

            foreach (var item in result.columnsNames)
            {
                resultDataGridView.Columns.Add(item, item);
            }

            foreach (var value in result.data_obj)
            {
                resultDataGridView.Rows.Add(value);
            }
            RowCountResult.Text = "Количество строк - " + resultDataGridView.Rows.Count.ToString();

            ResultPanel.Controls.Clear();
            ResultPanel.Controls.Add(resultDataGridView);

            ResultPanel.AutoSize = true;
            ResultPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }

        // Общий метод для выполнения операции и отображения результатов
        public void PerformOperationAndDisplayResult(string select)
        {
            selectedOperation = select;
            result = PerformOperation(selectedOperation);
            lastOperationSuccess = result != null;
            if (result != null)
            {
                DisplayResultTableDataGridView(result);
                if (!doNotShowSaveMsgBox)
                {
                    if (selectedOperation == lastOperation)
                    {
                        return;
                    }
                    lastOperation = selectedOperation;

                    DialogResult dialogResult = CustomMessageBox.Show();

                    if (dialogResult == DialogResult.Yes)
                    {
                        SaveResultToFile(result);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        doNotShowSaveMsgBox = true;
                    }
                }
            }
        }

        public void DisplayResultComb(Table result, bool res)
        {
            lastOperationSuccess = result != null;
            if (result != null)
            {
                DisplayResultTableDataGridView(result);
                if (res)
                {
                    if (!doNotShowSaveMsgBox)
                    {
                        if (selectedOperation == lastOperation)
                        {
                            return;
                        }
                        lastOperation = selectedOperation;

                        DialogResult dialogResult = CustomMessageBox.Show();

                        if (dialogResult == DialogResult.Yes)
                        {
                            SaveResultToFile(result);
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            doNotShowSaveMsgBox = true;
                        }
                    }

                }
            }
        }

        private void SaveResultToFile(Table result)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV File|*.csv|JSON File|*.json",
                Title = "Сохранить результат"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        SaveResultToCsv(result, saveFileDialog.FileName);
                        break;
                    case 2:
                        SaveResultToJson(result, saveFileDialog.FileName);
                        break;
                }
            }
        }


        private void SaveResultToCsv(Table result, string fileName)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.GetEncoding("windows-1251")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                // Записываем заголовки
                foreach (var header in result.columnsNames)
                {
                    csv.WriteField(header);
                }
                csv.NextRecord();

                // Записываем строки
                foreach (var row in result.data_obj)
                {
                    foreach (var cell in row)
                    {
                        csv.WriteField(cell.ToString());
                    }
                    csv.NextRecord();
                }
            }
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
            DataGridViewDesignHelper.ApplyDesign(TableOne);
            DataGridViewDesignHelper.ApplyDesign(TableTwo);
            DataGridViewDesignHelper.ApplyDesign(TableThree);

            TableOne.Rows.Clear();
            TableOne.Columns.Clear();

            foreach (var item in table1.columnsNames)
            {
                TableOne.Columns.Add(item, item);
            }

            TableOne.AllowUserToAddRows = false;

            foreach (var value in table1.data_obj)
            {
                TableOne.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }

            RowCouneTableOne.Text = "Количество строк - " + TableOne.Rows.Count.ToString();

            TableTwo.Rows.Clear();
            TableTwo.Columns.Clear();

            foreach (var item in table2.columnsNames)
            {
                TableTwo.Columns.Add(item, item);
            }

            TableTwo.AllowUserToAddRows = false;
            foreach (var value in table2.data_obj)
            {
                TableTwo.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }

            RowCouneTableTwo.Text = "Количество строк - " + TableTwo.Rows.Count.ToString();

            TableThree.Rows.Clear();
            TableThree.Columns.Clear();

            foreach (var item in table3.columnsNames)
            {
                TableThree.Columns.Add(item, item);
            }

            TableThree.AllowUserToAddRows = false;
            foreach (var value in table3.data_obj)
            {
                TableThree.Rows.Add(Array.ConvertAll(value, e => e.ToString()));
            }

            RowCouneTableThree.Text = "Количество строк - " + TableThree.Rows.Count.ToString();
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
                selectedOperation = operationSelectionForm.GetSelectedOperation();
                if (operationSelectionForm.GetForm1() != null && operationSelectionForm.GetForm2() != null && selectedOperation != "Projection")
                {
                    form1 = operationSelectionForm.GetForm1();
                    form2 = operationSelectionForm.GetForm2();
                }
                else if ((selectedOperation == "Projection" || selectedOperation == "Select") && operationSelectionForm.GetForm1() != null)
                    form1 = operationSelectionForm.GetForm1();
                checkBoxOrder = operationSelectionForm.GetFormOrder();
                columnName = operationSelectionForm.GetSelectedAttribute();
                AttributeNames = operationSelectionForm.GetColNames();
                (ColNameSelect, Operator, Condition) = operationSelectionForm.GetCondition();
                if (operation == "InnerJoin" || operation == "LeftJoin" || operation == "RightJoin" || operation == "FullJoin")
                {
                    AttributeNames = null;
                    ColNameSelect = null;
                    Operator = null;
                    Condition = null;
                    if (columnName != null)
                        return operationDelegates[operation](table1, table2, table3, form1, form2, columnName, AttributeNames, ColNameSelect, Operator, Condition);
                    else
                        return null;
                }
                else if (operation == "Projection")
                {
                    ColNameSelect = null;
                    Operator = null;
                    Condition = null;
                    if (AttributeNames.Length > 0 && AttributeNames[0] != "")
                        return operationDelegates[operation](table1, table2, table3, form1, form2, columnName, AttributeNames, ColNameSelect, Operator, Condition);
                    else
                        return null;
                }
                else if (operation == "Select")
                {
                    AttributeNames = null;
                    if (ColNameSelect != null && Operator != null && Condition != null)
                        return operationDelegates[operation](table1, table2, table3, form1, form2, columnName, AttributeNames, ColNameSelect, Operator, Condition);
                    else
                        return null;
                }
                else
                {
                    ColNameSelect = null;
                    Operator = null;
                    Condition = null;
                    AttributeNames = null;
                    columnName = "";
                    return operationDelegates[operation](table1, table2, table3, form1, form2, columnName, AttributeNames, ColNameSelect, Operator, Condition);
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
            Table results = RelationalOperations.Union(table1, table2);
            return results;
        }

        public (Table, Table) GetTables()
        {
            tableMapping = new Dictionary<int, Table>()
    {
        { 1, table1 },
        { 2, table2 },
        { 3, table3 }
    };

            table1 = tableMapping[checkBoxOrder[0]];
            table2 = tableMapping[checkBoxOrder[1]];

            return (table1, table2);
        }


        public Table Intersection(Table table1, Table table2)
        {
            Table results = RelationalOperations.Intersection(table1, table2);
            return results;
        }

        public Table Difference(Table table1, Table table2)
        {
            (table1, table2) = GetTables();
            Table results = RelationalOperations.Difference(table1, table2);
            return results;
        }

        public Table CartesianProduct(Table table1, Table table2)
        {
            Table results = RelationalOperations.CartesianProduct(table1, table2);
            return results;
        }

        public Table InnerJoin(Table table1, Table table2, string columnNameJoin)
        {
            Table results = RelationalOperations.InnerJoin(table1, table2, columnNameJoin);
            return results;
        }

        public Table FullJoin(Table table1, Table table2, string columnNameJoin)
        {
            Table results = RelationalOperations.FullJoin(table1, table2, columnNameJoin);
            return results;
        }

        public Table LeftJoin(Table table1, Table table2, string columnNameJoin)
        {
            (table1, table2) = GetTables();
            Table results = RelationalOperations.LeftJoin(table1, table2, columnNameJoin);
            return results;
        }

        public Table RightJoin(Table table1, Table table2, string columnNameJoin)
        {
            (table1, table2) = GetTables();
            Table results = RelationalOperations.RightJoin(table1, table2, columnNameJoin);
            return results;
        }

        public Table Divide(Table table1, Table table2)
        {
            Table results = RelationalOperations.Divide(table1, table2);
            return results;
        }

        public Table Projection(Table table1, string[] AttributeNames)
        {
            Table results = RelationalOperations.Projection(table1, AttributeNames);
            return results;
        }

        public Table Select(Table table1, string ColNameSelect, string Operator, string Condition)
        {
            Table results = RelationalOperations.Selection(table1, ColNameSelect, Operator, Condition);
            return results;
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

                if (TableOne.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(TableOne.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    List<object> values = new List<object>();
                    for (int i = 1; i < TableOne.Columns.Count; i++)
                    {
                        values.Add(TableOne.Rows[e.RowIndex].Cells[i].Value);
                    }

                    int rowIndex = e.RowIndex;
                    string columnName = TableOne.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table1.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(TableOne, rowIndex, e.ColumnIndex, previousValues, currentValues);
                    _undoStack.Execute(rowIndex, columnName, command);

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
                    TableOne.Rows[e.RowIndex].Cells[0].Value = table1.GetRows()[e.RowIndex][0];
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

                if (TableTwo.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(TableTwo.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    List<object> values = new List<object>();
                    for (int i = 1; i < TableTwo.Columns.Count; i++)
                    {
                        values.Add(TableTwo.Rows[e.RowIndex].Cells[i].Value);
                    }

                    int rowIndex = e.RowIndex;
                    string columnName = TableTwo.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table2.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(TableTwo, rowIndex, e.ColumnIndex, previousValues, currentValues);
                    _undoStack2.Execute(rowIndex, columnName, command);

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
                    TableTwo.Rows[e.RowIndex].Cells[0].Value = table2.GetRows()[e.RowIndex][0];
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
                CopySelectionToClipboard(TableOne);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardValues(TableOne);
            }
            else if(e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = TableOne.CurrentCell.RowIndex;
                string columnIndex = TableOne.Columns[TableOne.CurrentCell.ColumnIndex].Name;
                _undoStack.Undo(rowIndex, columnIndex);
                _undoStack.Pop(rowIndex, columnIndex);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectionFromCell(TableOne);
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

        private void RemoveSelectionFromCell(DataGridView dataGridView)
        {
            if (dataGridView.SelectedCells.Count > 0)
            {
                // Получаем выбранную ячейку
                DataGridViewCell cell = dataGridView.SelectedCells[0];

                // Очищаем значение ячейки
                cell.Value = null;
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
            TableOne.CurrentCell = TableOne.Rows[e.RowIndex].Cells[0];
        }

        /// <summary>
        /// Функция добавления строки в таблицу 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            TableTwo.CurrentCell = TableTwo.Rows[e.RowIndex].Cells[0];
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
                CopySelectionToClipboard(TableTwo);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardValues(TableTwo);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = TableTwo.CurrentCell.RowIndex;
                string columnIndex = TableTwo.Columns[TableTwo.CurrentCell.ColumnIndex].Name;
                _undoStack2.Undo(rowIndex, columnIndex);
                _undoStack2.Pop(rowIndex, columnIndex);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectionFromCell(TableTwo);
            }
        }

        private void TableThree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectionToClipboard(TableThree);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteClipboardValues(TableThree);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                int rowIndex = TableThree.CurrentCell.RowIndex;
                string columnIndex = TableThree.Columns[TableThree.CurrentCell.ColumnIndex].Name;
                _undoStack3.Undo(rowIndex, columnIndex);
                _undoStack3.Pop(rowIndex, columnIndex);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectionFromCell(TableThree);
            }
        }

        private void EditTableOne_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem addItem = new ToolStripMenuItem("Добавить атрибут");
                ToolStripMenuItem recordItem = new ToolStripMenuItem("Добавить запись");
                ToolStripMenuItem removeItem = new ToolStripMenuItem("Удалить атрибут");
                ToolStripMenuItem removeRecord = new ToolStripMenuItem("Удалить запись");
                ToolStripMenuItem saveRecord = new ToolStripMenuItem("Сохранить отношение");

                addItem.Click += AddItem_Click;
                recordItem.Click += RecordItem_Click;
                removeItem.Click += RemoveItem_Click;
                removeRecord.Click += RemoveRecord_Click;
                saveRecord.Click += SaveRecord_Click;

                contextMenuStrip.Items.AddRange(new[] { addItem, recordItem, removeItem, removeRecord, saveRecord });
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void SaveRecord_Click(object sender, EventArgs e)
        {
            SaveResultToFile(table1);
        }

        private void EditTableTwo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem addItem = new ToolStripMenuItem("Добавить атрибут");
                ToolStripMenuItem recordItem = new ToolStripMenuItem("Добавить запись");
                ToolStripMenuItem removeItem = new ToolStripMenuItem("Удалить атрибут");
                ToolStripMenuItem removeRecord = new ToolStripMenuItem("Удалить запись");
                ToolStripMenuItem saveRecord = new ToolStripMenuItem("Сохранить отношение");

                addItem.Click += AddItem_ClickTwo;
                recordItem.Click += RecordItem_ClickTwo;
                removeItem.Click += RemoveItem_ClickTwo;
                removeRecord.Click += RemoveRecord_ClickTwo;
                saveRecord.Click += SaveRecord_ClickTwo;

                contextMenuStrip.Items.AddRange(new[] { addItem, recordItem, removeItem, removeRecord, saveRecord });
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void SaveRecord_ClickTwo(object sender, EventArgs e)
        {
            SaveResultToFile(table2);
        }

        private void EditTableThree_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                ToolStripMenuItem addItem = new ToolStripMenuItem("Добавить атрибут");
                ToolStripMenuItem recordItem = new ToolStripMenuItem("Добавить запись");
                ToolStripMenuItem removeItem = new ToolStripMenuItem("Удалить атрибут");
                ToolStripMenuItem removeRecord = new ToolStripMenuItem("Удалить запись");
                ToolStripMenuItem saveRecord = new ToolStripMenuItem("Сохранить отношение");

                addItem.Click += AddItem_ClickThree;
                recordItem.Click += RecordItem_ClickThree;
                removeItem.Click += RemoveItem_ClickThree;
                removeRecord.Click += RemoveRecord_ClickThree;
                saveRecord.Click += SaveRecord_ClickThree;

                contextMenuStrip.Items.AddRange(new[] { addItem, recordItem, removeItem, removeRecord, saveRecord });
                contextMenuStrip.Show(Cursor.Position);
            }
        }

        private void SaveRecord_ClickThree(object sender, EventArgs e)
        {
            SaveResultToFile(table3);
        }

        private void AddColumn(Table table)
        {
            if (!table.IsEmpty())
            {
                AddColum newColumName = new AddColum(flag = false);
                newColumName.ShowDialog();
                string cleanedString = newColumName.getColumName().Trim().Replace("\n", "");

                if (!string.IsNullOrEmpty(cleanedString))
                {
                    if (table.columnsNames.Contains(cleanedString))
                    {
                        MessageBox.Show("Столбец с таким названием уже есть !");
                        return;
                    }

                    table.AddColumn(cleanedString);
                    PopulateDataGridView(Fill.Manual);
                }
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddRecord(Table table)
        {
            if (!table.IsEmpty())
            {
                table.AddRow();
                PopulateDataGridView(Fill.Manual);
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RemoveRecord(Table table, int rowIndex)
        {
            if (!table.IsEmpty())
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить эту строку?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    table.RemoveRow(rowIndex);
                    PopulateDataGridView(Fill.Manual);
                }
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RemoveColumn(Table table)
        {
            if (!table.IsEmpty() && table.columnsNames.Length > 3)
            {
                AddColum newColumName = new AddColum(flag = true);
                newColumName.ShowDialog();
                newColmName = newColumName.getColumName();
                string columnName = newColmName.Trim().Replace("\n", "");

                for (int i = 3; i < table.columnsNames.Length; i++)
                {
                    if (columnName == table.columnsNames[i])
                    {
                        table.RemoveColumn(i);
                        PopulateDataGridView(Fill.Manual);
                        return;
                    }
                }
                if (columnName != "")
                    MessageBox.Show("Столбец с таким названием не найден или вы пытаетесь удалить первоначальные атрибуты.");
            }
            else
            {
                MessageBox.Show("Вы не заполнили таблицу, или в таблице осталось только 3 столбца", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            AddColumn(table1);
        }

        private void RecordItem_Click(object sender, EventArgs e)
        {
            AddRecord(table1);
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            RemoveColumn(table1);
        }

        private void RemoveRecord_Click(object sender, EventArgs e)
        {
            if (!table1.IsEmpty())
            {
                int rowIndex = TableOne.CurrentCell.RowIndex;
                RemoveRecord(table1, rowIndex);
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddItem_ClickTwo(object sender, EventArgs e)
        {
            AddColumn(table2);
        }

        private void RecordItem_ClickTwo(object sender, EventArgs e)
        {
            AddRecord(table2);
        }

        private void RemoveItem_ClickTwo(object sender, EventArgs e)
        {
            RemoveColumn(table2);
        }

        private void CombOperation_Click(object sender, EventArgs e)
        {
            if (table1.IsEmpty() && table2.IsEmpty() && table3.IsEmpty())
            {
                MessageBox.Show("Нужно заполнить таблицы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CombinationOperation CombOper = new CombinationOperation(table1, table2, table3, this);
            CombOper.ShowDialog();
        }

        private void RemoveRecord_ClickTwo(object sender, EventArgs e)
        {
            if (!table2.IsEmpty())
            {
                int rowIndex = TableTwo.CurrentCell.RowIndex;
                RemoveRecord(table2, rowIndex);
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddItem_ClickThree(object sender, EventArgs e)
        {
            AddColumn(table3);
        }

        private void RecordItem_ClickThree(object sender, EventArgs e)
        {
            AddRecord(table3);
        }

        private void RemoveItem_ClickThree(object sender, EventArgs e)
        {
            RemoveColumn(table3);
        }

        private void RemoveRecord_ClickThree(object sender, EventArgs e)
        {
            if (!table3.IsEmpty())
            {
                int rowIndex = TableThree.CurrentCell.RowIndex;
                RemoveRecord(table3, rowIndex);
            }
            else
                MessageBox.Show("Вы не заполнили таблицу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void TableThree_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id;

                if (TableThree.Rows[e.RowIndex].Cells[0].Value != null &&
                    int.TryParse(TableThree.Rows[e.RowIndex].Cells[0].Value.ToString(), out id))
                {
                    List<object> values = new List<object>();
                    for (int i = 1; i < TableThree.Columns.Count; i++)
                    {
                        values.Add(TableThree.Rows[e.RowIndex].Cells[i].Value);
                    }

                    int rowIndex = e.RowIndex;
                    string columnName = TableThree.Columns[e.ColumnIndex].Name;
                    object[] previousValues = table3.GetRows()[e.RowIndex];
                    object[] currentValues = new object[values.Count + 1];
                    currentValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        currentValues[i + 1] = values[i];
                    }
                    var command = new UndoCommand(TableThree, rowIndex, e.ColumnIndex, previousValues, currentValues);
                    _undoStack3.Execute(rowIndex, columnName, command);

                    object[] rowValues = new object[values.Count + 1];
                    rowValues[0] = id;
                    for (int i = 0; i < values.Count; i++)
                    {
                        rowValues[i + 1] = values[i];
                    }
                    table3.RewriteRow(rowValues, e.RowIndex);
                }
                else
                {
                    MessageBox.Show("Неверное значение ID!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TableThree.Rows[e.RowIndex].Cells[0].Value = table3.GetRows()[e.RowIndex][0];
                    return;
                }
            }
        }

        private void информацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Обучающее приложение по \"Реляционной алгебре\"\n");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.J))
            {
                ShowLastOperationStatus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowLastOperationStatus()
        {
            if (lastOperationSuccess)
            {
                MessageBox.Show("Последняя операция была выполнена успешно!", "Статус операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Последняя операция не была выполнена успешно.", "Статус операции", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

enum Fill 
{ 
    Auto,
    Manual
};