using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PicturesSoft
{
    public partial class DBConnSettingsForm : Form
    {
        public DBConnSettingsForm()
        {
            InitializeComponent();

            //< Data init for testing comfort
            this.serverIDTextBox.Text = "192.168.1.222";
            this.portIDTextBox.Text = "5432";
            this.userNameTextBox.Text = "postgres";
            this.passwdTextBox.Text = "postgres";
            this.dataBaseNameTextBox.Text = "set";
            //>
        }

        private void ApplyDbSettingsBtn_Click(object sender, EventArgs e)
        {          
            string serverId = this.serverIDTextBox.Text;
            string port = this.portIDTextBox.Text;
            string userName = this.userNameTextBox.Text;
            string passwd = this.passwdTextBox.Text;
            string dataBaseName = this.dataBaseNameTextBox.Text;

            //validation here

            if(MakeLocalCatalog(serverId, port, userName, passwd, dataBaseName))
            {
                MessageBox.Show("Локальный каталог был успешно создан. Программа готова к работе!",
                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        public bool MakeLocalCatalog(string serverId, string port,
            string user, string passwd, string dataBaseName)
        {
            List<Shop> shops = new List<Shop>();
            List<CashBox> cashBoxes = new List<CashBox>();
            List<TopologyTemp> topologyTemps;

            string filepath = Path.Combine(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location), @"Data\topology.structure");

            if(!File.Exists(filepath))
            {
                MessageBox.Show("Нет файла справочника! Перейдите в настройки сервера" +
                    " подключения и загрузите его!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                topologyTemps = new List<TopologyTemp>(JsonConvert.DeserializeObject<List<TopologyTemp>>(json));
            }

            int indexOfScndComa;
            int indexofThirdComa;
            string currentShopCode = String.Empty;

            List<string> shopCodesCheck = new List<string>();

            foreach (var tplg in topologyTemps)
            {
                if (tplg.type.Equals("CENTRUM"))
                    continue;

                var result = tplg.topologyAddress
                        .Select((ch, index) => new { ch, index })
                        .Where(x => x.ch == '.')
                        .Skip(1)
                        .FirstOrDefault();

                indexOfScndComa = result.index;

                indexofThirdComa = tplg.topologyAddress.LastIndexOf('.');

                currentShopCode = tplg.topologyAddress
                    .Substring(indexOfScndComa + 1, indexofThirdComa - indexOfScndComa - 1);

                if (shopCodesCheck.Count != 0)
                {
                    if (!shopCodesCheck.Contains(currentShopCode))
                    {
                        shopCodesCheck.Add(currentShopCode);
                        shops.Add(new Shop()
                        {
                            Code = String.Copy(currentShopCode)
                        });
                    }
                }
                else
                {
                    shopCodesCheck.Add(currentShopCode);
                    shops.Add(new Shop()
                    {
                        Code = String.Copy(currentShopCode)
                    });
                }

                cashBoxes.Add(new CashBox()
                {
                    IpAddress = tplg.topologyPointIP,
                    Number = tplg.topologyAddress.Substring(indexofThirdComa + 1),
                    ShopCode = String.Copy(currentShopCode)
                });
            }

            foreach (Shop shop in shops)
            {
                foreach (CashBox cashBox in cashBoxes)
                {
                    if (shop.Code.Equals(cashBox.ShopCode))
                    {
                        shop.CashBoxes.Add(new CashBox()
                        {
                            IpAddress = String.Copy(cashBox.IpAddress),
                            Number = String.Copy(cashBox.Number),
                            ShopCode = String.Copy(cashBox.ShopCode)
                        });
                    }
                }
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                    "User Id={2};Password={3};Database={4};",
                    serverId, port, user, passwd, dataBaseName);
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT number, name, address FROM topology_shop";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show(msg.ToString() + "\nВозникли проблемы с подключением к базе данных." +
                    "Обратитесь к администратору либо проверьте введенные данные",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DataRow[] foundRows;

            foreach (Shop shop in shops)
            {
                foundRows = dt.Select("number = " + shop.Code);

                shop.Name = foundRows[0]["name"].ToString();
                shop.Address = foundRows[0]["address"].ToString();
            }

            if (!CreateLocalCatalogXmlFile(shops))
                return false;

            return true;
        }

        private bool CreateLocalCatalogXmlFile(List<Shop> shops)
        {
            XDocument xDoc = new XDocument();

            //create shopListXElement
            XElement shopListXElement = new XElement("shops");

            XElement shopXElement;

            foreach (Shop shop in shops)
            {
                shopXElement = new XElement("shop",
                    new XAttribute("number", shop.Code),
                    new XAttribute("name", shop.Name),
                    new XAttribute("address", shop.Address)
                    );

                foreach (CashBox cashBox in shop.CashBoxes)
                {
                    shopXElement.Add(new XElement("cashBox",
                        new XAttribute("ipAddress", cashBox.IpAddress),
                        new XAttribute("number", cashBox.Number)
                        ));
                }

                shopListXElement.Add(shopXElement);
            }

            //making shopListXElement to be root element
            xDoc.Add(shopListXElement);

            try
            {
                xDoc.Save("Data/CashBoxesCatalog.xml");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "Не удалось сохранить файл!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
