using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ORM_Projekt_HAN0334.ORM.DAO
{
    public class Database
    {
        private SqlConnection Connection { get; set; }
        private SqlTransaction SqlTransaction { get; set; }
        public Database()
        {
            Connection = new SqlConnection();
        }

        public bool Connect(string conString)
        {
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.ConnectionString = conString;
                Connection.Open();
            }
            return true;

        }
        public bool Connect()
        {
            bool ret = true;
            if (Connection.State != System.Data.ConnectionState.Open)
            {
                // connection string is stored in file App.config or Web.config
                ret = Connect(ConfigurationManager.ConnectionStrings["ConnectionStringMsSql"].ConnectionString);
            }
            return ret;
        }

        public void Close()
        {
            Connection.Close();
        }

        public SqlCommand CreateCommand(string strCommand)
        {
            SqlCommand command = new SqlCommand(strCommand, Connection);

            if (SqlTransaction != null)
            {
                command.Transaction = SqlTransaction;
            }
            return command;
        }

        /// <summary>
        /// Select encapulated in the command.
        /// </summary>
        public SqlDataReader Select(SqlCommand command)
        {
            SqlDataReader sqlReader = command.ExecuteReader();
            return sqlReader;
        }

        public int ExecuteNonQuery(SqlCommand command)
        {
            int rowNumber = 0;
            try
            {
                rowNumber = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            return rowNumber;
        }
    }
}
