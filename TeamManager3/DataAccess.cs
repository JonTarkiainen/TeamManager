using System;
using SQLite;

namespace TeamManager3
{
    class DataAccess : SQLiteConnection
    {
        private static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "database.db");

        public DataAccess() : base(Path, true)
        {

        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(Path, true);
        }

        public static void CreateDatabase(SQLiteConnection connection)
        {
            CreateTables();
        }

        public static void CreateTables()
        {
            using (var conn = GetConnection())
            {
                conn.CreateTable<Player>();
            }
        }

        public static void Initialize()
        {
            CreateDatabase(GetConnection());
        }
    }
}