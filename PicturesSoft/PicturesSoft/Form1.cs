using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

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
        public string UnixSpecifedXmlCnfgFilePath { get; set; }
        /// <summary>
        /// File path for alternative weightCatalog-xml-config.xml file
        /// which is downloaded from cash box every time after connect and
        /// according to which images from cash box are downloaded. 
        /// </summary>
        public string AlternativeXmlCnfgFilePath { get; set; }
        public WorkMode AppWorkMode { get; set; }
        public int GlobalListViewRelatedPageNumber { get; private set; }
        public ChildRepository ChildRep { get; private set; } = new ChildRepository();
        public GroupRepository GroupRep { get; private set; } = new GroupRepository();
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
            */

            //Setting AppWorkMode for now is not really necessary.
            //Idea was that we can build weightCatalog-xml-config.xml file
            //from nothing. So this property is just for supporting this old function.
            AppWorkMode = new WorkMode() { WorkType = WorkModeType.LoadFromFinalXml };

            AlternativeXmlCnfgFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Alt\weightCatalog-xml-config.xml");

            DestImgFolderPath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Temp\Images");

            UnixSpecifedXmlCnfgFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"UnixSpecifed\weightCatalog-xml-config.xml");

            //the first step is to check if directory structure is set
            //if not we build it
            DirectoryStructureInit();

            LocalCatalogFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Data\CashBoxesCatalog.xml");
            
            //before calling LoadShopsFromLocalCatalog function need to check
            //if file exist and all related checks need to be done

            if (File.Exists(LocalCatalogFilePath))
                ListOfShopsWithCashBoxes = LoadShopsFromLocalCatalog(LocalCatalogFilePath);
            else
            {
                MainAppSettingsInit();
                if (File.Exists(LocalCatalogFilePath))
                    ListOfShopsWithCashBoxes = LoadShopsFromLocalCatalog(LocalCatalogFilePath);
            }                

            XmlCnfgFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Temp\weightCatalog-xml-config.xml");

            if (File.Exists(XmlCnfgFilePath))
                RepositoriesInit();
            else
            {
                MessageBox.Show("Не найден файл weightCatalog-xml-config.xml. Для корректной " +
                    "работы программы выберите кассу и загрузите с неё необходимый файл.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.createNewItemBtn.Enabled = false;
                this.editSelectedBtn.Enabled = false;
                this.deleteSelectedBtn.Enabled = false;
            }                

            //filling comboBox
            if(ListOfShopsWithCashBoxes.Count != 0)
                ShopListComboBoxInit();

            //filling group listview
            if(GroupRep.GetGroups().Count != 0)
                GroupListViewRedraw();

            MainMenuStripInit();
        }

        private void RepositoriesInit()
        {
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.childsListView.Hide();
            this.BackToGroupsBtn.Hide();

            this.moveBackBetweenPagesBtn.Enabled = false;
            this.moveForwardBetweenPagesBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;
        }

        /// <summary>
        /// Method sets default directory structure
        /// </summary>
        private void DirectoryStructureInit()
        {
            string helpFilesFolderPath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Data");

            if (!Directory.Exists(helpFilesFolderPath))
                Directory.CreateDirectory(helpFilesFolderPath);

            if (!Directory.Exists(DestImgFolderPath))
                Directory.CreateDirectory(DestImgFolderPath);

            string altFilesFolderpath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"Alt");

            if (!Directory.Exists(altFilesFolderpath))
                Directory.CreateDirectory(altFilesFolderpath);

            string unixSpecifedFolderForXmlCnfgFilePath = Path.Combine(Path.GetDirectoryName(
                     Assembly.GetExecutingAssembly().Location), @"UnixSpecifed");

            if (!Directory.Exists(unixSpecifedFolderForXmlCnfgFilePath))
                Directory.CreateDirectory(unixSpecifedFolderForXmlCnfgFilePath);
        }

        private void MainAppSettingsInit()
        {
            var result = MessageBox.Show("Для корректной работы программы необходимо создать локальный каталог" +
                "касс в разрезе магазинов. Для этого необходимо ввести данные для подключения к серверу, " +
                "а также к соответствующей базе данных. Хотите выполнить это сейчас?",
                "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(result == DialogResult.Yes)
            {
                ConnectToServerAndDownloadTopologyStructureFile();
                ConnectToDbAndMakeLocalCatalog();
            }
            else
            {
                MessageBox.Show("Дерево касс в левой части экрана будет недоступно. Чтобы активировать " +
                    "его, перейдите в раздел Настройки", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            ConnectToServerAndDownloadTopologyStructureFile();
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

        private void ConnectToServerAndDownloadTopologyStructureFile()
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
            
            if (groupsListView.Items.Count > 0 && globalSelectedItem != null
                && globalSelectedItem.GetType().Name.Equals("Group"))
            {
                int indexOfGlobalSelectedItemFromRepoList =
                    GroupRep.GetGroups().IndexOf((Group)globalSelectedItem);
                this.groupsListView.Items[indexOfGlobalSelectedItemFromRepoList]
                    .Selected = true;
            }
        }

        private void ChildListViewRedraw()
        {
            var childsListBelongToGroup = ChildRep.GetChildsBelongToGroup(groupOwner.Id);

            if (this.childsListView.Items.Count != 0)
            {
                childsListView.Items.Clear();
            }

            int childsListBelongToGroupCount = childsListBelongToGroup.Count;

            int startIndexForChildListRedraw = (GlobalListViewRelatedPageNumber - 1) * 9;
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

            if (childsListView.Items.Count > 0 && globalSelectedItem != null 
                && globalSelectedItem.GetType().Name.Equals("Child"))
            {
                int indexOfGlobalSelectedItemFromRepoList =
                    ChildRep.GetChildsBelongToGroup(groupOwner.Id).IndexOf((Child)globalSelectedItem);
                this.childsListView.Items[indexOfGlobalSelectedItemFromRepoList]
                    .Selected = true;
            }
        }

        private void SetMoveBetweenPagesBtnsEnabledProperty()
        {
            int navBtwPagesTableLayoutPanelControlsCount = 
                this.navBtwPagesTableLayout.Controls.Count;

            if (GlobalListViewRelatedPageNumber == 1)
            {
                this.moveBackBetweenPagesBtn.Enabled = false;
                if(navBtwPagesTableLayoutPanelControlsCount > 1)
                    this.moveForwardBetweenPagesBtn.Enabled = true;
            }
            else if(GlobalListViewRelatedPageNumber < navBtwPagesTableLayoutPanelControlsCount)
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

            GlobalListViewRelatedPageNumber = 1;

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
                if(this.groupsListView.Items.Count != 0)
                {
                    CreateAndEditGroupForm CrAnEdGrForm =
                       new CreateAndEditGroupForm(workMode, DestImgFolderPath);
                    CrAnEdGrForm.ShowDialog(this);
                }
                else
                {
                    CreateAndEditGroupForm CrAnEdGrForm =
                       new CreateAndEditGroupForm(workMode, DestImgFolderPath, true);
                    CrAnEdGrForm.ShowDialog(this);
                }
            }
            else if (this.childsListView.Visible)
            {
                if (this.childsListView.Items.Count != 0)
                {
                    CreateAndEditChildForm CrAnEdChildForm =
                        new CreateAndEditChildForm(workMode, groupOwner, DestImgFolderPath);
                    CrAnEdChildForm.ShowDialog(this);
                }
                else
                {
                    CreateAndEditChildForm CrAnEdChildForm =
                        new CreateAndEditChildForm(workMode, groupOwner, DestImgFolderPath, true);
                    CrAnEdChildForm.ShowDialog(this);
                }                    
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

            GlobalListViewRelatedPageNumber--;
            ChildListViewRedraw();
        }

        private void moveForwardBetweenPagesBtn_Click(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            GlobalListViewRelatedPageNumber++;
            ChildListViewRedraw();
        }

        private void navBtwPagesButton_Click(object sender, EventArgs eventArgs)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            GlobalListViewRelatedPageNumber = Int32.Parse(((Button)sender).Text);
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
                this.groupsListView.Focus();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap - 3;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
                this.childsListView.Focus();
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
                this.groupsListView.Focus();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap + 3;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
                this.childsListView.Focus();
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
                this.groupsListView.Focus();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap - 1;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
                this.childsListView.Focus();
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
                this.groupsListView.Focus();
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                int firstChildIndexToSwap =
                    ChildRep.GetChilds().IndexOf((Child)globalSelectedItem);
                int secondChildIndexToSwap = firstChildIndexToSwap + 1;

                this.ChildRep.SwapChilds(firstChildIndexToSwap, secondChildIndexToSwap);
                this.ChildListViewRedraw();
                this.childsListView.Focus();
            }

            SetMoveObjBtnsEnabledProperty();
        }

        #endregion //Move objects buttons Events

        private void shopListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.selectAllCashBoxesCheckBox.Checked)
                this.selectAllCashBoxesCheckBox.Checked = false;

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

        private void selectAllCashBoxesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cashesCheckedListBox.Items.Count == 0)
                return;

            if(this.selectAllCashBoxesCheckBox.Checked)
            {
                for(int i = 0; i < this.cashesCheckedListBox.Items.Count; i++)
                {
                    this.cashesCheckedListBox.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < this.cashesCheckedListBox.Items.Count; i++)
                {
                    this.cashesCheckedListBox.SetItemChecked(i, false);
                }
            }            
        }

        /// <summary>
        /// Simply download xml config file and all connected images to local folder
        /// without any checks when local folder is empty.
        /// </summary>
        private void GetTemplateFromCashBoxIfLocalFolderIsEmpty(string cashBoxIpAddress)
        {
            using (ScpClient clientScp = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    clientScp.Connect();
                    clientScp.Download("/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml",
                        new FileInfo(XmlCnfgFilePath));
                    clientScp.Download("/home/tc/storage/crystal-cash/images/", new DirectoryInfo(DestImgFolderPath));
                    clientScp.Disconnect();
                    MessageBox.Show("Загрузка файла прошла успешно!", "Информация о загрузке файла с кассы",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RepositoriesInit();
                    GroupListViewRedraw();

                    if (!this.createNewItemBtn.Enabled)
                        this.createNewItemBtn.Enabled = true;
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды" +
                        "Возможно на кассе отсутствует файл конфигурации." +
                        "Попробуйте выполнить действие ещё раз.При повтороной ошибке обратитесь к системному администратору.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if(clientScp.IsConnected)
                        clientScp.Disconnect();
                }                
            }            
        }

        private void getTemplateFromCashBoxBtn_Click(object sender, EventArgs e)
        {
            string cashBoxIpAddress = "192.168.0.224";

            if (!File.Exists(XmlCnfgFilePath))
            {
                GetTemplateFromCashBoxIfLocalFolderIsEmpty(cashBoxIpAddress);
                return;
            }

            bool isFuncNeededToAbort = false;

            //< Downloading AlternativeXmlCnfgFilePath from cash box
            if (File.Exists(AlternativeXmlCnfgFilePath))
            {
                File.Delete(AlternativeXmlCnfgFilePath);
            }

            using (var client = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    client.Connect();
                    client.Download("/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml",
                               new FileInfo(AlternativeXmlCnfgFilePath));
                    client.Disconnect();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "Возможно на кассе отсутствует файл конфигурации." +
                        "Попробуйте выполнить действие ещё раз. При повтороной ошибке обратитесь к системному администратору.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isFuncNeededToAbort = true;
                }
                finally
                {
                    if (client.IsConnected)
                        client.Disconnect();
                }                
            }
            //>

            if (isFuncNeededToAbort)
                return;

            bool isXmlConfigFileNeededToDownload;
            List<string> listOfNewDownloadingImageFileNames = new List<string>();
            List<string> listOfDownloadingImageFileNamesNeedToBeReplaced = new List<string>();

            using (SshClient sshClient = new Renci.SshNet.SshClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                sshClient.Connect();

                //< assigning result of comparing xml config file size from remote server
                //with the same file on local machine to isXmlConfigFileNeededToDownload variable
                //to use it later for making decision about xml config file downloading
                string xmlConfigFileSizeOnCashBox = sshClient.RunCommand(
                    "cd /home/tc/storage/crystal-cash/config/plugins; wc -c < weightCatalog-xml-config.xml")
                    .Result.ToString();
                xmlConfigFileSizeOnCashBox = xmlConfigFileSizeOnCashBox.Replace("\n", "");

                string xmlConfigFileSizeOnLocalMachine = new FileInfo(XmlCnfgFilePath).Length.ToString();
                isXmlConfigFileNeededToDownload = !xmlConfigFileSizeOnCashBox.Equals(xmlConfigFileSizeOnLocalMachine);
                //>

                //< Filling list of all existing image file names according
                //to xml config file from cash box
                string listOfAllImageFilesFromCashBoxInOneString =
                    sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; ls").Result.ToString();
                List<string> listOfAllImageFilesFromCashBox =
                    listOfAllImageFilesFromCashBoxInOneString.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries).ToList<string>();

                //generally listOfAllImageFileNamesFromXmlConfigFileFromCashBox will be pretty enough
                //but using additional check is usefull for preventing downloading not existing image
                //files which cause exception
                List<string> listOfAllImageFileNamesFromXmlConfigFileFromCashBox = 
                    GetListOfAllImageFileNamesFromXmlConfigFile(AlternativeXmlCnfgFilePath);

                List<string> listOfAllExistingImageFileNamesAccordingToXmlConfigFileFromCashBox =
                    new List<string>();

                foreach(string imageFileNameFromXmlConfigFileFromCashBox in
                    listOfAllImageFileNamesFromXmlConfigFileFromCashBox)
                {
                    if(listOfAllImageFilesFromCashBox.Contains(imageFileNameFromXmlConfigFileFromCashBox))
                    {
                        listOfAllExistingImageFileNamesAccordingToXmlConfigFileFromCashBox
                            .Add(imageFileNameFromXmlConfigFileFromCashBox);
                    }
                }
                //>

                string[] arrayListOfAllImageFilesOnLocalMachine = Directory.GetFiles(DestImgFolderPath);

                for(int i = 0; i < arrayListOfAllImageFilesOnLocalMachine.Count(); i++)
                {
                    string imageFileNameOnLocalMachine = arrayListOfAllImageFilesOnLocalMachine[i];
                    imageFileNameOnLocalMachine = imageFileNameOnLocalMachine
                        .Substring(imageFileNameOnLocalMachine.LastIndexOf("\\") + 1);
                    arrayListOfAllImageFilesOnLocalMachine[i] = imageFileNameOnLocalMachine;
                }

                //< filling listOfDownloadingImageFileNamesNeedToBeReplaced and
                //listOfNewDownloadingImageFileNames lists with values
                foreach (string imageFileNameFromCashBox in listOfAllExistingImageFileNamesAccordingToXmlConfigFileFromCashBox)
                {
                    if (arrayListOfAllImageFilesOnLocalMachine.Contains<string>(imageFileNameFromCashBox))
                    {
                        string imageFileSizeFromCashBox =
                        sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; wc -c < " + imageFileNameFromCashBox)
                        .Result.ToString();
                        imageFileSizeFromCashBox = imageFileSizeFromCashBox.Replace("\n", "");

                        string imageFileSizeOnLocalMachine =
                            new FileInfo(DestImgFolderPath + @"\" + imageFileNameFromCashBox).Length.ToString();

                        if (!imageFileSizeFromCashBox.Equals(imageFileSizeOnLocalMachine))
                        {
                            listOfDownloadingImageFileNamesNeedToBeReplaced.Add(imageFileNameFromCashBox);
                        }
                    }
                    else
                    {
                        listOfNewDownloadingImageFileNames.Add(imageFileNameFromCashBox);
                    }
                }
                //>

                sshClient.Disconnect();
            }                  

            using (var client = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                client.Connect();

                if (isXmlConfigFileNeededToDownload)
                {
                    var result = MessageBox.Show("Подтвердите замену файла конфигурации weightCatalog-xml-config.xml",
                        "Информация о замене файла конфигурации при загрузке с кассы",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (result == DialogResult.OK)
                    {
                        if (File.Exists(XmlCnfgFilePath))
                        {
                            File.Delete(XmlCnfgFilePath);
                        }

                        client.Download("/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml",
                            new FileInfo(XmlCnfgFilePath));

                        RepositoriesInit();
                    }
                }
                else
                    MessageBox.Show("Файл конфигурации weightCatalog-xml-config.xml не требует замены",
                        "Информация о замене файла конфигурации при загрузке с кассы",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                StringBuilder infoAboutNewImageFiles = new StringBuilder();

                if(listOfNewDownloadingImageFileNames.Any())
                {
                    infoAboutNewImageFiles.Append("Новые файлы: ");
                    foreach (string newImageFileName in listOfNewDownloadingImageFileNames)
                    {
                        infoAboutNewImageFiles.AppendLine(newImageFileName);
                    }

                    var result = MessageBox.Show(infoAboutNewImageFiles.ToString() + 
                        "\nПодтвердите скачивание новых файлов", "Информация о новых файлах при загрузке с кассы", 
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if(result == DialogResult.OK)
                    {
                        foreach (string newImageFileName in listOfNewDownloadingImageFileNames)
                        {                                
                            client.Download("/home/tc/storage/crystal-cash/images/" + newImageFileName,
                                new FileInfo(DestImgFolderPath + @"\" + newImageFileName));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет новых файлов для скачивания.", "Информация о новых файлах при загрузке с кассы",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                StringBuilder infoAboutImageFilesNeedToBeReplaced = new StringBuilder();

                if(listOfDownloadingImageFileNamesNeedToBeReplaced.Any())
                {
                    infoAboutImageFilesNeedToBeReplaced.Append("Файлы, которые будут заменены:");
                    foreach (string imageFileNamesNeedToBeReplaced in listOfDownloadingImageFileNamesNeedToBeReplaced)
                    {
                        infoAboutImageFilesNeedToBeReplaced.AppendLine(imageFileNamesNeedToBeReplaced);
                    }

                    var result = MessageBox.Show(infoAboutImageFilesNeedToBeReplaced.ToString() +
                        "\nПодтвердите замену файлов", "Информация о файлах для скачивания с заменой при загрузке с кассы",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (result == DialogResult.OK)
                    {
                        foreach (string imageFileNameToReplace in listOfDownloadingImageFileNamesNeedToBeReplaced)
                        {
                            client.Download("/home/tc/storage/crystal-cash/images/" + imageFileNameToReplace,
                                new FileInfo(DestImgFolderPath + @"\" + imageFileNameToReplace));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет файлов для скачивания с заменой.",
                        "Информация о файлах для скачивания с заменой при загрузке с кассы",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }               

                client.Disconnect();
            }
            
            GroupListViewRedraw();

            if(!this.createNewItemBtn.Enabled)
                this.createNewItemBtn.Enabled = true;
        }

        private List<string> GetListOfAllImageFileNamesFromXmlConfigFile(string xDocFilePath)
        {
            List<string> listOfAllImageFileNamesFromXmlConfigFile = new List<string>();

            XDocument xDoc = XDocument.Load(xDocFilePath);

            XNamespace xmlns = XNamespace.Get("http://crystals.ru/cash/settings");

            foreach (XElement groupElem in xDoc.Element(xmlns + "moduleConfig").
                Element(xmlns + "property").Elements(xmlns + "group"))
            {
                listOfAllImageFileNamesFromXmlConfigFile.Add((string)groupElem.Attribute("image-name"));

                foreach (XElement childElem in groupElem.Elements(xmlns + "good"))
                {
                    listOfAllImageFileNamesFromXmlConfigFile.Add(
                        (string)childElem.Attribute("item") + ".png");
                }
            }

            return listOfAllImageFileNamesFromXmlConfigFile;
        }

        private void uploadInfoToCashBoxBtn_Click(object sender, EventArgs e)
        {
            //if there is no xml config then there is no need to continue 
            if (!File.Exists(XmlCnfgFilePath))
            {
                MessageBox.Show("Не найден файл weightCatalog-xml-config.xml. Для корректной " +
                   "работы программы выберите кассу и загрузите с неё необходимый файл.",
                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> listOfAllImageFileNamesFromXmlConfigFileOnLocalMachine =
                GetListOfAllImageFileNamesFromXmlConfigFile(XmlCnfgFilePath);

            List<string> listOfExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine =
                new List<string>();

            List<string> listOfNotExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine =
                new List<string>();

            string[] arrayListOfAllImageFilesOnLocalMachine = Directory.GetFiles(DestImgFolderPath);

            for (int i = 0; i < arrayListOfAllImageFilesOnLocalMachine.Count(); i++)
            {
                string imageFileNameOnLocalMachine = arrayListOfAllImageFilesOnLocalMachine[i];
                imageFileNameOnLocalMachine = imageFileNameOnLocalMachine
                    .Substring(imageFileNameOnLocalMachine.LastIndexOf("\\") + 1);
                arrayListOfAllImageFilesOnLocalMachine[i] = imageFileNameOnLocalMachine;
            }

            foreach (string imageFileNameFromXmlConfigFileOnLocalMachine
                in listOfAllImageFileNamesFromXmlConfigFileOnLocalMachine)
            {
                if (arrayListOfAllImageFilesOnLocalMachine.Contains(imageFileNameFromXmlConfigFileOnLocalMachine))
                    listOfExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine
                        .Add(imageFileNameFromXmlConfigFileOnLocalMachine);
                else
                    listOfNotExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine
                        .Add(imageFileNameFromXmlConfigFileOnLocalMachine);
            }

            //< Checking if there is any non existing image file according to 
            //xml cnfg file on local machine. If there are some then ask user to
            //continue uploading or not
            StringBuilder infoAboutNotExistingImageFileNames = new StringBuilder();

            if (listOfNotExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine.Any())
            {
                infoAboutNotExistingImageFileNames
                    .Append("В соответствии с файлом конфигурации, не хватает следующих файлов изображений: ");
                foreach (string newImageFileName
                    in listOfNotExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine)
                {
                    infoAboutNotExistingImageFileNames.AppendLine(newImageFileName);
                }

                var result = MessageBox.Show(infoAboutNotExistingImageFileNames.ToString() +
                    "\nПродолжить загрузку на кассу?", "Информация о недостающих файлах при загрузке на кассу",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.Cancel)
                    return;
            }
            //>

            string cashBoxIpAddress = "192.168.0.224";

            bool isFuncNeededToAbort = false;

            bool isCashBoxInfoEmpty = false;

            bool isXmlConfigFileNeededToUpload = true;
            List<string> listOfNewUploadingImageFileNames = new List<string>();
            List<string> listOfUploadingImageFileNamesNeedToBeReplaced = new List<string>();

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.NewLineChars = "\n";
            xmlWriterSettings.Indent = true;

            XmlDocument doc = new XmlDocument();
            doc.Load(XmlCnfgFilePath);

            if (File.Exists(UnixSpecifedXmlCnfgFilePath))
                File.Delete(UnixSpecifedXmlCnfgFilePath);

            using (XmlWriter writer = XmlWriter.Create(UnixSpecifedXmlCnfgFilePath, xmlWriterSettings))
            {
                doc.WriteTo(writer);
            }

            using (SshClient sshClient = new Renci.SshNet.SshClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    sshClient.Connect();

                    //< assigning result of comparing xml config file size from remote server
                    //with the same file on local machine to isXmlConfigFileNeededToUpload variable
                    //to use it later for making decision about xml config file uploading
                    string xmlConfigFileSizeOnCashBox = sshClient.RunCommand(
                        "cd /home/tc/storage/crystal-cash/config/plugins; wc -c < weightCatalog-xml-config.xml")
                        .Result.ToString();
                    xmlConfigFileSizeOnCashBox = xmlConfigFileSizeOnCashBox.Replace("\n", "");

                    string xmlConfigFileSizeOnLocalMachine = new FileInfo(UnixSpecifedXmlCnfgFilePath).Length.ToString();
                    isXmlConfigFileNeededToUpload = !xmlConfigFileSizeOnCashBox.Equals(xmlConfigFileSizeOnLocalMachine);
                    //>

                    string listOfAllImageFilesFromCashBoxInOneString =
                       sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; ls").Result.ToString();

                    string[] arrayListOfAllImageFilesFromCashBox =
                        listOfAllImageFilesFromCashBoxInOneString.Split(new[] { '\n' },
                        StringSplitOptions.RemoveEmptyEntries);

                    //< filling listOfUploadingImageFileNamesNeedToBeReplaced and
                    //listOfNewUploadingImageFileNames lists with values
                    foreach (string existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine
                        in listOfExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine)
                    {
                        if (arrayListOfAllImageFilesFromCashBox
                            .Contains<string>(existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine))
                        {
                            string imageFileSizeFromCashBox =
                            sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; wc -c < "
                            + existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine).Result.ToString();
                            imageFileSizeFromCashBox = imageFileSizeFromCashBox.Replace("\n", "");

                            string imageFileSizeOnLocalMachine =
                                new FileInfo(DestImgFolderPath + @"\" + existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine)
                                .Length.ToString();

                            if (!imageFileSizeFromCashBox.Equals(imageFileSizeOnLocalMachine))
                            {
                                listOfUploadingImageFileNamesNeedToBeReplaced
                                    .Add(existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine);
                            }
                        }
                        else
                        {
                            listOfNewUploadingImageFileNames
                                .Add(existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine);
                        }
                    }
                    //>

                    sshClient.Disconnect();
                }
                catch(Exception exception)
                {
                    if(exception.Message.Contains("No such file"))
                    {
                        isCashBoxInfoEmpty = true;
                        MessageBox.Show("На кассе нет файлов. Сейчас будет выполнена загрузка файлов на кассу",
                        "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "\nПопробуйте ещё раз загрузить данные. При повторной ошибке обратитесь к системному администратору",
                        "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    isFuncNeededToAbort = true;
                }
                finally
                {
                    if(sshClient.IsConnected)
                        sshClient.Disconnect();
                }               
            }           

            if(isCashBoxInfoEmpty)
            {
                UploadTemplateToEmptyCashBox(cashBoxIpAddress);
            }

            if (isFuncNeededToAbort)
                return;

            using (var client = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    client.Connect();

                    if (isXmlConfigFileNeededToUpload)
                    {
                        var result = MessageBox.Show("Подтвердите замену файла конфигурации weightCatalog-xml-config.xml",
                            "Информация о замене файла конфигурации при загрузке на кассу",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);                        

                        if (result == DialogResult.OK)
                        {
                            client.Upload(new FileInfo(UnixSpecifedXmlCnfgFilePath),
                        "/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml");
                        }
                    }
                    else
                        MessageBox.Show("Файл конфигурации weightCatalog-xml-config.xml не требует замены",
                            "Информация о замене файла конфигурации при загрузке на кассу",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                    StringBuilder infoAboutNewImageFiles = new StringBuilder();

                    if (listOfNewUploadingImageFileNames.Any())
                    {
                        infoAboutNewImageFiles.Append("Новые файлы: ");
                        foreach (string newImageFileName in listOfNewUploadingImageFileNames)
                        {
                            infoAboutNewImageFiles.AppendLine(newImageFileName);
                        }

                        var result = MessageBox.Show(infoAboutNewImageFiles.ToString() +
                            "\nПодтвердите загрузку новых файлов на кассу", "Информация о новых файлах при загрузке на кассу",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (result == DialogResult.OK)
                        {
                            foreach (string newImageFileName in listOfNewUploadingImageFileNames)
                            {
                                client.Upload(new FileInfo(DestImgFolderPath + @"\" + newImageFileName),
                                    "/home/tc/storage/crystal-cash/images/" + newImageFileName);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет новых файлов для загрузки на кассу.", "Информация о новых файлах при загрузке на кассу",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    StringBuilder infoAboutImageFilesNeedToBeReplaced = new StringBuilder();

                    if (listOfUploadingImageFileNamesNeedToBeReplaced.Any())
                    {
                        infoAboutImageFilesNeedToBeReplaced.Append("Файлы, которые будут заменены:");
                        foreach (string imageFileNamesNeedToBeReplaced in listOfUploadingImageFileNamesNeedToBeReplaced)
                        {
                            infoAboutImageFilesNeedToBeReplaced.AppendLine(imageFileNamesNeedToBeReplaced);
                        }

                        var result = MessageBox.Show(infoAboutImageFilesNeedToBeReplaced.ToString() +
                            "\nПодтвердите замену файлов", "Информация о файлах для загрузки с заменой на кассу",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (result == DialogResult.OK)
                        {
                            foreach (string imageFileNameToReplace in listOfUploadingImageFileNamesNeedToBeReplaced)
                            {
                                client.Upload(new FileInfo(DestImgFolderPath + @"\" + imageFileNameToReplace),
                                    "/home/tc/storage/crystal-cash/images/" + imageFileNameToReplace);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет файлов для загрузки на кассу с заменой.",
                            "Информация о замене файлов при загрузке на кассу",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    client.Disconnect();
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "\nПопробуйте ещё раз загрузить данные. При повторной ошибке обратитесь к системному администратору",
                        "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if(client.IsConnected)
                        client.Disconnect();
                }               
            }
        }

        private void UploadTemplateToEmptyCashBox(string cashBoxIpAddress)
        {           
            using (var client = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    client.Connect();
                    client.Upload(new FileInfo(XmlCnfgFilePath),
                       "/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml");
                    client.Upload(new DirectoryInfo(DestImgFolderPath), "/home/tc/storage/crystal-cash/images");
                    client.Disconnect();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "\nПопробуйте ещё раз загрузить данные. При повторной ошибке обратитесь к системному администратору",
                        "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (client.IsConnected)
                        client.Disconnect();
                }                
            }
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
