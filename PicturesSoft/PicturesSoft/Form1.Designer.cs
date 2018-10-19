namespace PicturesSoft
{
    partial class Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.BackToGroupsBtn = new System.Windows.Forms.Button();
            this.groupsListView = new System.Windows.Forms.ListView();
            this.childsListView = new System.Windows.Forms.ListView();
            this.createNewItemBtn = new System.Windows.Forms.Button();
            this.editSelectedBtn = new System.Windows.Forms.Button();
            this.deleteSelectedBtn = new System.Windows.Forms.Button();
            this.createFinalXmlBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 26);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 83);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 131);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 1;
            // 
            // BackToGroupsBtn
            // 
            this.BackToGroupsBtn.Location = new System.Drawing.Point(342, 341);
            this.BackToGroupsBtn.Name = "BackToGroupsBtn";
            this.BackToGroupsBtn.Size = new System.Drawing.Size(75, 23);
            this.BackToGroupsBtn.TabIndex = 2;
            this.BackToGroupsBtn.Text = "Back";
            this.BackToGroupsBtn.UseVisualStyleBackColor = true;
            this.BackToGroupsBtn.Click += new System.EventHandler(this.BackToGroupsBtn_Click);
            // 
            // groupsListView
            // 
            this.groupsListView.HideSelection = false;
            this.groupsListView.Location = new System.Drawing.Point(342, 117);
            this.groupsListView.MultiSelect = false;
            this.groupsListView.Name = "groupsListView";
            this.groupsListView.Size = new System.Drawing.Size(339, 218);
            this.groupsListView.TabIndex = 3;
            this.groupsListView.UseCompatibleStateImageBehavior = false;
            this.groupsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.groupsListView_ItemSelectionChanged);
            this.groupsListView.VisibleChanged += new System.EventHandler(this.groupsListView_VisibleChanged);
            this.groupsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.groupsListView_MouseDoubleClick);
            // 
            // childsListView
            // 
            this.childsListView.HideSelection = false;
            this.childsListView.Location = new System.Drawing.Point(342, 117);
            this.childsListView.MultiSelect = false;
            this.childsListView.Name = "childsListView";
            this.childsListView.Size = new System.Drawing.Size(339, 218);
            this.childsListView.TabIndex = 4;
            this.childsListView.UseCompatibleStateImageBehavior = false;
            this.childsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.childsListView_ItemSelectionChanged);
            this.childsListView.VisibleChanged += new System.EventHandler(this.childsListView_VisibleChanged);
            // 
            // createNewItemBtn
            // 
            this.createNewItemBtn.Location = new System.Drawing.Point(687, 123);
            this.createNewItemBtn.Name = "createNewItemBtn";
            this.createNewItemBtn.Size = new System.Drawing.Size(75, 23);
            this.createNewItemBtn.TabIndex = 5;
            this.createNewItemBtn.Text = "Create";
            this.createNewItemBtn.UseVisualStyleBackColor = true;
            this.createNewItemBtn.Click += new System.EventHandler(this.createNewItemBtn_Click);
            // 
            // editSelectedBtn
            // 
            this.editSelectedBtn.Location = new System.Drawing.Point(687, 152);
            this.editSelectedBtn.Name = "editSelectedBtn";
            this.editSelectedBtn.Size = new System.Drawing.Size(75, 23);
            this.editSelectedBtn.TabIndex = 6;
            this.editSelectedBtn.Text = "Edit";
            this.editSelectedBtn.UseVisualStyleBackColor = true;
            this.editSelectedBtn.Click += new System.EventHandler(this.editSelectedBtn_Click);
            // 
            // deleteSelectedBtn
            // 
            this.deleteSelectedBtn.Location = new System.Drawing.Point(687, 181);
            this.deleteSelectedBtn.Name = "deleteSelectedBtn";
            this.deleteSelectedBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteSelectedBtn.TabIndex = 7;
            this.deleteSelectedBtn.Text = "Delete";
            this.deleteSelectedBtn.UseVisualStyleBackColor = true;
            this.deleteSelectedBtn.Click += new System.EventHandler(this.deleteSelectedBtn_Click);
            // 
            // createFinalXmlBtn
            // 
            this.createFinalXmlBtn.Location = new System.Drawing.Point(12, 242);
            this.createFinalXmlBtn.Name = "createFinalXmlBtn";
            this.createFinalXmlBtn.Size = new System.Drawing.Size(75, 23);
            this.createFinalXmlBtn.TabIndex = 8;
            this.createFinalXmlBtn.Text = "Create final xml";
            this.createFinalXmlBtn.UseVisualStyleBackColor = true;
            this.createFinalXmlBtn.Click += new System.EventHandler(this.createFinalXmlBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.createFinalXmlBtn);
            this.Controls.Add(this.deleteSelectedBtn);
            this.Controls.Add(this.editSelectedBtn);
            this.Controls.Add(this.createNewItemBtn);
            this.Controls.Add(this.childsListView);
            this.Controls.Add(this.groupsListView);
            this.Controls.Add(this.BackToGroupsBtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button BackToGroupsBtn;
        private System.Windows.Forms.ListView groupsListView;
        private System.Windows.Forms.ListView childsListView;
        private System.Windows.Forms.Button createNewItemBtn;
        private System.Windows.Forms.Button editSelectedBtn;
        private System.Windows.Forms.Button deleteSelectedBtn;
        private System.Windows.Forms.Button createFinalXmlBtn;
    }
}

