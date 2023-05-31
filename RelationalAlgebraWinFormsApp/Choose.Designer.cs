namespace RelationalAlgebraWinFormsApp
{
    partial class Choose
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.A = new System.Windows.Forms.CheckBox();
            this.B = new System.Windows.Forms.CheckBox();
            this.C = new System.Windows.Forms.CheckBox();
            this.ShowResultButton = new System.Windows.Forms.Button();
            this.labelA = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.GetText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(87, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите таблицы для объединения";
            // 
            // A
            // 
            this.A.AutoSize = true;
            this.A.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.A.Location = new System.Drawing.Point(135, 61);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(40, 25);
            this.A.TabIndex = 1;
            this.A.Text = "A";
            this.A.UseVisualStyleBackColor = true;
            // 
            // B
            // 
            this.B.AutoSize = true;
            this.B.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B.Location = new System.Drawing.Point(224, 61);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(39, 25);
            this.B.TabIndex = 2;
            this.B.Text = "B";
            this.B.UseVisualStyleBackColor = true;
            // 
            // C
            // 
            this.C.AutoSize = true;
            this.C.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.C.Location = new System.Drawing.Point(305, 61);
            this.C.Name = "C";
            this.C.Size = new System.Drawing.Size(39, 25);
            this.C.TabIndex = 3;
            this.C.Text = "C";
            this.C.UseVisualStyleBackColor = true;
            // 
            // ShowResultButton
            // 
            this.ShowResultButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.ShowResultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowResultButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowResultButton.ForeColor = System.Drawing.Color.White;
            this.ShowResultButton.Location = new System.Drawing.Point(160, 118);
            this.ShowResultButton.Name = "ShowResultButton";
            this.ShowResultButton.Size = new System.Drawing.Size(149, 52);
            this.ShowResultButton.TabIndex = 5;
            this.ShowResultButton.Text = "Выполнить операцию";
            this.ShowResultButton.UseVisualStyleBackColor = true;
            this.ShowResultButton.Click += new System.EventHandler(this.ShowResultButton_Click);
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelA.Location = new System.Drawing.Point(115, 62);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(0, 21);
            this.labelA.TabIndex = 6;
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelB.Location = new System.Drawing.Point(204, 62);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(0, 21);
            this.labelB.TabIndex = 7;
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelC.Location = new System.Drawing.Point(285, 62);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(0, 21);
            this.labelC.TabIndex = 8;
            // 
            // GetText
            // 
            this.GetText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GetText.Location = new System.Drawing.Point(135, 86);
            this.GetText.Name = "GetText";
            this.GetText.Size = new System.Drawing.Size(208, 26);
            this.GetText.TabIndex = 9;
            this.GetText.Visible = false;
            // 
            // Choose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 177);
            this.Controls.Add(this.GetText);
            this.Controls.Add(this.labelC);
            this.Controls.Add(this.labelB);
            this.Controls.Add(this.labelA);
            this.Controls.Add(this.ShowResultButton);
            this.Controls.Add(this.C);
            this.Controls.Add(this.B);
            this.Controls.Add(this.A);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Choose";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор отношений";
            this.Load += new System.EventHandler(this.Choose_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox A;
        private System.Windows.Forms.CheckBox B;
        private System.Windows.Forms.CheckBox C;
        private System.Windows.Forms.Button ShowResultButton;
        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelC;
        private System.Windows.Forms.TextBox GetText;
    }
}