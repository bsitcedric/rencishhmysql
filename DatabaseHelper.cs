using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// SSH
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Net;

//Mysql
using MySql.Data.MySqlClient;


using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Data;


namespace RenciModule //Just Change The NameSpace
{
    class DatabaseHelper
    {
        private Connection connectionInfo;
        private ServerConnectionInfo serverInfo;
        public DatabaseHelper(Connection connectionInfo, ServerConnectionInfo serverInfo)
        {
            this.connectionInfo = connectionInfo;
            this.serverInfo = serverInfo;

        }

        public bool testSHHConnection()
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                    return false;
                }
            }
        }


        public MySqlDataReader executeReader(Query query)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    con.Open();
                                    using (MySqlCommand cmd = new MySqlCommand(query.getQuery(), con))
                                    {
                                        foreach (var pair in query.getParameters())
                                        {
                                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                                        }
                                        return cmd.ExecuteReader();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return null;
        }
        public MySqlDataReader executeReader(string query)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    con.Open();
                                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                                    {

                                        return cmd.ExecuteReader();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return null;
        }


        public bool executeTransaction(List<Query> queries)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    con.Open();

                                    using (MySqlTransaction trans = con.BeginTransaction())
                                    {
                                        try
                                        {
                                            foreach(Query query in queries)
                                            {
                                                using (MySqlCommand cmd = new MySqlCommand(query.getQuery(), con, trans))
                                                {
                                                    
                                                    foreach(var pair in query.getParameters())
                                                    {
                                                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                                                    }
                                                    cmd.ExecuteNonQuery();
                                                    cmd.Parameters.Clear();
                                                }
                                            }
                                            trans.Commit();
                                        }catch(Exception ex)
                                        {

                                            trans.Rollback();
                                            MessageBox.Show(ex.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        public bool executeQuery(string query)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    con.Open();
                                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                                    {
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        return true;
                                    }
                                    
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool executeQuery(Query query)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();

                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    con.Open();
                                    using (MySqlCommand cmd = new MySqlCommand(query.getQuery(), con))
                                    {
                                        foreach (var pair in query.getParameters())
                                        {
                                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                                        }
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        return true;
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception e)   
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public DataTable getDataTable(string query)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                                    {
                                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                                        {
                                            DataTable dt = new DataTable();
                                            da.Fill(dt);
                                            return dt;
                                        }
                                    }
                                }
                            }
                        }
                        client.Disconnect();
                    }
                    catch (Exception e)
                    {
                        ShowErrMessage(e.ToString(), "Error");
                    }
                }
            }
            return null;
        }



        public DataTable getDataTable(Query query)
        {
            using (PasswordConnectionInfo passConInfo = new PasswordConnectionInfo(serverInfo.Server, serverInfo.Port, serverInfo.Username, serverInfo.Password))
            {
                passConInfo.Timeout = TimeSpan.FromSeconds(serverInfo.Timeout);

                using (var client = new SshClient(passConInfo))
                {
                    try
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            var portFwld = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                            client.AddForwardedPort(portFwld);
                            portFwld.Start();


                            if (portFwld.IsStarted)
                            {

                                using (MySqlConnection con = new MySqlConnection(connectionInfo.getConnectionString()))
                                {
                                    using (MySqlCommand cmd = new MySqlCommand(query.getQuery(), con))
                                    {
                                        foreach (var pair in query.getParameters())
                                        {
                                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                                        }
                                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                                        {
                                            DataTable dt = new DataTable();
                                            da.Fill(dt);
                                            return dt;
                                        }
                                    }
                                }
                            }
                        }
                        client.Disconnect();
                    }
                    catch (Exception e)
                    {
                        ShowErrMessage(e.ToString(), "Error");
                    }
                }
            }
            return null;
        }

        

        public Connection getConnectionInfo()
        {
            return this.connectionInfo;
        }

        public void setConectionInfo(Connection connectionInfo)
        {
            this.connectionInfo = connectionInfo;
        }

        public ServerConnectionInfo getServerInfo()
        {
            return this.serverInfo;
        }

        public void setServerInfo(ServerConnectionInfo serverInfo)
        {
            this.serverInfo = serverInfo;

        }


        public void ShowErrMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfoMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }


    [Serializable]
    public class DatabaseHelperException : Exception
    {
        public DatabaseHelperException(string message)
            : base(message)
        { }
        protected DatabaseHelperException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }



}
