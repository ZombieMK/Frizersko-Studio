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
    public partial class selectPackage : Form
    {
        public CPackage package;

        public selectPackage()
        {
            InitializeComponent();
        }

        private void selectPackage_Load(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
            connection.Open();

            OleDbCommand getPackages = new OleDbCommand();
            getPackages.Connection = connection;
            getPackages.CommandText = "SELECT * FROM Packages";

            OleDbDataReader packageReader = getPackages.ExecuteReader();

            while (packageReader.Read())
            {
                string id = packageReader[0].ToString();
                string name = packageReader[1].ToString();
                string price = packageReader[2].ToString();
                
                OleDbCommand getPackageServices = new OleDbCommand();
                getPackageServices.Connection = connection;
                getPackageServices.CommandText = "SELECT Services.ID, Service_Name, Price FROM Services, Package_Services WHERE Package_Services.ID_Package = " + id + " AND Services.ID = Package_Services.ID_Service";

                OleDbDataReader packageServicesReader = getPackageServices.ExecuteReader();

                List<CService> services = new List<CService>();
                while (packageServicesReader.Read()) 
                {
                    string serviceId = packageServicesReader[0].ToString();
                    string serviceName = packageServicesReader[1].ToString();
                    string servicePrice = packageServicesReader[2].ToString();
                    CService tmpSerice = new CService(serviceId, serviceName, float.Parse(servicePrice));
                    services.Add(tmpSerice);
                }
              
                CPackage tmpPackage = new CPackage(id, name, float.Parse(price), services);
                packagesList.Items.Add(tmpPackage);
            }

            OleDbCommand getEmployees = new OleDbCommand();
            getEmployees.Connection = connection;
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

                CEmployee tmpEmployee = new CEmployee(id,first_name, last_name, address, phone, birthday, dayOfEmploymend,fired);
                if (!fired) 
                    employeeList.Items.Add(tmpEmployee);
            }

            connection.Close();

            if (packagesList.Items.Count == 0) 
            {
                doneBtn.Enabled = false;
                employeeList.Enabled = false;
            }
            else 
                packagesList.SelectedIndex = 0;
        }

        private void packagesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            package = packagesList.SelectedItem as CPackage;

            servicesList.Items.Clear();
            for (int i = 0; i < package.services.Count; i++)
            {
                package.services[i].employee = null;
                servicesList.Items.Add(package.services[i]);
            }

            servicesList.SelectedIndex = 0;
            employeeList.SelectedIndex = -1;
        }

        private void servicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CService tmpService = servicesList.SelectedItem as CService;
            if (tmpService.employee != null) 
                employeeList.SelectedItem = tmpService.employee;
            else 
                employeeList.SelectedIndex = -1;
        }

        private void employeeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (employeeList.SelectedIndex > -1)
                package.services[servicesList.SelectedIndex].employee = employeeList.SelectedItem as CEmployee;
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void doneBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < package.services.Count; i++)
            {
                if (package.services[i].employee == null) 
                {
                    MessageBox.Show("You must enter employees for every service!","Missing Employees", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
