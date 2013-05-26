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
    public partial class customerForm : Form
    {
        public CCustomer customer { set; get; }
        public bool edit { set; get; }

        public customerForm()
        {
            InitializeComponent();
        }

        private void customerForm_Load(object sender, EventArgs e)
        {
            if (edit)
            {
                nameTxt.Text = customer.name;
                lastNameTxt.Text = customer.lastName;
                addressTxt.Text = customer.address;
                phoneTxt.Text = customer.phone;
                birthdayPicker.Value = customer.birthday;
            }
        }

        private void phoneTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (nameTxt.Text.Length < 1 || lastNameTxt.Text.Length < 1 || addressTxt.Text.Length < 1 || phoneTxt.Text.Length < 1)
            {
                MessageBox.Show("All fields must be entered!", "Invalid Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";

            connection.Open();

            OleDbCommand customerCommand = new OleDbCommand();
            customerCommand.Connection = connection;

            string name = nameTxt.Text;
            string lastName = lastNameTxt.Text;
            string address = addressTxt.Text;
            string phone = phoneTxt.Text;
            string birthday = birthdayPicker.Value.ToShortDateString();

            if (edit)
            {
                customerCommand.CommandText = "UPDATE Customers SET First_Name = '" + name + "', Last_Name = '" + lastName + "', Address = '" + address + "', Phone = '" + phone + "', Birthday = #" + birthday + "# WHERE ID = " + customer.id;
                customerCommand.ExecuteNonQuery();

                customer.name = name;
                customer.lastName = lastName;
                customer.address = address;
                customer.phone = phone;
                customer.birthday = birthdayPicker.Value.Date;
            }
            else
            {
                customerCommand.CommandText = "INSERT INTO Customers (First_Name,Last_Name,Address,Phone,Birthday) VALUES ('" + name + "', '" + lastName + "', '" + address + "', '" + phone + "', #" + birthday + "#)";
                customerCommand.ExecuteNonQuery();

                customerCommand.CommandText = "Select @@Identity";
                string idCustomer = customerCommand.ExecuteScalar().ToString();

                customer = new CCustomer(idCustomer, name, lastName, address, phone, birthdayPicker.Value.Date);
            }

            connection.Close();

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
