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
        private string selectedOperation, form;

        public OperationSelectionForm()
        {
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
            ChooseDiff FormDiff = new ChooseDiff();
            FormDiff.ShowDialog();
            form = FormDiff.GetForm();
            Close();
        }

        public string GetSelectedOperation()
        {
            return selectedOperation;
        }

        public string GetForms()
        {
            return form;
        }


        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedOperation = "CartesianProduct";
            Close();
        }
    }
}
