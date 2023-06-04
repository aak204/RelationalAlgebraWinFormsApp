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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FillInAutomaticallyButton = new System.Windows.Forms.Button();
            this.FillInManuallyButton = new System.Windows.Forms.Button();
            this.SelectOperationButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TableOne = new System.Windows.Forms.DataGridView();
            this.TableTwo = new System.Windows.Forms.DataGridView();
            this.ResultPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.EditTableThree = new System.Windows.Forms.Panel();
            this.EditTableTwo = new System.Windows.Forms.Panel();
            this.EditTableOne = new System.Windows.Forms.Panel();
            this.RowCouneTableThree = new System.Windows.Forms.Label();
            this.RowCouneTableTwo = new System.Windows.Forms.Label();
            this.RowCouneTableOne = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TableThree = new System.Windows.Forms.DataGridView();
            this.RowCountResult = new System.Windows.Forms.Label();
            this.CombOperation = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableTwo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableThree)).BeginInit();
            this.SuspendLayout();
            // 
            // FillInAutomaticallyButton
            // 
            this.FillInAutomaticallyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.FillInAutomaticallyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FillInAutomaticallyButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FillInAutomaticallyButton.ForeColor = System.Drawing.Color.White;
            this.FillInAutomaticallyButton.Location = new System.Drawing.Point(517, 176);
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
            this.FillInManuallyButton.Location = new System.Drawing.Point(517, 234);
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
            this.SelectOperationButton.Location = new System.Drawing.Point(517, 292);
            this.SelectOperationButton.Name = "SelectOperationButton";
            this.SelectOperationButton.Size = new System.Drawing.Size(149, 52);
            this.SelectOperationButton.TabIndex = 2;
            this.SelectOperationButton.Text = "Выбрать операцию";
            this.SelectOperationButton.UseVisualStyleBackColor = true;
            this.SelectOperationButton.Click += new System.EventHandler(this.SelectOperationButton_Click);
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
            this.menuStrip1.Size = new System.Drawing.Size(1154, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem});
            this.менюToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить результат";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // информацияToolStripMenuItem
            // 
            this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
            this.информацияToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.информацияToolStripMenuItem.Text = "Информация";
            this.информацияToolStripMenuItem.Click += new System.EventHandler(this.информацияToolStripMenuItem_Click);
            // 
            // TableOne
            // 
            this.TableOne.AllowUserToResizeRows = false;
            this.TableOne.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableOne.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableOne.Location = new System.Drawing.Point(17, 28);
            this.TableOne.MultiSelect = false;
            this.TableOne.Name = "TableOne";
            this.TableOne.RowHeadersVisible = false;
            this.TableOne.Size = new System.Drawing.Size(369, 266);
            this.TableOne.TabIndex = 5;
            this.TableOne.TabStop = false;
            this.TableOne.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.TableOne.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.TableOne.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // TableTwo
            // 
            this.TableTwo.AllowUserToResizeRows = false;
            this.TableTwo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableTwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableTwo.Location = new System.Drawing.Point(17, 353);
            this.TableTwo.MultiSelect = false;
            this.TableTwo.Name = "TableTwo";
            this.TableTwo.RowHeadersVisible = false;
            this.TableTwo.Size = new System.Drawing.Size(369, 266);
            this.TableTwo.TabIndex = 6;
            this.TableTwo.TabStop = false;
            this.TableTwo.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged_1);
            this.TableTwo.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView2_RowsAdded);
            this.TableTwo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // ResultPanel
            // 
            this.ResultPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResultPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ResultPanel.Location = new System.Drawing.Point(742, 77);
            this.ResultPanel.Name = "ResultPanel";
            this.ResultPanel.Size = new System.Drawing.Size(391, 518);
            this.ResultPanel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(885, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "Результат";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.EditTableThree);
            this.panel1.Controls.Add(this.EditTableTwo);
            this.panel1.Controls.Add(this.EditTableOne);
            this.panel1.Controls.Add(this.RowCouneTableThree);
            this.panel1.Controls.Add(this.RowCouneTableTwo);
            this.panel1.Controls.Add(this.RowCouneTableOne);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TableThree);
            this.panel1.Controls.Add(this.TableOne);
            this.panel1.Controls.Add(this.TableTwo);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 576);
            this.panel1.TabIndex = 10;
            // 
            // EditTableThree
            // 
            this.EditTableThree.BackColor = System.Drawing.SystemColors.Control;
            this.EditTableThree.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EditTableThree.BackgroundImage")));
            this.EditTableThree.ForeColor = System.Drawing.SystemColors.Control;
            this.EditTableThree.Location = new System.Drawing.Point(386, 657);
            this.EditTableThree.Name = "EditTableThree";
            this.EditTableThree.Size = new System.Drawing.Size(16, 17);
            this.EditTableThree.TabIndex = 18;
            this.EditTableThree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EditTableThree_MouseClick);
            // 
            // EditTableTwo
            // 
            this.EditTableTwo.BackColor = System.Drawing.SystemColors.Control;
            this.EditTableTwo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EditTableTwo.BackgroundImage")));
            this.EditTableTwo.ForeColor = System.Drawing.SystemColors.Control;
            this.EditTableTwo.Location = new System.Drawing.Point(386, 333);
            this.EditTableTwo.Name = "EditTableTwo";
            this.EditTableTwo.Size = new System.Drawing.Size(16, 17);
            this.EditTableTwo.TabIndex = 17;
            this.EditTableTwo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EditTableTwo_MouseClick);
            // 
            // EditTableOne
            // 
            this.EditTableOne.BackColor = System.Drawing.SystemColors.Control;
            this.EditTableOne.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EditTableOne.BackgroundImage")));
            this.EditTableOne.ForeColor = System.Drawing.SystemColors.Control;
            this.EditTableOne.Location = new System.Drawing.Point(387, 10);
            this.EditTableOne.Name = "EditTableOne";
            this.EditTableOne.Size = new System.Drawing.Size(16, 17);
            this.EditTableOne.TabIndex = 16;
            this.EditTableOne.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EditTableOne_MouseClick);
            // 
            // RowCouneTableThree
            // 
            this.RowCouneTableThree.AutoSize = true;
            this.RowCouneTableThree.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RowCouneTableThree.ForeColor = System.Drawing.Color.Black;
            this.RowCouneTableThree.Location = new System.Drawing.Point(203, 649);
            this.RowCouneTableThree.Name = "RowCouneTableThree";
            this.RowCouneTableThree.Size = new System.Drawing.Size(161, 21);
            this.RowCouneTableThree.TabIndex = 16;
            this.RowCouneTableThree.Text = "Количество строк -";
            // 
            // RowCouneTableTwo
            // 
            this.RowCouneTableTwo.AutoSize = true;
            this.RowCouneTableTwo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RowCouneTableTwo.ForeColor = System.Drawing.Color.Black;
            this.RowCouneTableTwo.Location = new System.Drawing.Point(203, 329);
            this.RowCouneTableTwo.Name = "RowCouneTableTwo";
            this.RowCouneTableTwo.Size = new System.Drawing.Size(161, 21);
            this.RowCouneTableTwo.TabIndex = 15;
            this.RowCouneTableTwo.Text = "Количество строк -";
            // 
            // RowCouneTableOne
            // 
            this.RowCouneTableOne.AutoSize = true;
            this.RowCouneTableOne.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RowCouneTableOne.ForeColor = System.Drawing.Color.Black;
            this.RowCouneTableOne.Location = new System.Drawing.Point(203, 4);
            this.RowCouneTableOne.Name = "RowCouneTableOne";
            this.RowCouneTableOne.Size = new System.Drawing.Size(161, 21);
            this.RowCouneTableOne.TabIndex = 14;
            this.RowCouneTableOne.Text = "Количество строк -";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(13, 653);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "Отношение C";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(13, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Отношение B";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "Отношение A";
            // 
            // TableThree
            // 
            this.TableThree.AllowUserToResizeRows = false;
            this.TableThree.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TableThree.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableThree.Location = new System.Drawing.Point(17, 677);
            this.TableThree.MultiSelect = false;
            this.TableThree.Name = "TableThree";
            this.TableThree.RowHeadersVisible = false;
            this.TableThree.Size = new System.Drawing.Size(369, 261);
            this.TableThree.TabIndex = 11;
            this.TableThree.TabStop = false;
            this.TableThree.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableThree_CellValueChanged);
            this.TableThree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TableThree_KeyDown);
            // 
            // RowCountResult
            // 
            this.RowCountResult.AutoSize = true;
            this.RowCountResult.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.RowCountResult.ForeColor = System.Drawing.Color.Black;
            this.RowCountResult.Location = new System.Drawing.Point(950, 598);
            this.RowCountResult.Name = "RowCountResult";
            this.RowCountResult.Size = new System.Drawing.Size(161, 21);
            this.RowCountResult.TabIndex = 15;
            this.RowCountResult.Text = "Количество строк -";
            // 
            // CombOperation
            // 
            this.CombOperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.CombOperation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CombOperation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CombOperation.ForeColor = System.Drawing.Color.White;
            this.CombOperation.Location = new System.Drawing.Point(517, 350);
            this.CombOperation.Name = "CombOperation";
            this.CombOperation.Size = new System.Drawing.Size(149, 52);
            this.CombOperation.TabIndex = 3;
            this.CombOperation.Text = "Комбинации операций";
            this.CombOperation.UseVisualStyleBackColor = true;
            this.CombOperation.Click += new System.EventHandler(this.CombOperation_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 642);
            this.Controls.Add(this.CombOperation);
            this.Controls.Add(this.RowCountResult);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResultPanel);
            this.Controls.Add(this.SelectOperationButton);
            this.Controls.Add(this.FillInManuallyButton);
            this.Controls.Add(this.FillInAutomaticallyButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обучающее приложение \"Реляционная алгебра\"";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableTwo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableThree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FillInAutomaticallyButton;
        private System.Windows.Forms.Button FillInManuallyButton;
        private System.Windows.Forms.Button SelectOperationButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.DataGridView TableOne;
        private System.Windows.Forms.DataGridView TableTwo;
        private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
        private System.Windows.Forms.Panel ResultPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView TableThree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label RowCouneTableOne;
        private System.Windows.Forms.Label RowCouneTableThree;
        private System.Windows.Forms.Label RowCouneTableTwo;
        private System.Windows.Forms.Label RowCountResult;
        private System.Windows.Forms.Panel EditTableOne;
        private System.Windows.Forms.Panel EditTableTwo;
        private System.Windows.Forms.Panel EditTableThree;
        private System.Windows.Forms.Button CombOperation;
    }
}

