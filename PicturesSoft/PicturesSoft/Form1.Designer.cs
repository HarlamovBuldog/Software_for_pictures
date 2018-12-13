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
            this.catalogOrientationLabel = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.moveObjectsBtnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackToGroupsBtn
            // 
            this.BackToGroupsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BackToGroupsBtn.BackColor = System.Drawing.Color.Blue;
            this.BackToGroupsBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackToGroupsBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.BackToGroupsBtn.Location = new System.Drawing.Point(1002, 242);
            this.BackToGroupsBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BackToGroupsBtn.Name = "BackToGroupsBtn";
            this.BackToGroupsBtn.Size = new System.Drawing.Size(204, 28);
            this.BackToGroupsBtn.TabIndex = 2;
            this.BackToGroupsBtn.Text = "Вернуться назад к группам";
            this.BackToGroupsBtn.UseVisualStyleBackColor = false;
            this.BackToGroupsBtn.Click += new System.EventHandler(this.BackToGroupsBtn_Click);
            // 
            // groupsListView
            // 
            this.groupsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupsListView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupsListView.HideSelection = false;
            this.groupsListView.Location = new System.Drawing.Point(378, 97);
            this.groupsListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupsListView.MultiSelect = false;
            this.groupsListView.Name = "groupsListView";
            this.groupsListView.Scrollable = false;
            this.groupsListView.Size = new System.Drawing.Size(601, 433);
            this.groupsListView.TabIndex = 3;
            this.groupsListView.UseCompatibleStateImageBehavior = false;
            this.groupsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.groupsListView_ItemSelectionChanged);
            this.groupsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.groupsListView_MouseDoubleClick);
            // 
            // childsListView
            // 
            this.childsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.childsListView.BackColor = System.Drawing.SystemColors.Window;
            this.childsListView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.childsListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.childsListView.HideSelection = false;
            this.childsListView.Location = new System.Drawing.Point(378, 97);
            this.childsListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.childsListView.MultiSelect = false;
            this.childsListView.Name = "childsListView";
            this.childsListView.Scrollable = false;
            this.childsListView.Size = new System.Drawing.Size(600, 433);
            this.childsListView.TabIndex = 4;
            this.childsListView.UseCompatibleStateImageBehavior = false;
            this.childsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.childsListView_ItemSelectionChanged);
            this.childsListView.VisibleChanged += new System.EventHandler(this.childsListView_VisibleChanged);
            this.childsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.childsListView_MouseDoubleClick);
            // 
            // createNewItemBtn
            // 
            this.createNewItemBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createNewItemBtn.BackColor = System.Drawing.Color.Blue;
            this.createNewItemBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createNewItemBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.createNewItemBtn.Location = new System.Drawing.Point(1002, 104);
            this.createNewItemBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.createNewItemBtn.Name = "createNewItemBtn";
            this.createNewItemBtn.Size = new System.Drawing.Size(141, 28);
            this.createNewItemBtn.TabIndex = 5;
            this.createNewItemBtn.Text = "Создать";
            this.createNewItemBtn.UseVisualStyleBackColor = false;
            this.createNewItemBtn.Click += new System.EventHandler(this.createNewItemBtn_Click);
            // 
            // editSelectedBtn
            // 
            this.editSelectedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editSelectedBtn.BackColor = System.Drawing.Color.Blue;
            this.editSelectedBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editSelectedBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.editSelectedBtn.Location = new System.Drawing.Point(1002, 140);
            this.editSelectedBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.editSelectedBtn.Name = "editSelectedBtn";
            this.editSelectedBtn.Size = new System.Drawing.Size(141, 28);
            this.editSelectedBtn.TabIndex = 6;
            this.editSelectedBtn.Text = "Редактировать";
            this.editSelectedBtn.UseVisualStyleBackColor = false;
            this.editSelectedBtn.Click += new System.EventHandler(this.editSelectedBtn_Click);
            // 
            // deleteSelectedBtn
            // 
            this.deleteSelectedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteSelectedBtn.BackColor = System.Drawing.Color.Blue;
            this.deleteSelectedBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteSelectedBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.deleteSelectedBtn.Location = new System.Drawing.Point(1002, 175);
            this.deleteSelectedBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.deleteSelectedBtn.Name = "deleteSelectedBtn";
            this.deleteSelectedBtn.Size = new System.Drawing.Size(141, 28);
            this.deleteSelectedBtn.TabIndex = 7;
            this.deleteSelectedBtn.Text = "Удалить";
            this.deleteSelectedBtn.UseVisualStyleBackColor = false;
            this.deleteSelectedBtn.Click += new System.EventHandler(this.deleteSelectedBtn_Click);
            // 
            // navBtwPagesTableLayout
            // 
            this.navBtwPagesTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.navBtwPagesTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.navBtwPagesTableLayout.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.navBtwPagesTableLayout.ForeColor = System.Drawing.Color.GhostWhite;
            this.navBtwPagesTableLayout.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.navBtwPagesTableLayout.Location = new System.Drawing.Point(443, 542);
            this.navBtwPagesTableLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.navBtwPagesTableLayout.Name = "navBtwPagesTableLayout";
            this.navBtwPagesTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.navBtwPagesTableLayout.Size = new System.Drawing.Size(157, 55);
            this.navBtwPagesTableLayout.TabIndex = 9;
            // 
            // moveBackBetweenPagesBtn
            // 
            this.moveBackBetweenPagesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.moveBackBetweenPagesBtn.BackColor = System.Drawing.Color.Blue;
            this.moveBackBetweenPagesBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveBackBetweenPagesBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.moveBackBetweenPagesBtn.Location = new System.Drawing.Point(378, 541);
            this.moveBackBetweenPagesBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveBackBetweenPagesBtn.Name = "moveBackBetweenPagesBtn";
            this.moveBackBetweenPagesBtn.Size = new System.Drawing.Size(57, 57);
            this.moveBackBetweenPagesBtn.TabIndex = 10;
            this.moveBackBetweenPagesBtn.Text = "←";
            this.moveBackBetweenPagesBtn.UseVisualStyleBackColor = false;
            this.moveBackBetweenPagesBtn.Click += new System.EventHandler(this.moveBackBetweenPagesBtn_Click);
            // 
            // moveForwardBetweenPagesBtn
            // 
            this.moveForwardBetweenPagesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveForwardBetweenPagesBtn.BackColor = System.Drawing.Color.Blue;
            this.moveForwardBetweenPagesBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveForwardBetweenPagesBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.moveForwardBetweenPagesBtn.Location = new System.Drawing.Point(923, 538);
            this.moveForwardBetweenPagesBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveForwardBetweenPagesBtn.Name = "moveForwardBetweenPagesBtn";
            this.moveForwardBetweenPagesBtn.Size = new System.Drawing.Size(56, 57);
            this.moveForwardBetweenPagesBtn.TabIndex = 11;
            this.moveForwardBetweenPagesBtn.Text = "→";
            this.moveForwardBetweenPagesBtn.UseVisualStyleBackColor = false;
            this.moveForwardBetweenPagesBtn.Click += new System.EventHandler(this.moveForwardBetweenPagesBtn_Click);
            // 
            // moveObjectsBtnPanel
            // 
            this.moveObjectsBtnPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjectsPanelLabel);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjLeftBtn);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjRightBtn);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjDownBtn);
            this.moveObjectsBtnPanel.Controls.Add(this.moveObjUpBtn);
            this.moveObjectsBtnPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveObjectsBtnPanel.Location = new System.Drawing.Point(1002, 312);
            this.moveObjectsBtnPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveObjectsBtnPanel.Name = "moveObjectsBtnPanel";
            this.moveObjectsBtnPanel.Size = new System.Drawing.Size(246, 149);
            this.moveObjectsBtnPanel.TabIndex = 12;
            // 
            // moveObjectsPanelLabel
            // 
            this.moveObjectsPanelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveObjectsPanelLabel.AutoSize = true;
            this.moveObjectsPanelLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveObjectsPanelLabel.ForeColor = System.Drawing.Color.Blue;
            this.moveObjectsPanelLabel.Location = new System.Drawing.Point(47, 11);
            this.moveObjectsPanelLabel.Name = "moveObjectsPanelLabel";
            this.moveObjectsPanelLabel.Size = new System.Drawing.Size(149, 16);
            this.moveObjectsPanelLabel.TabIndex = 4;
            this.moveObjectsPanelLabel.Text = "Перемещение объектов";
            // 
            // moveObjLeftBtn
            // 
            this.moveObjLeftBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveObjLeftBtn.BackColor = System.Drawing.Color.MediumBlue;
            this.moveObjLeftBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveObjLeftBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.moveObjLeftBtn.Location = new System.Drawing.Point(33, 85);
            this.moveObjLeftBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveObjLeftBtn.Name = "moveObjLeftBtn";
            this.moveObjLeftBtn.Size = new System.Drawing.Size(55, 47);
            this.moveObjLeftBtn.TabIndex = 3;
            this.moveObjLeftBtn.Text = "←";
            this.moveObjLeftBtn.UseVisualStyleBackColor = false;
            this.moveObjLeftBtn.Click += new System.EventHandler(this.moveObjLeftBtn_Click);
            // 
            // moveObjRightBtn
            // 
            this.moveObjRightBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveObjRightBtn.BackColor = System.Drawing.Color.MediumBlue;
            this.moveObjRightBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveObjRightBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.moveObjRightBtn.Location = new System.Drawing.Point(155, 85);
            this.moveObjRightBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveObjRightBtn.Name = "moveObjRightBtn";
            this.moveObjRightBtn.Size = new System.Drawing.Size(55, 47);
            this.moveObjRightBtn.TabIndex = 2;
            this.moveObjRightBtn.Text = "→";
            this.moveObjRightBtn.UseVisualStyleBackColor = false;
            this.moveObjRightBtn.Click += new System.EventHandler(this.moveObjRightBtn_Click);
            // 
            // moveObjDownBtn
            // 
            this.moveObjDownBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveObjDownBtn.BackColor = System.Drawing.Color.MediumBlue;
            this.moveObjDownBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveObjDownBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.moveObjDownBtn.Location = new System.Drawing.Point(94, 85);
            this.moveObjDownBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveObjDownBtn.Name = "moveObjDownBtn";
            this.moveObjDownBtn.Size = new System.Drawing.Size(55, 47);
            this.moveObjDownBtn.TabIndex = 1;
            this.moveObjDownBtn.Text = "↓";
            this.moveObjDownBtn.UseVisualStyleBackColor = false;
            this.moveObjDownBtn.Click += new System.EventHandler(this.moveObjDownBtn_Click);
            // 
            // moveObjUpBtn
            // 
            this.moveObjUpBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveObjUpBtn.BackColor = System.Drawing.Color.MediumBlue;
            this.moveObjUpBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.moveObjUpBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.moveObjUpBtn.Location = new System.Drawing.Point(93, 31);
            this.moveObjUpBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.moveObjUpBtn.Name = "moveObjUpBtn";
            this.moveObjUpBtn.Size = new System.Drawing.Size(55, 47);
            this.moveObjUpBtn.TabIndex = 0;
            this.moveObjUpBtn.Text = "↑";
            this.moveObjUpBtn.UseVisualStyleBackColor = false;
            this.moveObjUpBtn.Click += new System.EventHandler(this.moveObjUpBtn_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(1377, 24);
            this.mainMenuStrip.TabIndex = 16;
            // 
            // shopListComboBox
            // 
            this.shopListComboBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.shopListComboBox.ForeColor = System.Drawing.Color.Blue;
            this.shopListComboBox.FormattingEnabled = true;
            this.shopListComboBox.Location = new System.Drawing.Point(73, 130);
            this.shopListComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.shopListComboBox.Name = "shopListComboBox";
            this.shopListComboBox.Size = new System.Drawing.Size(235, 24);
            this.shopListComboBox.TabIndex = 20;
            this.shopListComboBox.Text = "Выберите магазин...";
            this.shopListComboBox.SelectedIndexChanged += new System.EventHandler(this.shopListComboBox_SelectedIndexChanged);
            // 
            // cashesCheckedListBox
            // 
            this.cashesCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cashesCheckedListBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cashesCheckedListBox.ForeColor = System.Drawing.Color.Blue;
            this.cashesCheckedListBox.FormattingEnabled = true;
            this.cashesCheckedListBox.Location = new System.Drawing.Point(73, 190);
            this.cashesCheckedListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cashesCheckedListBox.Name = "cashesCheckedListBox";
            this.cashesCheckedListBox.Size = new System.Drawing.Size(235, 364);
            this.cashesCheckedListBox.TabIndex = 21;
            // 
            // uploadInfoToCashBoxBtn
            // 
            this.uploadInfoToCashBoxBtn.BackColor = System.Drawing.Color.Blue;
            this.uploadInfoToCashBoxBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uploadInfoToCashBoxBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.uploadInfoToCashBoxBtn.Location = new System.Drawing.Point(73, 55);
            this.uploadInfoToCashBoxBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uploadInfoToCashBoxBtn.Name = "uploadInfoToCashBoxBtn";
            this.uploadInfoToCashBoxBtn.Size = new System.Drawing.Size(160, 28);
            this.uploadInfoToCashBoxBtn.TabIndex = 22;
            this.uploadInfoToCashBoxBtn.Text = "Отправить на кассы";
            this.uploadInfoToCashBoxBtn.UseVisualStyleBackColor = false;
            this.uploadInfoToCashBoxBtn.Click += new System.EventHandler(this.uploadInfoToCashBoxBtn_Click);
            // 
            // getTemplateFromCashBoxBtn
            // 
            this.getTemplateFromCashBoxBtn.BackColor = System.Drawing.Color.Blue;
            this.getTemplateFromCashBoxBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.getTemplateFromCashBoxBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.getTemplateFromCashBoxBtn.Location = new System.Drawing.Point(73, 91);
            this.getTemplateFromCashBoxBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getTemplateFromCashBoxBtn.Name = "getTemplateFromCashBoxBtn";
            this.getTemplateFromCashBoxBtn.Size = new System.Drawing.Size(160, 28);
            this.getTemplateFromCashBoxBtn.TabIndex = 23;
            this.getTemplateFromCashBoxBtn.Text = "Загрузить с кассы";
            this.getTemplateFromCashBoxBtn.UseVisualStyleBackColor = false;
            this.getTemplateFromCashBoxBtn.Click += new System.EventHandler(this.getTemplateFromCashBoxBtn_Click);
            // 
            // selectAllCashBoxesCheckBox
            // 
            this.selectAllCashBoxesCheckBox.AutoSize = true;
            this.selectAllCashBoxesCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.selectAllCashBoxesCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.selectAllCashBoxesCheckBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectAllCashBoxesCheckBox.ForeColor = System.Drawing.Color.Blue;
            this.selectAllCashBoxesCheckBox.Location = new System.Drawing.Point(73, 162);
            this.selectAllCashBoxesCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.selectAllCashBoxesCheckBox.Name = "selectAllCashBoxesCheckBox";
            this.selectAllCashBoxesCheckBox.Size = new System.Drawing.Size(101, 20);
            this.selectAllCashBoxesCheckBox.TabIndex = 24;
            this.selectAllCashBoxesCheckBox.Text = "Выбрать все";
            this.selectAllCashBoxesCheckBox.UseVisualStyleBackColor = false;
            this.selectAllCashBoxesCheckBox.CheckedChanged += new System.EventHandler(this.selectAllCashBoxesCheckBox_CheckedChanged);
            // 
            // catalogOrientationLabel
            // 
            this.catalogOrientationLabel.AutoSize = true;
            this.catalogOrientationLabel.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.catalogOrientationLabel.ForeColor = System.Drawing.Color.Blue;
            this.catalogOrientationLabel.Location = new System.Drawing.Point(380, 55);
            this.catalogOrientationLabel.Name = "catalogOrientationLabel";
            this.catalogOrientationLabel.Size = new System.Drawing.Size(101, 25);
            this.catalogOrientationLabel.TabIndex = 25;
            this.catalogOrientationLabel.Text = "Каталог";
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.testButton.ForeColor = System.Drawing.Color.Blue;
            this.testButton.Location = new System.Drawing.Point(1096, 526);
            this.testButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(87, 28);
            this.testButton.TabIndex = 26;
            this.testButton.Text = "ТЕСТ";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 678);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.catalogOrientationLabel);
            this.Controls.Add(this.selectAllCashBoxesCheckBox);
            this.Controls.Add(this.getTemplateFromCashBoxBtn);
            this.Controls.Add(this.uploadInfoToCashBoxBtn);
            this.Controls.Add(this.cashesCheckedListBox);
            this.Controls.Add(this.shopListComboBox);
            this.Controls.Add(this.moveObjectsBtnPanel);
            this.Controls.Add(this.moveForwardBetweenPagesBtn);
            this.Controls.Add(this.moveBackBetweenPagesBtn);
            this.Controls.Add(this.navBtwPagesTableLayout);
            this.Controls.Add(this.childsListView);
            this.Controls.Add(this.deleteSelectedBtn);
            this.Controls.Add(this.editSelectedBtn);
            this.Controls.Add(this.createNewItemBtn);
            this.Controls.Add(this.groupsListView);
            this.Controls.Add(this.BackToGroupsBtn);
            this.Controls.Add(this.mainMenuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
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
        private System.Windows.Forms.Label catalogOrientationLabel;
        private System.Windows.Forms.Button testButton;
    }
}

