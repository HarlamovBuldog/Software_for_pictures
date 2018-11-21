namespace PicturesSoft
{
    partial class CreateAndEditGroupForm
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
            this.components = new System.ComponentModel.Container();
            this.opnFileDlgGrBtn = new System.Windows.Forms.Button();
            this.groupImgPathTextBox = new System.Windows.Forms.TextBox();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.groupIdTextBox = new System.Windows.Forms.TextBox();
            this.imgPathLable = new System.Windows.Forms.Label();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.groupIdName = new System.Windows.Forms.Label();
            this.CreateAndEditGrCancelBtn = new System.Windows.Forms.Button();
            this.CreateAndEditGrSaveBtn = new System.Windows.Forms.Button();
            this.addToTheEndRadioBtn = new System.Windows.Forms.RadioButton();
            this.addAfterSelectedRadioBtn = new System.Windows.Forms.RadioButton();
            this.radioBtnsStoragePanel = new System.Windows.Forms.Panel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.radioBtnsStoragePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // opnFileDlgGrBtn
            // 
            this.opnFileDlgGrBtn.Location = new System.Drawing.Point(280, 126);
            this.opnFileDlgGrBtn.Name = "opnFileDlgGrBtn";
            this.opnFileDlgGrBtn.Size = new System.Drawing.Size(92, 25);
            this.opnFileDlgGrBtn.TabIndex = 18;
            this.opnFileDlgGrBtn.Text = "Проводник...";
            this.opnFileDlgGrBtn.UseVisualStyleBackColor = true;
            this.opnFileDlgGrBtn.Click += new System.EventHandler(this.opnFileDlgGrBtn_Click);
            // 
            // groupImgPathTextBox
            // 
            this.groupImgPathTextBox.Enabled = false;
            this.groupImgPathTextBox.Location = new System.Drawing.Point(130, 126);
            this.groupImgPathTextBox.Name = "groupImgPathTextBox";
            this.groupImgPathTextBox.Size = new System.Drawing.Size(135, 20);
            this.groupImgPathTextBox.TabIndex = 17;
            this.groupImgPathTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupImgPathTextBox_Validating);
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Location = new System.Drawing.Point(130, 80);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.groupNameTextBox.TabIndex = 16;
            this.groupNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupNameTextBox_Validating);
            // 
            // groupIdTextBox
            // 
            this.groupIdTextBox.Location = new System.Drawing.Point(131, 35);
            this.groupIdTextBox.Name = "groupIdTextBox";
            this.groupIdTextBox.Size = new System.Drawing.Size(135, 20);
            this.groupIdTextBox.TabIndex = 15;
            this.groupIdTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupIdTextBox_Validating);
            // 
            // imgPathLable
            // 
            this.imgPathLable.AutoSize = true;
            this.imgPathLable.Location = new System.Drawing.Point(18, 132);
            this.imgPathLable.Name = "imgPathLable";
            this.imgPathLable.Size = new System.Drawing.Size(107, 13);
            this.imgPathLable.TabIndex = 14;
            this.imgPathLable.Text = "Название картинки";
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(41, 87);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(83, 13);
            this.groupNameLabel.TabIndex = 13;
            this.groupNameLabel.Text = "Наименование";
            // 
            // groupIdName
            // 
            this.groupIdName.AutoSize = true;
            this.groupIdName.Location = new System.Drawing.Point(99, 38);
            this.groupIdName.Name = "groupIdName";
            this.groupIdName.Size = new System.Drawing.Size(26, 13);
            this.groupIdName.TabIndex = 12;
            this.groupIdName.Text = "Код";
            // 
            // CreateAndEditGrCancelBtn
            // 
            this.CreateAndEditGrCancelBtn.Location = new System.Drawing.Point(67, 223);
            this.CreateAndEditGrCancelBtn.Name = "CreateAndEditGrCancelBtn";
            this.CreateAndEditGrCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditGrCancelBtn.TabIndex = 11;
            this.CreateAndEditGrCancelBtn.Text = "Отмена";
            this.CreateAndEditGrCancelBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditGrCancelBtn.Click += new System.EventHandler(this.CreateAndEditGrCancelBtn_Click);
            // 
            // CreateAndEditGrSaveBtn
            // 
            this.CreateAndEditGrSaveBtn.Location = new System.Drawing.Point(253, 224);
            this.CreateAndEditGrSaveBtn.Name = "CreateAndEditGrSaveBtn";
            this.CreateAndEditGrSaveBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditGrSaveBtn.TabIndex = 10;
            this.CreateAndEditGrSaveBtn.Text = "Сохранить";
            this.CreateAndEditGrSaveBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditGrSaveBtn.Click += new System.EventHandler(this.CreateAndEditGrSaveBtn_Click);
            // 
            // addToTheEndRadioBtn
            // 
            this.addToTheEndRadioBtn.AutoSize = true;
            this.addToTheEndRadioBtn.Checked = true;
            this.addToTheEndRadioBtn.Location = new System.Drawing.Point(19, 3);
            this.addToTheEndRadioBtn.Name = "addToTheEndRadioBtn";
            this.addToTheEndRadioBtn.Size = new System.Drawing.Size(156, 17);
            this.addToTheEndRadioBtn.TabIndex = 19;
            this.addToTheEndRadioBtn.TabStop = true;
            this.addToTheEndRadioBtn.Text = "Добавить в конец списка";
            this.addToTheEndRadioBtn.UseVisualStyleBackColor = true;
            // 
            // addAfterSelectedRadioBtn
            // 
            this.addAfterSelectedRadioBtn.AutoSize = true;
            this.addAfterSelectedRadioBtn.Location = new System.Drawing.Point(19, 26);
            this.addAfterSelectedRadioBtn.Name = "addAfterSelectedRadioBtn";
            this.addAfterSelectedRadioBtn.Size = new System.Drawing.Size(172, 17);
            this.addAfterSelectedRadioBtn.TabIndex = 20;
            this.addAfterSelectedRadioBtn.Text = "Добавить после выбранного";
            this.addAfterSelectedRadioBtn.UseVisualStyleBackColor = true;
            // 
            // radioBtnsStoragePanel
            // 
            this.radioBtnsStoragePanel.Controls.Add(this.addToTheEndRadioBtn);
            this.radioBtnsStoragePanel.Controls.Add(this.addAfterSelectedRadioBtn);
            this.radioBtnsStoragePanel.Location = new System.Drawing.Point(111, 164);
            this.radioBtnsStoragePanel.Name = "radioBtnsStoragePanel";
            this.radioBtnsStoragePanel.Size = new System.Drawing.Size(217, 53);
            this.radioBtnsStoragePanel.TabIndex = 21;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateAndEditGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 259);
            this.Controls.Add(this.radioBtnsStoragePanel);
            this.Controls.Add(this.opnFileDlgGrBtn);
            this.Controls.Add(this.groupImgPathTextBox);
            this.Controls.Add(this.groupNameTextBox);
            this.Controls.Add(this.groupIdTextBox);
            this.Controls.Add(this.imgPathLable);
            this.Controls.Add(this.groupNameLabel);
            this.Controls.Add(this.groupIdName);
            this.Controls.Add(this.CreateAndEditGrCancelBtn);
            this.Controls.Add(this.CreateAndEditGrSaveBtn);
            this.Name = "CreateAndEditGroupForm";
            this.radioBtnsStoragePanel.ResumeLayout(false);
            this.radioBtnsStoragePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button opnFileDlgGrBtn;
        private System.Windows.Forms.TextBox groupImgPathTextBox;
        private System.Windows.Forms.TextBox groupNameTextBox;
        private System.Windows.Forms.TextBox groupIdTextBox;
        private System.Windows.Forms.Label imgPathLable;
        private System.Windows.Forms.Label groupNameLabel;
        private System.Windows.Forms.Label groupIdName;
        private System.Windows.Forms.Button CreateAndEditGrCancelBtn;
        private System.Windows.Forms.Button CreateAndEditGrSaveBtn;
        private System.Windows.Forms.RadioButton addToTheEndRadioBtn;
        private System.Windows.Forms.RadioButton addAfterSelectedRadioBtn;
        private System.Windows.Forms.Panel radioBtnsStoragePanel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}