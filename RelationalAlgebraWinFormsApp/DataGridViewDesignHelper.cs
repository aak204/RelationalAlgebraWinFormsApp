using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelationalAlgebraWinFormsApp
{
    internal class DataGridViewDesignHelper
    {
        public MainForm.OperationDelegate OperationDelegate
        {
            get => default;
            set
            {
            }
        }

        public MainForm MainForm
        {
            get => default;
            set
            {
            }
        }

        public static void ApplyDesign(DataGridView dataGridView)
        {
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

            dataGridView.ColumnHeadersDefaultCellStyle = headerStyle;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle;
            dataGridView.GridColor = Color.FromArgb(100, 100, 100);
            dataGridView.BackgroundColor = Color.FromArgb(40, 40, 40);
            dataGridView.BorderStyle = BorderStyle.None;
        }
    }
}
