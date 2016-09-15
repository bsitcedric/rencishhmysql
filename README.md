# rencishhmysql
Connecting C# Applications to Your webhost and execute sql commands via SSH
Created by Cedric Coloma

Instructions
1. Add Renci SHH.NET in your project! 
   >Tools > Nugget Package Manager > Package Manager Console > Paste this

   Install-Package SSH.NET

2. Import the following
   // For Renci SSH
      using Renci.SshNet;
      using Renci.SshNet.Common;
      using System.Net;

    //Mysql <-- Please add reference to Mysql.Data.dll
            using MySql.Data.MySqlClient;

3. Add the MysqlConnectionInfo, DatabaseHelper, Query, ServerConnectionInfo classes
4. See examples

Includes
Renci SSH.NET
Mysql.net

Examples


            /*If you want to execute a transaction. Also works with update and delete etc*/
            List<Query> queries = new List<Query>();

            Query query1 = new Query("INSERT INTO `tblstudents`(`firstname`, `middlename`) VALUES (@fname, @mname)");
            query1.getParameters().Add("fname", "query1");
            query1.getParameters().Add("mname", "number1");
            queries.Add(query1);

            Query query2 = new Query("UPDATE tblstudents set firstname=@fname, middlename=@mname where SID=1");
            query2.getParameters().Add("fname", "query2");
            query2.getParameters().Add("mname", "number2");
            queries.Add(query2);


            Query query3 = new Query("INSERT INTO `tblstudents`(`firstname`, `middlename`) VALUES (@fname, @mname)");
            query3.getParameters().Add("fname", "query3");
            query3.getParameters().Add("mname", "number3");
            queries.Add(query3);


            Query query4 = new Query("INSERT INTO `tblstudents`(`firstname`, `middlename`) VALUES (@fname, @mname)");
            query4.getParameters().Add("fname", "query4");
            query4.getParameters().Add("mname", "number4");
            queries.Add(query4);

            helper.executeTransaction(queries);

            /* If you want to execute single query with parameters*/
            Query query5 = new Query("INSERT INTO `tblstudents`(`firstname`, `middlename`) VALUES (@fname, @mname)");
            query5.getParameters().Add("fname", "query5");
            query5.getParameters().Add("mname", "number5");
            helper.executeQuery(query5);

            /*If you want to execute a single query with no parameters; Also works with Update etc*/
            helper.executeQuery("DELETE FROM tblstudents where SID=3");

            /*If you want to display data to datagrid and your query has parameters*/
            Query query6 = new Query("SELECT * FROM tblstudents WHERE SID=@sid");
            query6.getParameters().Add("sid", 6);
            dgvusers.DataSource = helper.getDataTable(query6);


            /*If you want to display data to datagrid and your query has no parameters*/
            Query query7 = new Query("SELECT * FROM tblstudents WHERE SID=6");
            dgvusers.DataSource = helper.getDataTable(query7);
    
