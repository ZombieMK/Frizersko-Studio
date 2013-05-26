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
using Frezersko_Studio.Forms;
using Frezersko_Studio.Classes;

namespace Frezersko_Studio
{
    public partial class mainForm : Form
    {
        float nb_TotalPrice, ab_TotalPrice;
        int ab_TotalBills;
        CCustomer nb_Customer;
        Connection connection;
        List<CEmployee> e_employees;

        public mainForm()
        {
            InitializeComponent();

            connection = new Connection();

            // Defualt Values
            nb_TotalPrice = 0.0f;
            ab_TotalPrice = 0.0f;
            ab_TotalBills = 0;
            nb_TotalPriceTxt.Text = "0";
            nb_DeleteService.Enabled = false;
            nb_DateTimePicker.Value = DateTime.Now;
            nb_CompliteBllBtn.Enabled = false;

            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

            e_ToDP.Value = lastDay;
            e_FromDP.Value = firstDay;
            ab_To.Value = lastDay;
            ab_From.Value = firstDay;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            connection.openConnection();
            // Screen 2

            OleDbCommand getCustomers = new OleDbCommand();
            getCustomers.Connection = connection.connection;

            getCustomers.CommandText = "SELECT * FROM Customers";
            OleDbDataReader customerReader = getCustomers.ExecuteReader();

            while (customerReader.Read()) 
            {
                string id = customerReader[0].ToString();
                string first_Name = customerReader[1].ToString();
                string last_Name = customerReader[2].ToString();
                string address = customerReader[3].ToString();
                string phone = customerReader[4].ToString();
                DateTime birthday = (DateTime)customerReader[5];

                CCustomer tmpCustomer = new CCustomer(id, first_Name, last_Name, address, phone, birthday);
                c_CustomersList.Items.Add(tmpCustomer);
            }
            
            // Screen 3

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
                s_ServicesList.Items.Add(tmpService);

            }

            OleDbCommand getPackages = new OleDbCommand();
            getPackages.Connection = connection.connection;
            getPackages.CommandText = "SELECT * FROM Packages";

            OleDbDataReader packageReader = getPackages.ExecuteReader();

            while (packageReader.Read())
            {
                string id = packageReader[0].ToString();
                string name = packageReader[1].ToString();
                string price = packageReader[2].ToString();

                OleDbCommand getPackageServices = new OleDbCommand();
                getPackageServices.Connection = connection.connection;
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
                s_PackagesList.Items.Add(tmpPackage);
            }

            // Screen 4

            OleDbCommand getEmployees = new OleDbCommand();
            getEmployees.Connection = connection.connection;

            getEmployees.CommandText = "SELECT * FROM Employees";
            OleDbDataReader employeesReader = getEmployees.ExecuteReader();

            e_employees = new List<CEmployee>();
            while (employeesReader.Read())
            {
                string id = employeesReader[0].ToString();
                string first_Name = employeesReader[1].ToString();
                string last_Name = employeesReader[2].ToString();
                string address = employeesReader[3].ToString();
                string phone = employeesReader[4].ToString();
                DateTime birthday = (DateTime)employeesReader[5];
                DateTime employmend = (DateTime)employeesReader[6];
                bool fired = (bool)employeesReader[7];

                CEmployee tmpEmployee = new CEmployee(id, first_Name, last_Name, address, phone, birthday, employmend,fired);
                e_employees.Add(tmpEmployee);
            }

            connection.closeConnection();

            if (c_CustomersList.Items.Count > 0)
                c_CustomersList.SelectedIndex = 0;
            else
            {
                c_EditBtn.Enabled = false;
                c_DeleteBtn.Enabled = false;
                c_LoadBtn.Enabled = false;
                c_ViewBilBtn.Enabled = false;
            }

            if (s_ServicesList.Items.Count > 0)
                s_ServicesList.SelectedIndex = 0;
            else 
            {
                s_ServiceDelete.Enabled = false;
                s_ServiceEdit.Enabled = false;
                s_PackageAdd.Enabled = false;
            }
            if (s_PackagesList.Items.Count > 0)
                s_PackagesList.SelectedIndex = 0;
            else 
            {
                s_PackageDelete.Enabled = false;
                s_PackageEdit.Enabled = false;
            }

            fillE_EmployeesList();
        }

        private void fillE_EmployeesList() 
        {
            e_EmployeesList.Items.Clear();
            if (e_FiredECB.Checked)
            {
                for (int i = 0; i < e_employees.Count; i++)
                {
                    if (!e_employees[i].fired)
                        e_EmployeesList.Items.Add(e_employees[i]);
                }
                for (int i = 0; i < e_employees.Count; i++)
                {
                    if (e_employees[i].fired)
                        e_EmployeesList.Items.Add(e_employees[i]);
                }
            }
            else
            {
                for (int i = 0; i < e_employees.Count; i++)
                {
                    if (!e_employees[i].fired)
                        e_EmployeesList.Items.Add(e_employees[i]);

                    if (e_employees.Count == e_EmployeesList.Items.Count)
                        e_FiredECB.Visible = false;
                    else
                        e_FiredECB.Visible = true;
                }
            }

            e_FiredECB.Visible = false;
            for (int i = 0; i < e_employees.Count; i++)
            {
                if (e_employees[i].fired) 
                {
                    e_FiredECB.Visible = true;
                    break;
                }
            }
                
            if (e_employees.Count == 0)
            {
                e_FireBtn.Visible = false;
                e_DeleteBtn.Enabled = false;
                e_EditBtn.Enabled = false;

                e_FromDP.Enabled = false;
                e_ToDP.Enabled = false;

                e_EmployeesList.SelectedIndex = -1;

                e_FromDP.Enabled = false;
                e_ToDP.Enabled = false;
                e_RefreshBtn.Enabled = false;

                e_NameTxt.Text = "";
                e_LatNameTxt.Text = "";
                e_AddressTxt.Text = "";
                e_PhoneTxt.Text = "";
                e_BirthdayTxt.Text = "";
                e_EmplymendTxt.Text = "";
                e_TotalCostTxt.Text = "";
                e_TotalServicesTxt.Text = "";
                e_ServicesList.Items.Clear();
            }
            else if (e_EmployeesList.Items.Count == 0)
            {
                e_FireBtn.Visible = false;
                e_DeleteBtn.Enabled = false;
                e_EditBtn.Enabled = false;

                e_FromDP.Enabled = false;
                e_ToDP.Enabled = false;
                e_RefreshBtn.Enabled = false;

                e_EmployeesList.SelectedIndex = -1;

                e_FromDP.Enabled = false;
                e_ToDP.Enabled = false;

                e_NameTxt.Text = "";
                e_LatNameTxt.Text = "";
                e_AddressTxt.Text = "";
                e_PhoneTxt.Text = "";
                e_BirthdayTxt.Text = "";
                e_EmplymendTxt.Text = "";
                e_TotalCostTxt.Text = "";
                e_TotalServicesTxt.Text = "";
                e_ServicesList.Items.Clear();
            }
            else 
            {
                e_FireBtn.Visible = true;
                e_DeleteBtn.Enabled = true;
                e_EditBtn.Enabled = true;

                e_FromDP.Enabled = true;
                e_ToDP.Enabled = true;
                e_RefreshBtn.Enabled = true;

                e_FiredECB.Enabled = true;

                e_EmployeesList.SelectedIndex = 0;
            }
        }

        private void calculateTotalPrice() 
        {
            if (!nb_CustomPriceCB.Checked) 
            {
                nb_TotalPrice = 0.0f;
                int n = nb_ServicesList.Items.Count;
                for (int i = 0; i < n; i++) 
                {
                    if (nb_ServicesList.Items[i].GetType() == typeof(CService))
                    {
                        CService tmpService = nb_ServicesList.Items[i] as CService;
                        nb_TotalPrice += tmpService.price;
                    }
                    else 
                    {
                        CPackage tmpPackage = nb_ServicesList.Items[i] as CPackage;
                        nb_TotalPrice += tmpPackage.price;
                    }
                }
                nb_TotalPriceTxt.Text = nb_TotalPrice.ToString();
            }
        }

        private void customPriceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (nb_CustomPriceCB.Checked) 
                nb_TotalPriceTxt.ReadOnly = false;
            else
            {
                nb_TotalPriceTxt.ReadOnly = true;
                calculateTotalPrice();
            }
        }

        private void nb_AddServiceBtn_Click(object sender, EventArgs e)
        {
            selectService newForm = new selectService();
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nb_ServicesList.Items.Add(newForm.service);
                calculateTotalPrice();

                if (nb_ServicesList.SelectedIndex == -1)
                    nb_ServicesList.SelectedIndex = 0;

                nb_DeleteService.Enabled = true;

                if (nb_Customer != null) nb_CompliteBllBtn.Enabled = true;
            }
        }

        private void nb_AddPackageBtn_Click(object sender, EventArgs e)
        {
            selectPackage newForm = new selectPackage();
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nb_ServicesList.Items.Add(newForm.package);
                calculateTotalPrice();

                if (nb_ServicesList.SelectedIndex == -1)
                    nb_ServicesList.SelectedIndex = 0;

                nb_DeleteService.Enabled = true;

                if (nb_Customer != null) nb_CompliteBllBtn.Enabled = true;
            }
        }

        private void nb_deleteService_Click(object sender, EventArgs e)
        {
            if (nb_ServicesList.SelectedIndex != -1) 
            {
                nb_ServicesList.Items.RemoveAt(nb_ServicesList.SelectedIndex);
                calculateTotalPrice();
            }

            if (nb_ServicesList.Items.Count == 0)
            {
                nb_DeleteService.Enabled = false;
                nb_CompliteBllBtn.Enabled = false;
            }
            
        }

        private void nb_SelectCustomerBtn_Click(object sender, EventArgs e)
        {
            selectCustomer newForm = new selectCustomer();
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nb_Customer = newForm.customer;
                nb_CustomerTxt.Text = newForm.customer.ToString();
                if (nb_ServicesList.Items.Count > 0) nb_CompliteBllBtn.Enabled = true;
            }
        }

        private void nb_CompliteBllBtn_Click(object sender, EventArgs e)
        {
             connection.openConnection();
           
            // Create Bill

            string date = nb_DateTimePicker.Value.ToString();
            string price = nb_TotalPriceTxt.Text;
            string note = nb_NoteBox.Text;
            string userID = nb_Customer.id;

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection.connection;
            command.CommandText = @"INSERT INTO Bills (Bill_Date, Price, [Note], ID_Customer) VALUES (#" + date + "#, " + price + ", '" + note + "', " + userID + ")";
            command.ExecuteNonQuery();
            command.CommandText = "Select @@Identity";

            int billID = (int)command.ExecuteScalar();

            // Insert Bill Services & Bill Packages

            for (int i = 0; i < nb_ServicesList.Items.Count; i++) 
            {
                if (nb_ServicesList.Items[i].GetType() == typeof(CService))
                {
                    CService tmpService = nb_ServicesList.Items[i] as CService;

                    command.CommandText = "INSERT INTO Bill_Services (ID_Bill, ID_Service, ID_Employee) VALUES (" + billID.ToString() + ", " + tmpService.id + ", " + tmpService.employee.id + ")";
                    command.ExecuteNonQuery();
                }
                else if (nb_ServicesList.Items[i].GetType() == typeof(CPackage))
                {
                    CPackage tmpPackage = nb_ServicesList.Items[i] as CPackage;

                    for (int j = 0; j < tmpPackage.services.Count; j++) 
                    {
                        CService tmpService = tmpPackage.services[j];

                        command.CommandText = "INSERT INTO Bill_PServices (ID_Bill, ID_Package, ID_Service, ID_Employee) VALUES (" + billID.ToString() + ", "+ tmpPackage.id + ", " + tmpService.id + "," + tmpService.employee.id + ")";
                        command.ExecuteNonQuery();
                    }

                }
            }

            connection.closeConnection();

            MessageBox.Show("The bill has been succesfully recorded!", "Bill recorded!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            nb_Customer = null;
            nb_CustomerTxt.Text = "";
            nb_DateTimePicker.Value = DateTime.Now;
            nb_ServicesList.Items.Clear();
            nb_TotalPrice = 0.0f;
            nb_TotalPriceTxt.Text = "0";
            nb_CustomPriceCB.Checked = false;
            nb_NoteBox.Text = "";
        }

        private void s_ServicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CService tmpService = s_ServicesList.SelectedItem as CService;
            if (tmpService != null)
            {
                s_ServiceNameTxt.Text = tmpService.name;
                s_ServicePriceTxt.Text = tmpService.price.ToString();
            }
        }

        private void s_PackagesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CPackage tmpPackage = s_PackagesList.SelectedItem as CPackage;
            if (tmpPackage != null)
            {
                s_PackageNameTxt.Text = tmpPackage.name;
                s_PackagePriceTxt.Text = tmpPackage.price.ToString();

                s_PackageServicesList.Items.Clear();
                for (int i = 0; i < tmpPackage.services.Count; i++)
                    s_PackageServicesList.Items.Add(tmpPackage.services[i]);
            }
        }

        private void s_ServiceDelete_Click(object sender, EventArgs e)
        {
            CService tmpService = s_ServicesList.SelectedItem as CService;

            DialogResult result = MessageBox.Show("Are you sure you want to delete " + tmpService.name + "?", "Deleting " + tmpService.name, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            connection.openConnection();

            OleDbCommand deleteService = new OleDbCommand();
            deleteService.Connection = connection.connection;
            deleteService.CommandText = "DELETE FROM Services WHERE ID = " + tmpService.id;
            
            try
            {
                deleteService.ExecuteNonQuery();
                s_ServicesList.Items.Remove(tmpService);

                if (s_ServicesList.Items.Count == 0)
                {
                    s_ServiceDelete.Enabled = false;
                    s_ServiceEdit.Enabled = false;
                    s_PackageAdd.Enabled = false;
                    s_ServicesList.SelectedIndex = -1;
                    s_ServiceNameTxt.Text = "";
                    s_ServicePriceTxt.Text = "";
                }
                else
                    s_ServicesList.SelectedIndex = 0;
            }
            catch (OleDbException exception)
            {
                if (exception.Message.Contains("Package_Service")) 
                    MessageBox.Show("Service was not deleted because is included in a package!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (exception.Message.Contains("Bill"))
                    MessageBox.Show("Service was not deleted because is included in a bill!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Service was not deleted because of unknown error!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            connection.closeConnection();
        }

        private void s_ServiceAdd_Click(object sender, EventArgs e)
        {
            serviceForm newForm = new serviceForm();
            newForm.Text = "Add New Service";
            newForm.edit = false;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                s_ServicesList.Items.Add(newForm.service);
                s_ServiceDelete.Enabled = true;
                s_ServiceEdit.Enabled = true;
                s_PackageAdd.Enabled = true;
                s_ServicesList.SelectedIndex = s_ServicesList.Items.Count - 1;
            }
        }

        private void s_ServiceEdit_Click(object sender, EventArgs e)
        {
            CService tmpService = s_ServicesList.SelectedItem as CService;
            serviceForm newForm = new serviceForm();
            newForm.Text = "Editing" + tmpService.name;
            newForm.edit = true;
            newForm.service = tmpService;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                s_ServicesList.Items[s_ServicesList.SelectedIndex] = newForm.service;

                for (int i = 0; i < s_PackagesList.Items.Count; i++)
                {
                    CPackage tmpPackage = s_PackagesList.Items[i] as CPackage;

                    for (int j = 0; j < tmpPackage.services.Count; j++)
                    {
                        if (tmpPackage.services[j].id.Equals(newForm.service.id))
                        {
                            tmpPackage.services[j].name = newForm.service.name;
                            tmpPackage.services[j].price = newForm.service.price;
                        }
                    }
                }

                int index = s_PackagesList.SelectedIndex;
                s_PackagesList.ClearSelected();
                s_PackagesList.SelectedIndex = index;
            }
        }

        private void s_PackageAdd_Click(object sender, EventArgs e)
        {
            packageForm newForm = new packageForm();
            newForm.Text = "Add New Package";
            newForm.edit = false;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                s_PackagesList.Items.Add(newForm.package);
                s_PackageDelete.Enabled = true;
                s_PackageEdit.Enabled = true;
                s_PackagesList.SelectedIndex = s_PackagesList.Items.Count - 1;
            }
        }

        private void s_PackageEdit_Click(object sender, EventArgs e)
        {
            CPackage tmpPackage = s_PackagesList.SelectedItem as CPackage;
            packageForm newForm = new packageForm();
            newForm.Text = "Editing " + tmpPackage.name;
            newForm.edit = true;
            newForm.package = tmpPackage;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                s_PackagesList.Items[s_PackagesList.SelectedIndex] = newForm.package;
        }

        private void s_PackageDelete_Click(object sender, EventArgs e)
        {
            CPackage tmpPackage = s_PackagesList.SelectedItem as CPackage;

            DialogResult result = MessageBox.Show("Are you sure you want to delete " + tmpPackage.name + "?", "Deleting " + tmpPackage.name, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            connection.openConnection();

            OleDbCommand deletePackage = new OleDbCommand();
            deletePackage.Connection = connection.connection;
            deletePackage.CommandText = "DELETE FROM Packages WHERE ID = " + tmpPackage.id;

            try
            {
                deletePackage.ExecuteNonQuery();
                s_PackagesList.Items.Remove(tmpPackage);

                if (s_PackagesList.Items.Count == 0)
                {
                    s_PackageDelete.Enabled = false;
                    s_PackageEdit.Enabled = false;
                    s_PackagesList.SelectedIndex = -1;
                    s_PackageNameTxt.Text = "";
                    s_PackagePriceTxt.Text = "";
                    s_PackageServicesList.Items.Clear();
                }
                else
                    s_PackagesList.SelectedIndex = 0;
            }
            catch (OleDbException exception)
            {
                if (exception.Message.Contains("Bill"))
                    MessageBox.Show("Package was not deleted because is included in a bill!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Package was not deleted because of unknown error!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.closeConnection();
        }

        private void e_EmployeesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CEmployee tmpEmployee = e_EmployeesList.SelectedItem as CEmployee;
            if (tmpEmployee != null)
            {
                e_NameTxt.Text = tmpEmployee.name;
                e_LatNameTxt.Text = tmpEmployee.lastName;
                e_AddressTxt.Text = tmpEmployee.address;
                e_PhoneTxt.Text = tmpEmployee.phone;
                e_BirthdayTxt.Text = tmpEmployee.birthday.Date.ToShortDateString();
                e_EmplymendTxt.Text = tmpEmployee.dateOfEmploymend.ToString();
                if (tmpEmployee.fired)
                {
                    e_InfoLbl.Visible = true;
                    e_FireBtn.Text = "Hire";
                }
                else
                {
                    e_InfoLbl.Visible = false;
                    e_FireBtn.Text = "Fire";
                }

                e_ServicesList.Items.Clear();
                e_TotalCostTxt.Text = "";
                e_TotalServicesTxt.Text = "";
                e_RefreshBtn.Enabled = true;
            }
        }

        private void e_FiredECB_CheckedChanged(object sender, EventArgs e)
        {
            fillE_EmployeesList();
        }

        private void e_AddBtn_Click(object sender, EventArgs e)
        {
            employeeForm newForm = new employeeForm();
            newForm.Text = "Add New Employee";
            newForm.edit = false;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                e_employees.Add(newForm.employee);
                fillE_EmployeesList();
                e_EmployeesList.SelectedItem = newForm.employee;
            }
        }

        private void e_EditBtn_Click(object sender, EventArgs e)
        {
            CEmployee tmpEmployee = e_EmployeesList.SelectedItem as CEmployee;
            employeeForm newForm = new employeeForm();
            newForm.Text = "Editing " + tmpEmployee.name + " " + tmpEmployee.lastName;
            newForm.employee = tmpEmployee;
            newForm.edit = true;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                e_employees[e_EmployeesList.SelectedIndex] = newForm.employee;
                fillE_EmployeesList();
                e_EmployeesList.SelectedItem = newForm.employee;
            }
        }

        private void e_FireBtn_Click(object sender, EventArgs e)
        {
            CEmployee tmpEmployee = e_EmployeesList.SelectedItem as CEmployee;

            if (tmpEmployee.fired)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to hire " + tmpEmployee.name + " " + tmpEmployee.lastName + " again?", "Hiring " + tmpEmployee.name + " " + tmpEmployee.lastName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to fire " + tmpEmployee.name + " " + tmpEmployee.lastName + "?", "Firing " + tmpEmployee.name + " " + tmpEmployee.lastName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            connection.openConnection();

            OleDbCommand fireCommand = new OleDbCommand();
            fireCommand.Connection = connection.connection;

            if (tmpEmployee.fired)
            {
                fireCommand.CommandText = "UPDATE Employees SET Fired = false, Employmend = #" + DateTime.Now.ToString() + "# WHERE ID = " + tmpEmployee.id;
                fireCommand.ExecuteNonQuery();

                for (int i = 0; i < e_employees.Count; i++)
                    if (e_employees[i].id.Equals(tmpEmployee.id)) 
                    {
                        e_employees[i].fired = false;
                        e_employees[i].dateOfEmploymend = DateTime.Now;
                        break;
                    }   
            }
            else 
            {
                fireCommand.CommandText = "UPDATE Employees SET Fired = true WHERE ID = " + tmpEmployee.id;
                fireCommand.ExecuteNonQuery();

                for (int i = 0; i < e_employees.Count; i++)
                    if (e_employees[i].id.Equals(tmpEmployee.id))
                        e_employees[i].fired = true;
            }

            connection.closeConnection();

            fillE_EmployeesList();
        }

        private void e_DeleteBtn_Click(object sender, EventArgs e)
        {
            CEmployee tmpEmployee = e_EmployeesList.SelectedItem as CEmployee;

            DialogResult result = MessageBox.Show("Are you sure you want to delete " + tmpEmployee.name + " " + tmpEmployee.lastName + "?", "Deleting " + tmpEmployee.name + " " + tmpEmployee.lastName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            connection.openConnection();

            OleDbCommand deleteEmployee = new OleDbCommand();
            deleteEmployee.Connection = connection.connection;
            deleteEmployee.CommandText = "DELETE FROM Employees WHERE ID = " + tmpEmployee.id;

            try
            {
                deleteEmployee.ExecuteNonQuery();
                e_employees.Remove(tmpEmployee);

                fillE_EmployeesList();
            }
            catch (OleDbException exception)
            {
                if (exception.Message.Contains("Bill"))
                    MessageBox.Show("Employee was not deleted because is included in a bill!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Employee was not deleted because of unknown error!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.closeConnection();
        }

        private void e_RefreshBtn_Click(object sender, EventArgs e)
        {
            e_RefreshBtn.Enabled = false;

            CEmployee tmpEmployee = e_EmployeesList.SelectedItem as CEmployee;

            connection.openConnection();

            OleDbCommand getServicesCommand = new OleDbCommand();
            getServicesCommand.Connection = connection.connection;
            getServicesCommand.CommandText = "SELECT * FROM Services";

            OleDbDataReader servicesReader = getServicesCommand.ExecuteReader();

            List<CService> tmpServiceList = new List<CService>();
            while (servicesReader.Read())
            {
                string id = servicesReader[0].ToString();
                string name = servicesReader[1].ToString();
                string price = servicesReader[2].ToString();
                CService tmpSerice = new CService(id, name, float.Parse(price));
                tmpServiceList.Add(tmpSerice);
            }

            OleDbCommand getEmployeeServices = new OleDbCommand();
            getEmployeeServices.Connection = connection.connection;
            getEmployeeServices.CommandText = "SELECT ID, Bill_Date FROM Bills";

            OleDbDataReader billReader = getEmployeeServices.ExecuteReader();

            List<string> tmpServiceIDList = new List<string>();
            while (billReader.Read())
            {
                string billID = billReader[0].ToString();
                DateTime bill_date = (DateTime)billReader[1];

                if (!(bill_date.Date >= e_FromDP.Value.Date && bill_date.Date <= e_ToDP.Value.Date))
                    continue;

                OleDbCommand getServicesForBill = new OleDbCommand();
                getServicesForBill.Connection = connection.connection;
                getServicesForBill.CommandText = "SELECT ID_Service FROM Bill_Services WHERE ID_Bill = " + billID + "AND ID_Employee = " + tmpEmployee.id;

                OleDbDataReader billServiceReader = getServicesForBill.ExecuteReader();

                while (billServiceReader.Read())
                {
                    string tmpServiceID = billServiceReader[0].ToString();
                    tmpServiceIDList.Add(tmpServiceID);
                }

                OleDbCommand getPServicesForBill = new OleDbCommand();
                getPServicesForBill.Connection = connection.connection;
                getPServicesForBill.CommandText = "SELECT ID_Service FROM Bill_PServices WHERE ID_Bill = " + billID + "AND ID_Employee = " + tmpEmployee.id;

                OleDbDataReader billPServiceReader = getPServicesForBill.ExecuteReader();

                while (billPServiceReader.Read())
                {
                    string tmpPServiceID = billPServiceReader[0].ToString();
                    tmpServiceIDList.Add(tmpPServiceID);
                }
            }

            connection.closeConnection();

            e_ServicesList.Items.Clear();
            float tmpTotalPrice = 0;
            int tmpTotalServices = 0;
            for (int i = 0; i < tmpServiceList.Count; i++)
            {
                string tmpID = tmpServiceList[i].id;
                int number = 0;
                for (int j = 0; j < tmpServiceIDList.Count; j++)
                    if (tmpID.Equals(tmpServiceIDList[j]))
                        number++;

                tmpTotalPrice += tmpServiceList[i].price * number;
                tmpTotalServices += number;

                if (number > 0)
                    e_ServicesList.Items.Add(tmpServiceList[i] + " (" + tmpServiceList[i].price + ") x " + number);
            }

            if (e_ServicesList.Items.Count == 0)
            {
                MessageBox.Show(tmpEmployee.name + " " + tmpEmployee.lastName + " doesn't have any services in this data range!", "No services found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e_TotalCostTxt.Text = "";
                e_TotalServicesTxt.Text = "";
            }
            else
            {
                e_TotalCostTxt.Text = tmpTotalPrice.ToString();
                e_TotalServicesTxt.Text = tmpTotalServices.ToString();
            }
        }

        private void e_FromDP_ValueChanged(object sender, EventArgs e)
        {
            if (e_FromDP.Value > e_ToDP.Value)
                e_ToDP.Value = e_FromDP.Value;
            e_RefreshBtn.Enabled = true;
        }

        private void e_ToDP_ValueChanged(object sender, EventArgs e)
        {
            if (e_ToDP.Value < e_FromDP.Value)
                e_FromDP.Value = e_ToDP.Value;
            e_RefreshBtn.Enabled = true;
        }

        private void c_CustomersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CCustomer tmpCustomer = c_CustomersList.SelectedItem as CCustomer;
            if (tmpCustomer != null) 
            {
                c_NameTxt.Text = tmpCustomer.name;
                c_LastNameTxt.Text = tmpCustomer.lastName;
                c_AddressTxt.Text = tmpCustomer.address;
                c_PhoneTxt.Text = tmpCustomer.phone;
                c_BirthdayTxt.Text = tmpCustomer.birthday.ToShortDateString();

                c_LoadBtn.Enabled = true;
                c_ViewBilBtn.Enabled = false;
                c_BillsList.Items.Clear();
            }
        }

        private void c_DeleteBtn_Click(object sender, EventArgs e)
        {
            CCustomer tmpCustomer = c_CustomersList.SelectedItem as CCustomer;

            DialogResult result = MessageBox.Show("Are you sure you want to delete " + tmpCustomer.name + " " + tmpCustomer.lastName + "?", "Deleting " + tmpCustomer.name + " " + tmpCustomer.lastName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            connection.openConnection();

            OleDbCommand deleteEmployee = new OleDbCommand();
            deleteEmployee.Connection = connection.connection;
            deleteEmployee.CommandText = "DELETE FROM Customers WHERE ID = " + tmpCustomer.id;

            try
            {
                deleteEmployee.ExecuteNonQuery();
                c_CustomersList.Items.Remove(tmpCustomer);

                if (c_CustomersList.Items.Count == 0)
                {
                    c_EditBtn.Enabled = false;
                    c_DeleteBtn.Enabled = false;
                    c_LoadBtn.Enabled = false;
                    c_ViewBilBtn.Enabled = false;

                    c_CustomersList.SelectedIndex = -1;
                    c_BillsList.Items.Clear();

                    c_NameTxt.Text = "";
                    c_LastNameTxt.Text = "";
                    c_AddressTxt.Text = "";
                    c_PhoneTxt.Text = "";
                    c_BirthdayTxt.Text = "";
                }
                else
                    s_PackagesList.SelectedIndex = 0;
            }
            catch (OleDbException exception)
            {
                if (exception.Message.Contains("Bill"))
                    MessageBox.Show("Customers was not deleted because is included in a bill!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Customer was not deleted because of unknown error!", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.closeConnection();
        }

        private void c_AddBtn_Click(object sender, EventArgs e)
        {
            customerForm newForm = new customerForm();
            newForm.Text = "Add New Customer";
            newForm.edit = false;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                c_CustomersList.Items.Add(newForm.customer);
                c_EditBtn.Enabled = true;
                c_DeleteBtn.Enabled = true;
                c_CustomersList.SelectedIndex = c_CustomersList.Items.Count - 1;
            }
        }

        private void c_EditBtn_Click(object sender, EventArgs e)
        {
            CCustomer tmpCustomer = c_CustomersList.SelectedItem as CCustomer;
            customerForm newForm = new customerForm();
            newForm.Text = "Editing " + tmpCustomer.name + " " + tmpCustomer.lastName;
            newForm.customer = tmpCustomer;
            newForm.edit = true;
            if (newForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                c_CustomersList.Items[c_CustomersList.SelectedIndex] = newForm.customer;
                c_CustomersList.SelectedItem = newForm.customer;
            }
        }

        private void c_LoadBtn_Click(object sender, EventArgs e)
        {
            c_LoadBtn.Enabled = false;
            c_BillsList.Items.Clear();

            CCustomer tmpCustomer = c_CustomersList.SelectedItem as CCustomer;

            connection.openConnection();

            OleDbCommand billsCommand = new OleDbCommand();
            billsCommand.Connection = connection.connection;

            billsCommand.CommandText = "SELECT * FROM Bills WHERE ID_Customer = " + tmpCustomer.id + " ORDER BY Bill_Date ASC";
            OleDbDataReader billsReader = billsCommand.ExecuteReader();

            while (billsReader.Read()) 
            {
                string idBill = billsReader[0].ToString();
                DateTime billDate = (DateTime)billsReader[1];
                string billPrice = billsReader[2].ToString();
                string billNote = billsReader[3].ToString();

                OleDbCommand billServicesCommand = new OleDbCommand();
                billServicesCommand.Connection = connection.connection;

                billServicesCommand.CommandText = "SELECT Services.* FROM Services, Bill_Services WHERE Bill_Services.ID_Service = Services.ID AND Bill_Services.ID_Bill = " + idBill;
                OleDbDataReader billServicesReader = billServicesCommand.ExecuteReader();

                List<CService> tmpServices = new List<CService>();
                while (billServicesReader.Read()) 
                {
                    string idService = billServicesReader[0].ToString();
                    string name = billServicesReader[1].ToString();
                    string servicePrice = billServicesReader[2].ToString();
                    CService tmpSerice = new CService(idService, name, float.Parse(servicePrice));

                    OleDbCommand billServiceEmployeeCommand = new OleDbCommand();
                    billServiceEmployeeCommand.Connection = connection.connection;

                    billServiceEmployeeCommand.CommandText = "SELECT Employees.* FROM Employees, Bill_Services WHERE Bill_Services.ID_Bill = " + idBill + " AND Bill_Services.ID_Service = " + idService + "AND Bill_Services.ID_Employee = Employees.ID";
                    OleDbDataReader billServiceEmployeeReader = billServiceEmployeeCommand.ExecuteReader();

                    billServiceEmployeeReader.Read();

                    string idEmployee = billServiceEmployeeReader[0].ToString();
                    string first_name = billServiceEmployeeReader[1].ToString();
                    string last_name = billServiceEmployeeReader[2].ToString();
                    string address = billServiceEmployeeReader[3].ToString();
                    string phone = billServiceEmployeeReader[4].ToString();
                    DateTime birthday = (DateTime)billServiceEmployeeReader[5];
                    DateTime dayOfEmploymend = (DateTime)billServiceEmployeeReader[6];
                    bool fired = (bool)billServiceEmployeeReader[7];

                    CEmployee tmpEmployee = new CEmployee(idEmployee, first_name, last_name, address, phone, birthday, dayOfEmploymend, fired);
                    tmpSerice.employee = tmpEmployee;

                    tmpServices.Add(tmpSerice);
                }

                OleDbCommand billPackages = new OleDbCommand();
                billPackages.Connection = connection.connection;

                billPackages.CommandText = "SELECT DISTINCT Packages.* FROM Packages, Bill_PServices WHERE Packages.ID = Bill_PServices.ID_Package AND Bill_PServices.ID_Bill = " + idBill;
                OleDbDataReader billPackagesReader = billPackages.ExecuteReader();

                List<CPackage> tmpPackages = new List<CPackage>();
                while (billPackagesReader.Read()) 
                {
                    string idPackage = billPackagesReader[0].ToString();
                    string packageName = billPackagesReader[1].ToString();
                    string packagePrice = billPackagesReader[2].ToString();

                    OleDbCommand billPackageServices = new OleDbCommand();
                    billPackageServices.Connection = connection.connection;

                    billPackageServices.CommandText = "SELECT Services.* FROM Services, Bill_PServices WHERE Services.ID = Bill_PServices.ID_Service AND Bill_PServices.ID_Package = " + idPackage;
                    OleDbDataReader billPackageServicesReader = billPackageServices.ExecuteReader();

                    List<CService> tmpPServices = new List<CService>();
                    while (billPackageServicesReader.Read()) 
                    {
                        string idService = billPackageServicesReader[0].ToString();
                        string name = billPackageServicesReader[1].ToString();
                        string servicePrice = billPackageServicesReader[2].ToString();
                        CService tmpSerice = new CService(idService, name, float.Parse(servicePrice));

                        OleDbCommand billServiceEmployeeCommand = new OleDbCommand();
                        billServiceEmployeeCommand.Connection = connection.connection;

                        billServiceEmployeeCommand.CommandText = "SELECT Employees.* FROM Employees, Bill_PServices WHERE Bill_PServices.ID_Bill = " + idBill + " AND Bill_PServices.ID_Service = " + idService + " AND Bill_PServices.ID_Employee = Employees.ID AND Bill_PServices.ID_Package = " + idPackage;
                        OleDbDataReader billServiceEmployeeReader = billServiceEmployeeCommand.ExecuteReader();

                        billServiceEmployeeReader.Read();

                        string idEmployee = billServiceEmployeeReader[0].ToString();
                        string first_name = billServiceEmployeeReader[1].ToString();
                        string last_name = billServiceEmployeeReader[2].ToString();
                        string address = billServiceEmployeeReader[3].ToString();
                        string phone = billServiceEmployeeReader[4].ToString();
                        DateTime birthday = (DateTime)billServiceEmployeeReader[5];
                        DateTime dayOfEmploymend = (DateTime)billServiceEmployeeReader[6];
                        bool fired = (bool)billServiceEmployeeReader[7];

                        CEmployee tmpEmployee = new CEmployee(idEmployee, first_name, last_name, address, phone, birthday, dayOfEmploymend, fired);
                        tmpSerice.employee = tmpEmployee;

                        tmpPServices.Add(tmpSerice);
                    }

                    CPackage tmpPackage = new CPackage(idPackage, packageName, float.Parse(packagePrice), tmpPServices);
                    tmpPackages.Add(tmpPackage);
                }

                CBill tmpBill = new CBill(idBill,billNote,float.Parse(billPrice),tmpPackages,tmpServices,billDate,tmpCustomer);
                c_BillsList.Items.Add(tmpBill);
            }

            if (c_BillsList.Items.Count > 0)
            {
                c_BillsList.SelectedIndex = 0;
                c_ViewBilBtn.Enabled = true;
            }
            else
                MessageBox.Show(tmpCustomer.name + " " + tmpCustomer.lastName + " doesn't have any bills yet!", "No bills found", MessageBoxButtons.OK, MessageBoxIcon.Information);

            connection.closeConnection();
        }

        private void c_ViewBilBtn_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 1;
            CBill tmpBill = c_BillsList.SelectedItem as CBill;

            allBillsList.Items.Clear();

            ab_From.Value = tmpBill.date.Date;
            ab_To.Value = tmpBill.date.Date;
            refreshBtn.PerformClick();

            for (int i = 0; i < allBillsList.Items.Count; i++) 
            {
                CBill bill = allBillsList.Items[i] as CBill;
                if (bill.id.Equals(tmpBill.id))
                {
                    allBillsList.SelectedIndex = i;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshBtn.Enabled = false;
            allBillsList.Items.Clear();
            ab_TotalBills = 0;
            ab_TotalPrice = 0.0f;

            connection.openConnection();

            OleDbCommand billsCommand = new OleDbCommand();
            billsCommand.Connection = connection.connection;

            billsCommand.CommandText = "SELECT * FROM Bills ORDER BY Bill_Date ASC";
            OleDbDataReader billsReader = billsCommand.ExecuteReader();

            while (billsReader.Read())
            {
                string idBill = billsReader[0].ToString();
                DateTime billDate = (DateTime)billsReader[1];
                string billPrice = billsReader[2].ToString();
                string billNote = billsReader[3].ToString();
                string idCustomer = billsReader[4].ToString(); 

                // FIlter by Date

                if (!(billDate.Date >= ab_From.Value.Date && billDate.Date <= ab_To.Value.Date))
                    continue;

                // Get Customer

                OleDbCommand customerComm = new OleDbCommand();
                customerComm.Connection = connection.connection;
                customerComm.CommandText = "SELECT * FROM Customers WHERE ID = " + idCustomer;

                OleDbDataReader customerReader = customerComm.ExecuteReader();
                customerReader.Read();

                string cID = customerReader[0].ToString();
                string CFN = customerReader[1].ToString();
                string CLN = customerReader[2].ToString();
                string CA = customerReader[3].ToString();
                string CP = customerReader[4].ToString();
                DateTime CB = (DateTime)customerReader[5];
                CCustomer tmpCustomer = new CCustomer(cID, CFN, CLN, CA, CP, CB);

                OleDbCommand billServicesCommand = new OleDbCommand();
                billServicesCommand.Connection = connection.connection;

                billServicesCommand.CommandText = "SELECT Services.* FROM Services, Bill_Services WHERE Bill_Services.ID_Service = Services.ID AND Bill_Services.ID_Bill = " + idBill;
                OleDbDataReader billServicesReader = billServicesCommand.ExecuteReader();

                List<CService> tmpServices = new List<CService>();
                while (billServicesReader.Read())
                {
                    string idService = billServicesReader[0].ToString();
                    string name = billServicesReader[1].ToString();
                    string servicePrice = billServicesReader[2].ToString();
                    CService tmpSerice = new CService(idService, name, float.Parse(servicePrice));

                    OleDbCommand billServiceEmployeeCommand = new OleDbCommand();
                    billServiceEmployeeCommand.Connection = connection.connection;

                    billServiceEmployeeCommand.CommandText = "SELECT Employees.* FROM Employees, Bill_Services WHERE Bill_Services.ID_Bill = " + idBill + " AND Bill_Services.ID_Service = " + idService + "AND Bill_Services.ID_Employee = Employees.ID";
                    OleDbDataReader billServiceEmployeeReader = billServiceEmployeeCommand.ExecuteReader();

                    billServiceEmployeeReader.Read();

                    string idEmployee = billServiceEmployeeReader[0].ToString();
                    string first_name = billServiceEmployeeReader[1].ToString();
                    string last_name = billServiceEmployeeReader[2].ToString();
                    string address = billServiceEmployeeReader[3].ToString();
                    string phone = billServiceEmployeeReader[4].ToString();
                    DateTime birthday = (DateTime)billServiceEmployeeReader[5];
                    DateTime dayOfEmploymend = (DateTime)billServiceEmployeeReader[6];
                    bool fired = (bool)billServiceEmployeeReader[7];

                    CEmployee tmpEmployee = new CEmployee(idEmployee, first_name, last_name, address, phone, birthday, dayOfEmploymend, fired);
                    tmpSerice.employee = tmpEmployee;

                    tmpServices.Add(tmpSerice);
                }

                OleDbCommand billPackages = new OleDbCommand();
                billPackages.Connection = connection.connection;

                billPackages.CommandText = "SELECT DISTINCT Packages.* FROM Packages, Bill_PServices WHERE Packages.ID = Bill_PServices.ID_Package AND Bill_PServices.ID_Bill = " + idBill;
                OleDbDataReader billPackagesReader = billPackages.ExecuteReader();

                List<CPackage> tmpPackages = new List<CPackage>();
                while (billPackagesReader.Read())
                {
                    string idPackage = billPackagesReader[0].ToString();
                    string packageName = billPackagesReader[1].ToString();
                    string packagePrice = billPackagesReader[2].ToString();

                    OleDbCommand billPackageServices = new OleDbCommand();
                    billPackageServices.Connection = connection.connection;

                    billPackageServices.CommandText = "SELECT Services.* FROM Services, Bill_PServices WHERE Services.ID = Bill_PServices.ID_Service AND Bill_PServices.ID_Package = " + idPackage;
                    OleDbDataReader billPackageServicesReader = billPackageServices.ExecuteReader();

                    List<CService> tmpPServices = new List<CService>();
                    while (billPackageServicesReader.Read())
                    {
                        string idService = billPackageServicesReader[0].ToString();
                        string name = billPackageServicesReader[1].ToString();
                        string servicePrice = billPackageServicesReader[2].ToString();
                        CService tmpSerice = new CService(idService, name, float.Parse(servicePrice));

                        OleDbCommand billServiceEmployeeCommand = new OleDbCommand();
                        billServiceEmployeeCommand.Connection = connection.connection;

                        billServiceEmployeeCommand.CommandText = "SELECT Employees.* FROM Employees, Bill_PServices WHERE Bill_PServices.ID_Bill = " + idBill + " AND Bill_PServices.ID_Service = " + idService + " AND Bill_PServices.ID_Employee = Employees.ID AND Bill_PServices.ID_Package = " + idPackage;
                        OleDbDataReader billServiceEmployeeReader = billServiceEmployeeCommand.ExecuteReader();

                        billServiceEmployeeReader.Read();

                        string idEmployee = billServiceEmployeeReader[0].ToString();
                        string first_name = billServiceEmployeeReader[1].ToString();
                        string last_name = billServiceEmployeeReader[2].ToString();
                        string address = billServiceEmployeeReader[3].ToString();
                        string phone = billServiceEmployeeReader[4].ToString();
                        DateTime birthday = (DateTime)billServiceEmployeeReader[5];
                        DateTime dayOfEmploymend = (DateTime)billServiceEmployeeReader[6];
                        bool fired = (bool)billServiceEmployeeReader[7];

                        CEmployee tmpEmployee = new CEmployee(idEmployee, first_name, last_name, address, phone, birthday, dayOfEmploymend, fired);
                        tmpSerice.employee = tmpEmployee;

                        tmpPServices.Add(tmpSerice);
                    }

                    CPackage tmpPackage = new CPackage(idPackage, packageName, float.Parse(packagePrice), tmpPServices);
                    tmpPackages.Add(tmpPackage);
                }

                CBill tmpBill = new CBill(idBill, billNote, float.Parse(billPrice), tmpPackages, tmpServices, billDate, tmpCustomer);
                tmpBill.inAll = true;

                allBillsList.Items.Add(tmpBill);

                ab_TotalBills += 1;
                ab_TotalPrice += tmpBill.price;
            }

            connection.closeConnection();

            if (allBillsList.Items.Count == 0)
            {
                MessageBox.Show("There arent any bills in that period", "No bills found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button26.Enabled = false;
                ab_TotalBillTxt.Text = "";
                ab_TotalIncomeTxt.Text = "";
            }
            else
            {
                button26.Enabled = true;
                ab_TotalBillTxt.Text = ab_TotalBills.ToString();
                ab_TotalIncomeTxt.Text = ab_TotalPrice.ToString();
                allBillsList.SelectedIndex = 0;
            }
        }

        private void allBillsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ab_ServicesList.Items.Clear();
            CBill tmpBill = allBillsList.SelectedItem as CBill;
            if (tmpBill != null)
            {
                ab_PriceTxt.Text = tmpBill.price.ToString();
                ab_DateTxt.Text = tmpBill.date.ToString();
                ab_CustomerTxt.Text = tmpBill.customer.ToString();
                if (tmpBill.note.Length > 0)
                    ab_Note.Text = tmpBill.note;
                else ab_Note.Text = "There is no note for this bill";

                for (int i = 0; i < tmpBill.services.Count; i++)
                    ab_ServicesList.Items.Add(tmpBill.services[i]);

                for (int i = 0; i < tmpBill.packages.Count; i++)
                    ab_ServicesList.Items.Add(tmpBill.packages[i]);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            CBill tmpBill = allBillsList.SelectedItem as CBill;

            DialogResult result = MessageBox.Show("Are you sure you want to delete " + tmpBill.customer.ToString() + "'s bill on  " + tmpBill.date.ToString() + "?", "Deleting bill", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;

            allBillsList.Items.Remove(tmpBill);

            connection.openConnection();

            OleDbCommand deleteCommand = new OleDbCommand();
            deleteCommand.Connection = connection.connection;
            deleteCommand.CommandText = "DELETE FROM Bills WHERE ID = " + tmpBill.id;

            int n = deleteCommand.ExecuteNonQuery();

            connection.closeConnection();

            if (allBillsList.Items.Count == 0)
            {
                button26.Enabled = false;
                ab_PriceTxt.Text = "";
                ab_DateTxt.Text = "";
                ab_Note.Text = "";
                ab_CustomerTxt.Text = "";
                ab_TotalBillTxt.Text = "";
                ab_TotalIncomeTxt.Text = "";
                ab_ServicesList.Items.Clear();
                ab_SEList.Items.Clear();
                refreshBtn.Enabled = true;
            }
            else
            {
                allBillsList.SelectedIndex = 0;
                ab_TotalBills -= 1;
                ab_TotalBillTxt.Text = ab_TotalBills.ToString();
                ab_TotalPrice -= tmpBill.price;
                ab_TotalIncomeTxt.Text = ab_TotalPrice.ToString();
            }
        }

        private void ab_From_ValueChanged(object sender, EventArgs e)
        {
            refreshBtn.Enabled = true;
        }

        private void ab_To_ValueChanged(object sender, EventArgs e)
        {
            refreshBtn.Enabled = true;
        }

        private void ab_ServicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ab_SEList.Items.Clear();

            if (ab_ServicesList.SelectedItem.GetType() == typeof(CService))
            {
                CService tmpService = ab_ServicesList.SelectedItem as CService;
                ab_SEList.Items.Add(tmpService.employee);
            }
            else
            {
                CPackage tmpPackage = ab_ServicesList.SelectedItem as CPackage;

                for (int i = 0; i < tmpPackage.services.Count; i++) 
                {
                    CService tmpService = tmpPackage.services[i] as CService;
                    ab_SEList.Items.Add(tmpService.employee + " (" + tmpService + ")");
                }
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
                refreshBtn.Enabled = true;
            if (tabControl.SelectedIndex == 2 && c_CustomersList.SelectedIndex > -1)
                c_LoadBtn.Enabled = true;
            if (tabControl.SelectedIndex == 4 && e_EmployeesList.SelectedIndex > -1)
                e_RefreshBtn.Enabled = true;
        }
    }
}