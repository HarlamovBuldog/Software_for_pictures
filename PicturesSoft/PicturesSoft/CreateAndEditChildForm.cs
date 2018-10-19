using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class CreateAndEditChildForm : Form
    {
        #region Public properties

        public Child ChildToEditOrCreate { get; set; }
        public WorkMode WorkMode { get; set; }
        public Group GroupOwner { get; set; }

        #endregion //public properties

        #region Form creation

        public CreateAndEditChildForm(WorkMode workMode, Group groupOwner)
        {
            this.WorkMode = workMode;
            this.ChildToEditOrCreate = Child.CreateNewChild();
            GroupOwner = groupOwner;

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                this.Text = "Create new Child";
                InitializeComponent();
                this.childGroupCodeTextBox.Text = groupOwner.Id.ToString();
            }
        }

        public CreateAndEditChildForm(WorkMode workMode, Child child)
        {
            this.WorkMode = workMode;
            this.ChildToEditOrCreate = child;

            if (WorkMode.WorkType.Equals(WorkModeType.Edit))
            {
                this.Text = "Editing " + ChildToEditOrCreate.Name;
                InitializeComponent();

                //< filling TextBoxes with values
                this.childCodeTextBox.Text = ChildToEditOrCreate.Code.ToString();
                this.childNameTextBox.Text = ChildToEditOrCreate.Name;
                this.childSimpleNameTextBox.Text = ChildToEditOrCreate.SimpleName;
                this.childGroupCodeTextBox.Text = ChildToEditOrCreate.GroupCode.ToString();
                this.childImgPathTextBox.Text = ChildToEditOrCreate.ImgName;
                //>
            }
        }

        #endregion //form creation

        private void opnFileDlgChildBtn_Click(object sender, EventArgs e)
        {
            string openFileDialogFilter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            DialogInvoker dialogInvoker = new DialogInvoker(openFileDialogFilter);

            if (dialogInvoker.Invoke() == DialogResult.OK)
            {
                //System folder path of selected item (full source file name)
                this.childImgPathTextBox.Text = dialogInvoker.InvokeDialog.FileName;
            }
        }

        private void CreateAndEditChildCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAndEditChildSaveBtn_Click(object sender, EventArgs e)
        {
            //coping image file to /Image folder and renaming it on fly

            //System folder path of selected item (full source file name)
            string sourceFileName = this.childImgPathTextBox.Text;

            if (WorkMode.WorkType.Equals(WorkModeType.Edit)
                && !this.childImgPathTextBox.Text.Contains("\\"))
            {
                sourceFileName = ((Form1)this.Owner).DestImgFolderPath + 
                    "\\" + this.childImgPathTextBox.Text;
            }

            //Get image extension
            string imgExtension = sourceFileName.Substring(sourceFileName.LastIndexOf('.'));

            if (File.Exists(sourceFileName))
            {
                // Full destination path for file(full destination file name)
                string destFolderName;

                if (((Form1)this.Owner).AppWorkMode.WorkType == WorkModeType.LoadFromFinalXml)
                {
                    destFolderName = ((Form1)this.Owner).DestImgFolderPath +
                        "\\" + this.childCodeTextBox.Text +
                        imgExtension;
                    if(!sourceFileName.Equals(destFolderName))
                    {
                        if (sourceFileName.Contains(((Form1)this.Owner).DestImgFolderPath))
                            File.Move(sourceFileName, destFolderName);                        
                        else                        
                            File.Copy(sourceFileName, destFolderName, true);                        
                    }     
                }
                else
                {
                    destFolderName = Path.Combine(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location), @"Images\") +
                        this.childCodeTextBox.Text +
                        imgExtension;
                    File.Copy(sourceFileName, destFolderName, true);
                }
            }

            // File name for saving in .xml file
            string fileName = this.childCodeTextBox.Text + imgExtension;

            //need validation here
            //< getting values from textboxes
            ChildToEditOrCreate.Code = Int32.Parse(this.childCodeTextBox.Text);
            ChildToEditOrCreate.Name = this.childNameTextBox.Text;
            ChildToEditOrCreate.SimpleName = this.childSimpleNameTextBox.Text;
            ChildToEditOrCreate.GroupCode = Int32.Parse(this.childGroupCodeTextBox.Text);
            ChildToEditOrCreate.ImgName = fileName;
            //>

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                if (this.Owner != null)
                {
                    ((Form1)this.Owner).AddNewChild(ChildToEditOrCreate);
                }
            }
            else if (WorkMode.WorkType.Equals(WorkModeType.Edit))
            {
                if (this.Owner != null)
                {
                    ((Form1)this.Owner).UpdateChild(ChildToEditOrCreate);
                }
            }

            //remaking final xml file
            //((Form1)this.Owner).CreateFinalXmlFile();

            this.Close();
        }
    }
}
