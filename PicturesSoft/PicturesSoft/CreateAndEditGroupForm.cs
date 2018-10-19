﻿using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class CreateAndEditGroupForm : Form
    {
        #region Public properties

        public Group GroupToEditOrCreate { get; set; }
        public WorkMode WorkMode { get; set; }

        #endregion //public properties

        #region Form creation

        public CreateAndEditGroupForm(WorkMode workMode)
        {
            this.WorkMode = workMode;
            this.GroupToEditOrCreate = Group.CreateNewGroup();

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                this.Text = "Create new Group";
                InitializeComponent();
            }
        }

        public CreateAndEditGroupForm(WorkMode workMode, Group group)
        {
            this.WorkMode = workMode;
            this.GroupToEditOrCreate = group;

            if(WorkMode.WorkType.Equals(WorkModeType.Edit))
            {
                this.Text = "Editing " + GroupToEditOrCreate.Name;
                InitializeComponent();

                //< filling TextBoxes with values
                this.groupNameTextBox.Text = GroupToEditOrCreate.Name;
                this.groupIdTextBox.Text = GroupToEditOrCreate.Id.ToString();
                this.groupImgPathTextBox.Text = GroupToEditOrCreate.ImgName;
                //>
            }
        }

        #endregion //form creation


        private void opnFileDlgGrBtn_Click(object sender, System.EventArgs e)
        {
            string openFileDialogFilter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            DialogInvoker dialogInvoker = new DialogInvoker(openFileDialogFilter);

            if (dialogInvoker.Invoke() == DialogResult.OK)
            {
                //System folder path of selected item (full source file name)
                this.groupImgPathTextBox.Text = dialogInvoker.InvokeDialog.FileName;
            }
        }

        private void CreateAndEditGrCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAndEditGrSaveBtn_Click(object sender, EventArgs e)
        {
            //coping image file to /Image folder and renaming it on fly

            //System folder path of selected item (full source file name)
            string sourceFileName = this.groupImgPathTextBox.Text;
            //Get image extension
            string imgExtension = sourceFileName.Substring(sourceFileName.LastIndexOf('.'));
            // File name for saving in .xml file
            string fileName = sourceFileName.Substring(sourceFileName.LastIndexOf("\\") + 1);

            if (File.Exists(sourceFileName))
            {
                //Full destination path for file (full destination file name)
                string destFolderName;

                if (((Form1)this.Owner).AppWorkMode.WorkType == WorkModeType.LoadFromFinalXml)
                {
                    destFolderName = ((Form1)this.Owner).DestImgFolderPath +
                        "\\" + fileName;
                    if(!sourceFileName.Equals(destFolderName))
                    {
                        File.Copy(sourceFileName, destFolderName, true);
                    } 
                }
                else
                {
                    destFolderName = Path.Combine(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location), @"Images\") +
                        this.groupIdTextBox.Text +
                        imgExtension;
                    File.Copy(sourceFileName, destFolderName, true);
                }
            }

            //need validation here
            //< getting values from textboxes
            GroupToEditOrCreate.Id = Int32.Parse(this.groupIdTextBox.Text);
            GroupToEditOrCreate.Name = this.groupNameTextBox.Text;
            GroupToEditOrCreate.ImgName = fileName;
            //>

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                if(this.Owner != null)
                {
                    ((Form1)this.Owner).AddNewGroup(GroupToEditOrCreate);
                }
            }
            else if(WorkMode.WorkType.Equals(WorkModeType.Edit))
            {
                if (this.Owner != null)
                {
                    ((Form1)this.Owner).UpdateGroup(GroupToEditOrCreate);
                }
            }

            //remaking final xml file
            //((Form1)this.Owner).CreateFinalXmlFile();

            this.Close();
        }
    }
}
