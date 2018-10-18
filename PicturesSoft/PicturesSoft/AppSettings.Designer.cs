namespace PicturesSoft
{
    partial class AppSettings
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
            this.openFileDlgXmlCnfgFileButton = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.openFolderBrowserDlgBtn = new System.Windows.Forms.Button();
            this.xmlCnfgFilePathTextBox = new System.Windows.Forms.TextBox();
            this.destImgFolderTextBox = new System.Windows.Forms.TextBox();
            this.xmlCnfgFilePathLabel = new System.Windows.Forms.Label();
            this.destImgFolderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDlgXmlCnfgFileButton
            // 
            this.openFileDlgXmlCnfgFileButton.Location = new System.Drawing.Point(253, 72);
            this.openFileDlgXmlCnfgFileButton.Name = "openFileDlgXmlCnfgFileButton";
            this.openFileDlgXmlCnfgFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileDlgXmlCnfgFileButton.TabIndex = 0;
            this.openFileDlgXmlCnfgFileButton.Text = "Browse...";
            this.openFileDlgXmlCnfgFileButton.UseVisualStyleBackColor = true;
            this.openFileDlgXmlCnfgFileButton.Click += new System.EventHandler(this.openFileDlgXmlCnfgFileButton_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(149, 178);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Let\'s go!";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // openFolderBrowserDlgBtn
            // 
            this.openFolderBrowserDlgBtn.Location = new System.Drawing.Point(253, 119);
            this.openFolderBrowserDlgBtn.Name = "openFolderBrowserDlgBtn";
            this.openFolderBrowserDlgBtn.Size = new System.Drawing.Size(75, 23);
            this.openFolderBrowserDlgBtn.TabIndex = 2;
            this.openFolderBrowserDlgBtn.Text = "Browse...";
            this.openFolderBrowserDlgBtn.UseVisualStyleBackColor = true;
            this.openFolderBrowserDlgBtn.Click += new System.EventHandler(this.openFolderBrowserDlgBtn_Click);
            // 
            // xmlCnfgFilePathTextBox
            // 
            this.xmlCnfgFilePathTextBox.Enabled = false;
            this.xmlCnfgFilePathTextBox.Location = new System.Drawing.Point(33, 72);
            this.xmlCnfgFilePathTextBox.Name = "xmlCnfgFilePathTextBox";
            this.xmlCnfgFilePathTextBox.Size = new System.Drawing.Size(201, 20);
            this.xmlCnfgFilePathTextBox.TabIndex = 3;
            this.xmlCnfgFilePathTextBox.TextChanged += new System.EventHandler(this.xmlCnfgFilePathTextBox_TextChanged);
            // 
            // destImgFolderTextBox
            // 
            this.destImgFolderTextBox.Enabled = false;
            this.destImgFolderTextBox.Location = new System.Drawing.Point(33, 119);
            this.destImgFolderTextBox.Name = "destImgFolderTextBox";
            this.destImgFolderTextBox.Size = new System.Drawing.Size(201, 20);
            this.destImgFolderTextBox.TabIndex = 4;
            this.destImgFolderTextBox.TextChanged += new System.EventHandler(this.destImgFolderTextBox_TextChanged);
            // 
            // xmlCnfgFilePathLabel
            // 
            this.xmlCnfgFilePathLabel.AutoSize = true;
            this.xmlCnfgFilePathLabel.Location = new System.Drawing.Point(33, 53);
            this.xmlCnfgFilePathLabel.Name = "xmlCnfgFilePathLabel";
            this.xmlCnfgFilePathLabel.Size = new System.Drawing.Size(96, 13);
            this.xmlCnfgFilePathLabel.TabIndex = 5;
            this.xmlCnfgFilePathLabel.Text = "Xml-config file path";
            // 
            // destImgFolderLabel
            // 
            this.destImgFolderLabel.AutoSize = true;
            this.destImgFolderLabel.Location = new System.Drawing.Point(33, 99);
            this.destImgFolderLabel.Name = "destImgFolderLabel";
            this.destImgFolderLabel.Size = new System.Drawing.Size(120, 13);
            this.destImgFolderLabel.TabIndex = 6;
            this.destImgFolderLabel.Text = "Destination image folder";
            // 
            // AppSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 213);
            this.Controls.Add(this.destImgFolderLabel);
            this.Controls.Add(this.xmlCnfgFilePathLabel);
            this.Controls.Add(this.destImgFolderTextBox);
            this.Controls.Add(this.xmlCnfgFilePathTextBox);
            this.Controls.Add(this.openFolderBrowserDlgBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.openFileDlgXmlCnfgFileButton);
            this.Name = "AppSettings";
            this.Text = "AppSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileDlgXmlCnfgFileButton;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button openFolderBrowserDlgBtn;
        private System.Windows.Forms.TextBox xmlCnfgFilePathTextBox;
        private System.Windows.Forms.TextBox destImgFolderTextBox;
        private System.Windows.Forms.Label xmlCnfgFilePathLabel;
        private System.Windows.Forms.Label destImgFolderLabel;
    }
}