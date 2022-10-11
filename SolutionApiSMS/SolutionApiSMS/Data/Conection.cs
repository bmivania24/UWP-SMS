using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ApiSMS.Data
{
    public class Conection
    {
        SqlConnection con = new SqlConnection();

        public SqlConnection OpenConnection()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=DB_SMS;Integrated Security=True";


            try

            {
                // open connection
                con = new SqlConnection(connectionString);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public SqlConnection CloseConnection()
        {

            try
            {
                //close connection
                con.Close();
                return con;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}