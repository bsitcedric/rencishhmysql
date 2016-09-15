using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenciModule //Just Change The NameSpace
{
    class MysqlConnectionInfo 
    {
        private string server;
        public string Server
        {
            get { return this.server; }
            set { this.server = value; }
        }

        private string database;
        public string Database
        {
            get { return this.database; }
            set { this.database = value; }
        }

        private string uid;
        public string Uid
        {
            get { return this.server; }
            set { this.server = value; }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        private string port;
        public string Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        private string connectionString = "";

        public string getConnectionString()
        {
            return this.connectionString;
        }

        public MysqlConnectionInfo(string server, string database, string uid, string password, string port = "3306")
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            this.port = port;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }
    }
}
