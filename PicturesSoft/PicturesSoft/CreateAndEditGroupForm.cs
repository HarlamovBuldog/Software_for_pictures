using System;
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
            Stream fileStream = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";

            if (openFileDialog.ShowDialog() == DialogResult.OK && (fileStream = openFileDialog.OpenFile()) != null)
            {
                //System folder path of selected item (full source file name)
                string sourceFileName = openFileDialog.FileName;

                //File name for saving in .xml file
                string fileName = sourceFileName.Substring(sourceFileName.LastIndexOf("\\") + 1);
                this.groupImgPathTextBox.Text = fileName;

                //Full destination path for file (Full file name)
                string destFolderName = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), @"Images\") + fileName;

                using (fileStream)
                {
                    File.Copy(sourceFileName, destFolderName, true);
                }
            }
        }

        private void CreateAndEditGrCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAndEditGrSaveBtn_Click(object sender, EventArgs e)
        {
            //getting values from textboxes
            GroupToEditOrCreate.Id = Int32.Parse(this.groupIdTextBox.Text);
            GroupToEditOrCreate.Name = this.groupNameTextBox.Text;
            GroupToEditOrCreate.ImgName = this.groupImgPathTextBox.Text;

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                if(this.Owner != null)
                {
                    ((Form1)this.Owner).AddNewGroup(GroupToEditOrCreate);
                }
            }
            else if(WorkMode.WorkType.Equals(WorkModeType.Edit))
            {

            }

            this.Close();
        }
    }
}
