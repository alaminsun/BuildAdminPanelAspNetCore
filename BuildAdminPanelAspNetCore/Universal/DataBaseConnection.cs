using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Systems.Universal
{
    public class DataBaseConnection
    {

        string connectionString = "";
        public DataBaseConnection()
        {
            SAConnStrReader();
        }
        public string SAConnStrReader()
        {
            //connectionString = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.128.110)(PORT = 1521)))(CONNECT_DATA =(SID = sildb1)(SERVER = DEDICATED)));User Id=spl_srms;Password=splsrms";
            //connectionString = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.3.8.62)(PORT = 1521)))(CONNECT_DATA =(SID = silsqadb1)(SERVER = DEDICATED)));User Id=spl_srms;Password=splsrms";
            //connectionString = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.189.15)(PORT = 1521)))(CONNECT_DATA =(SID = srmsdb1)(SERVER = DEDICATED)));User Id=spl_srms;Password=splsrms";
            //connectionString = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.3.8.62)(PORT = 1521)))(CONNECT_DATA =(SID = silsqadb1)(SERVER = DEDICATED)));User Id=spl_srms;Password=splsrms";
            //(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.3.8.62)(PORT = 1521)))(CONNECT_DATA =(SID = silsqadb1)(SERVER = DEDICATED)))
            connectionString = "Data Source=SILBAYSW195D;Initial Catalog=SRMS_HOME;User ID=sa;Password=262651;Integrated Security=False;MultipleActiveResultSets=True";
            return connectionString;
        }
    }
}