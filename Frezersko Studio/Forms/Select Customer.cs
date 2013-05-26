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
    public partial class selectCustomer : Form
    {
        public CCustomer customer;

        public selectCustomer()
        {
            InitializeComponent();
        }

        private void selectCustomer_Load(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
            connection.Open();

            OleDbCommand getCustomers = new OleDbCommand();
            getCustomers.Connection = connection;
            getCustomers.CommandText = "SELECT * FROM Customers";

            OleDbDataReader customersReader = getCustomers.ExecuteReader();

            while (customersReader.Read())
            {
                string id = customersReader[0].ToString();
                string first_name = customersReader[1].ToString();
                string last_name = customersReader[2].ToString();
                string address = customersReader[3].ToString();
                string phone = customersReader[4].ToString();
                DateTime birthday = (DateTime)customersReader[5];

                CCustomer tmpCustomer = new CCustomer(id,first_name,last_name,address,phone,birthday);
                customersList.Items.Add(tmpCustomer);
            }
            connection.Close();

            if (customersList.Items.Count == 0) 
            {
                doneBtn.Enabled = false;
            }
            else 
                customersList.SelectedIndex = 0;
        }

        private void customersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            customer = customersList.SelectedItem as CCustomer;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
