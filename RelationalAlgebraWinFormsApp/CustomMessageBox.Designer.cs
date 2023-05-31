namespace RelationalAlgebraWinFormsApp
{
    partial class CustomMessageBox
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
            this.messageLabel = new System.Windows.Forms.Label();
            this.yesButton = new System.Windows.Forms.Button();
            this.doNotShowButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageLabel.Location = new System.Drawing.Point(15, 9);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(345, 21);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "Хотите ли вы сохранить результат в файл?";
            // 
            // yesButton
            // 
            this.yesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.yesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yesButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yesButton.ForeColor = System.Drawing.Color.White;
            this.yesButton.Location = new System.Drawing.Point(19, 49);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(95, 50);
            this.yesButton.TabIndex = 17;
            this.yesButton.Text = "Да";
            this.yesButton.UseVisualStyleBackColor = true;
            // 
            // doNotShowButton
            // 
            this.doNotShowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.doNotShowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.doNotShowButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.doNotShowButton.ForeColor = System.Drawing.Color.White;
            this.doNotShowButton.Location = new System.Drawing.Point(274, 49);
            this.doNotShowButton.Name = "doNotShowButton";
            this.doNotShowButton.Size = new System.Drawing.Size(95, 50);
            this.doNotShowButton.TabIndex = 18;
            this.doNotShowButton.Text = "Больше не показывать";
            this.doNotShowButton.UseVisualStyleBackColor = true;
            // 
            // noButton
            // 
            this.noButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(150)))), ((int)(((byte)(200)))));
            this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.noButton.ForeColor = System.Drawing.Color.White;
            this.noButton.Location = new System.Drawing.Point(152, 49);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(95, 50);
            this.noButton.TabIndex = 19;
            this.noButton.Text = "Нет";
            this.noButton.UseVisualStyleBackColor = true;
            // 
            // CustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 111);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.doNotShowButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.messageLabel);
            this.Name = "CustomMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сохранение результата";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button doNotShowButton;
        private System.Windows.Forms.Button noButton;
    }
}