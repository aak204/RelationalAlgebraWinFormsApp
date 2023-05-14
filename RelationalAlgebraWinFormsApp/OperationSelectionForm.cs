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
        private string selectedOperation, form, selectAttribute = null;
        private bool flag;
        private Table _table1, _table2;

        public OperationSelectionForm(Table table1, Table table2)
        {
            _table1 = table1;
            _table2 = table2;
            InitializeComponent();
            selectedOperation = "";
        }


        private void UnionButton_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "Union";
            Close();
        }

        private void IntersectionButton_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "Intersection";
            Close();
        }

        private void DifferenceButton_Click_1(object sender, EventArgs e)
        {
            selectedOperation = "Difference";
            Choose FormDiff = new Choose("Difference");
            FormDiff.ShowDialog();
            form = FormDiff.GetForm();
            if (form != "")
                Close();
        }

        public string GetSelectedOperation()
        {
            return selectedOperation;
        }

        public string GetSelectedAttribute()
        {
            return selectAttribute;
        }

        public string GetForms()
        {
            return form;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            selectedOperation = "Divide";
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedOperation = "InnerJoin";
            selectAttribute = Selection();
            if (selectAttribute != null)
                Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedOperation = "Select";
            Choose FormDiff = new Choose("Select");
            FormDiff.ShowDialog();
            form = FormDiff.GetForm();
            if (form != "")
                Close();
        }

        private void LeftJoin_Click(object sender, EventArgs e)
        {
            selectedOperation = "LeftJoin";
            selectAttribute = Selection();
            if (selectAttribute != null)
                Close();
        }

        private void RightJoin_Click(object sender, EventArgs e)
        {
            selectedOperation = "RightJoin";
            selectAttribute = Selection();
            if (selectAttribute != null)
                Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedOperation = "CartesianProduct";
            Close();
        }

        private string Selection()
        {
            AddColum newColumName = new AddColum(flag = true);
            newColumName.ShowDialog();
            string ColName = newColumName.getColumName();
            bool check = false;
            if (ColName != "")
            {
                for (int i = 0; i < _table1.columnsNames.Length; i++)
                {
                    if (ColName == _table1.columnsNames[i])
                    {
                        check = true;
                    }
                }
                check = false;
                for (int i = 0; i < _table2.columnsNames.Length; i++)
                {
                    if (ColName == _table2.columnsNames[i])
                    {
                        check = true;
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
