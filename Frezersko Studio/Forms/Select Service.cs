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
    public partial class selectService : Form
    {
        public CService service { get; set; }

        public selectService()
        {
            InitializeComponent();
        }

        private void selectService_Load(object sender, EventArgs e)
        {
            Connection connection = new Connection();

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
                CService tmpSerice = new CService(id, name, float.Parse(price));
                servicesList.Items.Add(tmpSerice);
            }

            OleDbCommand getEmployees = new OleDbCommand();
            getEmployees.Connection = connection.connection;
            getEmployees.CommandText = "SELECT * FROM Employees";

            OleDbDataReader employeesReader = getEmployees.ExecuteReader();

            while (employeesReader.Read())
            {
                string id = employeesReader[0].ToString();
                string first_name = employeesReader[1].ToString();
                string last_name = employeesReader[2].ToString();
                string address = employeesReader[3].ToString();
                string phone = employeesReader[4].ToString();
                DateTime birthday = (DateTime)employeesReader[5];
                DateTime dayOfEmploymend = (DateTime)employeesReader[6];
                bool fired = (bool)employeesReader[7];

                CEmployee tmpEmployee = new CEmployee(id, first_name, last_name, address, phone, birthday, dayOfEmploymend,fired);
                if (!fired) 
                    employeesList.Items.Add(tmpEmployee);
            }

            connection.closeConnection();

            if (servicesList.Items.Count == 0) 
            {
                doneBtn.Enabled = false;
                employeesList.Enabled = false;
            }
            else 
                servicesList.SelectedIndex = 0;
        }

        private void servicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (service != null)
                service.employee = null;
            service = servicesList.SelectedItem as CService;

            employeesList.SelectedIndex = -1;
        }

        private void employeeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeesList.SelectedIndex > -1)
                service.employee = employeesList.SelectedItem as CEmployee;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            if (service.employee == null) 
            {
                MessageBox.Show("You must enter employee for the service!", "Missing Employee", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
