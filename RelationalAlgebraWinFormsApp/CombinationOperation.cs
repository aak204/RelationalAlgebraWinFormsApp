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

        private ToolTip toolTip;
        private Table _table1, _table2, _table3;
        private List<Operation> operations = new List<Operation>();
        private Dictionary<string, Table> checkBoxToTableMapping;
        private int currentOperationIndex = 0;
        private MainForm MainForm;
        private Table[] Selected = new Table[2];
        private bool flag = false, once = true, once2 = true, selct = false, proj = false, flag2 = false, flag3 = false;
        private Table resultTable = null;
        private string ColumnName, Operator, Condition, ColumnNameProj;
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
        {"R", resultTable }
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
            operations.Add(new Operation
            {
                OperationLabel = Divide,
                CheckBoxes = new CheckBox[] { A_Divide, B_Divide, C_Divide },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Select,
                CheckBoxes = new CheckBox[] { A_Select, B_Select, C_Select, R_1 },
                CheckedCount = 0
            });
            operations.Add(new Operation
            {
                OperationLabel = Projection,
                CheckBoxes = new CheckBox[] { A_Proj, B_Proj, C_Proj, R_2 },
                CheckedCount = 0
            });
            InitializeToolTip();
        }

        private void InitializeToolTip()
        {
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000; // Задержка перед скрытием подсказки
            toolTip.InitialDelay = 500; // Задержка перед показом подсказки
            toolTip.ReshowDelay = 500; // Задержка перед повторным показом подсказки

            // Привязываем события MouseEnter и MouseLeave к текстовому полю
            TextBoxSelect.MouseEnter += TextBoxSelect_MouseEnter;
            TextBoxSelect.MouseLeave += TextBoxSelect_MouseLeave;
            TextBoxProj.MouseEnter += TextBoxProj_MouseEnter;
            TextBoxProj.MouseLeave += TextBoxProj_MouseLeave;
        }

        private void TextBoxSelect_MouseEnter(object sender, EventArgs e)
        {
            // Отображаем подсказку рядом с текстовым полем
            toolTip.Show("Для поля ID доступны следующие операции: =, >, <, >=, <=.\nДля остальных полей только: =.\nПример: ID > 5, ФИО = Морозов.\n" +
                "R - результат предыдущих операций", TextBoxSelect, TextBoxSelect.Width, 0);
        }

        private void TextBoxSelect_MouseLeave(object sender, EventArgs e)
        {
            // Скрываем подсказку
            toolTip.Hide(TextBoxSelect);
        }

        private void TextBoxProj_MouseEnter(object sender, EventArgs e)
        {
            // Отображаем подсказку рядом с текстовым полем
            toolTip.Show("Если хотите ввести несолько атрибутов разбейте их запятыми.\nПример: ID, ФИО.", TextBoxProj, TextBoxProj.Width, 0);
        }

        private void TextBoxProj_MouseLeave(object sender, EventArgs e)
        {
            // Скрываем подсказку
            toolTip.Hide(TextBoxSelect);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox currentBox = sender as CheckBox;
            Operation currentOperation = operations.FirstOrDefault(operation => operation.CheckBoxes.Contains(currentBox));

            if (currentOperation == null)
                return;


            if (currentOperation.OperationLabel == Select && currentOperationIndex == 0)
            {
                R_2.Enabled = true;
                selct = true;
            }
            else
                selct = false;

            if (currentOperation.OperationLabel == Projection && currentOperationIndex == 0)
            {
                R_1.Enabled = true;
                proj = true;
            }
            else
                proj = false;


            // Обновим это условие, чтобы блокировать проверку флажка только тогда, когда 
            // текущая операция не является первой И в ней уже установлен один флажок
            if ((currentOperationIndex > 0 && currentOperation.CheckedCount >= 1) && currentOperation.OperationLabel != Select & currentOperation.OperationLabel != Projection)
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

            if (currentOperation.CheckedCount == 2 || (resultTable != null && currentOperation.CheckedCount == 1) || selct || proj)
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
                    else if (currentOperation.OperationLabel == Select)
                    {
                        if (!HandleSelectOperation(currentOperation, currentBox, selct, ref flag2, ref resultTable))
                        {
                            return;
                        }
                    }
                    else if (currentOperation.OperationLabel == Projection)
                    {
                        if (!HandleProjectionOperation(currentOperation, currentBox, proj, ref resultTable))
                        {
                            return;
                        }
                    }
                    else if (currentOperation.OperationLabel == Divide)
                    {
                        resultTable = RelationalOperations.Divide(resultTable, Selected[1]);
                    }
                }

                currentOperation.OperationLabel.Text = $"{currentOperationIndex + 1}";
                currentOperation.OperationLabel.ForeColor = Color.Red;
                currentOperationIndex++;
            }
            if (currentOperationIndex > 0 && once2)
            {
                var checkboxesToDisable = new List<CheckBox> { A_Select, B_Select, C_Select, A_Proj, B_Proj, C_Proj };

                foreach (var checkbox in checkboxesToDisable)
                {
                    checkbox.CheckedChanged -= CheckBox_CheckedChanged;
                    checkbox.Enabled = false;
                    checkbox.CheckedChanged += CheckBox_CheckedChanged;
                }

                if (!selct)
                    R_1.Enabled = true;
                if (!proj)
                    R_2.Enabled = true;
                once2 = false;
            }
            Combination.Text = CreateNestedFormula();
        }

        public bool HandleProjectionOperation(Operation currentOperation, CheckBox currentBox, bool proj, ref Table resultTable)
        {
            ColumnNameProj = TextBoxProj.Text.Trim();

            if (proj)
            {
                if (!checkBoxToTableMapping[currentBox.Text].columnsNames.Contains(ColumnNameProj))
                {
                    if (!flag3)
                        ShowErrorMessage($"Столбец '{ColumnNameProj}' должен быть в таблицe.");
                    flag3 = true;
                    ResetProjectionOperation(currentOperation, proj, true);
                    flag3 = false;
                    return false;
                }

                resultTable = RelationalOperations.Projection(checkBoxToTableMapping[currentBox.Text], ColumnNameProj);
            }
            else
            {
                if (!resultTable.columnsNames.Contains(ColumnNameProj))
                {
                    if (!flag3)
                        ShowErrorMessage($"Столбец '{ColumnNameProj}' должен быть в таблицe.");
                    flag3 = true;
                    ResetProjectionOperation(currentOperation, proj, false);
                    flag3 = false;
                    return false;
                }

                resultTable = RelationalOperations.Projection(resultTable, ColumnNameProj);
            }

            return true;
        }

        public void ResetProjectionOperation(Operation currentOperation, bool proj, bool forResultTable)
        {
            if (proj)
            {
                A_Proj.Enabled = true;
                B_Proj.Enabled = true;
                C_Proj.Enabled = true;
                A_Proj.Checked = false;
                B_Proj.Checked = false;
                C_Proj.Checked = false;
                R_1.Enabled = false;
                resultTable = null;
            }
            else if (!forResultTable)
            {
                R_2.Enabled = true;
                R_2.Checked = false;
            }

            currentOperation.CheckedCount = 0;
            TextBoxProj.Text = "";
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Все операции выполняются по принципу матрешки. Выполнили первую операцию у вас получился результат, далее вы работете только с этим результатом!\nКак бы они накладываются друг на дурга.");
        }

        public bool HandleSelectOperation(Operation currentOperation, CheckBox currentBox, bool selct, ref bool flag2, ref Table resultTable)
        {
            (ColumnName, Operator, Condition) = getSelectionCondition(TextBoxSelect);

            if (ColumnName == null & Operator == null && Condition == null)
            {
                if (!flag2)
                    ShowErrorMessage("Неверное условие выбора. Ожидаемый формат: [Имя колонки] [Оператор] [Условие].");
                flag2 = true;
                ResetSelectOperation(currentOperation, selct);
                flag2 = false;
                return false;
            }

            if (selct)
            {
                if (!checkBoxToTableMapping[currentBox.Text].columnsNames.Contains(ColumnName))
                {
                    if (!flag2)
                        ShowErrorMessage($"Столбец '{ColumnName}' должен быть в таблицe.");
                    flag2 = true;
                    ResetSelectOperation(currentOperation, selct);
                    flag2 = false;
                    return false;
                }

                resultTable = RelationalOperations.Selection(checkBoxToTableMapping[currentBox.Text], ColumnName, Operator, Condition);
            }
            else
            {
                if (!resultTable.columnsNames.Contains(ColumnName))
                {
                    if (!flag2)
                    {
                        ShowErrorMessage($"Столбец '{ColumnName}' должен быть в таблицe.");
                    }
                    flag2 = true;
                    ResetSelectOperation(currentOperation, selct, false);
                    flag2 = false;
                    return false;
                }

                resultTable = RelationalOperations.Selection(resultTable, ColumnName, Operator, Condition);
            }

            return true;
        }

        public void ResetSelectOperation(Operation currentOperation, bool selct, bool forResultTable = true)
        {
            TextBoxSelect.Text = "";

            if (selct)
            {
                A_Select.Enabled = true;
                B_Select.Enabled = true;
                C_Select.Enabled = true;
                A_Select.Checked = false;
                B_Select.Checked = false;
                C_Select.Checked = false;
                resultTable = null;
            }
            else if (forResultTable)
            {
                R_1.Enabled = true;
                R_1.Checked = false;
            }

            currentOperation.CheckedCount = 0;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public (string ColumnName, string Operator, string Condition) getSelectionCondition(TextBox Text)
        {
            // Получаем текст из текстового поля, удаляем пробелы по краям и разбиваем его на части
            string[] parts = Text.Text.Trim().Split(new[] { ' ' }, 3);

            // Если пользователь ввел не три части, вызываем исключение
            if (parts.Length != 3)
            {
                return (null, null, null);
            }

            // Удаляем пробелы из первых двух частей и оставляем третью часть как есть
            parts[0] = parts[0].Replace(" ", "");
            parts[1] = parts[1].Replace(" ", "");

            // Если входная строка пустая, возвращаем тройку null
            if (Text.Text == "")
            {
                return (null, null, null);
            }

            return (parts[0], parts[1], parts[2]);
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
            TextBoxSelect.Text = "";
            TextBoxProj.Text = "";
            R_1.Enabled = false;
            R_2.Enabled = false;
            once2 = true;
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
                    case "Divide":
                        operationName = "DIVIDE";
                        break;
                    case "Projection":
                        operationName = "PROJECTION";
                        break;
                    case "Select":
                        operationName = $"SELECT {Operator} {Condition}";
                        break;
                    default:
                        operationName = "UNKNOWN";
                        break;
                }

                List<string> selectedCheckBoxes = operation.CheckBoxes.Where(cb => cb.Checked).Select(cb => cb.Text).ToList();

                // При вычитании для правильного отображения
                if (operation.OperationLabel.Name == "Diff")
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
                    if (operation.OperationLabel.Name == "Select")
                        formula.Append($" {formattedSelectedCheckBoxes} {operationName})");
                    else
                        formula.Append($" {operationName} {formattedSelectedCheckBoxes})");
                }
                // Иначе начинаем формулу с текущей операции
                else
                {
                    if (operation.OperationLabel.Name == "Select")
                    {
                        formula.Append($"({formattedSelectedCheckBoxes} {operationName})");
                    }
                    else
                    {
                        string[] first = formattedSelectedCheckBoxes.Split(' ');
                        if (first.Length > 1)
                            formula.Append($"({first[0]} {operationName} {first[1]})");
                    }
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
            A_Select.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_Select.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_Select.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_Proj.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_Proj.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_Proj.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            A_Divide.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            B_Divide.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            C_Divide.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            R_1.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            R_2.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
        }
    }
}
