using System.Drawing;

namespace RelationalAlgebraWinFormsApp
{
    partial class MainForm
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
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.леваяТаблицаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.праваяТаблицаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabelOne = new System.Windows.Forms.DataGridView();
            this.TabelTwo = new System.Windows.Forms.DataGridView();
            this.resultListView = new System.Windows.Forms.ListView();
            this.ResultPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabelOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabelTwo)).BeginInit();
            this.SuspendLayout();
            // 
            // FillInAutomaticallyButton
            // 
            this.FillInAutomaticallyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.FillInAutomaticallyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FillInAutomaticallyButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillInAutomaticallyButton.ForeColor = System.Drawing.Color.White;
            this.FillInAutomaticallyButton.Location = new System.Drawing.Point(532, 86);
            this.FillInAutomaticallyButton.Name = "FillInAutomaticallyButton";
            this.FillInAutomaticallyButton.Size = new System.Drawing.Size(149, 52);
            this.FillInAutomaticallyButton.TabIndex = 0;
            this.FillInAutomaticallyButton.Text = "Заполнить автоматически";
            this.FillInAutomaticallyButton.UseVisualStyleBackColor = true;
            this.FillInAutomaticallyButton.Click += new System.EventHandler(this.FillInAutomaticallyButton_Click);
            // 
            // FillInManuallyButton
            // 
            this.FillInManuallyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.FillInManuallyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FillInManuallyButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillInManuallyButton.ForeColor = System.Drawing.Color.White;
            this.FillInManuallyButton.Location = new System.Drawing.Point(532, 144);
            this.FillInManuallyButton.Name = "FillInManuallyButton";
            this.FillInManuallyButton.Size = new System.Drawing.Size(149, 52);
            this.FillInManuallyButton.TabIndex = 1;
            this.FillInManuallyButton.Text = "Заполнить в ручном режиме";
            this.FillInManuallyButton.UseVisualStyleBackColor = true;
            this.FillInManuallyButton.Click += new System.EventHandler(this.FillInManuallyButton_Click);
            // 
            // SelectOperationButton
            // 
            this.SelectOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.SelectOperationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectOperationButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectOperationButton.ForeColor = System.Drawing.Color.White;
            this.SelectOperationButton.Location = new System.Drawing.Point(532, 202);
            this.SelectOperationButton.Name = "SelectOperationButton";
            this.SelectOperationButton.Size = new System.Drawing.Size(149, 52);
            this.SelectOperationButton.TabIndex = 2;
            this.SelectOperationButton.Text = "Выбрать операцию";
            this.SelectOperationButton.UseVisualStyleBackColor = true;
            this.SelectOperationButton.Click += new System.EventHandler(this.SelectOperationButton_Click);
            // 
            // ShowResultButton
            // 
            this.ShowResultButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.ShowResultButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShowResultButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ShowResultButton.ForeColor = System.Drawing.Color.White;
            this.ShowResultButton.Location = new System.Drawing.Point(532, 446);
            this.ShowResultButton.Name = "ShowResultButton";
            this.ShowResultButton.Size = new System.Drawing.Size(149, 52);
            this.ShowResultButton.TabIndex = 3;
            this.ShowResultButton.Text = "Результат";
            this.ShowResultButton.UseVisualStyleBackColor = true;
            this.ShowResultButton.Click += new System.EventHandler(this.ShowResultButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ForeColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.информацияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1123, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.добавитьСтрокуToolStripMenuItem,
            this.добавитьToolStripMenuItem});
            this.менюToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
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
            this.добавитьСтрокуToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.добавитьСтрокуToolStripMenuItem.Name = "добавитьСтрокуToolStripMenuItem";
            this.добавитьСтрокуToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.добавитьСтрокуToolStripMenuItem.Text = "Добавить запись";
            // 
            // леваяТаблицаToolStripMenuItem
            // 
            this.леваяТаблицаToolStripMenuItem.Name = "леваяТаблицаToolStripMenuItem";
            this.леваяТаблицаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.леваяТаблицаToolStripMenuItem.Text = "Верхняя таблица";
            this.леваяТаблицаToolStripMenuItem.Click += new System.EventHandler(this.леваяТаблицаToolStripMenuItem_Click);
            // 
            // праваяТаблицаToolStripMenuItem
            // 
            this.праваяТаблицаToolStripMenuItem.Name = "праваяТаблицаToolStripMenuItem";
            this.праваяТаблицаToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.праваяТаблицаToolStripMenuItem.Text = "Нижняя таблица";
            this.праваяТаблицаToolStripMenuItem.Click += new System.EventHandler(this.праваяТаблицаToolStripMenuItem_Click);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.леваяТаблицаToolStripMenuItem1,
            this.праваяТаблицаToolStripMenuItem1});
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить атрибут";
            // 
            // леваяТаблицаToolStripMenuItem1
            // 
            this.леваяТаблицаToolStripMenuItem1.Name = "леваяТаблицаToolStripMenuItem1";
            this.леваяТаблицаToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.леваяТаблицаToolStripMenuItem1.Text = "Верхняя таблица";
            this.леваяТаблицаToolStripMenuItem1.Click += new System.EventHandler(this.леваяТаблицаToolStripMenuItem1_Click);
            // 
            // праваяТаблицаToolStripMenuItem1
            // 
            this.праваяТаблицаToolStripMenuItem1.Name = "праваяТаблицаToolStripMenuItem1";
            this.праваяТаблицаToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.праваяТаблицаToolStripMenuItem1.Text = "Нижняя таблица";
            this.праваяТаблицаToolStripMenuItem1.Click += new System.EventHandler(this.праваяТаблицаToolStripMenuItem1_Click);
            // 
            // информацияToolStripMenuItem
            // 
            this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
            this.информацияToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.информацияToolStripMenuItem.Text = "Информация";
            this.информацияToolStripMenuItem.Click += new System.EventHandler(this.информацияToolStripMenuItem_Click);
            // 
            // TabelOne
            // 
            this.TabelOne.AllowUserToResizeRows = false;
            this.TabelOne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TabelOne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TabelOne.Location = new System.Drawing.Point(12, 27);
            this.TabelOne.MultiSelect = false;
            this.TabelOne.Name = "TabelOne";
            this.TabelOne.RowHeadersVisible = false;
            this.TabelOne.Size = new System.Drawing.Size(480, 269);
            this.TabelOne.TabIndex = 5;
            this.TabelOne.TabStop = false;
            this.TabelOne.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.TabelOne.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.TabelOne.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // TabelTwo
            // 
            this.TabelTwo.AllowUserToResizeRows = false;
            this.TabelTwo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TabelTwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TabelTwo.Location = new System.Drawing.Point(12, 315);
            this.TabelTwo.MultiSelect = false;
            this.TabelTwo.Name = "TabelTwo";
            this.TabelTwo.RowHeadersVisible = false;
            this.TabelTwo.Size = new System.Drawing.Size(480, 269);
            this.TabelTwo.TabIndex = 6;
            this.TabelTwo.TabStop = false;
            this.TabelTwo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged_1);
            this.TabelTwo.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView2_RowsAdded);
            this.TabelTwo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // resultListView
            // 
            this.resultListView.HideSelection = false;
            this.resultListView.Location = new System.Drawing.Point(781, 72);
            this.resultListView.Name = "resultListView";
            this.resultListView.Size = new System.Drawing.Size(204, 161);
            this.resultListView.TabIndex = 7;
            this.resultListView.UseCompatibleStateImageBehavior = false;
            // 
            // ResultPanel
            // 
            this.ResultPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ResultPanel.Location = new System.Drawing.Point(720, 66);
            this.ResultPanel.Name = "ResultPanel";
            this.ResultPanel.Size = new System.Drawing.Size(391, 518);
            this.ResultPanel.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(868, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "Результат";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 611);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResultPanel);
            this.Controls.Add(this.resultListView);
            this.Controls.Add(this.TabelTwo);
            this.Controls.Add(this.TabelOne);
            this.Controls.Add(this.ShowResultButton);
            this.Controls.Add(this.SelectOperationButton);
            this.Controls.Add(this.FillInManuallyButton);
            this.Controls.Add(this.FillInAutomaticallyButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обучающее приложение \"Реляционная алгебра\"";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabelOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TabelTwo)).EndInit();
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
        private System.Windows.Forms.DataGridView TabelOne;
        private System.Windows.Forms.DataGridView TabelTwo;
        private System.Windows.Forms.ToolStripMenuItem добавитьСтрокуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem леваяТаблицаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem праваяТаблицаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem леваяТаблицаToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem праваяТаблицаToolStripMenuItem1;
        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.Panel ResultPanel;
        private System.Windows.Forms.Label label1;
    }
}

