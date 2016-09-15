using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenciModule //Just Change The NameSpace
{
    class ServerConnectionInfo
    {

        public ServerConnectionInfo(string server, int port, string username, string password, int timeout)
        {
            this.server = server;
            this.port = port;
            this.password = password;
            this.timeout = timeout;
            this.username = username;
        }


        private string server;
        public string Server
        {
            get { return this.server; }
            set { this.server = value; }
        }

        private int port;
        public int Port
        {
            get { return this.port; }
            set { this.port = value; }
        }

        private string username;

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        private int timeout;
        public int Timeout
        {
            get { return this.timeout; }
            set { this.timeout = value; }
        }

    }
}
