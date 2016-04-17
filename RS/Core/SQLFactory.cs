using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RS.Models
{
    public class SQLFactory
    {
        private SqlConnection sqlConnection;
        private static SQLFactory instance = new SQLFactory();

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SQLFactory Instance { get { return instance; } }

        /// <summary>
        /// Get current session.
        /// </summary>
        public SqlConnection CurrentSession { get { return sqlConnection; } }

        private SQLFactory()
        {
            CreateSession();
        }

        private void CreateSession()
        {
            string connectionString = String.Format(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\{0}\Documents\Visual Studio 2015\Projects\RS\RS\App_Data\Database1.mdf';Integrated Security=True", getComputerName());
            this.sqlConnection = new SqlConnection(connectionString);
            this.sqlConnection.Disposed += OnDispose;
        }

        private void OnDispose(object sender, EventArgs e)
        {
            CreateSession();
        }

        private string getComputerName()
        {
            return Environment.UserName;
        }

        private void AddParameters(SqlCommand cmd, params QueryParameter[] array)
        {
            if (array != null)
            {
                foreach (QueryParameter parameter in array)
                {
                    cmd.Parameters.Add(parameter.Build());
                }
            }
        }

        private void OpenConnectionIfNeeded()
        {
            if (sqlConnection.State != System.Data.ConnectionState.Open)
            {
                sqlConnection.Open();
            }
        }

        public SqlDataReader SelectFromWhere(string what, string from, string where, params QueryParameter[] array)
        {
            string _sql = String.Format(@"SELECT {0} FROM {1} WHERE {2} ", what, from, where);
            var cmd = new SqlCommand(_sql, sqlConnection);

            OpenConnectionIfNeeded();

            // Add parameters to the command
            AddParameters(cmd, array);
            return cmd.ExecuteReader();
        }

        public object SelectFromWhereScalar(string what, string from, string where, params QueryParameter[] array)
        {
            string _sql = String.Format(@"SELECT {0} FROM {1} WHERE {2} ", what, from, where);
            var cmd = new SqlCommand(_sql, sqlConnection);

            OpenConnectionIfNeeded();

            // Add parameters to the command
            AddParameters(cmd, array);
            return cmd.ExecuteScalar();
        }

        public int InsertInto(string to, string pformat, string values, params QueryParameter[] array)
        {
            string _sql = String.Format(@"INSERT INTO {0}({1}) VALUES ({2})", to, pformat, values);
            var cmd = new SqlCommand(_sql, sqlConnection);

            OpenConnectionIfNeeded();

            // Add parameters to the command 
            AddParameters(cmd, array);
            return cmd.ExecuteNonQuery();
        }
    }
}