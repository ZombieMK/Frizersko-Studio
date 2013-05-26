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
    public partial class serviceForm : Form
    {
        public CService service;
        public bool edit;

        public serviceForm()
        {
            InitializeComponent();
        }

        private void serviceForm_Load(object sender, EventArgs e)
        {
            if (edit)
            {
                nameTxt.Text = service.name;
                priceTxt.Text = service.price.ToString();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text.Length < 1 || priceTxt.Text.Length < 1)
            {
                MessageBox.Show("All fields must be entered!", "Invalid Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";

            connection.Open();

            OleDbCommand serviceCommand = new OleDbCommand();
            serviceCommand.Connection = connection;

            if (edit)
            {
                serviceCommand.CommandText = "UPDATE Services SET Service_Name = '" + nameTxt.Text + "', Price = " + priceTxt.Text + " WHERE ID = " + service.id;
                serviceCommand.ExecuteNonQuery();

                service.name = nameTxt.Text;
                service.price = float.Parse(priceTxt.Text);
            }
            else
            {
                serviceCommand.CommandText = "INSERT INTO Services (Service_Name, Price) VALUES ('" + nameTxt.Text + "', " + priceTxt.Text + ")";
                serviceCommand.ExecuteNonQuery();

                serviceCommand.CommandText = "Select @@Identity";
                string serviceID = serviceCommand.ExecuteScalar().ToString();

                service = new CService(serviceID, nameTxt.Text, float.Parse(priceTxt.Text));
            }

            connection.Close();

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void priceTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
