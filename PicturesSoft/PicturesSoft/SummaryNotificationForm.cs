using GroupedListControl;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class SummaryNotificationForm : Form
    {
        public SummaryNotificationForm(GroupListControl groupListControl, WorkMode workMode,
            bool noActionsNeeded = false)
        {
            InitializeComponent();

            switch(workMode.WorkType)
            {
                case WorkModeType.DownloadFromCashBoxAndShowNotificationTable:
                    this.Text = "Загрузка информации с кассы";                    
                    if(noActionsNeeded == false)
                    {
                        this.descriptionTextBox.Text = "Подтвердите загрузку информации, указанной выше";
                    }
                    else
                    {
                        this.descriptionTextBox.Text = "Согласно полученным данным никаких действий проводить не требуется.";
                        this.cancelButton.Visible = false;
                    }
                    break;
                case WorkModeType.DownloadFromCashBoxAndShowCorrespondingResults:
                    this.Text = "Результаты загрузки информации с кассы";
                    this.descriptionTextBox.Text = "Выше показаны результаты загрузки информации с кассы.\n" +
                        "Если возникли ошибки, то просмотрите логи для более детального разбора\n" +
                        " либо свяжитесь с системным администратором.";
                    this.cancelButton.Visible = false;
                    break;                
                case WorkModeType.UploadToCashBoxAndShowNotificationTable:
                    this.Text = "Отправка информации на кассу";
                    if (noActionsNeeded == false)
                    {
                        this.descriptionTextBox.Text = "Подтвердите загрузку информации, указанной выше";
                    }
                    else
                    {
                        this.descriptionTextBox.Text = "Согласно полученным данным никаких действий проводить не требуется.";
                        this.cancelButton.Visible = false;
                    }                    
                    break;
                case WorkModeType.UploadToCashBoxAndShowCorrespondingResults:
                    this.Text = "Результаты отправки информации на кассу";
                    this.descriptionTextBox.Text = "Выше показаны результаты загрузки информации на кассы.\n" +
                        "Если возникли ошибки, то просмотрите логи для более детального разбора\n" +
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
