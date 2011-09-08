namespace PrologNet
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
            this.labelCode = new System.Windows.Forms.Label();
            this.textCode = new System.Windows.Forms.TextBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textStatus = new System.Windows.Forms.TextBox();
            this.labelResult = new System.Windows.Forms.Label();
            this.textResult = new System.Windows.Forms.TextBox();
            this.labelQuery = new System.Windows.Forms.Label();
            this.textQuery = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelCode.Location = new System.Drawing.Point(0, 0);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(67, 13);
            this.labelCode.TabIndex = 0;
            this.labelCode.Text = "Prolog code:";
            // 
            // textCode
            // 
            this.textCode.Dock = System.Windows.Forms.DockStyle.Top;
            this.textCode.Location = new System.Drawing.Point(67, 0);
            this.textCode.Multiline = true;
            this.textCode.Name = "textCode";
            this.textCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textCode.Size = new System.Drawing.Size(589, 127);
            this.textCode.TabIndex = 1;
            this.textCode.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Enabled = false;
            this.buttonExecute.Location = new System.Drawing.Point(67, 153);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 4;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.OnExecute);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 182);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(40, 13);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status:";
            // 
            // textStatus
            // 
            this.textStatus.Location = new System.Drawing.Point(67, 182);
            this.textStatus.Name = "textStatus";
            this.textStatus.Size = new System.Drawing.Size(587, 20);
            this.textStatus.TabIndex = 6;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Location = new System.Drawing.Point(13, 253);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(40, 13);
            this.labelResult.TabIndex = 7;
            this.labelResult.Text = "Result:";
            // 
            // textResult
            // 
            this.textResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textResult.Location = new System.Drawing.Point(67, 253);
            this.textResult.Multiline = true;
            this.textResult.Name = "textResult";
            this.textResult.Size = new System.Drawing.Size(587, 104);
            this.textResult.TabIndex = 8;
            // 
            // labelQuery
            // 
            this.labelQuery.AutoSize = true;
            this.labelQuery.Location = new System.Drawing.Point(12, 132);
            this.labelQuery.Name = "labelQuery";
            this.labelQuery.Size = new System.Drawing.Size(38, 13);
            this.labelQuery.TabIndex = 2;
            this.labelQuery.Text = "&Query:";
            // 
            // textQuery
            // 
            this.textQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.textQuery.Location = new System.Drawing.Point(67, 127);
            this.textQuery.Name = "textQuery";
            this.textQuery.Size = new System.Drawing.Size(589, 20);
            this.textQuery.TabIndex = 3;
            this.textQuery.TextChanged += new System.EventHandler(this.OnTextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 356);
            this.Controls.Add(this.textResult);
            this.Controls.Add(this.textStatus);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.textQuery);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.labelQuery);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelCode);
            this.Name = "MainForm";
            this.Text = "Prolog .NET";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textStatus;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.TextBox textResult;
        private System.Windows.Forms.Label labelQuery;
        private System.Windows.Forms.TextBox textQuery;
    }
}

