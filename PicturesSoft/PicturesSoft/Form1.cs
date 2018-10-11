using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesSoft
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GroupRepository rep = new GroupRepository("PicturesSoft.Data.Groups.xml");

            this.listBox1.Items.AddRange(rep.GetGroups().ToArray<Group>());
            
            foreach(Group gr in rep.GetGroups())
            {
                this.tableLayoutPanel1.Controls.Add(
                    new Button()
                    {
                        Name = gr.Name, Text = gr.Name, Anchor =
                    ((System.Windows.Forms.AnchorStyles)
                    ((((System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right))),
                        //BackgroundImage = Image.FromFile(
                        //@"D:\Work\GitHub_repos\Software_for_pictures\PicturesSoft\PicturesSoft\Images\coin.jpg"),
                        BackgroundImage = Image.FromFile(
                            Path.Combine(
                             Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                             "Images\\coin.jpg"))
                        
                    });


                ListViewItem lvi = new ListViewItem();
                lvi.Text = gr.Name;

                this.listView1.Items.Add(lvi);
            }
        }
    }
}
