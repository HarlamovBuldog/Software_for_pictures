using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PicturesSoft
{
    public partial class Form1 : Form
    {
        #region private fields

        private GroupRepository groupRep;
        private ChildRepository childRep;
        private object globalSelectedItem;
        private Group groupOwner;

        #endregion //private fields

        #region Form1 creation

        public Form1()
        {
            InitializeComponent();

            //Groups.xml file path
           // string groupXmlFilePath = Path.Combine(Path.GetDirectoryName(
            //        Assembly.GetExecutingAssembly().Location), @"Data\Groups.xml");

            //Childs.xml file path
           // string childXmlFilePath = Path.Combine(Path.GetDirectoryName(
           //         Assembly.GetExecutingAssembly().Location), @"Data\Childs.xml");

            groupRep = new GroupRepository("Data/Groups.xml");
            childRep = new ChildRepository("Data/Childs.xml");

            //filling listbox
            this.listBox1.Items.AddRange(groupRep.GetGroups().ToArray<Group>());
            
            foreach(Group gr in groupRep.GetGroups())
            {
                //< filling table layout panel
                this.tableLayoutPanel1.Controls.Add(
                    new Button()
                    {
                        Name = gr.Name, Text = gr.Name, Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                    ((((System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right))),
                        //BackgroundImage = Image.FromFile(
                        //@"D:\Work\GitHub_repos\Software_for_pictures\PicturesSoft\PicturesSoft\Images\coin.jpg"),
                        BackgroundImage = Image.FromFile(
                            Path.Combine(
                             Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                             "Images\\coin.jpg"))
                        
                    });
                //>

                //< filling groupListView
                ListViewItem lvi = new ListViewItem();
                lvi.Text = gr.Name;
                lvi.Tag = gr;

                this.groupsListView.Items.Add(lvi);
                //>
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.childsListView.Hide();
            this.BackToGroupsBtn.Hide();
        }

        #endregion //Form1 creation

        #region public methods

        //< Public Group methods
        public void AddNewGroup(Group newGroup)
        {
            groupRep.AddGroup(newGroup);
            GroupListViewRedraw();
        }

        public void UpdateGroup(Group groupToUpdate)
        {
            var groupListIndex = groupRep.GetGroups().IndexOf((Group)globalSelectedItem);

            groupRep.UpdateGroup(groupToUpdate, groupListIndex);
            GroupListViewRedraw();
        }

        public void DeleteGroup()
        {
            var groupListIndex = groupRep.GetGroups().IndexOf((Group)globalSelectedItem);

            groupRep.DeleteGroup(groupListIndex);
            GroupListViewRedraw();

            //need to also delete all childs connected to the group
            this.DeleteAllChildsBelongToGroup();
        }
        //>

        //< Public Child methods

        public void AddNewChild(Child newChild)
        {
            childRep.AddChild(newChild);
            ChildListViewRedraw();
        }

        public void UpdateChild(Child childToUpdate)
        {
            var childListIndex = childRep.GetChilds().IndexOf((Child)globalSelectedItem);

            childRep.UpdateChild(childToUpdate, childListIndex);
            ChildListViewRedraw();
        }

        public void DeleteChild()
        {
            var childListIndex = childRep.GetChilds().IndexOf((Child)globalSelectedItem);

            childRep.DeleteChild(childListIndex);
            ChildListViewRedraw();
        }
        //>

        #endregion //public methods

        #region Private helpers

        private void GroupListViewRedraw()
        {
            this.groupsListView.Clear();

            foreach (Group gr in groupRep.GetGroups())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = gr.Name;
                lvi.Tag = gr;

                this.groupsListView.Items.Add(lvi);
            }

            this.Refresh();
            this.Invalidate();
            this.groupsListView.Refresh();
            this.groupsListView.Invalidate();
        }

        private void ChildListViewRedraw()
        {
            var childsListBelongToGroup = childRep.GetChildsBelongToGroup(groupOwner.Id);

            if (this.childsListView.Items.Count != 0)
            {
                childsListView.Items.Clear();
            }

            foreach (Child ch in childsListBelongToGroup)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ch.Name;
                lvi.Tag = ch;

                this.childsListView.Items.Add(lvi);
            }

            this.Refresh();
            this.Invalidate();
            this.childsListView.Refresh();
            this.childsListView.Invalidate();
        }

        private void DeleteAllChildsBelongToGroup()
        {
            var childsListBelongToGroup = 
                childRep.GetChildsBelongToGroup(((Group)globalSelectedItem).Id);

            foreach (Child ch in childsListBelongToGroup)
            {
                var indexOfItemToDelete =
                    childRep.GetChilds().IndexOf(ch);
                this.childRep.DeleteChild(indexOfItemToDelete);
            }
        }

        #endregion //Private helpers

        #region groupListView Events

        private void groupsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var indexOfSelectedItem = groupsListView.SelectedIndices[0];
            groupOwner = groupRep.GetGroups()[indexOfSelectedItem];

            this.childsListView.BeginUpdate();
            this.ChildListViewRedraw();
            this.groupsListView.Hide();
            this.childsListView.EndUpdate();

            this.childsListView.Show();
            this.BackToGroupsBtn.Show();
        }

        private void groupsListView_VisibleChanged(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;
            this.groupsListView.SelectedItems.Clear();
        }

        private void groupsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (groupsListView.SelectedItems.Count != 0)
            {
                this.editSelectedBtn.Enabled = true;
                this.deleteSelectedBtn.Enabled = true;

                var indexOfSelectedItem = groupsListView.SelectedIndices[0];
                var selectedGroup = groupRep.GetGroups()[indexOfSelectedItem];
                this.globalSelectedItem = selectedGroup;
            }
            else
            {
                this.editSelectedBtn.Enabled = false;
                this.deleteSelectedBtn.Enabled = false;
            }
        }

        #endregion //groupListView Events

        #region childsListView Events

        private void childsListView_VisibleChanged(object sender, EventArgs e)
        {
            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;
            this.childsListView.SelectedItems.Clear();
        }

        private void childsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (childsListView.SelectedItems.Count != 0)
            {
                this.editSelectedBtn.Enabled = true;
                this.deleteSelectedBtn.Enabled = true;

                var indexOfSelectedItem = 
                    childRep.GetChilds().IndexOf((Child)childsListView.SelectedItems[0].Tag);
                this.globalSelectedItem = childRep.GetChilds()[indexOfSelectedItem];
            }
            else
            {
                this.editSelectedBtn.Enabled = false;
                this.deleteSelectedBtn.Enabled = false;
            }
        }

        #endregion //childsListView Events

        #region Common buttons Events

        private void BackToGroupsBtn_Click(object sender, EventArgs e)
        {
            this.groupsListView.Show();
            this.childsListView.Hide();
            this.BackToGroupsBtn.Hide();
        }

        private void createNewItemBtn_Click(object sender, EventArgs e)
        {
            WorkMode workMode = new WorkMode() { WorkType = WorkModeType.Create };

            if (this.groupsListView.Visible)
            {
                CreateAndEditGroupForm CrAnEdGrForm = new CreateAndEditGroupForm(workMode);
                CrAnEdGrForm.ShowDialog(this);
            }
            else if (this.childsListView.Visible)
            {
                CreateAndEditChildForm CrAnEdChildForm =
                    new CreateAndEditChildForm(workMode, groupOwner);
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
                Group groupToEdit = (Group)globalSelectedItem;

                CreateAndEditGroupForm CrAnEdGrForm = 
                    new CreateAndEditGroupForm(workMode, groupToEdit);
                CrAnEdGrForm.ShowDialog(this);
            }
            else if(globalSlctedItemType.Name.Equals("Child"))
            {
                Child childToEdit = (Child)globalSelectedItem;

                CreateAndEditChildForm CrAnEdChildForm =
                    new CreateAndEditChildForm(workMode, childToEdit);
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
                    this.DeleteGroup();
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
                    this.DeleteChild();
            }

            this.editSelectedBtn.Enabled = false;
            this.deleteSelectedBtn.Enabled = false;

        }

        #endregion //Common buttons Events

        private void createFinalXmlBtn_Click(object sender, EventArgs e)
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
            foreach(XAttribute XAttr in moduleConfigAttrList)
            {
                moduleConfigXElement.Add(XAttr);
            }

            //create property element
            XElement propertyXElem = new XElement(xmlns + "property");
            XAttribute keyAttr = new XAttribute("key", "catalog");
            propertyXElem.Add(keyAttr);

            foreach (Group gr in groupRep.GetGroups())
            {
                //create group element
                XElement groupXElem = new XElement(xmlns + "group");

                //create attributes for group element
                XAttribute groupName = new XAttribute("name", gr.Name);
                XAttribute groupImgName = new XAttribute("image-name", gr.ImgName);

                //add attributes to group element
                groupXElem.Add(groupName);
                groupXElem.Add(groupImgName);

                foreach(Child ch in childRep.GetChildsBelongToGroup(gr.Id))
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
    }
}
