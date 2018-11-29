using GroupedListControl;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

        public string XmlCnfgFilePath { get; private set; }
        public string DestImgFolderPath { get; private set; }
        public string LocalCatalogFilePath { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string UnixSpecifedXmlCnfgFilePath { get; private set; }
        /// <summary>
        /// File path for alternative weightCatalog-xml-config.xml file
        /// which is downloaded from cash box every time after connect and
        /// according to which images from cash box are downloaded. The only reason why we
        /// need this variable and additional folder is that we can't read xml config file
        /// directly from unix-system. So we need to download it.
        /// </summary>
        public string AlternativeXmlCnfgFilePath { get; private set; }
        public WorkMode AppWorkMode { get; set; }
        /// <summary>
        /// Listview page number under the same listview that is tracked globally.
        /// </summary>
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
            appSettingsDialog.Dispose();

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

                if(shopToAdd.CashBoxes.Count != 0)
                {
                    //sorting by number list of cashBoxes for each shop 
                    shopToAdd.CashBoxes = shopToAdd.CashBoxes
                    .OrderBy(s => Int32.Parse(s.Number)).ToList();
                }                    

                loadedShops.Add(shopToAdd);
            }

            return loadedShops;
        }

        private void ConnectToServerAndDownloadTopologyStructureFile()
        {
            ServerConnectionInfoForm ConnToServAndUpFile = new ServerConnectionInfoForm();
            ConnToServAndUpFile.ShowDialog(this);
            ConnToServAndUpFile.Dispose();
        }

        private void ConnectToDbAndMakeLocalCatalog()
        {
            DBConnSettingsForm ConnectToDbAndMakeLocalCatalog = new DBConnSettingsForm();
            ConnectToDbAndMakeLocalCatalog.ShowDialog(this);
            ConnectToDbAndMakeLocalCatalog.Dispose();
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
        }

        private void RestoreChildListViewItemHighLightedState()
        {
            if (childsListView.Items.Count > 0 && globalSelectedItem != null
                && globalSelectedItem.GetType().Name.Equals("Child"))
            {
                int indexOfGlobalSelectedItemFromRepoList =
                    ChildRep.GetChildsBelongToGroup(groupOwner.Id).IndexOf((Child)globalSelectedItem)
                    - (9 * (GlobalListViewRelatedPageNumber - 1));
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

        /// <summary>
        /// Method clears existing in navBtwPagesTableLayout list of controls
        /// and dynamically refills it as well as adding method to click event for each control.
        /// </summary>
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

            makeNavBtwPagesTableLayoutPanelControlHighlighted();
        }

        private void makeNavBtwPagesTableLayoutPanelControlHighlighted()
        {
            foreach(Control control in this.navBtwPagesTableLayout.Controls)
            {
                control.BackColor = Color.LightGray;
            }

            this.navBtwPagesTableLayout
                .Controls[GlobalListViewRelatedPageNumber - 1]
                .BackColor = Color.Yellow;
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

            this.catalogOrientationLabel.Text += " > " + groupOwner.Name;
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
            CrAnEdChildForm.Dispose();

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
                    CrAnEdGrForm.Dispose();
                }
                else
                {
                    CreateAndEditGroupForm CrAnEdGrForm =
                       new CreateAndEditGroupForm(workMode, DestImgFolderPath, true);
                    CrAnEdGrForm.ShowDialog(this);
                    CrAnEdGrForm.Dispose();
                }
            }
            else if (this.childsListView.Visible)
            {
                if (this.childsListView.Items.Count != 0)
                {
                    CreateAndEditChildForm CrAnEdChildForm =
                        new CreateAndEditChildForm(workMode, groupOwner, DestImgFolderPath);
                    CrAnEdChildForm.ShowDialog(this);
                    CrAnEdChildForm.Dispose();
                }
                else
                {
                    CreateAndEditChildForm CrAnEdChildForm =
                        new CreateAndEditChildForm(workMode, groupOwner, DestImgFolderPath, true);
                    CrAnEdChildForm.ShowDialog(this);
                    CrAnEdChildForm.Dispose();
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
                CrAnEdGrForm.Dispose();
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
                CrAnEdChildForm.Dispose();
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

            int initcatalogOrientationLabelStrLength =
                this.catalogOrientationLabel.Text.IndexOf('г') + 1;

            this.catalogOrientationLabel.Text =
                 this.catalogOrientationLabel.Text.Substring(0, initcatalogOrientationLabelStrLength);
        }

        private void moveBackBetweenPagesBtn_Click(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            GlobalListViewRelatedPageNumber--;
            ChildListViewRedraw();
            makeNavBtwPagesTableLayoutPanelControlHighlighted();
        }

        private void moveForwardBetweenPagesBtn_Click(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            GlobalListViewRelatedPageNumber++;
            ChildListViewRedraw();
            makeNavBtwPagesTableLayoutPanelControlHighlighted();
        }

        private void navBtwPagesButton_Click(object sender, EventArgs eventArgs)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

            this.moveObjectsBtnPanel.Enabled = false;

            GlobalListViewRelatedPageNumber = Int32.Parse(((Button)sender).Text);
            ChildListViewRedraw();
            SetMoveBetweenPagesBtnsEnabledProperty();
            makeNavBtwPagesTableLayoutPanelControlHighlighted();
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
                RestoreChildListViewItemHighLightedState();
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
                RestoreChildListViewItemHighLightedState();
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

                if(secondChildIndexToSwap > 0 && firstChildIndexToSwap % 9 == 0)
                {
                    GlobalListViewRelatedPageNumber--;
                }

                this.ChildListViewRedraw();
                RestoreChildListViewItemHighLightedState();
                this.childsListView.Focus();
                makeNavBtwPagesTableLayoutPanelControlHighlighted();
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

                if (secondChildIndexToSwap > 0 && secondChildIndexToSwap % 9 == 0)
                {
                    GlobalListViewRelatedPageNumber++;
                }

                this.ChildListViewRedraw();
                RestoreChildListViewItemHighLightedState();
                this.childsListView.Focus();
                makeNavBtwPagesTableLayoutPanelControlHighlighted();
            }

            SetMoveObjBtnsEnabledProperty();
        }

        #endregion //Move objects buttons Events

        #region Events connected with cash boxes checked list view at the left side of the window

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

        #endregion //Events connected with cash boxes checked list view at the left side of the window

        private void getTemplateFromCashBoxBtn_Click(object sender, EventArgs e)
        {
            DownloadAndCheckTemplateInfoFromCashBox();
        }

        private void DownloadAndCheckTemplateInfoFromCashBox()
        {
            //< Getting list of checked cash boxes from corresponding control
            //and check if there is more or less then 1 element checked
            var listOfCheckedCashBoxes = this.cashesCheckedListBox.CheckedItems;

            if (listOfCheckedCashBoxes.Count > 1)
            {
                MessageBox.Show("Отмечено несколько касс! Загрузить шаблон можно только с одной кассы!",
                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (listOfCheckedCashBoxes.Count == 0)
            {
                MessageBox.Show("Не отмечено ни одной кассы! Отметьте одну кассу, затем попробуйте ещё раз.",
                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //>

            //string cashBoxIpAddress = "192.168.0.224";

            string cashBoxIpAddress = ((CashBox)listOfCheckedCashBoxes[0]).IpAddress;
            string cashBoxNumber = ((CashBox)listOfCheckedCashBoxes[0]).Number;           

            bool isFuncNeededToAbort = false;

            //< Downloading AlternativeXmlCnfgFilePath from cash box.
            //We need to download xml config file from REMOTE cash box because
            //it is hard to read it without downloading (maybe i am wrong, just have chosen easiest way)
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
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "Возможно на кассе отсутствует файл конфигурации." +
                        "Попробуйте выполнить действие ещё раз. При повторной ошибке обратитесь к системному администратору.",
                        "Ошибка. Касса №" + cashBoxNumber + " " + cashBoxIpAddress, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //if (File.Exists(UnixSpecifedXmlCnfgFilePath))
            //    File.Delete(UnixSpecifedXmlCnfgFilePath);

            if (File.Exists(XmlCnfgFilePath))
                MakeUnixSpecifiedXmlConfigFile();

            //We are using GroupListControl.dll of some guy from github. It's ListView with some overloaded methods.
            //There is little bit strange way of building this table, but at least it's working as expected.
            //We are making groupListGroupControl adding ListGroup which can expand or collapse list of corresponding
            //listViewItems which store info about all file names that CashBox contains. their states and actions need to take.
            //We are filling these groupListGroupControl on fly by comparing md5 hash of files from cash box with the same files
            //on local machine.

            //< Making head for SummaryNotification form to fill table there
            GroupListControl notificationBeforeDownloadGroupListControl = new GroupListControl();

            //column headers for cashbox info
            ListGroup notificationBeforeDownloadColumnHeadersForCashBoxInfoListGroup = new ListGroup();
            notificationBeforeDownloadColumnHeadersForCashBoxInfoListGroup.BackColor = Color.Blue;
            notificationBeforeDownloadColumnHeadersForCashBoxInfoListGroup.Columns.Add("Ip кассы", 120);
            notificationBeforeDownloadColumnHeadersForCashBoxInfoListGroup.Columns.Add("Номер кассы", 150);
            notificationBeforeDownloadColumnHeadersForCashBoxInfoListGroup.Columns.Add("Рекомендации", 200);
            notificationBeforeDownloadGroupListControl.Controls.Add(notificationBeforeDownloadColumnHeadersForCashBoxInfoListGroup);

            //adding info about cashbox
            ListGroup infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup = new ListGroup();
            infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup.Columns.Add(cashBoxIpAddress, 120);
            infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup.Columns.Add(cashBoxNumber, 150);
            infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup.Columns.Add("Действий не требуется", 200);
            //lg.Name = "Group " + i;

            //column headers for files description for corresponding cashbox
            ListViewItem filesNotificationInfoBeforeDownloadColumHeaderslvi =
                infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup.Items.Add("Имя файла");
            filesNotificationInfoBeforeDownloadColumHeaderslvi.ForeColor = Color.White;
            filesNotificationInfoBeforeDownloadColumHeaderslvi.BackColor = Color.Blue;
            filesNotificationInfoBeforeDownloadColumHeaderslvi.SubItems.Add("Состояние");
            filesNotificationInfoBeforeDownloadColumHeaderslvi.SubItems.Add("Требуется");
            //>

            //List<KeyValuePair<bool, string>> listOfAllFileNamesNeededToDownloadWithActionIdentif =
            //new List<KeyValuePair<bool, string>>();
            bool isXmlConfigFileNeededToDownload = true;
            //bool = true if file is needed to be replaced.
            List<KeyValuePair<bool, string>> listOfAllImageFileNamesNeededToDownloadWithReplacementIdentif =
                new List<KeyValuePair<bool, string>>();

            using (SshClient sshClient = new Renci.SshNet.SshClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    sshClient.Connect();

                    //< assigning result of comparing xml config file MD5 hash sum from remote server
                    //with the same file on local machine to isXmlConfigFileNeededToDownload variable
                    //to use it later for making decision about xml config file downloading
                    string xmlConfigMD5HashSumOnCashBox = sshClient.RunCommand(
                        "cd /home/tc/storage/crystal-cash/config/plugins; md5sum weightCatalog-xml-config.xml")
                        .Result.ToString();
                    xmlConfigMD5HashSumOnCashBox = xmlConfigMD5HashSumOnCashBox
                        .Substring(0, xmlConfigMD5HashSumOnCashBox.IndexOf(' '));

                    ListViewItem xmlConfigItem = infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup
                        .Items.Add("weightCatalog-xml-config.xml");                    

                    if (File.Exists(UnixSpecifedXmlCnfgFilePath) && File.Exists(XmlCnfgFilePath))
                    {
                        string xmlConfigMD5HashSumOnLocalMachine;
                        using (var md5 = MD5.Create())
                        {
                            using (var stream = File.OpenRead(UnixSpecifedXmlCnfgFilePath))
                            {
                                var hash = md5.ComputeHash(stream);
                                xmlConfigMD5HashSumOnLocalMachine = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                            }
                        }
                        isXmlConfigFileNeededToDownload = !xmlConfigMD5HashSumOnCashBox.Equals(xmlConfigMD5HashSumOnLocalMachine);

                        if(isXmlConfigFileNeededToDownload)
                        {
                            xmlConfigItem.SubItems.Add("Присутствует");
                            xmlConfigItem.SubItems.Add("Загрузка с заменой");
                            //listOfAllImageFileNamesNeededToDownloadWithReplacementIdentif.Add(
                               // new KeyValuePair<bool, string>(true, "weightCatalog-xml-config.xml"));
                        }
                        else
                        {
                            xmlConfigItem.SubItems.Add("Присутствует");
                            xmlConfigItem.SubItems.Add("Ничего");
                        }
                    }
                    else
                    {
                        xmlConfigItem.SubItems.Add("Отсутствует");
                        xmlConfigItem.SubItems.Add("Загрузка");
                        //listOfAllImageFileNamesNeededToDownloadWithReplacementIdentif.Add(
                               // new KeyValuePair<bool, string>(false, "weightCatalog-xml-config.xml"));
                    }                    
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

                    foreach (string imageFileNameFromXmlConfigFileFromCashBox in
                        listOfAllImageFileNamesFromXmlConfigFileFromCashBox)
                    {
                        if (listOfAllImageFilesFromCashBox.Contains(imageFileNameFromXmlConfigFileFromCashBox))
                        {
                            listOfAllExistingImageFileNamesAccordingToXmlConfigFileFromCashBox
                                .Add(imageFileNameFromXmlConfigFileFromCashBox);
                        }
                    }
                    //>

                    string[] arrayListOfAllImageFilesOnLocalMachine = Directory.GetFiles(DestImgFolderPath);

                    for (int i = 0; i < arrayListOfAllImageFilesOnLocalMachine.Count(); i++)
                    {
                        string imageFileNameOnLocalMachine = arrayListOfAllImageFilesOnLocalMachine[i];
                        imageFileNameOnLocalMachine = imageFileNameOnLocalMachine
                            .Substring(imageFileNameOnLocalMachine.LastIndexOf("\\") + 1);
                        arrayListOfAllImageFilesOnLocalMachine[i] = imageFileNameOnLocalMachine;
                    }

                    //< Filling infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup with
                    //ListViewItems using MD5 hash sum comparison
                    foreach (string imageFileNameFromCashBox in listOfAllExistingImageFileNamesAccordingToXmlConfigFileFromCashBox)
                    {
                        ListViewItem imageInfoItem = infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup
                            .Items.Add(imageFileNameFromCashBox);

                        if (arrayListOfAllImageFilesOnLocalMachine.Contains<string>(imageFileNameFromCashBox))
                        {
                            //Note: RunCommand method will not throw exception if there is no such file. It will just return
                            //corresponding error message. But at the same time it can throw connection exception.
                            string imageFileMD5HashSumFromCashBox =
                            sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; md5sum " + imageFileNameFromCashBox)
                            .Result.ToString();
                            imageFileMD5HashSumFromCashBox = imageFileMD5HashSumFromCashBox
                            .Substring(0, imageFileMD5HashSumFromCashBox.IndexOf(' '));

                            string imageFileMD5HashSumOnLocalMachine;
                            using (var md5 = MD5.Create())
                            {
                                using (var stream = File.OpenRead(DestImgFolderPath + @"\" + imageFileNameFromCashBox))
                                {
                                    var hash = md5.ComputeHash(stream);
                                    imageFileMD5HashSumOnLocalMachine = BitConverter.ToString(hash).Replace("-", "")
                                        .ToLowerInvariant();
                                }
                            }

                            if (!imageFileMD5HashSumFromCashBox.Equals(imageFileMD5HashSumOnLocalMachine))
                            {
                                imageInfoItem.SubItems.Add("Присутствует");
                                imageInfoItem.SubItems.Add("Загрузка с заменой");
                                listOfAllImageFileNamesNeededToDownloadWithReplacementIdentif.Add(
                                new KeyValuePair<bool, string>(true, imageFileNameFromCashBox));
                            }
                            else
                            {
                                imageInfoItem.SubItems.Add("Присутствует");
                                imageInfoItem.SubItems.Add("Ничего");
                            }
                        }
                        else
                        {
                            imageInfoItem.SubItems.Add("Отсутствует");
                            imageInfoItem.SubItems.Add("Загрузка");
                            listOfAllImageFileNamesNeededToDownloadWithReplacementIdentif.Add(
                                new KeyValuePair<bool, string>(false, imageFileNameFromCashBox));
                        }
                    }
                    //>                   
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "Попробуйте выполнить действие ещё раз. При повторной ошибке обратитесь к системному администратору.",
                        "Ошибка. Касса №" + cashBoxNumber + " " + cashBoxIpAddress,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isFuncNeededToAbort = true;
                }
                finally
                {
                    if(sshClient.IsConnected)
                        sshClient.Disconnect();
                }                
            }

            if (isFuncNeededToAbort)
                return;

            bool noActionsNeeded = true;

            //We are changing text of "status" field for corresponding cashbox if needed. 
            //By default it's set to "No actions needed".
            //If there is any file that need some actions we change cash box text of "status" field to "Actions needed"
            foreach(ListViewItem lvi in infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup.Items)
            {
                foreach(ListViewItem.ListViewSubItem subLvi in lvi.SubItems)
                {
                    if(subLvi.Text.Equals("Загрузка с заменой") || subLvi.Text.Equals("Загрузка"))
                    {
                        infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup
                            .Columns[2].Text = "Требуются действия";
                        noActionsNeeded = false;
                        break;
                    }
                }
            }

            notificationBeforeDownloadGroupListControl.Controls
                .Add(infoAboutCashBoxWithNestedFilesNotificationInfoBeforeDownloadListGroup);

            SummaryNotificationForm summaryNotificationFormBeforeDownload = 
                new SummaryNotificationForm(notificationBeforeDownloadGroupListControl,
                    new WorkMode() { WorkType = WorkModeType.DownloadFromCashBoxAndShowNotificationTable},
                    noActionsNeeded);

            DialogResult userDecision = summaryNotificationFormBeforeDownload.ShowDialog();
            summaryNotificationFormBeforeDownload.Dispose();

            if (noActionsNeeded == true || userDecision == DialogResult.Cancel 
                || userDecision == DialogResult.No || userDecision == DialogResult.Abort)
                return;

            ILookup<bool, string> lookupOfAllImageFileNamesNeededToDownloadGrouppedByReplacementIdentif = 
                listOfAllImageFileNamesNeededToDownloadWithReplacementIdentif.ToLookup(kvp => kvp.Key, kvp => kvp.Value);

            GroupListControl downloadingResultsGroupListControl = new GroupListControl();
            
            //column headers for cashbox
            ListGroup downloadingResultsColumnHeadersForCashBoxInfoListGroup = new ListGroup();
            downloadingResultsColumnHeadersForCashBoxInfoListGroup.BackColor = Color.Blue;
            downloadingResultsColumnHeadersForCashBoxInfoListGroup.Columns.Add("Ip Адресс кассы", 120);
            downloadingResultsColumnHeadersForCashBoxInfoListGroup.Columns.Add("Номер кассы", 150);
            downloadingResultsColumnHeadersForCashBoxInfoListGroup.Columns.Add("Результат", 150);
            downloadingResultsGroupListControl.Controls.Add(downloadingResultsColumnHeadersForCashBoxInfoListGroup);

            //adding info about cashbox
            ListGroup infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup = new ListGroup();
            infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup.Columns.Add(cashBoxIpAddress, 120);
            infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup.Columns.Add(cashBoxNumber, 150);
            infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup.Columns.Add("Успешно", 150);
            //lg.Name = "Group " + i;
            
            //column headers for files description
            ListViewItem filesDonwloadingResultsColumHeaderslvi = infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup
                .Items.Add("Имя файла");
            filesDonwloadingResultsColumHeaderslvi.ForeColor = Color.White;
            filesDonwloadingResultsColumHeaderslvi.BackColor = Color.Blue;
            filesDonwloadingResultsColumHeaderslvi.SubItems.Add("Операция");
            filesDonwloadingResultsColumHeaderslvi.SubItems.Add("Результат");

            using (var client = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                try
                {
                    client.Connect();

                    if (isXmlConfigFileNeededToDownload)
                    {
                        ListViewItem xmlConfigItem = infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup
                            .Items.Add("weightCatalog-xml-config.xml");

                        if (File.Exists(XmlCnfgFilePath))
                        {
                            xmlConfigItem.SubItems.Add("Замена");
                            File.Delete(XmlCnfgFilePath);
                        }
                        else
                        {
                            xmlConfigItem.SubItems.Add("Скачивание");
                        }

                        try
                        {
                            client.Download("/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml",
                            new FileInfo(XmlCnfgFilePath));
                            xmlConfigItem.SubItems.Add("Успешно");
                        }
                        catch (Exception excep)
                        {
                            //write exception to log here
                            //listOfImageFilesThatCauseErrors.Add("weightCatalog-xml-config.xml");
                            xmlConfigItem.SubItems.Add("Ошибка");
                        }
                    }

                    //< Downloading images that are need to be replaced (that have the same names on LOCAL machine) 
                    //from REMOTE cash box
                    foreach (string fileNameNeededToDownloadWithReplacement 
                        in lookupOfAllImageFileNamesNeededToDownloadGrouppedByReplacementIdentif[true])
                    {
                        ListViewItem imageFileLvi = infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup
                            .Items.Add(fileNameNeededToDownloadWithReplacement);
                        imageFileLvi.SubItems.Add("Скачивание с заменой");

                        try
                        {
                            client.Download("/home/tc/storage/crystal-cash/images/" + fileNameNeededToDownloadWithReplacement,
                            new FileInfo(DestImgFolderPath + @"\" + fileNameNeededToDownloadWithReplacement));
                            imageFileLvi.SubItems.Add("Успешно");
                        }
                        catch (Exception excep)
                        {
                            //write exception to log here
                            imageFileLvi.SubItems.Add("Ошибка");
                        }
                    }
                    //>

                    //< Downloading new images (that are not exist on LOCAL machine) from REMOTE cash box
                    foreach (string fileNameNeededToDownloadWithoutReplacement
                        in lookupOfAllImageFileNamesNeededToDownloadGrouppedByReplacementIdentif[false])
                    {
                        ListViewItem imageFileLvi = infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup
                            .Items.Add(fileNameNeededToDownloadWithoutReplacement);
                        imageFileLvi.SubItems.Add("Скачивание");

                        try
                        {
                            client.Download("/home/tc/storage/crystal-cash/images/" + fileNameNeededToDownloadWithoutReplacement,
                            new FileInfo(DestImgFolderPath + @"\" + fileNameNeededToDownloadWithoutReplacement));
                            imageFileLvi.SubItems.Add("Успешно");
                        }
                        catch (Exception excep)
                        {
                            //write exception to log here
                            imageFileLvi.SubItems.Add("Ошибка");
                        }
                    }
                    //>              
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе либо выполнения команды." +
                        "Попробуйте выполнить действие ещё раз. При повторной ошибке обратитесь к системному администратору.",
                        "Ошибка. Касса №" + cashBoxNumber + " " + cashBoxIpAddress,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if(client.IsConnected)
                        client.Disconnect();
                }
                
            }

            //We are changing text of "status" field for corresponding cashbox if needed. 
            //By default it's set to "No actions needed".
            //If there is any file that need some actions we change cash box text of "status" field to "Actions needed"
            foreach (ListViewItem lvi in infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup.Items)
            {
                foreach (ListViewItem.ListViewSubItem subLvi in lvi.SubItems)
                {
                    if (subLvi.Text.Equals("Ошибка"))
                    {
                        infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup
                            .Columns[2].Text = "Есть ошибки";
                        break;
                    }
                }
            }

            downloadingResultsGroupListControl.Controls
                .Add(infoAboutCashBoxWithNestedFilesDownloadingResultsListGroup);

            SummaryNotificationForm summaryNotificationFormAfterDownload =
                new SummaryNotificationForm(downloadingResultsGroupListControl,
                    new WorkMode() { WorkType = WorkModeType.DownloadFromCashBoxAndShowCorrespondingResults });

            summaryNotificationFormAfterDownload.ShowDialog();
            summaryNotificationFormAfterDownload.Dispose();

            if (File.Exists(XmlCnfgFilePath))
                RepositoriesInit();

            GroupListViewRedraw();

            if (!this.createNewItemBtn.Enabled)
                this.createNewItemBtn.Enabled = true;             
        }

        /// <summary>
        /// Method runs through all XElements in xml config file and collect all image file names in one list.
        /// </summary>
        /// <param name="xDocFilePath">Full xml config file path.</param>
        /// <returns>Image file names list type of string.</returns>
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
            UploadAndCheckTemplateInfoToCashBox();
        }

        /// <summary>
        /// The method contains 3 blocks. FIRST BLOCK provides checking if image file names 
        /// listed in LOCAL xml config file exist in corresponding folder on LOCAL machine.
        /// SECOND BLOCK is about using ssh.client to connect to cash box and comparing MD5 hash
        /// sum of xml config file on LOCAL machine and on REMOTE cash box as well as comparing MD5 hash sums of
        /// image files on both places and filling corresponding "itemsto- and itemsnotto- UPLOAD" lists.
        /// With help of THIRD BLOCK programm perfoms directly UPLOADING template info TO cash box according to
        /// created in second block "itemsto- and itemsnotto- UPLOAD" lists by connecting TO cash box using SCP client.
        /// </summary>
        private void UploadAndCheckTemplateInfoToCashBox()
        {
            //if there is no xml config file on LOCAL machine then there is no need to continue 
            if (!File.Exists(XmlCnfgFilePath))
            {
                MessageBox.Show("Не найден файл weightCatalog-xml-config.xml. Для корректной " +
                   "работы программы выберите кассу и загрузите с неё необходимый файл.",
                   "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #region Checking if image file names listed in LOCAL xml config file exist on LOCAL machine.

            List<string> listOfAllImageFileNamesFromXmlConfigFileOnLocalMachine =
                GetListOfAllImageFileNamesFromXmlConfigFile(XmlCnfgFilePath);

            List<string> listOfExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine =
                new List<string>();

            List<string> listOfNotExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine =
                new List<string>();

            //< Filling list of ALL image file names existing in related folder on LOCAL machine
            string[] arrayListOfAllImageFilesOnLocalMachine = Directory.GetFiles(DestImgFolderPath);

            //the problem here is that we get full file pathes, but we need only file names.
            for (int i = 0; i < arrayListOfAllImageFilesOnLocalMachine.Count(); i++)
            {
                string imageFileNameOnLocalMachine = arrayListOfAllImageFilesOnLocalMachine[i];
                imageFileNameOnLocalMachine = imageFileNameOnLocalMachine
                    .Substring(imageFileNameOnLocalMachine.LastIndexOf("\\") + 1);
                arrayListOfAllImageFilesOnLocalMachine[i] = imageFileNameOnLocalMachine;
            }
            //>

            //the idea is that some image file names listed in xml config file
            //on LOCAL machine could not exist in related folder. The result will be empty image background
            //in programm interface on LOCAL machine as well as on REMOTE CASH BOX after uploading.
            //So we need to tell user about these occurences 
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

            //Checking if there is any non existing image file according to 
            //xml cnfg file on local machine. If there are some then ask user to
            //continue uploading or not.
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

            #endregion //Checking if image file names listed in LOCAL xml config file exist on LOCAL machine.

            MakeUnixSpecifiedXmlConfigFile();

            //< Getting list of all selected in corresponding checkedlistview cash boxes
            var listOfCheckedCashBoxes = this.cashesCheckedListBox.CheckedItems;

            if (listOfCheckedCashBoxes.Count == 0)
            {
                MessageBox.Show("Не отмечено ни одной кассы! Отметьте хотя бы одно кассу, затем попробуйте ещё раз.",
                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //>

            //< We getting hash sum of LOCAL xml config file before foreach close cause we need to get it once.
            //Note that we use LOCAL UnixSpecified xml config file, which was made before, in comparison below
            string xmlConfigFileHashSumOnLocalMachine;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(UnixSpecifedXmlCnfgFilePath))
                {
                    var hash = md5.ComputeHash(stream);
                    xmlConfigFileHashSumOnLocalMachine = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
            //>

            //We are using GroupListControl.dll of some guy from github. It's ListView with some overloaded methods.
            //There is little bit strange way of building this table, but at least it's working as expected.
            //We are making groupListGroupControl adding ListGroup which can expand or collapse list of corresponding
            //listViewItems which store info about all file names that CashBox contains. their states and actions need to take.
            //We are filling these groupListGroupControl on fly by comparing md5 hash of files from cash box with the same files
            //on local machine.

            //Making head for SummaryNotification form to fill table there
            GroupListControl notificationBeforeUploadGroupListControl = new GroupListControl();

            //column headers for cashbox info
            ListGroup notificationBeforeUploadColumnHeadersForCashBoxInfoListGroup = new ListGroup();
            notificationBeforeUploadColumnHeadersForCashBoxInfoListGroup.BackColor = Color.Blue;
            notificationBeforeUploadColumnHeadersForCashBoxInfoListGroup.Columns.Add("Ip кассы", 120);
            notificationBeforeUploadColumnHeadersForCashBoxInfoListGroup.Columns.Add("Номер кассы", 150);
            notificationBeforeUploadColumnHeadersForCashBoxInfoListGroup.Columns.Add("Рекомендации", 200);
            notificationBeforeUploadGroupListControl.Controls
                .Add(notificationBeforeUploadColumnHeadersForCashBoxInfoListGroup);

            //Making head for SummaryNotification form to fill table there
            GroupListControl uploadingResultsGroupListControl = new GroupListControl();

            //column headers for cashbox
            ListGroup uploadingResultsColumnHeadersForCashBoxInfoListGroup = new ListGroup();
            uploadingResultsColumnHeadersForCashBoxInfoListGroup.BackColor = Color.Blue;
            uploadingResultsColumnHeadersForCashBoxInfoListGroup.Columns.Add("Ip Адресс кассы", 120);
            uploadingResultsColumnHeadersForCashBoxInfoListGroup.Columns.Add("Номер кассы", 150);
            uploadingResultsColumnHeadersForCashBoxInfoListGroup.Columns.Add("Результат", 150);
            uploadingResultsGroupListControl.Controls.Add(uploadingResultsColumnHeadersForCashBoxInfoListGroup);

            bool isXmlConfigFileNeededToUpload = true;
            bool isXmlConfigFileStoredOnCashBox = true;

            bool noActionsNeededForAllCashBoxes = true;

            //bool = true if file is needed to be replaced.
            List<KeyValuePair<bool, string>> listOfAllImageFileNamesNeededToUploadWithReplacementIdentif =
                new List<KeyValuePair<bool, string>>();

            //We go through all cash boxes getting ip address on fly and then
            //connecting to cash box first using ssh client to decide which files we
            //need to upload and are we need to upload them at all, second using scp client
            //and finally perform uploading.
            foreach (CashBox cashBox in listOfCheckedCashBoxes)
            {
                string cashBoxIpAddress = cashBox.IpAddress;
                string cashBoxNumber = cashBox.Number;
                //string cashBoxIpAddress = "192.168.0.224";

                //adding info about cashbox
                ListGroup infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup = new ListGroup();
                infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup.Columns.Add(cashBoxIpAddress, 120);
                infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup.Columns.Add(cashBoxNumber, 150);
                infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup.Columns.Add("Действий не требуется", 200);
                //lg.Name = "Group " + i;

                //column headers for files description for corresponding cashbox
                ListViewItem filesNotificationInfoBeforeUploadColumHeaderslvi =
                    infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup.Items.Add("Имя файла");
                filesNotificationInfoBeforeUploadColumHeaderslvi.ForeColor = Color.White;
                filesNotificationInfoBeforeUploadColumHeaderslvi.BackColor = Color.Blue;
                filesNotificationInfoBeforeUploadColumHeaderslvi.SubItems.Add("Состояние");
                filesNotificationInfoBeforeUploadColumHeaderslvi.SubItems.Add("Требуется");
                //>

                //List<KeyValuePair<bool, string>> listOfAllFileNamesNeededToDownloadWithActionIdentif =
                //new List<KeyValuePair<bool, string>>();                

                bool isTemplateInfoUploadingToCashBoxNeededToAbort = false;

                #region Comparing MD5 hash sums on REMOTE cash box with the same files on LOCAL machine and filling related lists

                using (SshClient sshClient = new Renci.SshNet.SshClient(cashBoxIpAddress, 22, "tc", "324012"))
                {
                    try
                    {
                        sshClient.Connect();

                        //Getting xml config file size located ON CASH BOX                        
                        string xmlConfigFileMD5HashSumOnCashBox = sshClient.RunCommand(
                            "cd /home/tc/storage/crystal-cash/config/plugins; md5sum weightCatalog-xml-config.xml")
                            .Result.ToString();

                        ListViewItem xmlConfigItem = infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup
                            .Items.Add("weightCatalog-xml-config.xml");

                        if (!xmlConfigFileMD5HashSumOnCashBox.Contains("No such"))
                        {
                            //Calling method RunCommand will return hash sum and file name 
                            //seperated with whitespace in one string. So all we need is to get only hash code.
                            xmlConfigFileMD5HashSumOnCashBox = xmlConfigFileMD5HashSumOnCashBox
                                .Substring(0, xmlConfigFileMD5HashSumOnCashBox.IndexOf(' '));

                            //Assigning result of comparing xml config file hash sum from REMOTE server
                            //with the same file hash sum on LOCAL machine to isXmlConfigFileNeededToUpload variable
                            //to use it later for making decision about xml config file uploading TO CASH BOX
                            isXmlConfigFileNeededToUpload = !xmlConfigFileMD5HashSumOnCashBox
                                .Equals(xmlConfigFileHashSumOnLocalMachine);

                            if (isXmlConfigFileNeededToUpload)
                            {
                                xmlConfigItem.SubItems.Add("Присутствует");
                                xmlConfigItem.SubItems.Add("Загрузка с заменой");
                            }
                            else
                            {
                                xmlConfigItem.SubItems.Add("Присутствует");
                                xmlConfigItem.SubItems.Add("Ничего");
                            }
                        }
                        else
                        {
                            isXmlConfigFileStoredOnCashBox = false;
                            xmlConfigItem.SubItems.Add("Отсутствует");
                            xmlConfigItem.SubItems.Add("Загрузка");
                        }

                        string listOfAllImageFilesFromCashBoxInOneString =
                           sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; ls").Result.ToString();

                        string[] arrayListOfAllImageFilesFromCashBox =
                            listOfAllImageFilesFromCashBoxInOneString.Split(new[] { '\n' },
                            StringSplitOptions.RemoveEmptyEntries);

                        //< Filling listOfUploadingImageFileNamesNeedToBeReplaced and
                        //listOfNewUploadingImageFileNames lists with values
                        foreach (string existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine
                            in listOfExistingImageFileNamesAccordingToXmlCnfgFileOnLocalMachine)
                        {
                            ListViewItem imageInfoItem = infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup
                                .Items.Add(existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine);

                            if (arrayListOfAllImageFilesFromCashBox
                                .Contains<string>(existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine))
                            {
                                string imageFileMD5HashSumFromCashBox =
                                sshClient.RunCommand("cd /home/tc/storage/crystal-cash/images; md5sum "
                                + existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine).Result.ToString();
                                imageFileMD5HashSumFromCashBox = imageFileMD5HashSumFromCashBox
                                    .Substring(0, imageFileMD5HashSumFromCashBox.IndexOf(' '));

                                string imageFileMD5HashSumOnLocalMachine;

                                using (var md5 = MD5.Create())
                                {
                                    using (var stream = File.OpenRead(DestImgFolderPath + @"\" +
                                        existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine))
                                    {
                                        var hash = md5.ComputeHash(stream);
                                        imageFileMD5HashSumOnLocalMachine = BitConverter.ToString(hash)
                                            .Replace("-", "").ToLowerInvariant();
                                    }
                                }

                                if (!imageFileMD5HashSumFromCashBox.Equals(imageFileMD5HashSumOnLocalMachine))
                                {
                                    imageInfoItem.SubItems.Add("Присутствует");
                                    imageInfoItem.SubItems.Add("Загрузка с заменой");
                                    listOfAllImageFileNamesNeededToUploadWithReplacementIdentif.Add(
                                        new KeyValuePair<bool, string>(true,
                                            existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine));
                                }
                                else
                                {
                                    imageInfoItem.SubItems.Add("Присутствует");
                                    imageInfoItem.SubItems.Add("Ничего");
                                }
                            }
                            else
                            {
                                imageInfoItem.SubItems.Add("Отсутствует");
                                imageInfoItem.SubItems.Add("Загрузка");
                                listOfAllImageFileNamesNeededToUploadWithReplacementIdentif.Add(
                                    new KeyValuePair<bool, string>(false,
                                        existingImageFileNameAccordingToXmlCnfgFileOnLocalMachine));
                            }
                        }
                        //>
                    }
                    catch (Exception exception)
                    {
                        //write to log here

                        //too much message boxed can appear.
                        //better to make one more pivot table or create StringBuilder with info about all cash boxes
                        //which cause errors and show this stringbuilder in one messagebox
                        MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе №" +
                            cashBoxNumber + " " + cashBoxIpAddress + " либо выполнения команды." +
                        "\nПопробуйте ещё раз загрузить данные. При повторной ошибке обратитесь к системному администратору",
                        "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        isTemplateInfoUploadingToCashBoxNeededToAbort = true;
                    }
                    finally
                    {
                        if (sshClient.IsConnected)
                            sshClient.Disconnect();
                    }
                }

                #endregion //Comparing MD5 hash sums on REMOTE cash box with the same files on LOCAL machine and filling related lists

                //if something went wrong while connecting to cash box above there is no need to continue
                if (isTemplateInfoUploadingToCashBoxNeededToAbort)
                    continue;

                //We are changing text of "status" field for corresponding cashbox if needed. 
                //By default it's set to "No actions needed".
                //If there is any file that need some actions we change cash box text of "status" field to "Actions needed"
                foreach (ListViewItem lvi in infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup.Items)
                {
                    foreach (ListViewItem.ListViewSubItem subLvi in lvi.SubItems)
                    {
                        if (subLvi.Text.Equals("Загрузка с заменой") || subLvi.Text.Equals("Загрузка"))
                        {
                            infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup
                                .Columns[2].Text = "Требуются действия";
                            noActionsNeededForAllCashBoxes = false;
                            break;
                        }
                    }
                }

                notificationBeforeUploadGroupListControl.Controls
                    .Add(infoAboutCashBoxWithNestedFilesNotificationInfoBeforeUploadListGroup);
            }

            SummaryNotificationForm summaryNotificationFormBeforeUpload =
                new SummaryNotificationForm(notificationBeforeUploadGroupListControl,
                    new WorkMode() { WorkType = WorkModeType.UploadToCashBoxAndShowNotificationTable },
                    noActionsNeededForAllCashBoxes);

            DialogResult userDecision = summaryNotificationFormBeforeUpload.ShowDialog();
            summaryNotificationFormBeforeUpload.Dispose();

            if (noActionsNeededForAllCashBoxes || userDecision == DialogResult.Cancel || userDecision == DialogResult.No || userDecision == DialogResult.Abort)
                return;

            foreach (CashBox cashBox in listOfCheckedCashBoxes)
            {
                string cashBoxIpAddress = cashBox.IpAddress;
                string cashBoxNumber = cashBox.Number;

                #region UPLOADING info TO cash box according to created before "itemsto- and itemsnotto- UPLOAD" lists                

                ILookup<bool, string> lookupOfAllImageFileNamesNeededToUploadGrouppedByReplacementIdentif =
                listOfAllImageFileNamesNeededToUploadWithReplacementIdentif.ToLookup(kvp => kvp.Key, kvp => kvp.Value);

                //adding info about cashbox
                ListGroup infoAboutCashBoxWithNestedFilesUploadingResultsListGroup = new ListGroup();
                infoAboutCashBoxWithNestedFilesUploadingResultsListGroup.Columns.Add(cashBoxIpAddress, 120);
                infoAboutCashBoxWithNestedFilesUploadingResultsListGroup.Columns.Add(cashBoxNumber, 150);
                infoAboutCashBoxWithNestedFilesUploadingResultsListGroup.Columns.Add("Успешно", 150);
                //lg.Name = "Group " + i;

                //column headers for files description
                ListViewItem filesUploadingResultsColumHeaderslvi = infoAboutCashBoxWithNestedFilesUploadingResultsListGroup
                    .Items.Add("Имя файла");
                filesUploadingResultsColumHeaderslvi.ForeColor = Color.White;
                filesUploadingResultsColumHeaderslvi.BackColor = Color.Blue;
                filesUploadingResultsColumHeaderslvi.SubItems.Add("Операция");
                filesUploadingResultsColumHeaderslvi.SubItems.Add("Результат");

                using (var client = new Renci.SshNet.ScpClient(cashBoxIpAddress, 22, "tc", "324012"))
                {
                    try
                    {
                        client.Connect();

                        if (isXmlConfigFileNeededToUpload)
                        {
                            ListViewItem xmlConfigItem = infoAboutCashBoxWithNestedFilesUploadingResultsListGroup
                                .Items.Add("weightCatalog-xml-config.xml");

                            if(isXmlConfigFileStoredOnCashBox)
                                xmlConfigItem.SubItems.Add("Скачивание с заменой");
                            else
                                xmlConfigItem.SubItems.Add("Скачивание");

                            try
                            {
                                client.Upload(new FileInfo(UnixSpecifedXmlCnfgFilePath),
                                    "/home/tc/storage/crystal-cash/config/plugins/weightCatalog-xml-config.xml");
                                xmlConfigItem.SubItems.Add("Успешно");
                            }
                            catch (Exception excep)
                            {
                                //write exception to log here
                                xmlConfigItem.SubItems.Add("Ошибка");
                            }                                
                        }

                        //< Uploading images that are need to be replaced (that have the same names on LOCAL machine) 
                        //to REMOTE cash box
                        foreach (string fileNameNeededToUploadWithReplacement
                            in lookupOfAllImageFileNamesNeededToUploadGrouppedByReplacementIdentif[true])
                        {
                            ListViewItem imageFileLvi = infoAboutCashBoxWithNestedFilesUploadingResultsListGroup
                                .Items.Add(fileNameNeededToUploadWithReplacement);
                            imageFileLvi.SubItems.Add("Скачивание с заменой");

                            try
                            {
                                client.Download("/home/tc/storage/crystal-cash/images/" + fileNameNeededToUploadWithReplacement,
                                new FileInfo(DestImgFolderPath + @"\" + fileNameNeededToUploadWithReplacement));
                                imageFileLvi.SubItems.Add("Успешно");
                            }
                            catch (Exception excep)
                            {
                                //write exception to log here
                                imageFileLvi.SubItems.Add("Ошибка");
                            }
                        }
                        //>

                        //< Uploading new images (that are not exist on REMOTE cashbox) to REMOTE cashbox from LOCAL machine
                        foreach (string fileNameNeededToUploadWithoutReplacement
                            in lookupOfAllImageFileNamesNeededToUploadGrouppedByReplacementIdentif[false])
                        {
                            ListViewItem imageFileLvi = infoAboutCashBoxWithNestedFilesUploadingResultsListGroup
                                .Items.Add(fileNameNeededToUploadWithoutReplacement);
                            imageFileLvi.SubItems.Add("Скачивание");

                            try
                            {
                                client.Download("/home/tc/storage/crystal-cash/images/" + fileNameNeededToUploadWithoutReplacement,
                                new FileInfo(DestImgFolderPath + @"\" + fileNameNeededToUploadWithoutReplacement));
                                imageFileLvi.SubItems.Add("Успешно");
                            }
                            catch (Exception excep)
                            {
                                //write exception to log here
                                imageFileLvi.SubItems.Add("Ошибка");
                            }
                        }
                        //>              
                    }
                    catch (Exception exception)
                    {
                        //write to log here

                        //too much message boxed can appear.
                        //better to make one more pivot table or create StringBuilder with info about all cash boxes
                        //which cause errors and show this stringbuilder in one messagebox
                        MessageBox.Show(exception.Message + "\nПроизошла ошибка подключения к кассе №" +
                                cashBoxNumber + " " + cashBoxIpAddress + " либо выполнения команды." +
                            "\nПопробуйте ещё раз загрузить данные. При повторной ошибке обратитесь к системному администратору",
                            "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        if (client.IsConnected)
                            client.Disconnect();
                    }
                }

                //We are changing text of "Result" field for corresponding cashbox if needed. 
                //By default it's set to "Success". If there is any file that need some actions
                //we change cash box text of "Result" field to "There are errors"
                foreach (ListViewItem lvi in infoAboutCashBoxWithNestedFilesUploadingResultsListGroup.Items)
                {
                    foreach (ListViewItem.ListViewSubItem subLvi in lvi.SubItems)
                    {
                        if (subLvi.Text.Equals("Ошибка"))
                        {
                            infoAboutCashBoxWithNestedFilesUploadingResultsListGroup
                                .Columns[2].Text = "Есть ошибки";
                            break;
                        }
                    }
                }

                uploadingResultsGroupListControl.Controls
                    .Add(infoAboutCashBoxWithNestedFilesUploadingResultsListGroup);                

                #endregion //UPLOADING info TO cash box according to created "itemsto- and itemsnotto- UPLOAD" lists before
            }

            SummaryNotificationForm summaryNotificationFormAfterDownload =
                    new SummaryNotificationForm(uploadingResultsGroupListControl,
                        new WorkMode() { WorkType = WorkModeType.UploadToCashBoxAndShowCorrespondingResults });

            summaryNotificationFormAfterDownload.ShowDialog();
            summaryNotificationFormAfterDownload.Dispose();
        }

        /// <summary>
        /// Rewriting line ending chars in xml config file
        /// for work stability on Unix systems.
        /// </summary>
        private void MakeUnixSpecifiedXmlConfigFile()
        {
            //< Rewriting line ending chars in xml config file
            //for work stability on Unix systems before uploading. Also we do it before
            //checking file sizes to suit unix file style.
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();

            //"\n"=>"LF"(Unix), "\r\n"=>"CRLF"(Windows)
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
            //>
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            /*
            string resultHashFromLocalMachine;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(UnixSpecifedXmlCnfgFilePath))
                {
                    var hash = md5.ComputeHash(stream);
                    resultHashFromLocalMachine = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
            
            string cashBoxIpAddress = "192.168.3.157";
            using (SshClient sshClient = new Renci.SshNet.SshClient(cashBoxIpAddress, 22, "tc", "324012"))
            {
                sshClient.Connect();
               // var install = sshClient.RunCommand("sudo apt-get install libarchive-zip-perl").Result.ToString();
                var result = sshClient
                    .RunCommand("cd /home/tc/storage/crystal-cash/config/plugins; md5sum weightCatalog-xml-config.xml")
                    .Result.ToString();
                result = result.Substring(0, result.IndexOf(' '));
                MessageBox.Show("Unix: " + result + "\nWindows:" + resultHashFromLocalMachine);
                sshClient.Disconnect();
            }   
            */

            //SummaryNotificationForm summaryNotificationForm = new SummaryNotificationForm(ListOfShopsWithCashBoxes);
            //summaryNotificationForm.ShowDialog();
        }
    }
}
