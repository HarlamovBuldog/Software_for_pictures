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
            this.opnFileDlgGrBtn = new System.Windows.Forms.Button();
            this.groupImgPathTextBox = new System.Windows.Forms.TextBox();
            this.groupNameTextBox = new System.Windows.Forms.TextBox();
            this.groupIdTextBox = new System.Windows.Forms.TextBox();
            this.imgPathLable = new System.Windows.Forms.Label();
            this.groupNameLabel = new System.Windows.Forms.Label();
            this.groupIdName = new System.Windows.Forms.Label();
            this.CreateAndEditGrCancelBtn = new System.Windows.Forms.Button();
            this.CreateAndEditGrSaveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // opnFileDlgGrBtn
            // 
            this.opnFileDlgGrBtn.Location = new System.Drawing.Point(272, 126);
            this.opnFileDlgGrBtn.Name = "opnFileDlgGrBtn";
            this.opnFileDlgGrBtn.Size = new System.Drawing.Size(65, 19);
            this.opnFileDlgGrBtn.TabIndex = 18;
            this.opnFileDlgGrBtn.Text = "Browse...";
            this.opnFileDlgGrBtn.UseVisualStyleBackColor = true;
            // 
            // groupImgPathTextBox
            // 
            this.groupImgPathTextBox.Enabled = false;
            this.groupImgPathTextBox.Location = new System.Drawing.Point(130, 126);
            this.groupImgPathTextBox.Name = "groupImgPathTextBox";
            this.groupImgPathTextBox.Size = new System.Drawing.Size(135, 20);
            this.groupImgPathTextBox.TabIndex = 17;
            // 
            // groupNameTextBox
            // 
            this.groupNameTextBox.Location = new System.Drawing.Point(130, 80);
            this.groupNameTextBox.Name = "groupNameTextBox";
            this.groupNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.groupNameTextBox.TabIndex = 16;
            // 
            // groupIdTextBox
            // 
            this.groupIdTextBox.Location = new System.Drawing.Point(131, 35);
            this.groupIdTextBox.Name = "groupIdTextBox";
            this.groupIdTextBox.Size = new System.Drawing.Size(135, 20);
            this.groupIdTextBox.TabIndex = 15;
            // 
            // imgPathLable
            // 
            this.imgPathLable.AutoSize = true;
            this.imgPathLable.Location = new System.Drawing.Point(66, 133);
            this.imgPathLable.Name = "imgPathLable";
            this.imgPathLable.Size = new System.Drawing.Size(58, 13);
            this.imgPathLable.TabIndex = 14;
            this.imgPathLable.Text = "ImagePath";
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.Location = new System.Drawing.Point(89, 83);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(35, 13);
            this.groupNameLabel.TabIndex = 13;
            this.groupNameLabel.Text = "Name";
            // 
            // groupIdName
            // 
            this.groupIdName.AutoSize = true;
            this.groupIdName.Location = new System.Drawing.Point(108, 42);
            this.groupIdName.Name = "groupIdName";
            this.groupIdName.Size = new System.Drawing.Size(16, 13);
            this.groupIdName.TabIndex = 12;
            this.groupIdName.Text = "Id";
            // 
            // CreateAndEditGrCancelBtn
            // 
            this.CreateAndEditGrCancelBtn.Location = new System.Drawing.Point(78, 179);
            this.CreateAndEditGrCancelBtn.Name = "CreateAndEditGrCancelBtn";
            this.CreateAndEditGrCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditGrCancelBtn.TabIndex = 11;
            this.CreateAndEditGrCancelBtn.Text = "Cancel";
            this.CreateAndEditGrCancelBtn.UseVisualStyleBackColor = true;
            // 
            // CreateAndEditGrSaveBtn
            // 
            this.CreateAndEditGrSaveBtn.Location = new System.Drawing.Point(264, 180);
            this.CreateAndEditGrSaveBtn.Name = "CreateAndEditGrSaveBtn";
            this.CreateAndEditGrSaveBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditGrSaveBtn.TabIndex = 10;
            this.CreateAndEditGrSaveBtn.Text = "Save";
            this.CreateAndEditGrSaveBtn.UseVisualStyleBackColor = true;
            // 
            // CreateAndEditGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 236);
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
    }
}