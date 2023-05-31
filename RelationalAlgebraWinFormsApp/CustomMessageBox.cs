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
    public partial class CustomMessageBox : Form
    {
        public DialogResult Result { get; private set; }

        public CustomMessageBox()
        {
            InitializeComponent();

            yesButton.Click += (sender, e) => { Result = DialogResult.Yes; this.Close(); };
            noButton.Click += (sender, e) => { Result = DialogResult.No; this.Close(); };
            doNotShowButton.Click += (sender, e) => { Result = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(doNotShowButton);
            this.Controls.Add(noButton);
            this.Controls.Add(yesButton);
            this.Controls.Add(messageLabel);
        }

        public static new DialogResult Show()
        {
            CustomMessageBox msgBox = new CustomMessageBox();
            msgBox.ShowDialog();
            return msgBox.Result;
        }
    }
}
