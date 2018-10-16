﻿namespace PicturesSoft
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
            this.SuspendLayout();
            // 
            // opnFileDlgChildBtn
            // 
            this.opnFileDlgChildBtn.Location = new System.Drawing.Point(248, 216);
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
            // 
            // childNameTextBox
            // 
            this.childNameTextBox.Location = new System.Drawing.Point(106, 77);
            this.childNameTextBox.Name = "childNameTextBox";
            this.childNameTextBox.Size = new System.Drawing.Size(135, 20);
            this.childNameTextBox.TabIndex = 25;
            // 
            // childCodeTextBox
            // 
            this.childCodeTextBox.Location = new System.Drawing.Point(107, 32);
            this.childCodeTextBox.Name = "childCodeTextBox";
            this.childCodeTextBox.Size = new System.Drawing.Size(135, 20);
            this.childCodeTextBox.TabIndex = 24;
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
            this.CreateAndEditChildCancelBtn.Location = new System.Drawing.Point(54, 269);
            this.CreateAndEditChildCancelBtn.Name = "CreateAndEditChildCancelBtn";
            this.CreateAndEditChildCancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CreateAndEditChildCancelBtn.TabIndex = 20;
            this.CreateAndEditChildCancelBtn.Text = "Cancel";
            this.CreateAndEditChildCancelBtn.UseVisualStyleBackColor = true;
            this.CreateAndEditChildCancelBtn.Click += new System.EventHandler(this.CreateAndEditChildCancelBtn_Click);
            // 
            // CreateAndEditChildSaveBtn
            // 
            this.CreateAndEditChildSaveBtn.Location = new System.Drawing.Point(240, 270);
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
            // CreateAndEditChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 312);
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
    }
}