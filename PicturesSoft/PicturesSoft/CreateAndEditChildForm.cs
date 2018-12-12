using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        public string InitImgPath { get; set; }
        public int InitChildCode { get; set; }
        public string SourceFullFileName { get; set; }
        public string PredeterminedDestImgFolderPath { get; set; }

        #endregion //public properties

        #region Form creation

        public CreateAndEditChildForm(WorkMode workMode, Group groupOwner, string DestImgFolderPath, 
            bool isThisTheFirstElementInSequence = false)
        {
            this.WorkMode = workMode;
            this.ChildToEditOrCreate = Child.CreateNewChild();
            GroupOwner = groupOwner;
            PredeterminedDestImgFolderPath = DestImgFolderPath;           

            if (WorkMode.WorkType.Equals(WorkModeType.Create))
            {
                this.Text = "Create new Child";
                InitializeComponent();
                this.childGroupCodeTextBox.Text = groupOwner.Id.ToString();
            }

            if (isThisTheFirstElementInSequence)
                this.addAfterSelectedRadioBtn.Enabled = false;
        }

        public CreateAndEditChildForm(WorkMode workMode, Child child, string DestImgFolderPath)
        {
            this.WorkMode = workMode;
            this.ChildToEditOrCreate = child;
            PredeterminedDestImgFolderPath = DestImgFolderPath;
            InitImgPath = PredeterminedDestImgFolderPath + "\\" + ChildToEditOrCreate.ImgName;
            SourceFullFileName = PredeterminedDestImgFolderPath + "\\" + ChildToEditOrCreate.ImgName;
            InitChildCode = ChildToEditOrCreate.Code;

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

                this.radioBtnsStoragePanel.Visible = false;
            }
        }

        #endregion //form creation

        private void opnFileDlgChildBtn_Click(object sender, EventArgs e)
        {
            string openFileDialogFilter =
               "Image files (*.png;*.jpeg;*.jpg;*.gif;*.bmp)|*.png;*.jpeg;*.jpg;*.gif;*.bmp";

            do
            {
                using (DialogInvoker dialogInvoker = new DialogInvoker(openFileDialogFilter))
                {
                    if (dialogInvoker.Invoke() == DialogResult.OK)
                    {
                        FileInfo fileInfo = new FileInfo(dialogInvoker.InvokeDialog.FileName);
                        long fileSize = fileInfo.Length;

                        if (fileSize < 1024000)
                        {
                            //System folder path of selected item (full source file name)
                            SourceFullFileName = dialogInvoker.InvokeDialog.FileName;
                            this.childImgPathTextBox.Text =
                                SourceFullFileName.Substring(SourceFullFileName.LastIndexOf("\\") + 1);
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Размер изображения больше 1 мб. Выберите другой файл или измените " +
                                "текущий файл, чтобы он соответствовал требованиям", "Внимание",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                        break;
                }                    
            }
            while (true);
        }

        private void CreateAndEditChildCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateAndEditChildSaveBtn_Click(object sender, EventArgs e)
        {
            bool isValidChildCode = ValidateChildCode();
            bool isValidChildName = ValidateChildName();
            bool isValidChildSimpleName = ValidateChildSimpleName();
            bool isValidChildImgPath = ValidateChildImgPath();

            bool isImgNeeded = true;

            if (isValidChildCode && isValidChildName && isValidChildSimpleName && !isValidChildImgPath)
            {
                DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите сохранить позицию без картинки?",
                    "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.No || dialogResult == DialogResult.None)
                {
                    return;
                }
                else
                {
                    isImgNeeded = false;
                }
            }
            else if (!isValidChildCode || !isValidChildName || isValidChildSimpleName || !isValidChildImgPath)
            {
                return;
            }

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
                    if (this.addToTheEndRadioBtn.Checked)
                        ((Form1)this.Owner).AddNewChild(ChildToEditOrCreate);
                    else
                        ((Form1)this.Owner).AddNewChild(ChildToEditOrCreate, false);
                }
            }
            else if (WorkMode.WorkType.Equals(WorkModeType.Edit))
            {
                if (this.Owner != null)
                {
                    ((Form1)this.Owner).UpdateChild(ChildToEditOrCreate);
                }
            }

            if (!isImgNeeded)
            {
                this.Close();
                return;
            }

            //Get image extension
            string imgExtension = SourceFullFileName.Substring(SourceFullFileName.LastIndexOf('.'));

            // File name for saving in .xml file
            string fileName;

            fileName = this.childCodeTextBox.Text + imgExtension;

            if (!imgExtension.Equals(".png"))
                fileName = this.childCodeTextBox.Text + ".png";

            // Full destination path for file(full destination file name)
            string destFolderName;

            if (((Form1)this.Owner).AppWorkMode.WorkType == WorkModeType.LoadFromFinalXml)
            {
                destFolderName = PredeterminedDestImgFolderPath +
                    "\\" + fileName;

                string oldFileName =
                        InitImgPath.Substring(InitImgPath.LastIndexOf("\\") + 1);

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
                            catch(Exception excep)
                            {
                                Logger.Error("Не удалось сохранить картинку в .png формате!", excep);
                                MessageBox.Show("Не удалось сохранить картинку в .png формате!", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        File.Copy(SourceFullFileName, destFolderName);                     
                    }

                    if (WorkMode.WorkType.Equals(WorkModeType.Edit))
                    {
                        if (!SourceFullFileName.Contains(PredeterminedDestImgFolderPath)
                            && !oldFileName.Equals(fileName))
                            File.Delete(InitImgPath);
                    }        
                }     
            }
            else //oldMode
            {
                destFolderName = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), @"Images\") +
                    this.childCodeTextBox.Text +
                    imgExtension;
                File.Copy(SourceFullFileName, destFolderName, true);
            }                       

            //remaking final xml file
            //((Form1)this.Owner).CreateFinalXmlFile();

            this.Close();
        }

        private void childCodeTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateChildCode();
        }

        private void childNameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateChildName();
        }

        private void childSimpleNameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateChildSimpleName();
        }

        private void childImgPathTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ValidateChildImgPath();
        }

        private bool ValidateChildCode()
        {
            bool isValid = true;

            string childCodeToValidate = this.childCodeTextBox.Text.Trim();

            this.childCodeTextBox.Text = childCodeToValidate;

            if (childCodeToValidate.Equals(""))
            {
                errorProvider1.SetError(this.childCodeTextBox, "Please enter child code!");
                isValid = false;
            }
            else if (!int.TryParse(childCodeToValidate, out int result))
            {
                errorProvider1.SetError(this.childCodeTextBox, "Child code is incorrect. " +
                    "Please enter valid child code!");
                isValid = false;
            }
            else if(WorkMode.WorkType.Equals(WorkModeType.Create) && 
                !((Form1)this.Owner).ChildRep
                .ValidateChildCode(int.Parse(childCodeToValidate)))
            {
                errorProvider1.SetError(this.childCodeTextBox, "Child code is already exist. " +
                    "Please enter another child code!");
                isValid = false;
            }
            else if (WorkMode.WorkType.Equals(WorkModeType.Edit) &&
                InitChildCode != int.Parse(childCodeToValidate) &&
                !((Form1)this.Owner).ChildRep
                .ValidateChildCode(int.Parse(childCodeToValidate)))
            {
                errorProvider1.SetError(this.childCodeTextBox, "Child code is already exist. " +
                    "Please enter another child code!");
                isValid = false;
            }

            if (isValid)
                errorProvider1.SetError(this.childCodeTextBox, "");

            return isValid;
        }

        private bool ValidateChildName()
        {
            bool isValid = true;

            string childNameToValidate = this.childNameTextBox.Text.Trim();

            this.childNameTextBox.Text = childNameToValidate;

            if (childNameToValidate.Equals(""))
            {
                errorProvider1.SetError(this.childNameTextBox, "Please enter child name!");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(this.childNameTextBox, "");
            }

            return isValid;
        }

        private bool ValidateChildSimpleName()
        {
            bool isValid = true;

            string childSimpleNameToValidate = this.childSimpleNameTextBox.Text.Trim();

            this.childSimpleNameTextBox.Text = childSimpleNameToValidate;

            if (childSimpleNameToValidate.Equals(""))
            {
                errorProvider1.SetError(this.childSimpleNameTextBox, "Please enter child simple name!");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(this.childSimpleNameTextBox, "");
            }

            return isValid;
        }

        private bool ValidateChildImgPath()
        {
            bool isValid = true;

            string childImgPathToValidate = this.childImgPathTextBox.Text.Trim();

            this.childImgPathTextBox.Text = childImgPathToValidate;

            if (childImgPathToValidate.Equals(""))
            {
                errorProvider1.SetError(this.childImgPathTextBox,
                    "Please click browse button and choose image for child!");
                isValid = false;
            }
            else if(!childImgPathToValidate.Equals(""))
            {
                if(File.Exists(SourceFullFileName) == false)
                {
                    errorProvider1.SetError(this.childImgPathTextBox,
                   "Image file doesn't exist!");
                    isValid = false;
                }
            }
            else
                errorProvider1.SetError(this.childImgPathTextBox, "");

            return isValid;
        }
    }
}
