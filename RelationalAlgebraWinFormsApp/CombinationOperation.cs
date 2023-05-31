using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static RelationalAlgebraWinFormsApp.CombinationOperation;

namespace RelationalAlgebraWinFormsApp
{
    public partial class CombinationOperation : Form
    {
        public class Operation
        {
            public Label OperationLabel { get; set; }
            public CheckBox[] CheckBoxes { get; set; }
            public int CheckedCount { get; set; }
            public bool IsFirst => OperationLabel.Text == "";
            public bool IsSubsequent => !IsFirst;
            public Dictionary<CheckBox, Label> OrderLabels { get; set; }
        }

        private class ColumnNameResult
        {
            public string ColumnName { get; set; }
            public bool EmptyNameError { get; set; }
            public bool ColumnNotExistError { get; set; }
        }

        Table _table1, _table2, _table3;
        List<Operation> operations = new List<Operation>();
        Dictionary<string, Table> checkBoxToTableMapping;
        int currentOperationIndex = 0;
        private MainForm MainForm;
        private Table[] Selected = new Table[2];
        private bool flag = false, once = true;
        public CombinationOperation(Table table1, Table table2, Table table3, MainForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            _table1 = table1;
            _table2 = table2;
            _table3 = table3;
            checkBoxToTableMapping = new Dictionary<string, Table>()
    {
        { "A", _table1 },
        { "B", _table2 },
        { "C", _table3 },
    };
            operations.Add(new Operation
            {
                OperationLabel = Union,
                CheckBoxes = new CheckBox[] { A_Union, B_Union, C_Union },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Inter,
                CheckBoxes = new CheckBox[] { A_Intersection, B_Intersection, C_Intersection },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Diff,
                CheckBoxes = new CheckBox[] { A_Diff, B_Diff, С_Diff },
                CheckedCount = 0,
                OrderLabels = new Dictionary<CheckBox, Label>
    {
        { A_Diff, Diff_1 },
        { B_Diff, Diff_2 },
        { С_Diff, Diff_3 },
    }
            });
            operations.Add(new Operation
            {
                OperationLabel = CarProd,
                CheckBoxes = new CheckBox[] { A_CarProd, B_CarProd, C_CarProd },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Inner,
                CheckBoxes = new CheckBox[] { A_InnerJoin, B_InnerJoin, C_InnerJoin },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Left,
                CheckBoxes = new CheckBox[] { A_LeftJoin, B_LeftJoin, C_LeftJoin },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Right,
                CheckBoxes = new CheckBox[] { A_RightJoin, B_RightJoin, C_RightJoin },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Full,
                CheckBoxes = new CheckBox[] { A_FullJoin, B_FullJoin, C_FullJoin },
                CheckedCount = 0
            });
        }

        Table resultTable = null;

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox currentBox = sender as CheckBox;
            Operation currentOperation = operations.FirstOrDefault(operation => operation.CheckBoxes.Contains(currentBox));

            if (currentOperation == null)
                return;

            // Обновим это условие, чтобы блокировать проверку флажка только тогда, когда 
            // текущая операция не является первой И в ней уже установлен один флажок
            if ((currentOperationIndex > 0 && currentOperation.CheckedCount >= 1))
            {
                currentBox.CheckedChanged -= CheckBox_CheckedChanged;
                currentBox.Checked = false;
                currentBox.CheckedChanged += CheckBox_CheckedChanged;
                return;
            }

            currentOperation.CheckedCount++;
            if (currentBox.Checked)
            {
                currentBox.Enabled = false;
            }

            // Специальная обработка для работы с разницей, учитывая порядок
            if (currentOperation.OperationLabel == Diff && currentOperationIndex == 0)
            {
                currentOperation.OrderLabels[currentBox].Text = $"{currentOperation.CheckedCount}";
            }


            if (once)
            {
                Selected[0] = checkBoxToTableMapping[currentBox.Text];
                once = false;
            }
            else
                Selected[1] = checkBoxToTableMapping[currentBox.Text];

            if (currentOperation.CheckedCount == 2 || (resultTable != null && currentOperation.CheckedCount == 1))
            {
                if (resultTable == null)
                {
                    resultTable = Selected[0];
                }
                if (currentOperation.OperationLabel == Diff && currentOperationIndex == 0)
                {
                    // Если первая операция вычитания
                    if (currentOperation.OrderLabels[currentBox].Text == "1")
                    {
                        resultTable = RelationalOperations.Difference(Selected[0], Selected[1]);
                    }
                    else
                    {
                        resultTable = RelationalOperations.Difference(resultTable, Selected[1]);
                    }
                }
                else
                {
                    if (currentOperation.OperationLabel == Union)
                    {
                        resultTable = RelationalOperations.Union(resultTable, Selected[1]);
                    }
                    else if (currentOperation.OperationLabel == Inter)
                    {
                        resultTable = RelationalOperations.Intersection(resultTable, Selected[1]);
                    }
                    else if (currentOperation.OperationLabel == Diff)
                    {
                        resultTable = RelationalOperations.Difference(resultTable, Selected[1]);
                    }
                    else if (currentOperation.OperationLabel == CarProd)
                    {
                        resultTable = RelationalOperations.CartesianProduct(resultTable, Selected[1]);
                    }
                    else if (currentOperation.OperationLabel == Inner)
                    {
                        ColumnNameResult columnNameResult = getColumName(TextBoxInner, resultTable, Selected[1]);
                        if (columnNameResult.EmptyNameError || columnNameResult.ColumnNotExistError)
                        {
                            if (columnNameResult.EmptyNameError && !flag)
                            {
                                MessageBox.Show("Пожалуйста, укажите название столбца.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (columnNameResult.ColumnNotExistError && !flag)
                            {
                                MessageBox.Show($"Столбец '{columnNameResult.ColumnName}' должен быть в обоих таблицах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            flag = true;
                            ResetOperation(currentOperation, TextBoxInner);
                            flag = false;
                            return;
                        }

                        resultTable = RelationalOperations.InnerJoin(resultTable, Selected[1], columnNameResult.ColumnName);
                    }
                    else if (currentOperation.OperationLabel == Left)
                    {
                        ColumnNameResult columnNameResult = getColumName(TextBoxLeft, resultTable, Selected[1]);
                        if (columnNameResult.EmptyNameError || columnNameResult.ColumnNotExistError)
                        {
                            if (columnNameResult.EmptyNameError && !flag)
                            {
                                MessageBox.Show("Пожалуйста, укажите название столбца.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (columnNameResult.ColumnNotExistError && !flag)
                            {
                                MessageBox.Show($"Столбец '{columnNameResult.ColumnName}' должен быть в обоих таблицах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            flag = true;
                            ResetOperation(currentOperation, TextBoxLeft);
                            flag = false;
                            return;
                        }

                        resultTable = RelationalOperations.LeftJoin(resultTable, Selected[1], columnNameResult.ColumnName);
                    }
                    else if (currentOperation.OperationLabel == Right)
                    {
                        ColumnNameResult columnNameResult = getColumName(TextBoxRight, resultTable, Selected[1]);
                        if (columnNameResult.EmptyNameError || columnNameResult.ColumnNotExistError)
                        {
                            if (columnNameResult.EmptyNameError && !flag)
                            {
                                MessageBox.Show("Пожалуйста, укажите название столбца.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (columnNameResult.ColumnNotExistError && !flag)
                            {
                                MessageBox.Show($"Столбец '{columnNameResult.ColumnName}' должен быть в обоих таблицах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            flag = true;
                            ResetOperation(currentOperation, TextBoxRight);
                            flag = false;
                            return;
                        }

                        resultTable = RelationalOperations.RightJoin(resultTable, Selected[1], columnNameResult.ColumnName);
                    }
                    else if (currentOperation.OperationLabel == Full)
                    {
                        ColumnNameResult columnNameResult = getColumName(TextBoxFull, resultTable, Selected[1]);
                        if (columnNameResult.EmptyNameError || columnNameResult.ColumnNotExistError)
                        {
                            if (columnNameResult.EmptyNameError && !flag)
                            {
                                MessageBox.Show("Пожалуйста, укажите название столбца.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (columnNameResult.ColumnNotExistError && !flag)
                            {
                                MessageBox.Show($"Столбец '{columnNameResult.ColumnName}' должен быть в обоих таблицах.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            flag = true;
                            ResetOperation(currentOperation, TextBoxFull);
                            flag = false;
                            return;
                        }

                        resultTable = RelationalOperations.FullJoin(resultTable, Selected[1], columnNameResult.ColumnName);
                    }
                }

                currentOperation.OperationLabel.Text = $"{currentOperationIndex + 1}";
                currentOperation.OperationLabel.ForeColor = Color.Red;
                currentOperationIndex++;
            }
            Combination.Text = CreateNestedFormula();
        }

        private ColumnNameResult getColumName(TextBox name, Table resultTable, Table selectTable)
        {
            ColumnNameResult result = new ColumnNameResult();
            result.ColumnName = name.Text;

            if (string.IsNullOrWhiteSpace(result.ColumnName))
            {
                result.EmptyNameError = true;
            }
            else if (!resultTable.columnsNames.Contains(result.ColumnName) && !selectTable.columnsNames.Contains(result.ColumnName))
            {
                result.ColumnNotExistError = true;
            }

            return result;
        }

        private void ResetOperation(Operation currentOperation, TextBox name)
        {
            if (currentOperation.CheckedCount == 2)
                resultTable = null;

            currentOperation.CheckedCount = 0;

            foreach (CheckBox checkBox in currentOperation.CheckBoxes)
            {
                checkBox.Checked = false;
                checkBox.Enabled = true;
            }

            name.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MainForm.DisplayResultComb(resultTable, true);
            if (resultTable != null)
                Close();
        }

        private void ClearButton_Click_1(object sender, EventArgs e)
        {
            foreach (var operation in operations)
            {
                operation.CheckedCount = 0;
                operation.OperationLabel.Text = "";
                operation.OperationLabel.ForeColor = Color.Black;

                foreach (var checkbox in operation.CheckBoxes)
                {
                    checkbox.CheckedChanged -= CheckBox_CheckedChanged;
                    checkbox.Checked = false;
                    checkbox.Enabled = true;
                    checkbox.CheckedChanged += CheckBox_CheckedChanged;
                }
            }

            currentOperationIndex = 0;
            resultTable = null;
            TextBoxInner.Text = "";
            TextBoxLeft.Text = "";
            TextBoxRight.Text = "";
            TextBoxFull.Text = "";
            Combination.Text = "";
            once = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.DisplayResultComb(resultTable, false);
        }


        // Метод для создания вложенной формулы, основанной на выборе операций и checkbox'ов
        private string CreateNestedFormula()
        {
            StringBuilder formula = new StringBuilder();

            // Сортируем операции в порядке их выбора
            List<Operation> sortedOperations = operations.Where(op => op.OperationLabel.Text != "").OrderBy(op => int.Parse(op.OperationLabel.Text)).ToList();

            // Проходим по всем операциям в порядке их выбора
            for (int i = 0; i < sortedOperations.Count; i++)
            {
                Operation operation = sortedOperations[i];

                string operationName;
                switch (operation.OperationLabel.Name)
                {
                    case "Union":
                        operationName = "∪";
                        break;
                    case "Inter":
                        operationName = "∩";
                        break;
                    case "Diff":
                        operationName = "/";
                        break;
                    case "CarProd":
                        operationName = "CARTESIAN PRODUCT";
                        break;
                    case "Inner":
                        operationName = "INNER JOIN";
                        break;
                    case "Left":
                        operationName = "LEFT JOIN";
                        break;
                    case "Right":
                        operationName = "RIGHT JOIN";
                        break;
                    case "Full":
                        operationName = "FULL JOIN";
                        break;
                    default:
                        operationName = "UNKNOWN";
                        break;
                }

                List<string> selectedCheckBoxes = operation.CheckBoxes.Where(cb => cb.Checked).Select(cb => cb.Text).ToList();

                // При вычитании для правильного отображения
                if (operation.OperationLabel == Diff)
                {
                    selectedCheckBoxes.Sort((text1, text2) =>
                    {
                        CheckBox cb1 = operation.CheckBoxes.First(cb => cb.Text == text1);
                        CheckBox cb2 = operation.CheckBoxes.First(cb => cb.Text == text2);
                        return string.Compare(operation.OrderLabels[cb1].Text, operation.OrderLabels[cb2].Text);
                    });
                }

                string formattedSelectedCheckBoxes = string.Join(" ", selectedCheckBoxes);

                // Если формула уже начата, вкладываем уже существующую формулу в текущую операцию
                if (formula.Length > 0)
                {
                    formula.Insert(0, "(");
                    formula.Append($") {operationName} {formattedSelectedCheckBoxes}");
                }
                // Иначе начинаем формулу с текущей операции
                else
                {
                    string[] first = formattedSelectedCheckBoxes.Split(' ');
                    if (first.Length > 1)
                        formula.Append($"({first[0]} {operationName} {first[1]})");
                }
            }

            return formula.ToString();
        }


        private void CombinationOperation_Load(object sender, EventArgs e)
        {
            A_Union.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_Union.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_Union.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_Intersection.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_Intersection.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_Intersection.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_Diff.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_Diff.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            С_Diff.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_CarProd.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_CarProd.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_CarProd.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_InnerJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_InnerJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_InnerJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_LeftJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_LeftJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_LeftJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_RightJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_RightJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_RightJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_FullJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_FullJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_FullJoin.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
        }
    }
}
