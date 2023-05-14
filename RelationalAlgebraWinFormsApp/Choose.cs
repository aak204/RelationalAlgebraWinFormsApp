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
    public partial class Choose : Form
    {
        private string form = "", op;
        public Choose(string Operation)
        {
            op = Operation;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form = "1";
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form = "2";
            Close();
        }

        public string GetForm()
        {
            return form;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Choose_Load(object sender, EventArgs e)
        {
            if (op == "Difference")
                label1.Text = "Выберите из какой таблицы производить вычитание";
            else if (op == "Select")
                label1.Text = "Выберите таблицу для которой выполнить операцию выборки";
        }
    }
}
