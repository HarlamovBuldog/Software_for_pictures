using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class CreateAndEditGroupForm : Form
    {
        private WorkMode _workMode;
        private Group _groupToEdit;
        private Group _groupToCreate;

        public CreateAndEditGroupForm()
        {
            this._workMode = WorkMode.Create;
            this.Text = "Create new Group";
            this._groupToCreate = Group.CreateNewGroup();

            InitializeComponent();  
        }

        public CreateAndEditGroupForm(Group groupToEdit)
        {
            this._workMode = WorkMode.Edit;
            this.Text = "Editing " + groupToEdit.Name;

            //< filling TextBoxes with values
            this.groupNameTextBox.Text = groupToEdit.Name;
            this.groupIdTextBox.Text = groupToEdit.Id.ToString();
            this.groupImgPathTextBox.Text = groupToEdit.ImgPath;
            //>

            this._groupToEdit = groupToEdit;

            InitializeComponent();
        }
    }
}
