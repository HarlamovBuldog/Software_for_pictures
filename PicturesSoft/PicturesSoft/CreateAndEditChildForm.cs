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
            Stream fileStream = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK && (fileStream = openFileDialog.OpenFile()) != null)
            {
                //System folder path of selected item (full source file name)
                string sourceFileName = openFileDialog.FileName;

                //File name for saving in .xml file
                string fileName = sourceFileName.Substring(sourceFileName.LastIndexOf("\\") + 1);
                this.childImgPathTextBox.Text = fileName;

                //Full destination path for file (Full file name)
                string destFolderName = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), @"Images\") + fileName;

                using (fileStream)
                {
                    File.Copy(sourceFileName, destFolderName, true);
                }
            }
        }

        private void CreateAndEditChildCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAndEditChildSaveBtn_Click(object sender, EventArgs e)
        {
            //need validation here
            //< getting values from textboxes
            ChildToEditOrCreate.Code = Int32.Parse(this.childCodeTextBox.Text);
            ChildToEditOrCreate.Name = this.childNameTextBox.Text;
            ChildToEditOrCreate.SimpleName = this.childSimpleNameTextBox.Text;
            ChildToEditOrCreate.GroupCode = Int32.Parse(this.childGroupCodeTextBox.Text);
            ChildToEditOrCreate.ImgName = this.childImgPathTextBox.Text;
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

            this.Close();
        }
    }
}
