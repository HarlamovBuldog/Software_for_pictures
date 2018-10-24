namespace PicturesSoft
{
    partial class CreateAndEditChildForm
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
            this.opnFileDlgChildBtn = new System.Windows.Forms.Button();
            this.childImgPathTextBox = new System.Windows.Forms.TextBox();
            this.childNameTextBox = new System.Windows.Forms.TextBox();
            this.childCodeTextBox = new System.Windows.Forms.TextBox();
            this.childImgPathLable = new System.Windows.Forms.Label();
            this.childNameLabel = new System.Windows.Forms.Label();
            this.childCodeLabel = new System.Windows.Forms.Label();
            this.CreateAndEditChildCancelBtn = new System.Windows.Forms.Button();
            this.CreateAndEditChildSaveBtn = new System.Windows.Forms.Button();
            this.childSimpleNameTextBox = new System.Windows.Forms.TextBox();
            this.childSimpleNameLabel = new System.Windows.Forms.Label();
            this.childGroupCodeTextBox = new System.Windows.Forms.TextBox();
            this.childGroupCodeLabel = new System.Windows.Forms.Label();
            this.radioBtnsStoragePanel = new System.Windows.Forms.Panel();
            this.addToTheEndRadioBtn = new System.Windows.Forms.RadioButton();
            this.addAfterSelectedRadioBtn = new System.Windows.Forms.RadioButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.radioBtnsStoragePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // opnFileDlgChildBtn
            // 
            this.opnFileDlgChildBtn.Location = new System.Drawing.Point(277, 217);
            this.opnFileDlgChildBtn.Name = "opnFileDlgChildBtn";
            this.opnFileDlgChildBtn.Size = new System.Drawing.Size(65, 19);
            this.opnFileDlgChildBtn.TabIndex = 27;
            this.opnFileDlgChildBtn.Text = "Browse...";
            this.opnFileDlgChildBtn.UseVisualStyleBackColor = true;
            this.opnFileDlgChildBtn.Click += new System.EventHandler(this.opnFileDlgChildBtn_Click);
            // 
            // childImgPathTextBox
            // 
            this.childImgPathTextBox.Enabled = false;
            this.childImgPathTextBox.Location = new System.Drawing.Point(106, 216);
            this.childImgPathTextBox.Name = "childImgPathTextBox";
            this.childImgPathTextBox.Size = new System.Drawing.Size(135, 20);
            this.childImgPathTextBox.TabIndex = 26;
            this.childImgPathTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.childImgPathTextBox_Validating);
            // 
            // childNameTextBox
            // 
            this.childNameTextBox.Location = new System.Drawing.Point(106, 77);
            this.childNameTextBox.Name = "childNameTextBox";
            this.childNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.childNameTextBox.TabIndex = 25;
            this.childNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.childNameTextBox_Validating);
            // 
            // childCodeTextBox
            // 
            this.childCodeTextBox.Location = new System.Drawing.Point(107, 32);
            this.childCodeTextBox.Name = "childCodeTextBox";
            this.childCodeTextBox.Size = new System.Drawing.Size(135, 20);
            this.childCodeTextBox.TabIndex = 24;
            this.childCodeTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.childCodeTextBox_Validating);
            // 
            // childImgPathLable
            // 
            this.childImgPathLable.AutoSize = true;
            this.childImgPathLable.Location = new System.Drawing.Point(33, 219);
            this.childImgPathLable.Name = "childImgPathLable";
            this.childImgPathLable.Size = new System.Drawing.Size(65, 13);
            this.childImgPathLable.TabIndex = 23;
            this.childImgPathLable.Text = "Image name";
            // 
            // childNameLabel
            // 
            this.childNameLabel.AutoSize = true;
            this.childNameLabel.Location = new System.Drawing.Point(65, 80);
            this.childNameLabel.Name = "childNameLabel";
            this.childNameLabel.Size = new System.Drawing.Size(35, 13);
            this.childNameLabel.TabIndex = 22;
            this.childNameLabel.Text = "Name";
            // 
            // childCodeLabel
            // 
            this.childCodeLabel.AutoSize = true;
            this.childCodeLabel.Location = new System.Drawing.Point(68, 35);
            this.childCodeLabel.Name = "childCodeLabel";
            this.childCodeLabel.Size = new System.Drawing.Size(32, 13);
            this.childCodeLabel.TabIndex = 21;
            this.childCodeLabel.Text = "Code";
            // 
            // CreateAndEditChildCancelBtn
            // 
            this.CreateAndEditChildCancelBtn.Location = new System.Drawing.Point(51, 318);
            this.CreateAndEditChildCancelBtn.Name = "CreateAndEditChildCancelBtn";
            this.CreateAndEditChildCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditChildCancelBtn.TabIndex = 20;
            this.CreateAndEditChildCancelBtn.Text = "Cancel";
            this.CreateAndEditChildCancelBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditChildCancelBtn.Click += new System.EventHandler(this.CreateAndEditChildCancelBtn_Click);
            // 
            // CreateAndEditChildSaveBtn
            // 
            this.CreateAndEditChildSaveBtn.Location = new System.Drawing.Point(237, 319);
            this.CreateAndEditChildSaveBtn.Name = "CreateAndEditChildSaveBtn";
            this.CreateAndEditChildSaveBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditChildSaveBtn.TabIndex = 19;
            this.CreateAndEditChildSaveBtn.Text = "Save";
            this.CreateAndEditChildSaveBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditChildSaveBtn.Click += new System.EventHandler(this.CreateAndEditChildSaveBtn_Click);
            // 
            // childSimpleNameTextBox
            // 
            this.childSimpleNameTextBox.Location = new System.Drawing.Point(107, 123);
            this.childSimpleNameTextBox.Name = "childSimpleNameTextBox";
            this.childSimpleNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.childSimpleNameTextBox.TabIndex = 29;
            this.childSimpleNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.childSimpleNameTextBox_Validating);
            // 
            // childSimpleNameLabel
            // 
            this.childSimpleNameLabel.AutoSize = true;
            this.childSimpleNameLabel.Location = new System.Drawing.Point(33, 126);
            this.childSimpleNameLabel.Name = "childSimpleNameLabel";
            this.childSimpleNameLabel.Size = new System.Drawing.Size(67, 13);
            this.childSimpleNameLabel.TabIndex = 28;
            this.childSimpleNameLabel.Text = "Simple name";
            // 
            // childGroupCodeTextBox
            // 
            this.childGroupCodeTextBox.Enabled = false;
            this.childGroupCodeTextBox.Location = new System.Drawing.Point(106, 171);
            this.childGroupCodeTextBox.Name = "childGroupCodeTextBox";
            this.childGroupCodeTextBox.Size = new System.Drawing.Size(135, 20);
            this.childGroupCodeTextBox.TabIndex = 31;
            // 
            // childGroupCodeLabel
            // 
            this.childGroupCodeLabel.AutoSize = true;
            this.childGroupCodeLabel.Location = new System.Drawing.Point(37, 174);
            this.childGroupCodeLabel.Name = "childGroupCodeLabel";
            this.childGroupCodeLabel.Size = new System.Drawing.Size(63, 13);
            this.childGroupCodeLabel.TabIndex = 30;
            this.childGroupCodeLabel.Text = "Group code";
            // 
            // radioBtnsStoragePanel
            // 
            this.radioBtnsStoragePanel.Controls.Add(this.addToTheEndRadioBtn);
            this.radioBtnsStoragePanel.Controls.Add(this.addAfterSelectedRadioBtn);
            this.radioBtnsStoragePanel.Location = new System.Drawing.Point(88, 259);
            this.radioBtnsStoragePanel.Name = "radioBtnsStoragePanel";
            this.radioBtnsStoragePanel.Size = new System.Drawing.Size(174, 53);
            this.radioBtnsStoragePanel.TabIndex = 32;
            // 
            // addToTheEndRadioBtn
            // 
            this.addToTheEndRadioBtn.AutoSize = true;
            this.addToTheEndRadioBtn.Checked = true;
            this.addToTheEndRadioBtn.Location = new System.Drawing.Point(19, 3);
            this.addToTheEndRadioBtn.Name = "addToTheEndRadioBtn";
            this.addToTheEndRadioBtn.Size = new System.Drawing.Size(120, 17);
            this.addToTheEndRadioBtn.TabIndex = 19;
            this.addToTheEndRadioBtn.TabStop = true;
            this.addToTheEndRadioBtn.Text = "Add to the last page";
            this.addToTheEndRadioBtn.UseVisualStyleBackColor = true;
            // 
            // addAfterSelectedRadioBtn
            // 
            this.addAfterSelectedRadioBtn.AutoSize = true;
            this.addAfterSelectedRadioBtn.Location = new System.Drawing.Point(19, 26);
            this.addAfterSelectedRadioBtn.Name = "addAfterSelectedRadioBtn";
            this.addAfterSelectedRadioBtn.Size = new System.Drawing.Size(111, 17);
            this.addAfterSelectedRadioBtn.TabIndex = 20;
            this.addAfterSelectedRadioBtn.Text = "Add after selected";
            this.addAfterSelectedRadioBtn.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CreateAndEditChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 354);
            this.Controls.Add(this.radioBtnsStoragePanel);
            this.Controls.Add(this.childGroupCodeTextBox);
            this.Controls.Add(this.childGroupCodeLabel);
            this.Controls.Add(this.childSimpleNameTextBox);
            this.Controls.Add(this.childSimpleNameLabel);
            this.Controls.Add(this.opnFileDlgChildBtn);
            this.Controls.Add(this.childImgPathTextBox);
            this.Controls.Add(this.childNameTextBox);
            this.Controls.Add(this.childCodeTextBox);
            this.Controls.Add(this.childImgPathLable);
            this.Controls.Add(this.childNameLabel);
            this.Controls.Add(this.childCodeLabel);
            this.Controls.Add(this.CreateAndEditChildCancelBtn);
            this.Controls.Add(this.CreateAndEditChildSaveBtn);
            this.Name = "CreateAndEditChildForm";
            this.radioBtnsStoragePanel.ResumeLayout(false);
            this.radioBtnsStoragePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button opnFileDlgChildBtn;
        private System.Windows.Forms.TextBox childImgPathTextBox;
        private System.Windows.Forms.TextBox childNameTextBox;
        private System.Windows.Forms.TextBox childCodeTextBox;
        private System.Windows.Forms.Label childImgPathLable;
        private System.Windows.Forms.Label childNameLabel;
        private System.Windows.Forms.Label childCodeLabel;
        private System.Windows.Forms.Button CreateAndEditChildCancelBtn;
        private System.Windows.Forms.Button CreateAndEditChildSaveBtn;
        private System.Windows.Forms.TextBox childSimpleNameTextBox;
        private System.Windows.Forms.Label childSimpleNameLabel;
        private System.Windows.Forms.TextBox childGroupCodeTextBox;
        private System.Windows.Forms.Label childGroupCodeLabel;
        private System.Windows.Forms.Panel radioBtnsStoragePanel;
        private System.Windows.Forms.RadioButton addToTheEndRadioBtn;
        private System.Windows.Forms.RadioButton addAfterSelectedRadioBtn;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}