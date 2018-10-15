﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class Form1 : Form
    {
        #region private fields

        private GroupRepository groupRep;
        private ChildRepository childRep;
        private object globalSelectedItem;

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

        public void AddNewGroup(Group newGroup)
        {
            groupRep.AddGroup(newGroup);
            GroupListViewRedraw();
        }

        #endregion //public methods

        private void GroupListViewRedraw()
        {
            this.groupsListView.Clear();

            foreach (Group gr in groupRep.GetGroups())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = gr.Name;
                //lvi.Tag = gr;

                this.groupsListView.Items.Add(lvi);
            }

            this.Refresh();
            this.Invalidate();
            this.groupsListView.Refresh();
            this.groupsListView.Invalidate();
        }

            #region groupListView Events

        private void groupsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var indexOfSelectedItem = groupsListView.SelectedIndices[0];
            var selectedGroup = groupRep.GetGroups()[indexOfSelectedItem];

            var childsListBelongToGroup = childRep.GetChildsBelongToGroup(selectedGroup.Id);

            this.childsListView.BeginUpdate();

            if(this.childsListView.Items.Count != 0)
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

                var indexOfSelectedItem = childsListView.SelectedIndices[0];
                var selectedChild = childRep.GetChilds()[indexOfSelectedItem];
                this.globalSelectedItem = selectedChild;
            }
            else
            {
                this.editSelectedBtn.Enabled = false;
                this.deleteSelectedBtn.Enabled = false;
            }
        }

        #endregion //childsListView Events

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
                //childform
            }
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
                //childform
            }
            
        }

        private void deleteSelectedBtn_Click(object sender, EventArgs e)
        {
            var globalSlctedItemType = globalSelectedItem.GetType();
            WorkMode workMode = new WorkMode() { WorkType = WorkModeType.Edit };

            if (globalSlctedItemType.Name.Equals("Group"))
            {
                //groupform
                //do delete stuff here
            }
            else if (globalSlctedItemType.Name.Equals("Child"))
            {
                //childform
                //do delete stuff here
            }

        }

    }
}
