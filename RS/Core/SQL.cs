using RS.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RS.Models
{
    public class SQL
    {
        private MainDB database;
        private static SQL instance = new SQL();

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SQL Instance { get { return instance; } }

        /// <summary>
        /// Get current session.
        /// </summary>
        public MainDB Database { get { return database; } }

        private SQL()
        {
            database = new MainDB();
        }
    }
}