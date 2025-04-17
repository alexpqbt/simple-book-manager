using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.DataAccess
{
    internal class DatabaseSingleton
    {
        private static DatabaseSingleton? _instance;
        private readonly SQLiteConnection _connection;
        
        public DatabaseSingleton()
        {
            string databasePath = Path.Combine(Application.StartupPath, "Database", "library.db");
            string connectionString = $"Data Source={Path.GetFullPath(databasePath)};Version=3;";
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }

        public static DatabaseSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseSingleton();
                }
                return _instance;
            }
        }

        public SQLiteConnection Connection => _connection;
    }
}
