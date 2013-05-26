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
    public partial class employeeForm : Form
    {
        public CEmployee employee { set; get; }
        public bool edit;

        public employeeForm()
        {
            InitializeComponent();
        }

        private void employeeForm_Load(object sender, EventArgs e)
        {
            if (edit) 
            {
                nameTxt.Text = employee.name;
                lastNameTxt.Text = employee.lastName;
                addressTxt.Text = employee.address;
                phoneTxt.Text = employee.phone;
                birthdayPicker.Value = employee.birthday;
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

            if (birthdayPicker.Value.AddYears(18) > DateTime.Today)
            {
                MessageBox.Show("Your employee must be atleast 18 years old!", "Invalid Birthday", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Connection connection = new Connection();

            connection.openConnection();

            OleDbCommand employeeCommand = new OleDbCommand();
            employeeCommand.Connection = connection.connection;

            string name = nameTxt.Text;
            string lastName = lastNameTxt.Text;
            string address = addressTxt.Text;
            string phone = phoneTxt.Text;
            string birthday = birthdayPicker.Value.ToShortDateString();
            string employment = DateTime.Now.ToString();

            if (edit)
            {
                employeeCommand.CommandText = "UPDATE Employees SET First_Name = '" + name + "', Last_Name = '" + lastName + "', Address = '" + address + "', Phone = '" + phone + "', Birthday = #" + birthday + "# WHERE ID = " + employee.id;
                employeeCommand.ExecuteNonQuery();

                employee.name = name;
                employee.lastName = lastName;
                employee.address = address;
                employee.phone = phone;
                employee.birthday = birthdayPicker.Value.Date;
            }
            else 
            {
                employeeCommand.CommandText = "INSERT INTO Employees (First_Name,Last_Name,Address,Phone,Birthday,Employmend,Fired) VALUES ('" + name + "', '" + lastName + "', '" + address + "', '" + phone + "', #" + birthday + "#, #" + employment + "#, false)";
                employeeCommand.ExecuteNonQuery();

                employeeCommand.CommandText = "Select @@Identity";
                string idEmployee = employeeCommand.ExecuteScalar().ToString();

                employee = new CEmployee(idEmployee, name, lastName, address, phone, birthdayPicker.Value.Date, DateTime.Now, false);
            }

            connection.closeConnection();

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
