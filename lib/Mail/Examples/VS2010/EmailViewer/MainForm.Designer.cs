using Lesnikowski.Windows;

namespace EmailViewer
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
            this._mailBrowser = new Lesnikowski.Windows.MailBrowserControl();
            this._btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _mailBrowser
            // 
            this._mailBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._mailBrowser.Location = new System.Drawing.Point(12, 12);
            this._mailBrowser.Name = "_mailBrowser";
            this._mailBrowser.Size = new System.Drawing.Size(603, 437);
            this._mailBrowser.TabIndex = 0;
            // 
            // _btnLoad
            // 
            this._btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnLoad.Location = new System.Drawing.Point(540, 455);
            this._btnLoad.Name = "_btnLoad";
            this._btnLoad.Size = new System.Drawing.Size(75, 23);
            this._btnLoad.TabIndex = 1;
            this._btnLoad.Text = "Load email";
            this._btnLoad.UseVisualStyleBackColor = true;
            this._btnLoad.Click += new System.EventHandler(this._btnLoad_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 490);
            this.Controls.Add(this._btnLoad);
            this.Controls.Add(this._mailBrowser);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MailBrowserControl _mailBrowser;
        private System.Windows.Forms.Button _btnLoad;
    }
}

