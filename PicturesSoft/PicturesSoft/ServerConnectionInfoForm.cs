using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class ServerConnectionInfoForm : Form
    {
        public ServerConnectionInfoForm()
        {
            InitializeComponent();
        }

        private void connToServerAndDownloadBtn_Click(object sender, EventArgs e)
        {
            var ip = ipTextBox.Text;
            var port = Int32.Parse(portTextBox.Text);
            var login = loginTextBox.Text;
            var passwd = passwdTextBox.Text;

            try
            {
                using (var client = new Renci.SshNet.ScpClient(ip, port, login, passwd))
                {
                    client.Connect();
                    client.Download("/var/lib/jboss/acm/topology.structure",
                        new FileInfo(
                            Path.Combine(Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location), @"Data\topology.structure"))
                            );
                    client.Disconnect();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.Close();
        }
    }
}
