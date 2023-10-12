using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Systems.Universal
{
    public class SaHelper
    {
        //public DataTable DataTableRefCursorFn1(string ConnString, string funName, string FieldName, string FieldValue)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection objConn = new SqlConnection(ConnString))
        //    {
        //        using (SqlCommand objCmd = new SqlCommand())
        //        {
        //            objCmd.Connection = objConn;
        //            objCmd.CommandText = funName;
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add(FieldName, SqlDbType.VarChar).Value = FieldValue;
        //            objCmd.Parameters.Add("ReturnValue", SqlDbType.Cursor).Direction = ParameterDirection.ReturnValue;
        //            objConn.Open();
        //            objCmd.ExecuteNonQuery();
        //            using (OracleDataReader rdr = objCmd.ExecuteReader())
        //            {
        //                if (rdr.HasRows)
        //                {
        //                    dt.Load(rdr);
        //                }
        //            }
        //        }
        //    }
        //    return dt;
        //}
        public DataTable DataTableRefCursorFn1(string ConnString, string funName, string FieldName, string FieldValue)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(funName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.Add(new SqlParameter(FieldName, SqlDbType.VarChar) { Value = FieldValue });

                    // Add output parameter for cursor-like functionality
                    cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                    connection.Open();

                    // Execute the stored procedure
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Load the DataTable from the SqlDataReader
                            dt.Load(reader);
                        }
                    }
                }
            }

            return dt;
        }
        //public DataTable DataTableRefCursorFn2(string ConnString, string funName, string FieldName1, string FieldName2, string FieldValue1, string FieldValue2)
        //{
        //    DataTable dt = new DataTable();
        //    using (OracleConnection objConn = new OracleConnection(ConnString))
        //    {

        //        using (OracleCommand objCmd = new OracleCommand())
        //        {
        //            objCmd.Connection = objConn;
        //            objCmd.CommandText = funName;
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            objCmd.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;
        //            objCmd.Parameters.Add("ReturnValue", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
        //            objConn.Open();
        //            objCmd.ExecuteNonQuery();
        //            using (OracleDataReader rdr = objCmd.ExecuteReader())
        //            {
        //                if (rdr.HasRows)
        //                {
        //                    dt.Load(rdr);
        //                }
        //            }
        //        }
        //    }
        //    return dt;
        //}

        public DataTable DataTableRefCursorFn2(string ConnString, string funName, string FieldName1, string FieldName2, string FieldValue1, string FieldValue2)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(funName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                    cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });

                    // Add output parameter for cursor-like functionality
                    cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                    connection.Open();

                    // Execute the stored procedure
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Load the DataTable from the SqlDataReader
                            dt.Load(reader);
                        }
                    }
                }
            }

            return dt;
        }
        //public DataTable DataTableRefCursorFn3(string ConnString, string funName, string FieldName1, string FieldName2, string FieldName3, string FieldValue1, string FieldValue2, string FieldValue3)
        //{
        //    DataTable dt = new DataTable();
        //    using (OracleConnection objConn = new OracleConnection(ConnString))
        //    {

        //        using (OracleCommand objCmd = new OracleCommand())
        //        {
        //            objCmd.Connection = objConn;
        //            objCmd.CommandText = funName;
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            objCmd.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            objCmd.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;
        //            objCmd.Parameters.Add(FieldName3, OracleType.VarChar).Value = FieldValue3;
        //            objCmd.Parameters.Add("ReturnValue", OracleType.Cursor).Direction = ParameterDirection.ReturnValue;
        //            objConn.Open();
        //            objCmd.ExecuteNonQuery();
        //            using (OracleDataReader rdr = objCmd.ExecuteReader())
        //            {
        //                if (rdr.HasRows)
        //                {
        //                    dt.Load(rdr);
        //                }
        //            }
        //        }
        //    }
        //    return dt;
        //}

        public DataTable DataTableRefCursorFn3(string ConnString, string funName, string FieldName1, string FieldName2, string FieldName3, string FieldValue1, string FieldValue2, string FieldValue3)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(funName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add input parameter
                    cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                    cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });
                    cmd.Parameters.Add(new SqlParameter(FieldName3, SqlDbType.VarChar) { Value = FieldValue3 });

                    // Add output parameter for cursor-like functionality
                    cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue });

                    connection.Open();

                    // Execute the stored procedure
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {
                            // Load the DataTable from the SqlDataReader
                            dt.Load(reader);
                        }
                    }
                }
            }

            return dt;
        }
        //public Boolean ExecuteFn(string ConnString, string Qry)
        //{
        //    bool isTrue = false;
        //    using (OracleConnection con = new OracleConnection(ConnString))
        //    {
        //        OracleCommand cmd = new OracleCommand(Qry, con);
        //        con.Open();
        //        int noOfRows = cmd.ExecuteNonQuery();

        //        if (noOfRows > 0)
        //        {
        //            isTrue = true;

        //        }
        //    }
        //    return isTrue;
        //}

        public Boolean ExecuteFn(string ConnString, string Qry)
        {
            bool isTrue = false;

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(Qry, connection))
                {
                    try
                    {
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isTrue = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return isTrue;
        }

        //public Boolean ProcedureExecuteFn1(string ConnString, string Qry, string SPName, string FieldName, string FieldValue)
        //{
        //    bool isTrue = false;
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName, OracleType.VarChar).Value = FieldValue;
        //            oracleConnection.Open();
        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {

        //                isTrue = true;

        //            }
        //        }
        //    }
        //    return isTrue;
        //}

        public Boolean ProcedureExecuteFn1(string ConnString, string Qry, string SPName, string FieldName, string FieldValue)
        {
            bool isSuccessful = false;

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameter
                        cmd.Parameters.Add(new SqlParameter(FieldName, SqlDbType.VarChar) { Value = FieldValue });

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return isSuccessful;
        }

        //public Boolean ProcedureExecuteFn2(string ConnString, string SPName, string FieldName1, string FieldName2, string FieldValue1, string FieldValue2)
        //{
        //    bool isTrue = false;
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            oracleCommand.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;

        //            oracleConnection.Open();

        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {
        //                isTrue = true;
        //            }
        //        }
        //    }
        //    return isTrue;
        //}

        public Boolean ProcedureExecuteFn2(string ConnString, string SPName, string FieldName1, string FieldName2, string FieldValue1, string FieldValue2)
        {
            bool isSuccessful = false;

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameter
                        cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                        cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return isSuccessful;
        }

        //public Boolean ProcedureExecuteFn3(string ConnString, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldValue1, string FieldValue2, string FieldValue3)
        //{
        //    bool isTrue = false;
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            oracleCommand.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;
        //            oracleCommand.Parameters.Add(FieldName3, OracleType.VarChar).Value = FieldValue3;
        //            oracleConnection.Open();

        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {
        //                isTrue = true;
        //            }
        //        }
        //    }
        //    return isTrue;
        //}

        public Boolean ProcedureExecuteFn3(string ConnString, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldValue1, string FieldValue2, string FieldValue3)
        {
            bool isSuccessful = false;

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameter
                        cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                        cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });
                        cmd.Parameters.Add(new SqlParameter(FieldName3, SqlDbType.VarChar) { Value = FieldValue3 });
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return isSuccessful;
        }
        public List<MenuItemView> GetMenuItem(DataTable dt)
        {
            //Select SLID,ID,NodeName,NodeLevel,RefID  FROM Sa_Menu Order By SLID";
            List<MenuItemView> item;
            //using lamdaexpression
            item = (from DataRow row in dt.Rows
                    select new MenuItemView
                    {
                        MenuID = Convert.ToInt16(row["SLID"].ToString()),
                        MenuName = row["NodeName"].ToString(),
                        ParentID = Convert.ToInt16(row["NodeLevel"].ToString()),
                        FormURL = row["FormURL"].ToString(),

                    }).ToList();

            return item;
        }
        //public string GetValueFn(string ConnString, string Qry)
        //{
        //    string Value = "";
        //    using (OracleConnection odbcConnection = new OracleConnection(ConnString))
        //    {
        //        odbcConnection.Open();
        //        using (OracleCommand odbcCommand = new OracleCommand(Qry, odbcConnection))
        //        {
        //            using (OracleDataReader rdr = odbcCommand.ExecuteReader())
        //            {
        //                if (rdr.Read())
        //                {
        //                    Value = rdr[0].ToString();
        //                }
        //            }
        //        }
        //    }
        //    return Value;
        //}

        public string GetValueFn(string ConnString, string Qry)
        {
            string value = string.Empty;

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(Qry, connection))
                {
                    try
                    {
                        connection.Open();
                        var result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            value = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return value;
        }

        //public DataTable DataTableFn(string ConnString, string Qry)
        //{
        //    DataTable dt = new DataTable();
        //    using (OracleConnection objConn = new OracleConnection(ConnString))
        //    {
        //        OracleCommand objCmd = new OracleCommand();
        //        objCmd.CommandText = Qry;
        //        objCmd.Connection = objConn;
        //        objConn.Open();
        //        objCmd.ExecuteNonQuery();
        //        using (OracleDataReader rdr = objCmd.ExecuteReader())
        //        {
        //            if (rdr.HasRows)
        //            {
        //                dt.Load(rdr);
        //            }
        //        }
        //    }

        //    return dt;
        //}

        public DataTable DataTableFn(string ConnString, string Qry)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(Qry, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return dt;
        }


        //public Tuple<Boolean, string, string> ProcedureExecuteTFn2(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldValue1, string FieldValue2)
        //{
        //    bool isTrue = false;
        //    string MaxID = "";
        //    string IUMode = "";
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            oracleCommand.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;

        //            oracleConnection.Open();

        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {
        //                isTrue = true;
        //                if (FieldValue1 == "" || FieldValue1 == null || FieldValue1 == "0")
        //                {
        //                    IUMode = "I";
        //                    MaxID = GetValueFn(ConnString, Qry);
        //                }
        //                else
        //                {
        //                    IUMode = "U";
        //                    MaxID = FieldValue1;
        //                }

        //            }
        //        }
        //    }
        //    return Tuple.Create(isTrue, MaxID, IUMode);
        //}

        public Tuple<Boolean, string, string> ProcedureExecuteTFn2(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldValue1, string FieldValue2)
        {
            bool isSuccessful = false;
            string maxID = "";
            string iuMode = "";

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                        cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;

                            if (string.IsNullOrEmpty(FieldName1) || FieldName2 == "0")
                            {
                                iuMode = "I";
                                maxID = GetValueFn(ConnString, Qry);
                            }
                            else
                            {
                                iuMode = "U";
                                maxID = FieldName1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return Tuple.Create(isSuccessful, maxID, iuMode);
        }

        //public Tuple<Boolean, string, string> ProcedureExecuteTFn3(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldValue1, string FieldValue2, string FieldValue3)
        //{
        //    bool isTrue = false;
        //    string MaxID = "11";
        //    string IUMode = "";
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            oracleCommand.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;
        //            oracleCommand.Parameters.Add(FieldName3, OracleType.VarChar).Value = FieldValue3;
        //            oracleConnection.Open();

        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {
        //                isTrue = true;
        //                if (FieldValue1 == "" || FieldValue1 == null || FieldValue1=="0")
        //                {
        //                    IUMode = "I";
        //                    MaxID = GetValueFn(ConnString, Qry);
        //                }
        //                else
        //                {
        //                    IUMode = "U";
        //                    MaxID = FieldValue1;
        //                }
        //            }
        //        }
        //    }
        //    return Tuple.Create(isTrue, MaxID, IUMode);
        //}



        public Tuple<Boolean, string, string> ProcedureExecuteTFn3(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldValue1, string FieldValue2, string FieldValue3)
        {
            bool isSuccessful = false;
            string maxID = "";
            string iuMode = "";

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                        cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });
                        cmd.Parameters.Add(new SqlParameter(FieldName3, SqlDbType.VarChar) { Value = FieldValue3 });

                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;

                            if (string.IsNullOrEmpty(FieldName1) || FieldName2 == "0")
                            {
                                iuMode = "I";
                                maxID = GetValueFn(ConnString, Qry);
                            }
                            else
                            {
                                iuMode = "U";
                                maxID = FieldName1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return Tuple.Create(isSuccessful, maxID, iuMode);
        }


        //public Tuple<Boolean, string, string> ProcedureExecuteTFn4(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldName4, string FieldValue1, string FieldValue2, string FieldValue3, string FieldValue4)
        //{
        //    bool isTrue = false;
        //    string MaxID = "";
        //    string IUMode = "";
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            oracleCommand.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;
        //            oracleCommand.Parameters.Add(FieldName3, OracleType.VarChar).Value = FieldValue3;
        //            oracleCommand.Parameters.Add(FieldName4, OracleType.VarChar).Value = FieldValue4;
        //            oracleConnection.Open();

        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {
        //                isTrue = true;
        //                if (FieldValue1 == "" || FieldValue1 == null || FieldValue1 == "0")
        //                {
        //                    IUMode = "I";
        //                    MaxID = GetValueFn(ConnString, Qry);
        //                }
        //                else
        //                {
        //                    IUMode = "U";
        //                    MaxID = FieldValue1;
        //                }
        //            }
        //        }
        //    }
        //    return Tuple.Create(isTrue, MaxID, IUMode);
        //}
        public Tuple<Boolean, string, string> ProcedureExecuteTFn4(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldName4, string FieldValue1, string FieldValue2, string FieldValue3, string FieldValue4)
        {
            bool isSuccessful = false;
            string maxID = "";
            string iuMode = "";

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                        cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });
                        cmd.Parameters.Add(new SqlParameter(FieldName3, SqlDbType.VarChar) { Value = FieldValue3 });
                        cmd.Parameters.Add(new SqlParameter(FieldName4, SqlDbType.VarChar) { Value = FieldValue4 });


                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;

                            if (string.IsNullOrEmpty(FieldName1) || FieldName2 == "0")
                            {
                                iuMode = "I";
                                maxID = GetValueFn(ConnString, Qry);
                            }
                            else
                            {
                                iuMode = "U";
                                maxID = FieldName1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return Tuple.Create(isSuccessful, maxID, iuMode);
        }
        //public Tuple<Boolean, string, string> ProcedureExecuteTFn5(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldName4, string FieldName5, string FieldValue1, string FieldValue2, string FieldValue3, string FieldValue4, string FieldValue5)
        //{
        //    bool isTrue = false;
        //    string MaxID = "";
        //    string IUMode = "";
        //    using (OracleConnection oracleConnection = new OracleConnection(ConnString))
        //    {
        //        using (OracleCommand oracleCommand = new OracleCommand())
        //        {
        //            oracleCommand.Connection = oracleConnection;
        //            oracleCommand.CommandText = SPName;
        //            oracleCommand.CommandType = CommandType.StoredProcedure;
        //            oracleCommand.Parameters.Add(FieldName1, OracleType.VarChar).Value = FieldValue1;
        //            oracleCommand.Parameters.Add(FieldName2, OracleType.VarChar).Value = FieldValue2;
        //            oracleCommand.Parameters.Add(FieldName3, OracleType.VarChar).Value = FieldValue3;
        //            oracleCommand.Parameters.Add(FieldName4, OracleType.VarChar).Value = FieldValue4;
        //            oracleCommand.Parameters.Add(FieldName5, OracleType.VarChar).Value = FieldValue5;
        //            oracleConnection.Open();

        //            if (oracleCommand.ExecuteNonQuery() > 0)
        //            {
        //                isTrue = true;
        //                if (FieldValue1 == "" || FieldValue1 == null || FieldValue1 == "0")
        //                {
        //                    IUMode = "I";
        //                    MaxID = GetValueFn(ConnString, Qry);
        //                }
        //                else
        //                {
        //                    IUMode = "U";
        //                    MaxID = FieldValue1;
        //                }
        //            }
        //        }
        //    }
        //    return Tuple.Create(isTrue, MaxID, IUMode);
        //}


        public Tuple<Boolean, string, string> ProcedureExecuteTFn5(string ConnString, string Qry, string SPName, string FieldName1, string FieldName2, string FieldName3, string FieldName4, string FieldName5, string FieldValue1, string FieldValue2, string FieldValue3, string FieldValue4, string FieldValue5)
        {
            bool isSuccessful = false;
            string maxID = "";
            string iuMode = "";

            using (SqlConnection connection = new SqlConnection(ConnString))
            {
                using (SqlCommand cmd = new SqlCommand(SPName, connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        cmd.Parameters.Add(new SqlParameter(FieldName1, SqlDbType.VarChar) { Value = FieldValue1 });
                        cmd.Parameters.Add(new SqlParameter(FieldName2, SqlDbType.VarChar) { Value = FieldValue2 });
                        cmd.Parameters.Add(new SqlParameter(FieldName3, SqlDbType.VarChar) { Value = FieldValue3 });
                        cmd.Parameters.Add(new SqlParameter(FieldName4, SqlDbType.VarChar) { Value = FieldValue4 });
                        cmd.Parameters.Add(new SqlParameter(FieldName5, SqlDbType.VarChar) { Value = FieldValue5 });


                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSuccessful = true;

                            if (string.IsNullOrEmpty(FieldName1) || FieldName2 == "0")
                            {
                                iuMode = "I";
                                maxID = GetValueFn(ConnString, Qry);
                            }
                            else
                            {
                                iuMode = "U";
                                maxID = FieldName1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here
                        // For simplicity, we'll just rethrow the exception
                        throw ex;
                    }
                }
            }

            return Tuple.Create(isSuccessful, maxID, iuMode);
        }


    }
}