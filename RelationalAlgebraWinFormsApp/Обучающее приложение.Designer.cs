namespace RelationalAlgebraWinFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.FillInAutomaticallyButton = new System.Windows.Forms.Button();
            this.FillInManuallyButton = new System.Windows.Forms.Button();
            this.SelectOperationButton = new System.Windows.Forms.Button();
            this.ShowResultButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьСтрокуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.леваяТаблицаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.праваяТаблицаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // FillInAutomaticallyButton
            // 
            this.FillInAutomaticallyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillInAutomaticallyButton.Location = new System.Drawing.Point(37, 470);
            this.FillInAutomaticallyButton.Name = "FillInAutomaticallyButton";
            this.FillInAutomaticallyButton.Size = new System.Drawing.Size(149, 52);
            this.FillInAutomaticallyButton.TabIndex = 0;
            this.FillInAutomaticallyButton.Text = "Заполнить автоматически";
            this.FillInAutomaticallyButton.UseVisualStyleBackColor = true;
            this.FillInAutomaticallyButton.Click += new System.EventHandler(this.FillInAutomaticallyButton_Click);
            // 
            // FillInManuallyButton
            // 
            this.FillInManuallyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillInManuallyButton.Location = new System.Drawing.Point(209, 470);
            this.FillInManuallyButton.Name = "FillInManuallyButton";
            this.FillInManuallyButton.Size = new System.Drawing.Size(149, 52);
            this.FillInManuallyButton.TabIndex = 1;
            this.FillInManuallyButton.Text = "Заполнить в ручном режиме";
            this.FillInManuallyButton.UseVisualStyleBackColor = true;
            this.FillInManuallyButton.Click += new System.EventHandler(this.FillInManuallyButton_Click);
            // 
            // SelectOperationButton
            // 
            this.SelectOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectOperationButton.Location = new System.Drawing.Point(382, 470);
            this.SelectOperationButton.Name = "SelectOperationButton";
            this.SelectOperationButton.Size = new System.Drawing.Size(149, 52);
            this.SelectOperationButton.TabIndex = 2;
            this.SelectOperationButton.Text = "Выбрать операцию";
            this.SelectOperationButton.UseVisualStyleBackColor = true;
            this.SelectOperationButton.Click += new System.EventHandler(this.SelectOperationButton_Click);
            // 
            // ShowResultButton
            // 
            this.ShowResultButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowResultButton.Location = new System.Drawing.Point(751, 470);
            this.ShowResultButton.Name = "ShowResultButton";
            this.ShowResultButton.Size = new System.Drawing.Size(149, 52);
            this.ShowResultButton.TabIndex = 3;
            this.ShowResultButton.Text = "Результат";
            this.ShowResultButton.UseVisualStyleBackColor = true;
            this.ShowResultButton.Click += new System.EventHandler(this.ShowResultButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.информацияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1078, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.добавитьСтрокуToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // добавитьСтрокуToolStripMenuItem
            // 
            this.добавитьСтрокуToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.леваяТаблицаToolStripMenuItem,
            this.праваяТаблицаToolStripMenuItem});
            this.добавитьСтрокуToolStripMenuItem.Name = "добавитьСтрокуToolStripMenuItem";
            this.добавитьСтрокуToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.добавитьСтрокуToolStripMenuItem.Text = "Добавить запись";
            this.добавитьСтрокуToolStripMenuItem.Visible = false;
            // 
            // леваяТаблицаToolStripMenuItem
            // 
            this.леваяТаблицаToolStripMenuItem.Name = "леваяТаблицаToolStripMenuItem";
            this.леваяТаблицаToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.леваяТаблицаToolStripMenuItem.Text = "Левая таблица";
            this.леваяТаблицаToolStripMenuItem.Click += new System.EventHandler(this.леваяТаблицаToolStripMenuItem_Click);
            // 
            // праваяТаблицаToolStripMenuItem
            // 
            this.праваяТаблицаToolStripMenuItem.Name = "праваяТаблицаToolStripMenuItem";
            this.праваяТаблицаToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.праваяТаблицаToolStripMenuItem.Text = "Правая таблица";
            this.праваяТаблицаToolStripMenuItem.Click += new System.EventHandler(this.праваяТаблицаToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 110);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(501, 249);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(565, 110);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(501, 249);
            this.dataGridView2.TabIndex = 6;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged_1);
            this.dataGridView2.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView2_RowsAdded);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // информацияToolStripMenuItem
            // 
            this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
            this.информацияToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.информацияToolStripMenuItem.Text = "Информация";
            this.информацияToolStripMenuItem.Click += new System.EventHandler(this.информацияToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 570);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ShowResultButton);
            this.Controls.Add(this.SelectOperationButton);
            this.Controls.Add(this.FillInManuallyButton);
            this.Controls.Add(this.FillInAutomaticallyButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Обучающее приложение \"Реляционная алгебра\"";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FillInAutomaticallyButton;
        private System.Windows.Forms.Button FillInManuallyButton;
        private System.Windows.Forms.Button SelectOperationButton;
        private System.Windows.Forms.Button ShowResultButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripMenuItem добавитьСтрокуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem леваяТаблицаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem праваяТаблицаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
    }
}

