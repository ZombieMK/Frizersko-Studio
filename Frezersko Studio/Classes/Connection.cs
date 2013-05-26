using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Frezersko_Studio.Classes
{
    public class Connection
    {
        public OleDbConnection connection { get; set; }

        public Connection()
        {
            connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb;Persist Security Info=False;";
        }

        public void openConnection()
        {
            connection.Open();
        }

        public void closeConnection()
        {
            connection.Close();
        }
    }
}
