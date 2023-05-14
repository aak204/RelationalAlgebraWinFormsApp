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
    public partial class AddColum : Form
    {
        private string columName = "";
        private readonly bool _flag = false;
        public AddColum(bool flag)
        {
            _flag = flag;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            columName = textBox1.Text;
            Close();
        }

        public string getColumName()
        {
            return columName;
        }

        private void AddColum_Load(object sender, EventArgs e)
        {
            if (_flag)
            {
                label1.Text = "Введите название атрибута для соединения";
                this.Width = 400;
                button1.Location = new Point(110, 65);
                textBox1.Location = new Point(85, 33);
            }
        }
    }
}
