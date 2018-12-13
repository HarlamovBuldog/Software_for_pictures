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
            this.opnFileDlgGrBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opnFileDlgGrBtn.Location = new System.Drawing.Point(327, 155);
            this.opnFileDlgGrBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.opnFileDlgGrBtn.Name = "opnFileDlgGrBtn";
            this.opnFileDlgGrBtn.Size = new System.Drawing.Size(107, 31);
            this.opnFileDlgGrBtn.TabIndex = 18;
            this.opnFileDlgGrBtn.Text = "Проводник...";
            this.opnFileDlgGrBtn.UseVisualStyleBackColor = true;
            this.opnFileDlgGrBtn.Click += new System.EventHandler(this.opnFileDlgGrBtn_Click);
            // 
            // groupImgPathTextBox
            // 
            this.groupImgPathTextBox.Enabled = false;
            this.groupImgPathTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupImgPathTextBox.Location = new System.Drawing.Point(152, 155);
            this.groupImgPathTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupImgPathTextBox.Name = "groupImgPathTextBox";
            this.groupImgPathTextBox.Size = new System.Drawing.Size(157, 23);
            this.groupImgPathTextBox.TabIndex = 17;
            this.groupImgPathTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupImgPathTextBox_Validating);
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupNameTextBox.Location = new System.Drawing.Point(152, 98);
            this.groupNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(157, 23);
            this.groupNameTextBox.TabIndex = 16;
            this.groupNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupNameTextBox_Validating);
            // 
            // groupIdTextBox
            // 
            this.groupIdTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupIdTextBox.Location = new System.Drawing.Point(153, 43);
            this.groupIdTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupIdTextBox.Name = "groupIdTextBox";
            this.groupIdTextBox.Size = new System.Drawing.Size(157, 23);
            this.groupIdTextBox.TabIndex = 15;
            this.groupIdTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.groupIdTextBox_Validating);
            // 
            // imgPathLable
            // 
            this.imgPathLable.AutoSize = true;
            this.imgPathLable.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.imgPathLable.Location = new System.Drawing.Point(21, 162);
            this.imgPathLable.Name = "imgPathLable";
            this.imgPathLable.Size = new System.Drawing.Size(121, 16);
            this.imgPathLable.TabIndex = 14;
            this.imgPathLable.Text = "Название картинки";
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupNameLabel.Location = new System.Drawing.Point(48, 107);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(94, 16);
            this.groupNameLabel.TabIndex = 13;
            this.groupNameLabel.Text = "Наименование";
            // 
            // groupIdName
            // 
            this.groupIdName.AutoSize = true;
            this.groupIdName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupIdName.Location = new System.Drawing.Point(115, 47);
            this.groupIdName.Name = "groupIdName";
            this.groupIdName.Size = new System.Drawing.Size(29, 16);
            this.groupIdName.TabIndex = 12;
            this.groupIdName.Text = "Код";
            // 
            // CreateAndEditGrCancelBtn
            // 
            this.CreateAndEditGrCancelBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreateAndEditGrCancelBtn.Location = new System.Drawing.Point(78, 274);
            this.CreateAndEditGrCancelBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreateAndEditGrCancelBtn.Name = "CreateAndEditGrCancelBtn";
            this.CreateAndEditGrCancelBtn.Size = new System.Drawing.Size(87, 28);
            this.CreateAndEditGrCancelBtn.TabIndex = 11;
            this.CreateAndEditGrCancelBtn.Text = "Отмена";
            this.CreateAndEditGrCancelBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditGrCancelBtn.Click += new System.EventHandler(this.CreateAndEditGrCancelBtn_Click);
            // 
            // CreateAndEditGrSaveBtn
            // 
            this.CreateAndEditGrSaveBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CreateAndEditGrSaveBtn.Location = new System.Drawing.Point(295, 276);
            this.CreateAndEditGrSaveBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CreateAndEditGrSaveBtn.Name = "CreateAndEditGrSaveBtn";
            this.CreateAndEditGrSaveBtn.Size = new System.Drawing.Size(87, 28);
            this.CreateAndEditGrSaveBtn.TabIndex = 10;
            this.CreateAndEditGrSaveBtn.Text = "Сохранить";
            this.CreateAndEditGrSaveBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditGrSaveBtn.Click += new System.EventHandler(this.CreateAndEditGrSaveBtn_Click);
            // 
            // addToTheEndRadioBtn
            // 
            this.addToTheEndRadioBtn.AutoSize = true;
            this.addToTheEndRadioBtn.Checked = true;
            this.addToTheEndRadioBtn.Location = new System.Drawing.Point(22, 4);
            this.addToTheEndRadioBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addToTheEndRadioBtn.Name = "addToTheEndRadioBtn";
            this.addToTheEndRadioBtn.Size = new System.Drawing.Size(204, 25);
            this.addToTheEndRadioBtn.TabIndex = 19;
            this.addToTheEndRadioBtn.TabStop = true;
            this.addToTheEndRadioBtn.Text = "Добавить в конец списка";
            this.addToTheEndRadioBtn.UseVisualStyleBackColor = true;
            // 
            // addAfterSelectedRadioBtn
            // 
            this.addAfterSelectedRadioBtn.AutoSize = true;
            this.addAfterSelectedRadioBtn.Location = new System.Drawing.Point(22, 32);
            this.addAfterSelectedRadioBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addAfterSelectedRadioBtn.Name = "addAfterSelectedRadioBtn";
            this.addAfterSelectedRadioBtn.Size = new System.Drawing.Size(227, 25);
            this.addAfterSelectedRadioBtn.TabIndex = 20;
            this.addAfterSelectedRadioBtn.Text = "Добавить после выбранного";
            this.addAfterSelectedRadioBtn.UseVisualStyleBackColor = true;
            // 
            // radioBtnsStoragePanel
            // 
            this.radioBtnsStoragePanel.Controls.Add(this.addToTheEndRadioBtn);
            this.radioBtnsStoragePanel.Controls.Add(this.addAfterSelectedRadioBtn);
            this.radioBtnsStoragePanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioBtnsStoragePanel.Location = new System.Drawing.Point(129, 202);
            this.radioBtnsStoragePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioBtnsStoragePanel.Name = "radioBtnsStoragePanel";
            this.radioBtnsStoragePanel.Size = new System.Drawing.Size(253, 65);
            this.radioBtnsStoragePanel.TabIndex = 21;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateAndEditGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 319);
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
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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