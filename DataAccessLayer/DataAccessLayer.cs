
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Reflection;
using System.Collections.Generic;
using System.Text;



namespace Track.DAL
{

    public class DataAccess
    {
        private static string ConnString = ConfigurationManager.ConnectionStrings["ConnStringDb"].ToString();
        // SqlConnection connection;





        private static SqlConnection OpenConnection()
        {
            try
            {

                SqlConnection connection = new SqlConnection(ConnString);
                connection.Open();
                return connection;
            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
            }

        }

        private static void CloseConnection(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                    connection.Close();
            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
            }
        }
       
        public static DataSet ExecuteDataset(string query)
        {

            SqlCommand cmd;
            DataSet Dset = new DataSet();
            SqlConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd = new SqlCommand(query, connection);

                SqlDataAdapter Da = new SqlDataAdapter(cmd);
                Da.Fill(Dset);
                //
                //value = (Int32) cmd.ExecuteScalar();
            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return Dset;
        }

        public static DataSet ExecuteDataset(Dictionary<string, object> dicParams, string query)
        {

            SqlCommand cmd;
            DataSet Dset = new DataSet();
            SqlConnection connection = null;
            try
            {


                connection = OpenConnection();
                cmd = new SqlCommand(query, connection);

                foreach (KeyValuePair<string, object> key in dicParams)
                {
                    if (query.Contains(":" + key.Key))
                    {
                        cmd.Parameters.AddWithValue(key.Key, key.Value);
                    }
                }
                SqlDataAdapter Da = new SqlDataAdapter(cmd);
                Da.Fill(Dset);


            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return Dset;
        }
    
     

        public static int ExecuteFinalQuery(Dictionary<string, object> paramValues, string query)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = null;
            int rval;
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                rval = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return rval;

        }


        public static DataSet ExecuteProcedure(SqlParameter[] parameters, string procedureName)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da;
            SqlConnection connection = null;

            try
            {
                connection = OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter pm in parameters)
                {
                    cmd.Parameters.Add(pm);
                }
                cmd.CommandText = procedureName;
                cmd.Connection = connection;

                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);

            }
            return ds;
        }
  

        
        public static object ExecuteQueryScalar(Dictionary<string, object> dicParams, string query)
        {
            SqlCommand cmd;
            object value;
            SqlConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd = new SqlCommand(query, connection);
                foreach (KeyValuePair<string, object> key in dicParams)
                {
                    if (query.Contains(":" + key.Key))
                    {
                        if (key.Value != null)
                        {
                            cmd.Parameters.AddWithValue(key.Key, key.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(key.Key, DBNull.Value);
                        }
                    }
                }

                value = cmd.ExecuteScalar();
            }
            catch (SqlException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return value;
        }
   

    }
}
