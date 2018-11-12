using Newtonsoft.Json;
using Npgsql;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data;

namespace PicturesSoft
{
    public partial class Form1 : Form
    {
        #region private fields

        private object globalSelectedItem;
        private Group groupOwner;

        #endregion //private fields

        #region public properties

        public string XmlCnfgFilePath { get; set; }
        public string DestImgFolderPath { get; set; }
        public string LocalCatalogFilePath { get; set; }
        public WorkMode AppWorkMode { get; set; }
        public int globalListViewRelatedPageNumber { get; private set; }
        public ChildRepository ChildRep { get; private set; }
        public GroupRepository GroupRep { get; private set; }
        public List<Shop> ListOfShopsWithCashBoxes { get; private set; } = new List<Shop>();

        #endregion //public properties

        #region Form1 creation

        public Form1()
        {
            InitializeComponent();

            /*
            //Dialog for setting pathes
            AppSettings appSettingsDialog = new AppSettings();
            var result = appSettingsDialog.ShowDialog(this);

            if (result == DialogResult.Cancel)
                this.Close();

            //Groups.xml file path
            string groupXmlFilePath = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), @"Data\Groups.xml");

            //Childs.xml file path
            string childXmlFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Data\Childs.xml");
            

            string defaultXmlCnfgFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Data\weightCatalog-xml-config.xml");
            string defaultDestImgFolderPath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Images");
            */

            //maybe it is neccessary to check if these two exist
            //setting default pathes for template data
            XmlCnfgFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Data\weightCatalog-xml-config.xml");
            DestImgFolderPath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Images");

            LocalCatalogFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Data\CashBoxesCatalog.xml");

            //before calling LoadShopsFromLocalCatalog function need to check
            //if file exist and all related checks need to be done

            ListOfShopsWithCashBoxes = LoadShopsFromLocalCatalog(LocalCatalogFilePath);

            AppWorkMode = new WorkMode() { WorkType = WorkModeType.LoadFromFinalXml };

            if (this.AppWorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                GroupRep = new GroupRepository(XmlCnfgFilePath, AppWorkMode);
                ChildRep = new ChildRepository(XmlCnfgFilePath, AppWorkMode);
                createFinalXmlBtn.Enabled = false;
            }
            else
            {
                GroupRep = new GroupRepository("Data/Groups.xml");
                ChildRep = new ChildRepository("Data/Childs.xml");
            }

            //filling comboBox
            ShopListComboBoxInit();

            //filling group listview
            GroupListViewRedraw();

            MainMenuStripInit();

            /*
            //filling listbox
            this.listBox1.Items.AddRange(groupRep.GetGroups().ToArray<Group>());
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.childsListView.Hide();
            this.BackToGroupsBtn.Hide();

            this.moveBackBetweenPagesBtn.Enabled = false;
            this.moveForwardBetweenPagesBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;
        }

        private void MainMenuStripInit()
        {
            //root menu item strip creation
            ToolStripMenuItem Configuration =
                new ToolStripMenuItem("Настройки");
            
            ToolStripMenuItem downloadServerConfig =
                new ToolStripMenuItem("Настройки сервера-справочника");

            //adding click event for menu strip item
            downloadServerConfig.Click += downloadServerConfigMenuItem_Click;

            ToolStripMenuItem dbConnectSettings =
               new ToolStripMenuItem("Настройки подключения к БД для синхронизации");

            dbConnectSettings.Click += dbConnectSettingsMenuItem_Click;

            //adding downloadServerConfig to root Configuration
            Configuration.DropDownItems.Add(downloadServerConfig);
            Configuration.DropDownItems.Add(dbConnectSettings);

            //adding Configuration to MainMenuStripObject itself
            this.MainMenuStrip.Items.Add(Configuration);
        }

        void ShopListComboBoxInit()
        {
            foreach(Shop shop in ListOfShopsWithCashBoxes)
            {
                this.shopListComboBox.Items.Add(shop);
            }

            this.shopListComboBox.MaxDropDownItems = 100;
            this.shopListComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.shopListComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        void downloadServerConfigMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToServerAndUpdateFile();
            //MakeLocalCatalog();
        }

        void dbConnectSettingsMenuItem_Click(object sender, EventArgs e)
        {
            ConnectToDbAndMakeLocalCatalog();
        }

        #endregion //Form1 creation

        #region Public Group methods

        public void AddNewGroup(Group newGroup, bool addGroupToTheEndOfTheList = true)
        {
            if(addGroupToTheEndOfTheList)
                GroupRep.AddGroup(newGroup);
            else
            {
                var insertAfterGroup = (Group)globalSelectedItem;
                GroupRep.AddGroup(newGroup, insertAfterGroup);
            }
            GroupListViewRedraw();
        }

        public void UpdateGroup(Group groupToUpdate)
        {
            //var groupListIndex = groupRep.GetGroups().IndexOf((Group)globalSelectedItem);

            Group oldGroup = (Group)globalSelectedItem;

            /*
            Group oldGroup = Group.CreateGroup(
                castGroup.Id,
                castGroup.Name,
                castGroup.ImgName
                );
                */
            //need to also update all childs connected to the group
            //if property Id has changed
            if (groupToUpdate.Id != oldGroup.Id)
            {
                this.UpdateAllChildsBelongToGroup(groupToUpdate, oldGroup);
            }

            //groupRep.UpdateGroup(groupToUpdate, groupListIndex);
            GroupRep.UpdateGroup(groupToUpdate, oldGroup);   
            GroupListViewRedraw();   
        }

        public void DeleteGroup(Group groupToDelete)
        {
            //var groupListIndex = groupRep.GetGroups().IndexOf((Group)globalSelectedItem);

            //groupRep.DeleteGroup(groupListIndex);
            GroupRep.DeleteGroup(groupToDelete);
            GroupListViewRedraw();

            //need to also delete all childs connected to the group
            this.DeleteAllChildsBelongToGroup();
            
        }

        #endregion //Public Group methods

        #region Public Child methods

        public void AddNewChild(Child newChild, bool addChildToTheEndOfTheList = true)
        {
            if (addChildToTheEndOfTheList)
                ChildRep.AddChild(newChild);
            else
            {
                var insertAfterChild = (Child)globalSelectedItem;
                ChildRep.AddChild(newChild, insertAfterChild);
            }

            ChildListViewRedraw();
            NavBtwPagesTableLayoutRedraw();
        }

        public void UpdateChild(Child childToUpdate)
        {
            //var childListIndex = childRep.GetChilds().IndexOf((Child)globalSelectedItem);
            Child oldChild = (Child)globalSelectedItem;

            //childRep.UpdateChild(childToUpdate, childListIndex);
            ChildRep.UpdateChild(childToUpdate, oldChild);

            ChildListViewRedraw();
        }

        public void DeleteChild(Child childToDelete)
        {
            //var childListIndex = childRep.GetChilds().IndexOf((Child)globalSelectedItem);

            //childRep.DeleteChild(childListIndex);
            ChildRep.DeleteChild(childToDelete);

            var imgToDeleteFilePath = DestImgFolderPath + "\\" + childToDelete.ImgName;
            File.Delete(imgToDeleteFilePath);

            ChildListViewRedraw();
            NavBtwPagesTableLayoutRedraw();
        }

        #endregion //Public Child methods

        public void MakeLocalCatalogCall(string serverId, string port,
            string user, string passwd, string dataBaseName)
        {
            //MakeLocalCatalog(serverId, port, user, passwd, dataBaseName);
        }

        public void CreateFinalXmlFile()
        {
            XDocument xDoc = new XDocument();

            //getting namespaces
            XNamespace xmlns = XNamespace.Get("http://crystals.ru/cash/settings");
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace schemaLocation = XNamespace.Get("http://crystals.ru/cash/settings ../../module-config.xsd");

            //create moduleConfigElement
            XElement moduleConfigXElement = new XElement(xmlns + "moduleConfig");

            //< create list of attributes for moduleConfigElement
            List<XAttribute> moduleConfigAttrList = new List<XAttribute>()
            {
                new XAttribute(xsi + "schemaLocation", schemaLocation),
                new XAttribute("settingsGroup", "weightCatalog"),
                new XAttribute("visible", "true"),
                new XAttribute("description", "Catalog"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi)
            };
            //>

            //adding attributes to moduleConfigElement
            foreach (XAttribute XAttr in moduleConfigAttrList)
            {
                moduleConfigXElement.Add(XAttr);
            }

            //create property element
            XElement propertyXElem = new XElement(xmlns + "property");
            XAttribute keyAttr = new XAttribute("key", "catalog");
            propertyXElem.Add(keyAttr);

            foreach (Group gr in GroupRep.GetGroups())
            {
                //create group element
                XElement groupXElem = new XElement(xmlns + "group");

                //create attributes for group element
                XAttribute groupName = new XAttribute("name", gr.Name);
                XAttribute groupImgName = new XAttribute("image-name", gr.ImgName);

                //add attributes to group element
                groupXElem.Add(groupName);
                groupXElem.Add(groupImgName);

                foreach (Child ch in ChildRep.GetChildsBelongToGroup(gr.Id))
                {
                    //create good element
                    XElement goodXElem = new XElement(xmlns + "good");

                    //create attributes for good element
                    XAttribute goodId = new XAttribute("item", ch.Code);
                    XAttribute goodSimpleName = new XAttribute("name", ch.SimpleName);

                    //add attributes to good element
                    goodXElem.Add(goodId);
                    goodXElem.Add(goodSimpleName);

                    //add each good to group
                    groupXElem.Add(goodXElem);
                }

                //add each group element to property element
                propertyXElem.Add(groupXElem);
            }

            //add property element to moduleConfigElement
            moduleConfigXElement.Add(propertyXElem);

            //making moduleConfigElement to be root element
            xDoc.Add(moduleConfigXElement);

            xDoc.Save("Data/FinalXml.xml");
        }

        #region Private helpers

        private static List<Shop> LoadShopsFromLocalCatalog(string LocalCatalogFilePath)
        {
            List<Shop> loadedShops = new List<Shop>();

            XDocument xDoc = XDocument.Load(LocalCatalogFilePath);

            foreach (XElement shopElem in xDoc.Element("shops").Elements("shop"))
            {
                Shop shopToAdd = new Shop()
                {
                    Code = (string)shopElem.Attribute("number"),
                    Name = (string)shopElem.Attribute("name"),
                    Address = (string)shopElem.Attribute("address")
                };

                foreach (XElement cashBoxElem in shopElem.Elements("cashBox"))
                {
                    shopToAdd.CashBoxes.Add(new CashBox()
                    {
                        IpAddress = (string)cashBoxElem.Attribute("ipAddress"),
                        Number = (string)cashBoxElem.Attribute("number"),
                        ShopCode = (string)shopElem.Attribute("number")
                    });
                }

                loadedShops.Add(shopToAdd);
            }

            return loadedShops;
        }

        private void ConnectToServerAndUpdateFile()
        {
            var ConnToServAndUpFile = new ServerConnectionInfoForm();
            ConnToServAndUpFile.ShowDialog(this);
        }

        private void ConnectToDbAndMakeLocalCatalog()
        {
            var ConnectToDbAndMakeLocalCatalog = new DBConnSettingsForm();
            ConnectToDbAndMakeLocalCatalog.ShowDialog(this);
        }

        private void GroupListViewRedraw()
        {
            if (this.groupsListView.Items.Count != 0)
            {
                this.groupsListView.Clear();
            }

            //Image collection for listview
            ImageList imageGroupListViewCollection = new ImageList();
            imageGroupListViewCollection.ImageSize = new Size(64, 64);
            imageGroupListViewCollection.ColorDepth = ColorDepth.Depth32Bit;

            String[] imageFiles = Directory.GetFiles(DestImgFolderPath);

            int imgIndexCounter = 0;

            foreach (Group gr in GroupRep.GetGroups())
            {
                //< filling image collection for groupListView
                foreach (var imgFile in imageFiles)
                {
                    if (imgFile.Substring(imgFile.LastIndexOf("\\") + 1)
                        .Equals(gr.ImgName))
                    {
                        using (Stream fs = new FileStream(imgFile, FileMode.Open, FileAccess.Read))
                            imageGroupListViewCollection.Images.Add(Image.FromStream(fs));
                    }
                }
                //>

                //< filling groupListView
                ListViewItem lvi = new ListViewItem();
                lvi.Text = gr.Name;
                lvi.Tag = gr;
                lvi.ImageIndex = imgIndexCounter;

                this.groupsListView.Items.Add(lvi);

                imgIndexCounter++;
                //>
            }

            this.groupsListView.LargeImageList = imageGroupListViewCollection;

            this.Refresh();
            this.Invalidate();
            this.groupsListView.Refresh();
            this.groupsListView.Invalidate();
        }

        private void ChildListViewRedraw()
        {
            var childsListBelongToGroup = ChildRep.GetChildsBelongToGroup(groupOwner.Id);

            if (this.childsListView.Items.Count != 0)
            {
                childsListView.Items.Clear();
            }

            int childsListBelongToGroupCount = childsListBelongToGroup.Count;

            int startIndexForChildListRedraw = (globalListViewRelatedPageNumber - 1) * 9;
            int lastIndexFotChildListRedraw = startIndexForChildListRedraw + 9;

            //check if lastIndexFotChildListRedraw is not beyond the last index of repo
            if (lastIndexFotChildListRedraw > childsListBelongToGroupCount - 1)
                lastIndexFotChildListRedraw = startIndexForChildListRedraw +
                    childsListBelongToGroupCount % 9;

            //< Image collection init for childListView
            ImageList imageChildListViewCollection = new ImageList();
            imageChildListViewCollection.ImageSize = new Size(64, 64);
            imageChildListViewCollection.ColorDepth = ColorDepth.Depth32Bit;

            String[] imageFiles = Directory.GetFiles(DestImgFolderPath);

            int imgIndexCounter = 0;
            //>

            for (int i = startIndexForChildListRedraw; i < lastIndexFotChildListRedraw; i++)
            {
                //< filling image collection for childListView

                var ch = childsListBelongToGroup[i];

                foreach (var imgFile in imageFiles)
                {
                    if (imgFile.Substring(imgFile.LastIndexOf("\\") + 1)
                        .Equals(ch.ImgName))
                    {
                        using (Stream fs = new FileStream(imgFile, FileMode.Open, FileAccess.Read))
                            imageChildListViewCollection.Images.Add(Image.FromStream(fs));
                    }
                }
                //>

                //< filling childListView
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ch.SimpleName;
                lvi.Tag = ch;
                lvi.ImageIndex = imgIndexCounter;

                this.childsListView.Items.Add(lvi);

                imgIndexCounter++;
                //>
            }

            this.childsListView.LargeImageList = imageChildListViewCollection;

            SetMoveBetweenPagesBtnsEnabledProperty();

            this.Refresh();
            this.Invalidate();
            this.childsListView.Refresh();
            this.childsListView.Invalidate();
        }

        private void SetMoveBetweenPagesBtnsEnabledProperty()
        {
            int navBtwPagesTableLayoutPanelControlsCount = 
                this.navBtwPagesTableLayout.Controls.Count;

            if (globalListViewRelatedPageNumber == 1)
            {
                this.moveBackBetweenPagesBtn.Enabled = false;
                if(navBtwPagesTableLayoutPanelControlsCount > 1)
                    this.moveForwardBetweenPagesBtn.Enabled = true;
            }
            else if(globalListViewRelatedPageNumber < navBtwPagesTableLayoutPanelControlsCount)
            {
                this.moveBackBetweenPagesBtn.Enabled = true;
                this.moveForwardBetweenPagesBtn.Enabled = true;
            }
            else
            {
                this.moveForwardBetweenPagesBtn.Enabled = false;
                this.moveBackBetweenPagesBtn.Enabled = true;
            }
        }

        private void NavBtwPagesTableLayoutRedraw()
        {
            var childsListBelongToGroup = ChildRep.GetChildsBelongToGroup(groupOwner.Id);
            int childsListBelongToGroupCount = childsListBelongToGroup.Count;

            int pageNavBtnCount = childsListBelongToGroupCount / 9;
            if (childsListBelongToGroupCount % 9 != 0)
                pageNavBtnCount++;

            if (this.navBtwPagesTableLayout.Controls.Count != 0)
                this.navBtwPagesTableLayout.Controls.Clear();

            navBtwPagesTableLayout.ColumnStyles.Clear();
            navBtwPagesTableLayout.RowStyles.Clear();

            this.navBtwPagesTableLayout.RowCount = 1;
            this.navBtwPagesTableLayout.ColumnCount = pageNavBtnCount;

            this.navBtwPagesTableLayout.Size = 
                new System.Drawing.Size(45 * pageNavBtnCount, 45);

            for (int i = 0; i < pageNavBtnCount; i++)
            {
                this.navBtwPagesTableLayout.ColumnStyles.Add(
                    new ColumnStyle() { Width = 45, SizeType = SizeType.Absolute}
                    );
                    
                //< filling table layout panel
                this.navBtwPagesTableLayout.Controls.Add(
                    new Button()
                    {
                        Name = "navBtwPagesBtn" + i,
                        Text = (i + 1).ToString(),
                        Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                    ((((System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)))
                    }, i, 1);
            }

            navBtwPagesTableLayout.RowStyles.Add(
               new RowStyle() { Height = 45, SizeType = SizeType.Absolute }
               );

            foreach (var button in this.navBtwPagesTableLayout.Controls.OfType<Button>())
            {
                button.Click += navBtwPagesButton_Click;
            }

            this.Refresh();
            this.Invalidate();
            this.navBtwPagesTableLayout.Refresh();
            this.navBtwPagesTableLayout.Invalidate();
        }

        private void DeleteAllChildsBelongToGroup()
        {
            var childsListBelongToGroup = 
                ChildRep.GetChildsBelongToGroup(((Group)globalSelectedItem).Id);

            foreach (Child ch in childsListBelongToGroup)
            {
                var indexOfItemToDelete =
                    ChildRep.GetChilds().IndexOf(ch);
                this.ChildRep.DeleteChild(ch, false);

                var imgToDeleteFilePath = DestImgFolderPath + "\\" + ch.ImgName;
                File.Delete(imgToDeleteFilePath);
            }
        }

        private void UpdateAllChildsBelongToGroup(Group groupToUpdate, Group oldGroup)
        {
            var childsListBelongToOldGroup =
                ChildRep.GetChildsBelongToGroup(oldGroup.Id);

            foreach (Child oldCh in childsListBelongToOldGroup)
            {
                Child newCh = Child.CreateChild(
                    oldCh.Code,
                    oldCh.Name,
                    oldCh.SimpleName,
                    groupToUpdate.Id,
                    oldCh.ImgName
                    );
                this.ChildRep.UpdateChild(newCh, oldCh, false);
            }
        }

        private void SetMoveObjBtnsEnabledProperty()
        {
            var globalSlctedItemType = globalSelectedItem.GetType();
            int indexOfObject;
            int objectRepListLastIndex;

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                var tempGroupList = GroupRep.GetGroups();
                indexOfObject = tempGroupList.IndexOf((Group)globalSelectedItem);
                objectRepListLastIndex = tempGroupList.Count - 1;
            }
            else
            {
                var tempChildList = ChildRep.GetChildsBelongToGroup(groupOwner.Id);
                indexOfObject = tempChildList .IndexOf((Child)globalSelectedItem);
                objectRepListLastIndex = tempChildList.Count - 1;
            }

            //< Setting enabled move right
            //and move left button properties
            
            if(indexOfObject > 0 && indexOfObject  < objectRepListLastIndex)
            {
                this.moveObjLeftBtn.Enabled = true;
                this.moveObjRightBtn.Enabled = true;
            }
            else if(indexOfObject == 0)
            {
                this.moveObjLeftBtn.Enabled = false;
                this.moveObjRightBtn.Enabled = true;
            }
            else if(indexOfObject == objectRepListLastIndex)
            {
                this.moveObjLeftBtn.Enabled = true;
                this.moveObjRightBtn.Enabled = false;
            }
            //>

            //< Setting enabled move up
            //and move down button properties
            if (indexOfObject % 9 == 0 || indexOfObject % 9 == 1 || indexOfObject % 9 == 2)
            {
                this.moveObjUpBtn.Enabled = false;
                this.moveObjDownBtn.Enabled = true;
            }
            else if(indexOfObject % 9 == 6 || indexOfObject % 9 == 7 || indexOfObject % 9 == 8)
            {
                this.moveObjUpBtn.Enabled = true;
                this.moveObjDownBtn.Enabled = false;
            }
            else
            {
                this.moveObjUpBtn.Enabled = true;
                this.moveObjDownBtn.Enabled = true;
            }

            //additional conditions for moveObjDownBtn
            //when programm meets the end of the list
            if (objectRepListLastIndex - indexOfObject < 3)
                this.moveObjDownBtn.Enabled = false;
            //>

            //additional condition for all buttons
            //if there is only one element in list
            if (objectRepListLastIndex == indexOfObject && indexOfObject == 0)
            {
                this.moveObjectsBtnPanel.Enabled = false;
            }
        }

        #endregion //Private helpers

        #region groupListView Events

        private void groupsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (groupsListView.SelectedItems.Count == 0)
                return;

            var indexOfSelectedItem = groupsListView.SelectedIndices[0];
            groupOwner = GroupRep.GetGroups()[indexOfSelectedItem];

            globalListViewRelatedPageNumber = 1;

            this.NavBtwPagesTableLayoutRedraw();

            this.childsListView.BeginUpdate();
            this.ChildListViewRedraw();
            this.groupsListView.Hide();
            this.childsListView.EndUpdate();

            this.childsListView.Show();
            this.BackToGroupsBtn.Show();
        }

        private void groupsListView_VisibleChanged(object sender, EventArgs e)
        {
            this.groupsListView.SelectedItems.Clear();
        }

        private void groupsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (groupsListView.SelectedItems.Count != 0)
            {
                this.editSelectedBtn.Enabled = true;
                this.deleteSelectedBtn.Enabled = true;

                var indexOfSelectedItem = groupsListView.SelectedIndices[0];
                var selectedGroup = GroupRep.GetGroups()[indexOfSelectedItem];
                this.globalSelectedItem = selectedGroup;

                this.moveObjectsBtnPanel.Enabled = true;
                this.SetMoveObjBtnsEnabledProperty();
            }
            else
            {
                this.editSelectedBtn.Enabled = false;
                this.deleteSelectedBtn.Enabled = false;

                this.moveObjectsBtnPanel.Enabled = false;
            }
        }

        #endregion //groupListView Events

        #region childsListView Events

        private void childsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (childsListView.SelectedItems.Count == 0)
                return;

            WorkMode workMode = new WorkMode() { WorkType = WorkModeType.Edit };

            Child oldChild = (Child)globalSelectedItem;
            Child childToEdit = Child.CreateChild(
                oldChild.Code,
                oldChild.Name,
                oldChild.SimpleName,
                oldChild.GroupCode,
                oldChild.ImgName
                );

            CreateAndEditChildForm CrAnEdChildForm =
                new CreateAndEditChildForm(workMode, childToEdit, DestImgFolderPath);
            CrAnEdChildForm.ShowDialog(this);

            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;
        }

        private void childsListView_VisibleChanged(object sender, EventArgs e)
        {
            this.childsListView.SelectedItems.Clear();
        }

        private void childsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (childsListView.SelectedItems.Count != 0)
            {
                this.editSelectedBtn.Enabled = true;
                this.deleteSelectedBtn.Enabled = true;

                var indexOfSelectedItem = 
                    ChildRep.GetChilds().IndexOf((Child)childsListView.SelectedItems[0].Tag);
                this.globalSelectedItem = ChildRep.GetChilds()[indexOfSelectedItem];

                this.moveObjectsBtnPanel.Enabled = true;
                this.SetMoveObjBtnsEnabledProperty();
            }
            else
            {
                this.editSelectedBtn.Enabled = false;
                this.deleteSelectedBtn.Enabled = false;

                this.moveObjectsBtnPanel.Enabled = false;
            }
        }

        #endregion //childsListView Events

        #region CRUD buttons Events

        private void createNewItemBtn_Click(object sender, EventArgs e)
        {
            WorkMode workMode = new WorkMode() { WorkType = WorkModeType.Create };

            if (this.groupsListView.Visible)
            {
                CreateAndEditGroupForm CrAnEdGrForm = 
                    new CreateAndEditGroupForm(workMode, DestImgFolderPath);
                CrAnEdGrForm.ShowDialog(this);
            }
            else if (this.childsListView.Visible)
            {
                CreateAndEditChildForm CrAnEdChildForm =
                    new CreateAndEditChildForm(workMode, groupOwner, DestImgFolderPath);
                CrAnEdChildForm.ShowDialog(this);
            }

            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;
        }

        private void editSelectedBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();
            WorkMode workMode = new WorkMode() { WorkType = WorkModeType.Edit };

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                Group oldGroup = (Group)globalSelectedItem;
                Group groupToEdit = Group.CreateGroup(
                    oldGroup.Id,
                    oldGroup.Name,
                    oldGroup.ImgName
                    );  

                CreateAndEditGroupForm CrAnEdGrForm = 
                    new CreateAndEditGroupForm(workMode, groupToEdit, DestImgFolderPath);
                CrAnEdGrForm.ShowDialog(this);
            }
            else if(globalSlctedItemType.Name.Equals("Child"))
            {
                Child oldChild = (Child)globalSelectedItem;
                Child childToEdit = Child.CreateChild(
                    oldChild.Code,
                    oldChild.Name,
                    oldChild.SimpleName,
                    oldChild.GroupCode,
                    oldChild.ImgName
                    );

                CreateAndEditChildForm CrAnEdChildForm =
                    new CreateAndEditChildForm(workMode, childToEdit, DestImgFolderPath);
                CrAnEdChildForm.ShowDialog(this);
            }

            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;
        }

        private void deleteSelectedBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                Group groupToDelete = (Group)globalSelectedItem;

                var result = MessageBox.Show("Are you sure that you want to delete "
                    + groupToDelete.Name + "group?",
                    "Delete", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if(result == DialogResult.Yes)
                    this.DeleteGroup(groupToDelete);
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                Child childToDelete = (Child)globalSelectedItem;

                var result = MessageBox.Show("Are you sure that you want to delete "
                    + childToDelete.Name + "child?",
                    "Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                    this.DeleteChild(childToDelete);
            }

            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

        }

        #endregion //CRUD buttons Events

        private void createFinalXmlBtn_Click(object sender, EventArgs e)
        {
            CreateFinalXmlFile();
        }

        #region Navigation buttons Events

        private void BackToGroupsBtn_Click(object sender, EventArgs e)
        {
            this.groupsListView.Show();
            this.childsListView.Hide();
            this.BackToGroupsBtn.Hide();

            this.moveBackBetweenPagesBtn.Enabled = false;
            this.moveForwardBetweenPagesBtn.Enabled = false;

            if (this.navBtwPagesTableLayout.Controls.Count != 0)
                this.navBtwPagesTableLayout.Controls.Clear();
        }

        private void moveBackBetweenPagesBtn_Click(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            globalListViewRelatedPageNumber--;
            ChildListViewRedraw();
        }

        private void moveForwardBetweenPagesBtn_Click(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            globalListViewRelatedPageNumber++;
            ChildListViewRedraw();
        }

        private void navBtwPagesButton_Click(object sender, EventArgs eventArgs)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            globalListViewRelatedPageNumber = Int32.Parse(((Button)sender).Text);
            ChildListViewRedraw();
            SetMoveBetweenPagesBtnsEnabledProperty();
        }

        #endregion //Navigation buttons Events

        #region Move objects buttons Events

        private void moveObjUpBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                int firstGroupIndexToSwap = 
                    GroupRep.GetGroups().IndexOf((Group)globalSelectedItem);
                int secondGroupIndexToSwap = firstGroupIndexToSwap - 3;

                this.GroupRep.SwapGroups(firstGroupIndexToSwap, secondGroupIndexToSwap);
                this.GroupListViewRedraw();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap - 3;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
            }

            SetMoveObjBtnsEnabledProperty();
        }

        private void moveObjDownBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                int firstGroupIndexToSwap =
                    GroupRep.GetGroups().IndexOf((Group)globalSelectedItem);
                int secondGroupIndexToSwap = firstGroupIndexToSwap + 3;

                this.GroupRep.SwapGroups(firstGroupIndexToSwap, secondGroupIndexToSwap);
                this.GroupListViewRedraw();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap + 3;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
            }

            SetMoveObjBtnsEnabledProperty();
        }

        private void moveObjLeftBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                int firstGroupIndexToSwap =
                    GroupRep.GetGroups().IndexOf((Group)globalSelectedItem);
                int secondGroupIndexToSwap = firstGroupIndexToSwap - 1;

                this.GroupRep.SwapGroups(firstGroupIndexToSwap, secondGroupIndexToSwap);
                this.GroupListViewRedraw();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap - 1;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
            }

            SetMoveObjBtnsEnabledProperty();
        }

        private void moveObjRightBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                int firstGroupIndexToSwap =
                    GroupRep.GetGroups().IndexOf((Group)globalSelectedItem);
                int secondGroupIndexToSwap = firstGroupIndexToSwap + 1;

                this.GroupRep.SwapGroups(firstGroupIndexToSwap, secondGroupIndexToSwap);
                this.GroupListViewRedraw();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap + 1;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
            }

            SetMoveObjBtnsEnabledProperty();
        }

        #endregion //Move objects buttons Events

        private void SSHConnectBtn_Click(object sender, EventArgs e)
        {
            using (var client = new Renci.SshNet.ScpClient("192.168.0.224", 22, "tc", "324012"))
            {
                client.Connect();
                client.Download("/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml",
                    new FileInfo(
                        Path.Combine(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location), @"Data\weightCatalog-xml-config.xml"))
                        );
                client.Disconnect();
            }          
        }

        private void shopListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cashesCheckedListBox.BeginUpdate();

            if (cashesCheckedListBox.Items.Count != 0)
            {
                cashesCheckedListBox.Items.Clear();
            }

            int shopListIndex = ListOfShopsWithCashBoxes.IndexOf((Shop)shopListComboBox.SelectedItem);

            foreach(CashBox cashBox in ListOfShopsWithCashBoxes[shopListIndex].CashBoxes)
            {
                cashesCheckedListBox.Items.Add(cashBox);
            }

            cashesCheckedListBox.EndUpdate();
            cashesCheckedListBox.Refresh();
        }

        /*
        private void PopulateTreeView(List<Shop> shops)
        {
            this.cashBoxesTreeView.Nodes.Clear();
            this.cashBoxesTreeView.BeginUpdate();

            int counter = 0;

            foreach(Shop shop in shops)
            {
                this.cashBoxesTreeView.Nodes.Add("Shop " + shop.Code);

                foreach(CashBox cashBox in shop.CashBoxes)
                {
                    this.cashBoxesTreeView.Nodes[counter].Nodes
                        .Add("CashBox " + cashBox.Number + ' ' + cashBox.IpAddress);
                }

                counter++;
            }

            this.cashBoxesTreeView.EndUpdate();
            this.cashBoxesTreeView.Refresh();
        }
        */
    }
}
