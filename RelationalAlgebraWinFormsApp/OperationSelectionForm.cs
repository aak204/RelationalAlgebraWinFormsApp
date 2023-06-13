using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    public partial class OperationSelectionForm : Form
    {
        private string selectedOperation, value1, value2, selectAttribute = null, ColName, Operator, Condition;
        private string[] AttrNames;
        private Table _table1, _table2, _table3;
        private MainForm MainForm;
        private List<int> checkboxOrder;

        public OperationSelectionForm(Table table1, Table table2, Table table3, MainForm mainForm)
        {
            _table1 = table1;
            _table2 = table2;
            _table3 = table3;
            InitializeComponent();
            selectedOperation = "";
            MainForm = mainForm;
        }


        private void UnionButton_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "Union";
            Choose FormDiff = new Choose("Union", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed;
            FormDiff.Show();
        }

        private void FormDiff_FormClosed(object sender, FormClosedEventArgs e)
        {
            Choose FormDiff = (Choose)sender;
            (value1, value2) = FormDiff.GetForm();
            if (value1 != null && value2 != null)
            {
                MainForm.PerformOperationAndDisplayResult(selectedOperation);
                Close();
            }
        }

        private void FormDiff_FormClosed_Join(object sender, FormClosedEventArgs e)
        {
            Choose FormDiff = (Choose)sender;
            ColName = FormDiff.getColumName();
            (value1, value2) = FormDiff.GetForm();
            selectAttribute = Selection(ColName, value1, value2);
            checkboxOrder = FormDiff.GetCheckBoxOrder();
            if (value1 != null && value2 != null && selectAttribute != null && checkboxOrder != null)
            {
                MainForm.PerformOperationAndDisplayResult(selectedOperation);
                Close();
            }
        }

        private void FormDiff_FormClosed_Proj(object sender, FormClosedEventArgs e)
        {
            Choose FormDiff = (Choose)sender;
            AttrNames = FormDiff.getColumnNames();
            value1 = FormDiff.GetFormOne();
            if (value1 != null && AttrNames.Length > 0)
            {
                MainForm.PerformOperationAndDisplayResult(selectedOperation);
                Close();
            }
        }

        private void FormDiff_FormClosed_Diff(object sender, FormClosedEventArgs e)
        {
            Choose FormDiff = (Choose)sender;
            (value1, value2) = FormDiff.GetForm();
            checkboxOrder = FormDiff.GetCheckBoxOrder();
            if (value1 != null && value2 != null && checkboxOrder != null)
            {
                MainForm.PerformOperationAndDisplayResult(selectedOperation);
                Close();
            }
        }

        private void FormDiff_FormClosed_Select(object sender, FormClosedEventArgs e)
        {
            Choose FormDiff = (Choose)sender;
            value1 = FormDiff.GetFormOne();
            (ColName, Operator, Condition) = FormDiff.getSelectionCondition();
            if (value1 != null && ColName != null && Operator != null && Condition != null)
            {
                MainForm.PerformOperationAndDisplayResult(selectedOperation);
                Close();
            }
        }

        private void IntersectionButton_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "Intersection";
            Choose FormDiff = new Choose("Intersection", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed;
            FormDiff.Show();
        }

        private void DifferenceButton_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "Difference";
            Choose FormDiff = new Choose("Difference", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Diff;
            FormDiff.Show();
        }

        public string GetSelectedOperation()
        {
            return selectedOperation;
        }

        public string GetSelectedAttribute()
        {
            return selectAttribute;
        }

        public string GetForm1()
        {
            return value1;
        }

        public string GetForm2()
        {
            return value2;
        }

        public (string, string, string) GetCondition()
        {
            return (ColName, Operator, Condition);
        }

        public List<int> GetFormOrder()
        {
            return checkboxOrder;
        }

        public string[] GetColNames()
        {
            return AttrNames;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selectedOperation = "Divide";
            Choose FormDiff = new Choose("Divide", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Join;
            FormDiff.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "FullJoin";
            Choose FormDiff = new Choose("FullJoin", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Join;
            FormDiff.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedOperation = "Projection";
            Choose FormDiff = new Choose("Projection", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Proj;
            FormDiff.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedOperation = "InnerJoin";
            Choose FormDiff = new Choose("InnerJoin", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Join;
            FormDiff.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedOperation = "Select";
            Choose FormDiff = new Choose("Select", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Select;
            FormDiff.Show();
        }

        private void LeftJoin_Click(object sender, EventArgs e)
        {
            selectedOperation = "LeftJoin";
            Choose FormDiff = new Choose("LeftJoin", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Join;
            FormDiff.Show();
        }

        private void RightJoin_Click(object sender, EventArgs e)
        {
            selectedOperation = "RightJoin";
            Choose FormDiff = new Choose("RightJoin", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed_Join;
            FormDiff.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedOperation = "CartesianProduct";
            Choose FormDiff = new Choose("CartesianProduct", MainForm);
            FormDiff.FormClosed += FormDiff_FormClosed;
            FormDiff.Show();
        }

        private string Selection(string Col, string value1, string value2)
        {
            string ColName = Col;
            bool check = false;
            if (ColName != "")
            {
                Dictionary<string, Table> tables = new Dictionary<string, Table>()
        {
            { "A", _table1 },
            { "B", _table2 },
            { "C", _table3 }
        };

                Table table_new = tables[value1];
                Table table2_new = tables[value2];

                for (int i = 0; i < table_new.columnsNames.Length; i++)
                {
                    if (ColName == table_new.columnsNames[i])
                    {
                        check = true;
                    }
                }
                if (!check)
                {
                    for (int i = 0; i < table2_new.columnsNames.Length; i++)
                    {
                        if (ColName == table2_new.columnsNames[i])
                        {
                            check = true;
                        }
                    }
                }
                if (check)
                {
                     return ColName;
                }
                else
                {
                    MessageBox.Show("В обоих таблицах должен быть введеный атрибут", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return null;
        }

    }
}
