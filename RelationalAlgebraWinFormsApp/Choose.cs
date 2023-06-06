using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace RelationalAlgebraWinFormsApp
{
    public partial class Choose : Form
    {
        private string operation = "";
        private ToolTip toolTip;
        private List<CheckBox> checkboxes;
        private MainForm mainClass; // Экземпляр класса, содержащего PerformOperationAndDisplayResult
        private Dictionary<CheckBox, Label> checkBoxLabels;
        private List<CheckBox> checkBoxOrder;
        public Choose(string op, MainForm main)
        {
            operation = op;
            mainClass = main;
            InitializeComponent();
            InitializeCheckboxes();
            InitializeToolTip();
        }

        private void InitializeToolTip()
        {
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000; // Задержка перед скрытием подсказки
            toolTip.InitialDelay = 500; // Задержка перед показом подсказки
            toolTip.ReshowDelay = 500; // Задержка перед повторным показом подсказки

            // Привязываем события MouseEnter и MouseLeave к текстовому полю
            GetText.MouseEnter += GetText_MouseEnter;
            GetText.MouseLeave += GetText_MouseLeave;
        }

        private void GetText_MouseEnter(object sender, EventArgs e)
        {
            // Отображаем подсказку рядом с текстовым полем
            if (operation == "Select")
                toolTip.Show("Для поля ID доступны следующие операции: =, >, <, >=, <=.\nДля остальных полей только: =.\nПример: ID > 5, ФИО = Морозов.", GetText, GetText.Width, 0);
            if (operation == "Projection")
                toolTip.Show("Если хотите ввести несолько атрибутов разбейте их запятыми.\nПример: ID, ФИО.", GetText, GetText.Width, 0);
        }

        private void GetText_MouseLeave(object sender, EventArgs e)
        {
            // Скрываем подсказку
            if (operation == "Select" || operation == "Projection")
                toolTip.Hide(GetText);
        }

        private void InitializeCheckboxes()
        {
            checkboxes = new List<CheckBox>
        {
            A,
            B,
            C
        };

            checkBoxLabels = new Dictionary<CheckBox, System.Windows.Forms.Label>
        {
            { A, labelA },
            { B, labelB },
            { C, labelC }
        };

            checkBoxOrder = new List<CheckBox>();

            foreach (var checkbox in checkboxes)
            {
                checkbox.CheckedChanged += Checkbox_CheckedChanged;
            }
        }

        private void Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox current = (CheckBox)sender;

            if (operation == "Projection" || operation == "Select")
            {
                if (current.Checked)
                {
                    // Снимаем отметку с остальных Checkbox
                    foreach (CheckBox checkbox in checkBoxOrder)
                    {
                        if (checkbox != current)
                        {
                            checkbox.Checked = false;
                        }
                    }

                    checkBoxOrder.Clear();
                    checkBoxOrder.Add(current);
                }
            }
            else
            {
                // Если выбрана другая операция
                if (current.Checked)
                {
                    if (checkBoxOrder.Count >= 2)
                    {
                        // Если выбрано более двух элементов, отменяем изменение текущего элемента
                        current.Checked = false;
                        return;
                    }

                    checkBoxOrder.Add(current);

                    if (operation == "Difference" || operation == "LeftJoin" || operation == "RightJoin")
                    {
                        checkBoxLabels[current].ForeColor = Color.Red;
                        checkBoxLabels[current].Text = (checkBoxOrder.Count).ToString();
                    }
                }
                else
                {
                    checkBoxOrder.Remove(current);
                    if (operation == "Difference" || operation == "LeftJoin" || operation == "RightJoin")
                    {
                        checkBoxLabels[current].Text = "";

                        // Перенастраиваем номера для оставшихся отмеченных флажков
                        for (int i = 0; i < checkBoxOrder.Count; i++)
                        {
                            checkBoxLabels[checkBoxOrder[i]].Text = (i + 1).ToString();
                        }
                    }
                }
            }
        }


        public string getColumName()
        {
            return GetText.Text;
        }

        public string[] getColumnNames()
        {
            // Получаем текст из текстового поля, удаляем пробелы по краям и разбиваем его по запятым
            string[] columnNames = GetText.Text.Trim().Split(',');

            // Удаляем пробелы из каждого элемента массива
            for (int i = 0; i < columnNames.Length; i++)
            {
                columnNames[i] = columnNames[i].Replace(" ", "");
            }

            return columnNames;
        }

        public (string ColumnName, string Operator, string Condition) getSelectionCondition()
        {
            // Получаем текст из текстового поля, удаляем пробелы по краям и разбиваем его на части
            string[] parts = GetText.Text.Trim().Split(new[] { ' ' }, 3);

            // Если пользователь ввел не три части, вызываем исключение
            if (parts.Length != 3)
            {
                return (null, null, null);
            }

            // Удаляем пробелы из первых двух частей и оставляем третью часть как есть
            parts[0] = parts[0].Replace(" ", "");
            parts[1] = parts[1].Replace(" ", "");

            // Если входная строка пустая, возвращаем тройку null
            if (GetText.Text == "")
            {
                return (null, null, null);
            }

            return (parts[0], parts[1], parts[2]);
        }

        public (string, string) GetForm()
        {
            string Value1 = null, Value2 = null;

            if (A.Checked && B.Checked)
            {
                Value1 = "A";
                Value2 = "B";
            }
            else if (A.Checked && C.Checked)
            {
                Value1 = "A";
                Value2 = "C";
            }
            else if (B.Checked && C.Checked)
            {
                Value1 = "B";
                Value2 = "C";
            }

            return (Value1, Value2);
        }

        public string GetFormOne()
        {
            string Value1 = null;

            if (A.Checked )
                Value1 = "A";
            else if (B.Checked)
                Value1 = "B";
            else if (C.Checked)
                Value1 = "C";

            return Value1;
        }

        private void Choose_Load(object sender, EventArgs e)
        {
            var operations = new Dictionary<string, string>
    {
        {"Intersection", "Выберите таблицы для пересечения"},
        {"Difference", "Выберите таблицы для вычитания"},
        {"CartesianProduct", "Выберите таблицы для декартового произведения"},
        {"InnerJoin", "Выберите таблицы для внутреннего соединения\n           и введите название атрибута"},
        {"LeftJoin", "Выберите таблицы для левого соединения\n           и введите название атрибута"},
        {"RightJoin", "Выберите таблицы для правого соединения\n           и введите название атрибута"},
        {"FullJoin", "Выберите таблицы для полного соединения\n           и введите название атрибута"},
        {"Divide", "      Выберите таблицы для деления"},
        {"Projection", "      Выберите таблицу для проекции"},
        {"Select", "      Выберите таблицу для выборки"}

    };

            if (operations.ContainsKey(operation))
            {
                label1.Text = operations[operation];
                GetText.Visible = operation == "InnerJoin" || operation == "LeftJoin" || operation == "RightJoin" || operation == "FullJoin" || operation == "Projection" || operation == "Select";
            }
        }


        private void ShowResultButton_Click(object sender, EventArgs e)
        {
            var checkedCount = checkboxes.Count(cb => cb.Checked);

            if (checkedCount >= 2 && operation != "Projection" && operation != "Select")
            {
                if (operation == "InnerJoin" || operation == "LeftJoin" || operation == "RightJoin" || operation ==  "FullJoin")
                {
                    if (GetText.Text == "")
                        MessageBox.Show("Нужно написать атрибут", "Ошибка",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        Close();
                }
                else
                    Close();
            }
            else if (operation != "Projection" && operation != "Select")
                MessageBox.Show("Нужно выбрать два отношения.", "Ошибка",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if ((operation == "Projection" || operation == "Select") && checkedCount == 1)
            {
                if (GetText.Text == "")
                    MessageBox.Show("Нужно написать атрибут", "Ошибка",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (operation == "Select")
                {
                    string[] parts = GetText.Text.Trim().Split(new[] { ' ' }, 3);
                    if (parts.Length != 3)
                    {
                        MessageBox.Show("Неверное условие выбора. Ожидаемый формат: [Имя колонки] [Оператор] [Условие]");
                    }
                    else
                        Close();
                }
                else
                    Close();
            }
            else
                MessageBox.Show("Нужно выбрать одно отношение.", "Ошибка",
MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public List<int> GetCheckBoxOrder()
        {
            return checkBoxOrder.Select(cb => checkboxes.IndexOf(cb) + 1).ToList();
        }
    }
}
