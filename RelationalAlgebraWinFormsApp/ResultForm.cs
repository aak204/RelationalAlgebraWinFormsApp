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
    public partial class ResultForm : Form
    {
        private DataTable result;
        private List<string> columnNames;

        public ResultForm(DataTable result, List<string> columnNames)
        {
            InitializeComponent();
            this.result = result;
            this.columnNames = columnNames;
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            dataGridView.AllowUserToAddRows = false;
            dataGridView.DataSource = result;

            // Установим все столбцы как не подлежащие сортировке
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}

