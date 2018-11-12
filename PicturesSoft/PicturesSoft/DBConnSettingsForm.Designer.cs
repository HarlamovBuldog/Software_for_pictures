namespace PicturesSoft
{
    partial class DBConnSettingsForm
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
            this.passwdLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.ApplyDbSettingsBtn = new System.Windows.Forms.Button();
            this.passwdTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.portIDTextBox = new System.Windows.Forms.TextBox();
            this.serverIDTextBox = new System.Windows.Forms.TextBox();
            this.dataBaseNameLabel = new System.Windows.Forms.Label();
            this.dataBaseNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // passwdLabel
            // 
            this.passwdLabel.AutoSize = true;
            this.passwdLabel.Location = new System.Drawing.Point(45, 102);
            this.passwdLabel.Name = "passwdLabel";
            this.passwdLabel.Size = new System.Drawing.Size(53, 13);
            this.passwdLabel.TabIndex = 17;
            this.passwdLabel.Text = "Password";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(65, 76);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(29, 13);
            this.userNameLabel.TabIndex = 16;
            this.userNameLabel.Text = "User";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(72, 50);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 15;
            this.portLabel.Text = "Port";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.Location = new System.Drawing.Point(60, 24);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(38, 13);
            this.serverLabel.TabIndex = 14;
            this.serverLabel.Text = "Server";
            // 
            // ApplyDbSettingsBtn
            // 
            this.ApplyDbSettingsBtn.Location = new System.Drawing.Point(104, 162);
            this.ApplyDbSettingsBtn.Name = "ApplyDbSettingsBtn";
            this.ApplyDbSettingsBtn.Size = new System.Drawing.Size(154, 32);
            this.ApplyDbSettingsBtn.TabIndex = 13;
            this.ApplyDbSettingsBtn.Text = "Применить";
            this.ApplyDbSettingsBtn.UseVisualStyleBackColor = true;
            this.ApplyDbSettingsBtn.Click += new System.EventHandler(this.ApplyDbSettingsBtn_Click);
            // 
            // passwdTextBox
            // 
            this.passwdTextBox.Location = new System.Drawing.Point(104, 99);
            this.passwdTextBox.Name = "passwdTextBox";
            this.passwdTextBox.Size = new System.Drawing.Size(170, 20);
            this.passwdTextBox.TabIndex = 12;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(104, 73);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(170, 20);
            this.userNameTextBox.TabIndex = 11;
            // 
            // portIDTextBox
            // 
            this.portIDTextBox.Location = new System.Drawing.Point(104, 47);
            this.portIDTextBox.Name = "portIDTextBox";
            this.portIDTextBox.Size = new System.Drawing.Size(170, 20);
            this.portIDTextBox.TabIndex = 10;
            // 
            // serverIDTextBox
            // 
            this.serverIDTextBox.Location = new System.Drawing.Point(104, 21);
            this.serverIDTextBox.Name = "serverIDTextBox";
            this.serverIDTextBox.Size = new System.Drawing.Size(170, 20);
            this.serverIDTextBox.TabIndex = 9;
            // 
            // dataBaseNameLabel
            // 
            this.dataBaseNameLabel.AutoSize = true;
            this.dataBaseNameLabel.Location = new System.Drawing.Point(45, 128);
            this.dataBaseNameLabel.Name = "dataBaseNameLabel";
            this.dataBaseNameLabel.Size = new System.Drawing.Size(53, 13);
            this.dataBaseNameLabel.TabIndex = 19;
            this.dataBaseNameLabel.Text = "Database";
            // 
            // dataBaseNameTextBox
            // 
            this.dataBaseNameTextBox.Location = new System.Drawing.Point(104, 125);
            this.dataBaseNameTextBox.Name = "dataBaseNameTextBox";
            this.dataBaseNameTextBox.Size = new System.Drawing.Size(170, 20);
            this.dataBaseNameTextBox.TabIndex = 18;
            // 
            // DBConnSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 209);
            this.Controls.Add(this.dataBaseNameLabel);
            this.Controls.Add(this.dataBaseNameTextBox);
            this.Controls.Add(this.passwdLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.ApplyDbSettingsBtn);
            this.Controls.Add(this.passwdTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.portIDTextBox);
            this.Controls.Add(this.serverIDTextBox);
            this.Name = "DBConnSettingsForm";
            this.Text = "Настройка подключения к БД";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label passwdLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.Button ApplyDbSettingsBtn;
        private System.Windows.Forms.TextBox passwdTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox portIDTextBox;
        private System.Windows.Forms.TextBox serverIDTextBox;
        private System.Windows.Forms.Label dataBaseNameLabel;
        private System.Windows.Forms.TextBox dataBaseNameTextBox;
    }
}