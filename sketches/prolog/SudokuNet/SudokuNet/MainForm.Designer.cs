namespace SudokuNet
{
    partial class MainForm
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
            this.labelBase = new System.Windows.Forms.Label();
            this.comboBase = new System.Windows.Forms.ComboBox();
            this.labelValues = new System.Windows.Forms.Label();
            this.textGiven = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelBase
            // 
            this.labelBase.AutoSize = true;
            this.labelBase.Location = new System.Drawing.Point(13, 13);
            this.labelBase.Name = "labelBase";
            this.labelBase.Size = new System.Drawing.Size(84, 13);
            this.labelBase.TabIndex = 0;
            this.labelBase.Text = "Base of sudoku:";
            // 
            // comboBase
            // 
            this.comboBase.FormattingEnabled = true;
            this.comboBase.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.comboBase.Location = new System.Drawing.Point(123, 13);
            this.comboBase.Name = "comboBase";
            this.comboBase.Size = new System.Drawing.Size(414, 21);
            this.comboBase.TabIndex = 1;
            // 
            // labelValues
            // 
            this.labelValues.AutoSize = true;
            this.labelValues.Location = new System.Drawing.Point(13, 45);
            this.labelValues.Name = "labelValues";
            this.labelValues.Size = new System.Drawing.Size(72, 13);
            this.labelValues.TabIndex = 2;
            this.labelValues.Text = "Given values:";
            // 
            // textGiven
            // 
            this.textGiven.Location = new System.Drawing.Point(123, 45);
            this.textGiven.Name = "textGiven";
            this.textGiven.Size = new System.Drawing.Size(414, 20);
            this.textGiven.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 414);
            this.Controls.Add(this.textGiven);
            this.Controls.Add(this.labelValues);
            this.Controls.Add(this.comboBase);
            this.Controls.Add(this.labelBase);
            this.Name = "MainForm";
            this.Text = "Sudoku Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBase;
        private System.Windows.Forms.ComboBox comboBase;
        private System.Windows.Forms.Label labelValues;
        private System.Windows.Forms.TextBox textGiven;
    }
}

