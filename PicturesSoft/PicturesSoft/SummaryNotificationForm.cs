using GroupedListControl;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class SummaryNotificationForm : Form
    {
        public SummaryNotificationForm(GroupListControl groupListControl, WorkMode workMode)
        {
            InitializeComponent();

            switch(workMode.WorkType)
            {
                case WorkModeType.DownloadFromCashBoxAndShowNotificationTable:
                    this.Text = "Подтверждение загрузки информации с кассы";
                    this.descriptionTextBox.Text = "Подтвердите загрузку информации, указанной выше";
                    break;
                case WorkModeType.DownloadFromCashBoxAndShowCorrespondingResults:
                    this.Text = "Результаты загрузки информации с кассы";
                    this.descriptionTextBox.Text = "Выше показаны результаты загрузки информации с кассы." +
                        "Если возникли ошибки, то просмотрите логи для более детального разбора" +
                        " либо свяжитесь с системным администратором.";
                    this.cancelButton.Visible = false;
                    break;                
                case WorkModeType.UploadToCashBoxAndShowNotificationTable:
                    this.Text = "Подтверждение загрузки информации на кассу";
                    this.descriptionTextBox.Text = "Подтвердите загрузку информации, указанной выше";
                    break;
                case WorkModeType.UploadToCashBoxAndShowCorrespondingResults:
                    this.Text = "Результаты загрузки информации на кассу";
                    this.descriptionTextBox.Text = "Выше показаны результаты загрузки информации на кассы." +
                        "Если возникли ошибки, то просмотрите логи для более детального разбора" +
                        " либо свяжитесь с системным администратором.";
                    this.cancelButton.Visible = false;
                    break;
            }

            int counter = groupListControl.Controls.Count;

            for (int i = 0; i < counter; i++)
            {
                this.groupListControl1.Controls.Add(groupListControl.Controls[0]);
            }

            groupListControl1.Update();
            groupListControl1.Invalidate(true);
        }

        private void confirmButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
