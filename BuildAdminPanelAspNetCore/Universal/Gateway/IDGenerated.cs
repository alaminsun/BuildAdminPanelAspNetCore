
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Systems.Universal;

namespace RMS_Square.Universal.Gateway
{
    public class IDGenerated
    {
        DataBaseConnection dbConnection = new DataBaseConnection();
        SaHelper dbHelper = new SaHelper();
        //public Int64 getMAXSL( string tableName,string columnName)
        //{
        //    Int64 MAXID = 0;
        //    string QueryString = "select NVL(MAX(" + columnName + "),0)+1 id from " + tableName + "";
        //    using (OracleConnection oracleConnection = new OracleConnection(dbConnection.SAConnStrReader()))
        //    {
        //        oracleConnection.Open();
        //        using (OracleCommand oracleCommand = new OracleCommand(QueryString, oracleConnection))
        //        {
        //            using (OracleDataReader rdr = oracleCommand.ExecuteReader())
        //            {
        //                if (rdr.Read())
        //                {
        //                    MAXID = Convert.ToInt64(rdr["id"].ToString());
        //                }
        //            }
        //        }
        //    }
        //    return MAXID;
        //}

    public long getMAXSL(string tableName, string columnName)
    {
        long maxId = 0;

        string queryString = $"SELECT ISNULL(MAX([{columnName}]), 0) + 1 AS id FROM [{tableName}]";

        using (SqlConnection connection = new SqlConnection(dbConnection.SAConnStrReader()))
        {
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.CommandType = CommandType.Text;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            maxId = Convert.ToInt64(reader["id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }
            }
        }

        return maxId;
    }

    //public string getMAXID(string tableName, string columnName, string fm9)
    //    {
    //        string MAXID = "";
    //        string QueryString = "select to_char((select NVL(MAX(" + columnName + "),0)+1 from " + tableName + " ), '" + fm9 + "') id from dual";
    //        using (OracleConnection oracleConnection = new OracleConnection(dbConnection.SAConnStrReader()))
    //        {
    //            oracleConnection.Open();
    //            using (OracleCommand oracleCommand = new OracleCommand(QueryString, oracleConnection))
    //            {
    //                using (OracleDataReader rdr = oracleCommand.ExecuteReader())
    //                {
    //                    if (rdr.Read())
    //                    {
    //                        MAXID = rdr[0].ToString();
    //                    }
    //                }
    //            }
    //        }
    //        return MAXID;
    //    }

    public string GetMaxId(string tableName, string columnName, string format)
    {
        string maxId = string.Empty;

        string queryString = $"SELECT CONVERT(NVARCHAR, ISNULL(MAX([{columnName}]), 0) + 1, '{format}') AS id FROM [{tableName}]";

        using (SqlConnection connection = new SqlConnection(dbConnection.SAConnStrReader()))
        {
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                command.CommandType = CommandType.Text;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            maxId = reader["id"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }
            }
        }

        return maxId;
    }



    //public String GetLanIPAddress()
    //    {
    //        //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a
    //        //client connecting to a web server through an HTTP proxy or load balancer
    //        String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //        if (string.IsNullOrEmpty(ip))
    //        {
    //            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    //        }
    //        if (ip == "::1")
    //        {
    //            ip = "127.0.0.1";
    //        }
    //        return ip;
    //    }

    public string GetLanIPAddress(HttpContext httpContext)
    {
        // The X-Forwarded-For (XFF) header is a de facto standard for identifying the originating IP address
        string ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (string.IsNullOrEmpty(ip))
        {
            ip = httpContext.Connection.RemoteIpAddress?.ToString();
        }

        // If IP is "::1", it's localhost
        if (ip == "::1")
        {
            ip = "127.0.0.1";
        }

        return ip;
    }



}
}