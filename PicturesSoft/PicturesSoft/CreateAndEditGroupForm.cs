using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        public int InitGroupId { get; set; }
        public string SourceFullFileName { get; set; }
        public string PredeterminedDestImgFolderPath { get; set; }

        #endregion //public properties

        #region Form creation

        public CreateAndEditGroupForm(WorkMode workMode, string DestImgFolderPath)
        {
            this.WorkMode = workMode;
            this.GroupToEditOrCreate = Group.CreateNewGroup();
            PredeterminedDestImgFolderPath = DestImgFolderPath;

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                this.Text = "Create new Group";
                InitializeComponent();
            }
        }

        public CreateAndEditGroupForm(WorkMode workMode, Group group, string DestImgFolderPath)
        {
            this.WorkMode = workMode;
            this.GroupToEditOrCreate = group;
            InitGroupId = GroupToEditOrCreate.Id;
            PredeterminedDestImgFolderPath = DestImgFolderPath;
            SourceFullFileName = PredeterminedDestImgFolderPath + "\\" + GroupToEditOrCreate.ImgName;

            if (WorkMode.WorkType.Equals(WorkModeType.Edit))
            {
                this.Text = "Editing " + GroupToEditOrCreate.Name;
                InitializeComponent();

                //< filling TextBoxes with values
                this.groupNameTextBox.Text = GroupToEditOrCreate.Name;
                this.groupIdTextBox.Text = GroupToEditOrCreate.Id.ToString();
                this.groupImgPathTextBox.Text = GroupToEditOrCreate.ImgName;
                //>

                this.radioBtnsStoragePanel.Visible = false;
            }
        }

        #endregion //form creation


        private void opnFileDlgGrBtn_Click(object sender, System.EventArgs e)
        {
            /*string openFileDialogFilter = "Bitmap Files|*.bmp" +
                "|Enhanced Windows MetaFile|*.emf" +
                "|Exchangeable Image File|*.exif" +
                "|Gif Files|*.gif|Icons|*.ico|JPEG Files|*.jpg" + 
                "|PNG Files|*.png|TIFF Files|*.tif|Windows MetaFile|*.wmf";
                */
            string openFileDialogFilter =
                "Image files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp";

            DialogInvoker dialogInvoker = new DialogInvoker(openFileDialogFilter);

            if (dialogInvoker.Invoke() == DialogResult.OK)
            {
                //System folder path of selected item (full source file name)
                SourceFullFileName = dialogInvoker.InvokeDialog.FileName;
                this.groupImgPathTextBox.Text =
                    SourceFullFileName.Substring(SourceFullFileName.LastIndexOf("\\") + 1);
            }
        }

        private void CreateAndEditGrCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAndEditGrSaveBtn_Click(object sender, EventArgs e)
        {
            if (ValidateForm() == false)
                return;

            //System folder path of selected item (full source file name)
            //string sourceFileName = this.groupImgPathTextBox.Text;
            //Get image extension
            string imgExtension = SourceFullFileName.Substring(SourceFullFileName.LastIndexOf('.'));

            // File name for saving in .xml file
            string fileName;

            fileName = SourceFullFileName
                .Substring(SourceFullFileName.LastIndexOf("\\") + 1);

            if (!imgExtension.Equals(".png"))
                fileName =
                    System.IO.Path.GetFileNameWithoutExtension(SourceFullFileName
                    .Substring(SourceFullFileName.LastIndexOf("\\") + 1)) + ".png";

            //Full destination path for file (full destination file name)
            string destFolderName;

            if (((Form1)this.Owner).AppWorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                destFolderName = ((Form1)this.Owner).DestImgFolderPath +
                "\\" + fileName;

                if (!SourceFullFileName.Equals(destFolderName))
                {
                    if (!imgExtension.Equals(".png"))
                    {
                        Image img;
                        using (Stream fs = new FileStream(SourceFullFileName, FileMode.Open, FileAccess.ReadWrite))
                        {
                            img = Image.FromStream(fs);

                            try
                            {
                                img.Save(destFolderName, ImageFormat.Png);
                            }
                            catch
                            {
                                MessageBox.Show("Failed to save image to Png format.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }       
                    }
                    else
                    {
                        File.Copy(SourceFullFileName, destFolderName);
                    }
                }
            }
            else //oldMode
            {
                destFolderName = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), @"Images\") +
                    this.groupIdTextBox.Text +
                    imgExtension;
                File.Copy(SourceFullFileName, destFolderName, true);
            }

            //< getting values from textboxes
            GroupToEditOrCreate.Id = Int32.Parse(this.groupIdTextBox.Text);
            GroupToEditOrCreate.Name = this.groupNameTextBox.Text;
            GroupToEditOrCreate.ImgName = fileName;
            //>

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                if(this.Owner != null)
                {
                    if(this.addToTheEndRadioBtn.Checked)
                        ((Form1)this.Owner).AddNewGroup(GroupToEditOrCreate);
                    else
                        ((Form1)this.Owner).AddNewGroup(GroupToEditOrCreate, false);
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

        private void groupIdTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateGroupId();
        }

        private void groupNameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateGroupName();
        }

        private void groupImgPathTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateGroupImgPath();
        }

        private bool ValidateGroupId()
        {
            bool isValid = true;

            string groupIdToValidate = this.groupIdTextBox.Text.Trim();

            this.groupIdTextBox.Text = groupIdToValidate;

            if (groupIdToValidate.Equals(""))
            {
                errorProvider1.SetError(this.groupIdTextBox, "Please enter group id!");
                isValid = false;
            }
            else if (!int.TryParse(groupIdToValidate, out int result))
            {
                errorProvider1.SetError(this.groupIdTextBox, "Group id is incorrect. " +
                    "Please enter valid group id!");
                isValid = false;
            }
            else if (WorkMode.WorkType.Equals(WorkModeType.Create) 
                && !((Form1)this.Owner).GroupRep
                    .ValidateGroupId(int.Parse(groupIdToValidate)))
            {
                errorProvider1.SetError(this.groupIdTextBox, "Group id is already exist. " +
                    "Please enter another group id!");
                isValid = false;
            }
            else if (WorkMode.WorkType.Equals(WorkModeType.Edit) &&
                InitGroupId != int.Parse(groupIdToValidate) &&
                !((Form1)this.Owner).GroupRep
                    .ValidateGroupId(int.Parse(groupIdToValidate)))
            {
                errorProvider1.SetError(this.groupIdTextBox, "Group id is already exist. " +
                    "Please enter another group id!");
                isValid = false;
            }

            if (isValid)
                errorProvider1.SetError(this.groupIdTextBox, "");

            return isValid;
        }

        private bool ValidateGroupName()
        {
            bool isValid = true;

            string groupNameToValidate = this.groupNameTextBox.Text.Trim();

            this.groupNameTextBox.Text = groupNameToValidate;

            if (groupNameToValidate.Equals(""))
            {
                errorProvider1.SetError(this.groupNameTextBox, "Please enter group name!");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(this.groupNameTextBox, "");
            }

            return isValid;
        }

        private bool ValidateGroupImgPath()
        {
            bool isValid = true;

            string groupImgPathToValidate = this.groupImgPathTextBox.Text.Trim();

            this.groupImgPathTextBox.Text = groupImgPathToValidate;

            if (groupImgPathToValidate.Equals(""))
            {
                errorProvider1.SetError(this.groupImgPathTextBox, 
                    "Please click browse button and choose image for group!");
                isValid = false;
            }
            else if(!groupImgPathToValidate.Equals(""))
            {
                if (File.Exists(SourceFullFileName) == false)
                {
                    errorProvider1.SetError(this.groupImgPathTextBox,
                   "Image file doesn't exist!");
                    isValid = false;
                }
            }
            else
                errorProvider1.SetError(this.groupImgPathTextBox, "");

            return isValid;
        }

        private bool ValidateForm()
        {
            bool isValid = false;

            bool isValidGroupId = ValidateGroupId();
            bool isValidGroupName = ValidateGroupName();
            bool isValidGroupImgPath = ValidateGroupImgPath();

            if (isValidGroupId && isValidGroupName
                && isValidGroupImgPath)
                isValid = true;

            return isValid;
        }
    }
}
