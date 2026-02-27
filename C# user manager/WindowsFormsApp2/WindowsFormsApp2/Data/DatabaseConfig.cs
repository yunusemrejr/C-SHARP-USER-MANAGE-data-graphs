using System;
using System.Configuration;

namespace WindowsFormsApp2.Data
{
    /// <summary>
    /// Manages database configuration and connection strings
    /// </summary>
    public static class DatabaseConfig
    {
        private static string _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"]?.ConnectionString
                        ?? "server=localhost;user=root;password=pass;port=3306;database=users;";
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static void SetConnectionString(string server, string user, string password, string database, int port = 3306)
        {
            _connectionString = $"server={server};user={user};password={password};port={port};database={database};";
        }
    }
}
