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
            this.BackToGroupsBtn = new System.Windows.Forms.Button();
            this.groupsListView = new System.Windows.Forms.ListView();
            this.childsListView = new System.Windows.Forms.ListView();
            this.createNewItemBtn = new System.Windows.Forms.Button();
            this.editSelectedBtn = new System.Windows.Forms.Button();
            this.deleteSelectedBtn = new System.Windows.Forms.Button();
            this.createFinalXmlBtn = new System.Windows.Forms.Button();
            this.navBtwPagesTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.moveBackBetweenPagesBtn = new System.Windows.Forms.Button();
            this.moveForwardBetweenPagesBtn = new System.Windows.Forms.Button();
            this.moveObjectsBtnPanel = new System.Windows.Forms.Panel();
            this.moveObjectsPanelLabel = new System.Windows.Forms.Label();
            this.moveObjLeftBtn = new System.Windows.Forms.Button();
            this.moveObjRightBtn = new System.Windows.Forms.Button();
            this.moveObjDownBtn = new System.Windows.Forms.Button();
            this.moveObjUpBtn = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.shopListComboBox = new System.Windows.Forms.ComboBox();
            this.cashesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.uploadInfoToCashBoxBtn = new System.Windows.Forms.Button();
            this.getTemplateFromCashBoxBtn = new System.Windows.Forms.Button();
            this.selectAllCashBoxesCheckBox = new System.Windows.Forms.CheckBox();
            this.moveObjectsBtnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackToGroupsBtn
            // 
            this.BackToGroupsBtn.Location = new System.Drawing.Point(742, 209);
            this.BackToGroupsBtn.Name = "BackToGroupsBtn";
            this.BackToGroupsBtn.Size = new System.Drawing.Size(89, 23);
            this.BackToGroupsBtn.TabIndex = 2;
            this.BackToGroupsBtn.Text = "Back to groups";
            this.BackToGroupsBtn.UseVisualStyleBackColor = true;
            this.BackToGroupsBtn.Click += new System.EventHandler(this.BackToGroupsBtn_Click);
            // 
            // groupsListView
            // 
            this.groupsListView.HideSelection = false;
            this.groupsListView.Location = new System.Drawing.Point(327, 99);
            this.groupsListView.MultiSelect = false;
            this.groupsListView.Name = "groupsListView";
            this.groupsListView.Size = new System.Drawing.Size(408, 330);
            this.groupsListView.TabIndex = 3;
            this.groupsListView.UseCompatibleStateImageBehavior = false;
            this.groupsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.groupsListView_ItemSelectionChanged);
            this.groupsListView.VisibleChanged += new System.EventHandler(this.groupsListView_VisibleChanged);
            this.groupsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.groupsListView_MouseDoubleClick);
            // 
            // childsListView
            // 
            this.childsListView.HideSelection = false;
            this.childsListView.Location = new System.Drawing.Point(327, 99);
            this.childsListView.MultiSelect = false;
            this.childsListView.Name = "childsListView";
            this.childsListView.Size = new System.Drawing.Size(408, 330);
            this.childsListView.TabIndex = 4;
            this.childsListView.UseCompatibleStateImageBehavior = false;
            this.childsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.childsListView_ItemSelectionChanged);
            this.childsListView.VisibleChanged += new System.EventHandler(this.childsListView_VisibleChanged);
            this.childsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.childsListView_MouseDoubleClick);
            // 
            // createNewItemBtn
            // 
            this.createNewItemBtn.Location = new System.Drawing.Point(742, 97);
            this.createNewItemBtn.Name = "createNewItemBtn";
            this.createNewItemBtn.Size = new System.Drawing.Size(75, 23);
            this.createNewItemBtn.TabIndex = 5;
            this.createNewItemBtn.Text = "Create";
            this.createNewItemBtn.UseVisualStyleBackColor = true;
            this.createNewItemBtn.Click += new System.EventHandler(this.createNewItemBtn_Click);
            // 
            // editSelectedBtn
            // 
            this.editSelectedBtn.Location = new System.Drawing.Point(742, 126);
            this.editSelectedBtn.Name = "editSelectedBtn";
            this.editSelectedBtn.Size = new System.Drawing.Size(75, 23);
            this.editSelectedBtn.TabIndex = 6;
            this.editSelectedBtn.Text = "Edit";
            this.editSelectedBtn.UseVisualStyleBackColor = true;
            this.editSelectedBtn.Click += new System.EventHandler(this.editSelectedBtn_Click);
            // 
            // deleteSelectedBtn
            // 
            this.deleteSelectedBtn.Location = new System.Drawing.Point(742, 155);
            this.deleteSelectedBtn.Name = "deleteSelectedBtn";
            this.deleteSelectedBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteSelectedBtn.TabIndex = 7;
            this.deleteSelectedBtn.Text = "Delete";
            this.deleteSelectedBtn.UseVisualStyleBackColor = true;
            this.deleteSelectedBtn.Click += new System.EventHandler(this.deleteSelectedBtn_Click);
            // 
            // createFinalXmlBtn
            // 
            this.createFinalXmlBtn.Location = new System.Drawing.Point(892, 471);
            this.createFinalXmlBtn.Name = "createFinalXmlBtn";
            this.createFinalXmlBtn.Size = new System.Drawing.Size(75, 23);
            this.createFinalXmlBtn.TabIndex = 8;
            this.createFinalXmlBtn.Text = "Create final xml";
            this.createFinalXmlBtn.UseVisualStyleBackColor = true;
            this.createFinalXmlBtn.Click += new System.EventHandler(this.createFinalXmlBtn_Click);
            // 
            // navBtwPagesTableLayout
            // 
            this.navBtwPagesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.navBtwPagesTableLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.navBtwPagesTableLayout.Location = new System.Drawing.Point(383, 431);
            this.navBtwPagesTableLayout.Name = "navBtwPagesTableLayout";
            this.navBtwPagesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.navBtwPagesTableLayout.Size = new System.Drawing.Size(135, 45);
            this.navBtwPagesTableLayout.TabIndex = 9;
            // 
            // moveBackBetweenPagesBtn
            // 
            this.moveBackBetweenPagesBtn.Location = new System.Drawing.Point(328, 431);
            this.moveBackBetweenPagesBtn.Name = "moveBackBetweenPagesBtn";
            this.moveBackBetweenPagesBtn.Size = new System.Drawing.Size(49, 46);
            this.moveBackBetweenPagesBtn.TabIndex = 10;
            this.moveBackBetweenPagesBtn.Text = "←";
            this.moveBackBetweenPagesBtn.UseVisualStyleBackColor = true;
            this.moveBackBetweenPagesBtn.Click += new System.EventHandler(this.moveBackBetweenPagesBtn_Click);
            // 
            // moveForwardBetweenPagesBtn
            // 
            this.moveForwardBetweenPagesBtn.Location = new System.Drawing.Point(687, 431);
            this.moveForwardBetweenPagesBtn.Name = "moveForwardBetweenPagesBtn";
            this.moveForwardBetweenPagesBtn.Size = new System.Drawing.Size(48, 46);
            this.moveForwardBetweenPagesBtn.TabIndex = 11;
            this.moveForwardBetweenPagesBtn.Text = "→";
            this.moveForwardBetweenPagesBtn.UseVisualStyleBackColor = true;
            this.moveForwardBetweenPagesBtn.Click += new System.EventHandler(this.moveForwardBetweenPagesBtn_Click);
            // 
            // moveObjectsBtnPanel
            // 
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjectsPanelLabel);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjLeftBtn);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjRightBtn);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjDownBtn);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjUpBtn);
            this.moveObjectsBtnPanel.Location = new System.Drawing.Point(756, 278);
            this.moveObjectsBtnPanel.Name = "moveObjectsBtnPanel";
            this.moveObjectsBtnPanel.Size = new System.Drawing.Size(211, 121);
            this.moveObjectsBtnPanel.TabIndex = 12;
            // 
            // moveObjectsPanelLabel
            // 
            this.moveObjectsPanelLabel.AutoSize = true;
            this.moveObjectsPanelLabel.Location = new System.Drawing.Point(67, 9);
            this.moveObjectsPanelLabel.Name = "moveObjectsPanelLabel";
            this.moveObjectsPanelLabel.Size = new System.Drawing.Size(71, 13);
            this.moveObjectsPanelLabel.TabIndex = 4;
            this.moveObjectsPanelLabel.Text = "Move objects";
            // 
            // moveObjLeftBtn
            // 
            this.moveObjLeftBtn.Location = new System.Drawing.Point(28, 69);
            this.moveObjLeftBtn.Name = "moveObjLeftBtn";
            this.moveObjLeftBtn.Size = new System.Drawing.Size(47, 38);
            this.moveObjLeftBtn.TabIndex = 3;
            this.moveObjLeftBtn.Text = "←";
            this.moveObjLeftBtn.UseVisualStyleBackColor = true;
            this.moveObjLeftBtn.Click += new System.EventHandler(this.moveObjLeftBtn_Click);
            // 
            // moveObjRightBtn
            // 
            this.moveObjRightBtn.Location = new System.Drawing.Point(133, 69);
            this.moveObjRightBtn.Name = "moveObjRightBtn";
            this.moveObjRightBtn.Size = new System.Drawing.Size(47, 38);
            this.moveObjRightBtn.TabIndex = 2;
            this.moveObjRightBtn.Text = "→";
            this.moveObjRightBtn.UseVisualStyleBackColor = true;
            this.moveObjRightBtn.Click += new System.EventHandler(this.moveObjRightBtn_Click);
            // 
            // moveObjDownBtn
            // 
            this.moveObjDownBtn.Location = new System.Drawing.Point(81, 69);
            this.moveObjDownBtn.Name = "moveObjDownBtn";
            this.moveObjDownBtn.Size = new System.Drawing.Size(47, 38);
            this.moveObjDownBtn.TabIndex = 1;
            this.moveObjDownBtn.Text = "↓";
            this.moveObjDownBtn.UseVisualStyleBackColor = true;
            this.moveObjDownBtn.Click += new System.EventHandler(this.moveObjDownBtn_Click);
            // 
            // moveObjUpBtn
            // 
            this.moveObjUpBtn.Location = new System.Drawing.Point(80, 25);
            this.moveObjUpBtn.Name = "moveObjUpBtn";
            this.moveObjUpBtn.Size = new System.Drawing.Size(47, 38);
            this.moveObjUpBtn.TabIndex = 0;
            this.moveObjUpBtn.Text = "↑";
            this.moveObjUpBtn.UseVisualStyleBackColor = true;
            this.moveObjUpBtn.Click += new System.EventHandler(this.moveObjUpBtn_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1046, 24);
            this.mainMenuStrip.TabIndex = 16;
            // 
            // shopListComboBox
            // 
            this.shopListComboBox.FormattingEnabled = true;
            this.shopListComboBox.Location = new System.Drawing.Point(65, 126);
            this.shopListComboBox.Name = "shopListComboBox";
            this.shopListComboBox.Size = new System.Drawing.Size(202, 21);
            this.shopListComboBox.TabIndex = 20;
            this.shopListComboBox.Text = "Выберите магазин...";
            this.shopListComboBox.SelectedIndexChanged += new System.EventHandler(this.shopListComboBox_SelectedIndexChanged);
            // 
            // cashesCheckedListBox
            // 
            this.cashesCheckedListBox.FormattingEnabled = true;
            this.cashesCheckedListBox.Location = new System.Drawing.Point(65, 175);
            this.cashesCheckedListBox.Name = "cashesCheckedListBox";
            this.cashesCheckedListBox.Size = new System.Drawing.Size(202, 319);
            this.cashesCheckedListBox.TabIndex = 21;
            // 
            // uploadInfoToCashBoxBtn
            // 
            this.uploadInfoToCashBoxBtn.Location = new System.Drawing.Point(65, 65);
            this.uploadInfoToCashBoxBtn.Name = "uploadInfoToCashBoxBtn";
            this.uploadInfoToCashBoxBtn.Size = new System.Drawing.Size(137, 23);
            this.uploadInfoToCashBoxBtn.TabIndex = 22;
            this.uploadInfoToCashBoxBtn.Text = "Отправить на кассы";
            this.uploadInfoToCashBoxBtn.UseVisualStyleBackColor = true;
            this.uploadInfoToCashBoxBtn.Click += new System.EventHandler(this.uploadInfoToCashBoxBtn_Click);
            // 
            // getTemplateFromCashBoxBtn
            // 
            this.getTemplateFromCashBoxBtn.Location = new System.Drawing.Point(65, 94);
            this.getTemplateFromCashBoxBtn.Name = "getTemplateFromCashBoxBtn";
            this.getTemplateFromCashBoxBtn.Size = new System.Drawing.Size(137, 23);
            this.getTemplateFromCashBoxBtn.TabIndex = 23;
            this.getTemplateFromCashBoxBtn.Text = "Загрузить с кассы";
            this.getTemplateFromCashBoxBtn.UseVisualStyleBackColor = true;
            this.getTemplateFromCashBoxBtn.Click += new System.EventHandler(this.getTemplateFromCashBoxBtn_Click);
            // 
            // selectAllCashBoxesCheckBox
            // 
            this.selectAllCashBoxesCheckBox.AutoSize = true;
            this.selectAllCashBoxesCheckBox.Location = new System.Drawing.Point(65, 152);
            this.selectAllCashBoxesCheckBox.Name = "selectAllCashBoxesCheckBox";
            this.selectAllCashBoxesCheckBox.Size = new System.Drawing.Size(91, 17);
            this.selectAllCashBoxesCheckBox.TabIndex = 24;
            this.selectAllCashBoxesCheckBox.Text = "Выбрать все";
            this.selectAllCashBoxesCheckBox.UseVisualStyleBackColor = true;
            this.selectAllCashBoxesCheckBox.CheckedChanged += new System.EventHandler(this.selectAllCashBoxesCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 551);
            this.Controls.Add(this.selectAllCashBoxesCheckBox);
            this.Controls.Add(this.getTemplateFromCashBoxBtn);
            this.Controls.Add(this.uploadInfoToCashBoxBtn);
            this.Controls.Add(this.cashesCheckedListBox);
            this.Controls.Add(this.shopListComboBox);
            this.Controls.Add(this.moveObjectsBtnPanel);
            this.Controls.Add(this.moveForwardBetweenPagesBtn);
            this.Controls.Add(this.moveBackBetweenPagesBtn);
            this.Controls.Add(this.navBtwPagesTableLayout);
            this.Controls.Add(this.createFinalXmlBtn);
            this.Controls.Add(this.childsListView);
            this.Controls.Add(this.deleteSelectedBtn);
            this.Controls.Add(this.editSelectedBtn);
            this.Controls.Add(this.createNewItemBtn);
            this.Controls.Add(this.groupsListView);
            this.Controls.Add(this.BackToGroupsBtn);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.moveObjectsBtnPanel.ResumeLayout(false);
            this.moveObjectsBtnPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BackToGroupsBtn;
        private System.Windows.Forms.ListView groupsListView;
        private System.Windows.Forms.ListView childsListView;
        private System.Windows.Forms.Button createNewItemBtn;
        private System.Windows.Forms.Button editSelectedBtn;
        private System.Windows.Forms.Button deleteSelectedBtn;
        private System.Windows.Forms.Button createFinalXmlBtn;
        private System.Windows.Forms.TableLayoutPanel navBtwPagesTableLayout;
        private System.Windows.Forms.Button moveBackBetweenPagesBtn;
        private System.Windows.Forms.Button moveForwardBetweenPagesBtn;
        private System.Windows.Forms.Panel moveObjectsBtnPanel;
        private System.Windows.Forms.Button moveObjLeftBtn;
        private System.Windows.Forms.Button moveObjRightBtn;
        private System.Windows.Forms.Button moveObjDownBtn;
        private System.Windows.Forms.Button moveObjUpBtn;
        private System.Windows.Forms.Label moveObjectsPanelLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ComboBox shopListComboBox;
        private System.Windows.Forms.CheckedListBox cashesCheckedListBox;
        private System.Windows.Forms.Button uploadInfoToCashBoxBtn;
        private System.Windows.Forms.Button getTemplateFromCashBoxBtn;
        private System.Windows.Forms.CheckBox selectAllCashBoxesCheckBox;
    }
}

