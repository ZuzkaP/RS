using RS.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RS.Models
{
    public class SQLConnection
    {
        private MainDB database;
        private static SQLConnection instance = new SQLConnection();

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SQLConnection Instance { get { return instance; } }

        /// <summary>
        /// Get current session.
        /// </summary>
        public MainDB Database { get { return database; } }

        private SQLConnection()
        {
            database = new MainDB();
        }
    }
}