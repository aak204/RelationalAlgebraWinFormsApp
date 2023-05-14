namespace RelationalAlgebraWinFormsApp
{
    partial class OperationSelectionForm
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
            this.UnionButton = new System.Windows.Forms.Button();
            this.IntersectionButton = new System.Windows.Forms.Button();
            this.DifferenceButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.InnerJoin = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LeftJoin = new System.Windows.Forms.Button();
            this.RightJoin = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UnionButton
            // 
            this.UnionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnionButton.Location = new System.Drawing.Point(49, 14);
            this.UnionButton.Name = "UnionButton";
            this.UnionButton.Size = new System.Drawing.Size(129, 58);
            this.UnionButton.TabIndex = 0;
            this.UnionButton.Text = "Объединение";
            this.UnionButton.UseVisualStyleBackColor = true;
            this.UnionButton.Click += new System.EventHandler(this.UnionButton_Click_1);
            // 
            // IntersectionButton
            // 
            this.IntersectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IntersectionButton.Location = new System.Drawing.Point(49, 78);
            this.IntersectionButton.Name = "IntersectionButton";
            this.IntersectionButton.Size = new System.Drawing.Size(129, 58);
            this.IntersectionButton.TabIndex = 1;
            this.IntersectionButton.Text = "Пересечение";
            this.IntersectionButton.UseVisualStyleBackColor = true;
            this.IntersectionButton.Click += new System.EventHandler(this.IntersectionButton_Click_1);
            // 
            // DifferenceButton
            // 
            this.DifferenceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DifferenceButton.Location = new System.Drawing.Point(49, 142);
            this.DifferenceButton.Name = "DifferenceButton";
            this.DifferenceButton.Size = new System.Drawing.Size(129, 58);
            this.DifferenceButton.TabIndex = 2;
            this.DifferenceButton.Text = "Вычитание";
            this.DifferenceButton.UseVisualStyleBackColor = true;
            this.DifferenceButton.Click += new System.EventHandler(this.DifferenceButton_Click_1);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(546, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 58);
            this.button1.TabIndex = 6;
            this.button1.Text = "Проекция";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(546, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 58);
            this.button2.TabIndex = 5;
            this.button2.Text = "Выборка";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InnerJoin
            // 
            this.InnerJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InnerJoin.Location = new System.Drawing.Point(63, 31);
            this.InnerJoin.Name = "InnerJoin";
            this.InnerJoin.Size = new System.Drawing.Size(125, 60);
            this.InnerJoin.TabIndex = 4;
            this.InnerJoin.Text = "Внутреннее соединение";
            this.InnerJoin.UseVisualStyleBackColor = true;
            this.InnerJoin.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.Location = new System.Drawing.Point(49, 206);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 58);
            this.button4.TabIndex = 3;
            this.button4.Text = "Декартово произведение";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.Location = new System.Drawing.Point(546, 179);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 58);
            this.button5.TabIndex = 7;
            this.button5.Text = "Деление";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RightJoin);
            this.groupBox1.Controls.Add(this.LeftJoin);
            this.groupBox1.Controls.Add(this.InnerJoin);
            this.groupBox1.Location = new System.Drawing.Point(249, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 250);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Соединения";
            // 
            // LeftJoin
            // 
            this.LeftJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LeftJoin.Location = new System.Drawing.Point(63, 97);
            this.LeftJoin.Name = "LeftJoin";
            this.LeftJoin.Size = new System.Drawing.Size(125, 60);
            this.LeftJoin.TabIndex = 5;
            this.LeftJoin.Text = "Левое соединение";
            this.LeftJoin.UseVisualStyleBackColor = true;
            this.LeftJoin.Click += new System.EventHandler(this.LeftJoin_Click);
            // 
            // RightJoin
            // 
            this.RightJoin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RightJoin.Location = new System.Drawing.Point(63, 163);
            this.RightJoin.Name = "RightJoin";
            this.RightJoin.Size = new System.Drawing.Size(125, 60);
            this.RightJoin.TabIndex = 6;
            this.RightJoin.Text = "Правое соединение";
            this.RightJoin.UseVisualStyleBackColor = true;
            this.RightJoin.Click += new System.EventHandler(this.RightJoin_Click);
            // 
            // OperationSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 269);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.DifferenceButton);
            this.Controls.Add(this.IntersectionButton);
            this.Controls.Add(this.UnionButton);
            this.Name = "OperationSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OperationSelectionForm";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UnionButton;
        private System.Windows.Forms.Button IntersectionButton;
        private System.Windows.Forms.Button DifferenceButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button InnerJoin;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button RightJoin;
        private System.Windows.Forms.Button LeftJoin;
    }
}