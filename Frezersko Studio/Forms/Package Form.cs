using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Frezersko_Studio.Classes;

namespace Frezersko_Studio.Forms
{
    public partial class packageForm : Form
    {
        public bool edit;
        public CPackage package;
        public Connection connection;

        public packageForm()
        {
            InitializeComponent();
            connection = new Connection();
        }

        private void packageForm_Load(object sender, EventArgs e)
        {
            connection.openConnection();

            OleDbCommand getServices = new OleDbCommand();
            getServices.Connection = connection.connection;
            getServices.CommandText = "SELECT * FROM Services";

            OleDbDataReader servicesReader = getServices.ExecuteReader();

            while (servicesReader.Read())
            {
                string id = servicesReader[0].ToString();
                string name = servicesReader[1].ToString();
                string price = servicesReader[2].ToString();
                CService tmpService = new CService(id, name, float.Parse(price));
                servicesList.Items.Add(tmpService);
            }

            connection.closeConnection();

            if (edit)
            {
                nameTxt.Text = package.name;
                priceTxt.Text = package.price.ToString();

                for (int i = 0; i < package.services.Count; i++)
                {
                    addedSerivesList.Items.Add(package.services[i]);

                    for (int j = 0; j < servicesList.Items.Count; j++)
                    {
                        CService tmpService = servicesList.Items[j] as CService;
                        if (package.services[i].id.Equals(tmpService.id))
                            servicesList.Items.RemoveAt(j);
                    }
                }

                if (servicesList.Items.Count == 0) addToPackageBtn.Enabled = false;
                else servicesList.SelectedIndex = 0;
                removeFromPackageBtn.Enabled = true;
            }
            else
            {
                removeFromPackageBtn.Enabled = false;
                servicesList.SelectedIndex = 0;
            }
        }

        private void addToPackageBtn_Click(object sender, EventArgs e)
        {
            CService tmpService = servicesList.SelectedItem as CService;
            addedSerivesList.Items.Add(tmpService);
            servicesList.Items.Remove(tmpService);

            removeFromPackageBtn.Enabled = true;
            if (servicesList.Items.Count == 0) addToPackageBtn.Enabled = false;
        }

        private void removeFromPackageBtn_Click(object sender, EventArgs e)
        {
            CService tmpService = addedSerivesList.SelectedItem as CService;
            servicesList.Items.Add(tmpService);
            addedSerivesList.Items.Remove(tmpService);
            
            addToPackageBtn.Enabled = true;
            if (addedSerivesList.Items.Count == 0) removeFromPackageBtn.Enabled = false;
        }

        private void priceTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text.Length < 1 || priceTxt.Text.Length < 1 || addedSerivesList.Items.Count == 0)
            {
                MessageBox.Show("All fields must be entered!", "Invalid Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<CService> services = new List<CService>();
            for (int i = 0; i < addedSerivesList.Items.Count; i++) 
            {
                CService tmpService = addedSerivesList.Items[i] as CService;
                services.Add(tmpService);
            }

            connection.openConnection();

            OleDbCommand packageCommand = new OleDbCommand();
            packageCommand.Connection = connection.connection;

            if (edit)
            {
                packageCommand.CommandText = "UPDATE Packages SET Package_Name = '" + nameTxt.Text + "', Price = " + priceTxt.Text + " WHERE ID = " + package.id;
                packageCommand.ExecuteNonQuery();

                packageCommand.CommandText = "DELETE FROM Package_Services WHERE ID_Package = " + package.id;
                packageCommand.ExecuteNonQuery();

                for (int i = 0; i < services.Count; i++)
                {
                    OleDbCommand insertPackageService = new OleDbCommand();
                    insertPackageService.Connection = connection.connection;
                    insertPackageService.CommandText = "INSERT INTO Package_Services (ID_Service, ID_Package) VALUES (" + services[i].id + ", " + package.id + ")";

                    insertPackageService.ExecuteNonQuery();
                }

                package.name = nameTxt.Text;
                package.price = float.Parse(priceTxt.Text);
                package.services = services;
            }
            else
            {
                packageCommand.CommandText = "INSERT INTO Packages (Package_Name, Price) VALUES ('" + nameTxt.Text + "', " + priceTxt.Text + ")";
                packageCommand.ExecuteNonQuery();

                packageCommand.CommandText = "Select @@Identity";
                string packageID = packageCommand.ExecuteScalar().ToString();

                for (int i = 0; i < services.Count; i++) 
                {
                    OleDbCommand insertPackageService = new OleDbCommand();
                    insertPackageService.Connection = connection.connection;
                    insertPackageService.CommandText = "INSERT INTO Package_Services (ID_Service, ID_Package) VALUES (" + services[i].id + ", " + packageID + ")";
                    
                    insertPackageService.ExecuteNonQuery();
                }

                    package = new CPackage(packageID, nameTxt.Text, float.Parse(priceTxt.Text), services);
            }

            connection.closeConnection();

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
