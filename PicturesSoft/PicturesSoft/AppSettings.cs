using System;
using System.IO;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class AppSettings : Form
    {
        public AppSettings()
        {
            InitializeComponent();
            this.startBtn.Enabled = false;
        }

        private void openFileDlgXmlCnfgFileButton_Click(object sender, EventArgs e)
        {
            Stream fileStream = null;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xml files (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK && (fileStream = openFileDialog.OpenFile()) != null)
            {
                this.xmlCnfgFilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void openFolderBrowserDlgBtn_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.destImgFolderTextBox.Text = fbd.SelectedPath;
                }
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            ((Form1)this.Owner).destImgFolderPath = this.destImgFolderTextBox.Text;
            ((Form1)this.Owner).xmlCnfgFilePath = this.xmlCnfgFilePathTextBox.Text;

            this.Close();
        }

        private void xmlCnfgFilePathTextBox_TextChanged(object sender, EventArgs e)
        {
            if(this.xmlCnfgFilePathTextBox.Text.Length != 0 && 
                this.destImgFolderTextBox.Text.Length != 0)
            {
                this.startBtn.Enabled = true;
            }
        }

        private void destImgFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.xmlCnfgFilePathTextBox.Text.Length != 0 &&
                this.destImgFolderTextBox.Text.Length != 0)
            {
                this.startBtn.Enabled = true;
            }
        }
    }
}
