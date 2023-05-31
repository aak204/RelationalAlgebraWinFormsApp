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
            this.ProjectionButton = new System.Windows.Forms.Button();
            this.SelectButton = new System.Windows.Forms.Button();
            this.InnerJoinButton = new System.Windows.Forms.Button();
            this.CPButton = new System.Windows.Forms.Button();
            this.DivideButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FullJoinButton = new System.Windows.Forms.Button();
            this.RightJoinButton = new System.Windows.Forms.Button();
            this.LeftJoinButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UnionButton
            // 
            this.UnionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UnionButton.Location = new System.Drawing.Point(47, 23);
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
            this.IntersectionButton.Location = new System.Drawing.Point(47, 87);
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
            this.DifferenceButton.Location = new System.Drawing.Point(47, 151);
            this.DifferenceButton.Name = "DifferenceButton";
            this.DifferenceButton.Size = new System.Drawing.Size(129, 58);
            this.DifferenceButton.TabIndex = 2;
            this.DifferenceButton.Text = "Вычитание";
            this.DifferenceButton.UseVisualStyleBackColor = true;
            this.DifferenceButton.Click += new System.EventHandler(this.DifferenceButton_Click_1);
            // 
            // ProjectionButton
            // 
            this.ProjectionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProjectionButton.Location = new System.Drawing.Point(551, 125);
            this.ProjectionButton.Name = "ProjectionButton";
            this.ProjectionButton.Size = new System.Drawing.Size(125, 58);
            this.ProjectionButton.TabIndex = 10;
            this.ProjectionButton.Text = "Проекция";
            this.ProjectionButton.UseVisualStyleBackColor = true;
            this.ProjectionButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectButton
            // 
            this.SelectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectButton.Location = new System.Drawing.Point(551, 61);
            this.SelectButton.Name = "SelectButton";
            this.SelectButton.Size = new System.Drawing.Size(125, 58);
            this.SelectButton.TabIndex = 9;
            this.SelectButton.Text = "Выборка";
            this.SelectButton.UseVisualStyleBackColor = true;
            this.SelectButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // InnerJoinButton
            // 
            this.InnerJoinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.InnerJoinButton.Location = new System.Drawing.Point(61, 19);
            this.InnerJoinButton.Name = "InnerJoinButton";
            this.InnerJoinButton.Size = new System.Drawing.Size(125, 60);
            this.InnerJoinButton.TabIndex = 5;
            this.InnerJoinButton.Text = "Внутреннее соединение";
            this.InnerJoinButton.UseVisualStyleBackColor = true;
            this.InnerJoinButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // CPButton
            // 
            this.CPButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CPButton.Location = new System.Drawing.Point(47, 215);
            this.CPButton.Name = "CPButton";
            this.CPButton.Size = new System.Drawing.Size(129, 58);
            this.CPButton.TabIndex = 3;
            this.CPButton.Text = "Декартово произведение";
            this.CPButton.UseVisualStyleBackColor = true;
            this.CPButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // DivideButton
            // 
            this.DivideButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DivideButton.Location = new System.Drawing.Point(551, 187);
            this.DivideButton.Name = "DivideButton";
            this.DivideButton.Size = new System.Drawing.Size(125, 58);
            this.DivideButton.TabIndex = 11;
            this.DivideButton.Text = "Деление";
            this.DivideButton.UseVisualStyleBackColor = true;
            this.DivideButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FullJoinButton);
            this.groupBox1.Controls.Add(this.RightJoinButton);
            this.groupBox1.Controls.Add(this.LeftJoinButton);
            this.groupBox1.Controls.Add(this.InnerJoinButton);
            this.groupBox1.Location = new System.Drawing.Point(249, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 288);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Соединения";
            // 
            // FullJoinButton
            // 
            this.FullJoinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FullJoinButton.Location = new System.Drawing.Point(61, 217);
            this.FullJoinButton.Name = "FullJoinButton";
            this.FullJoinButton.Size = new System.Drawing.Size(125, 60);
            this.FullJoinButton.TabIndex = 8;
            this.FullJoinButton.Text = "Полное соединение";
            this.FullJoinButton.UseVisualStyleBackColor = true;
            this.FullJoinButton.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // RightJoinButton
            // 
            this.RightJoinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RightJoinButton.Location = new System.Drawing.Point(61, 151);
            this.RightJoinButton.Name = "RightJoinButton";
            this.RightJoinButton.Size = new System.Drawing.Size(125, 60);
            this.RightJoinButton.TabIndex = 7;
            this.RightJoinButton.Text = "Правое соединение";
            this.RightJoinButton.UseVisualStyleBackColor = true;
            this.RightJoinButton.Click += new System.EventHandler(this.RightJoin_Click);
            // 
            // LeftJoinButton
            // 
            this.LeftJoinButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LeftJoinButton.Location = new System.Drawing.Point(61, 85);
            this.LeftJoinButton.Name = "LeftJoinButton";
            this.LeftJoinButton.Size = new System.Drawing.Size(125, 60);
            this.LeftJoinButton.TabIndex = 6;
            this.LeftJoinButton.Text = "Левое соединение";
            this.LeftJoinButton.UseVisualStyleBackColor = true;
            this.LeftJoinButton.Click += new System.EventHandler(this.LeftJoin_Click);
            // 
            // OperationSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 298);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DivideButton);
            this.Controls.Add(this.CPButton);
            this.Controls.Add(this.ProjectionButton);
            this.Controls.Add(this.SelectButton);
            this.Controls.Add(this.DifferenceButton);
            this.Controls.Add(this.IntersectionButton);
            this.Controls.Add(this.UnionButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OperationSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор операций";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button UnionButton;
        private System.Windows.Forms.Button IntersectionButton;
        private System.Windows.Forms.Button DifferenceButton;
        private System.Windows.Forms.Button ProjectionButton;
        private System.Windows.Forms.Button SelectButton;
        private System.Windows.Forms.Button InnerJoinButton;
        private System.Windows.Forms.Button CPButton;
        private System.Windows.Forms.Button DivideButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button RightJoinButton;
        private System.Windows.Forms.Button LeftJoinButton;
        private System.Windows.Forms.Button FullJoinButton;
    }
}