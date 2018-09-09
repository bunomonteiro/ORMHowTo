using System.IO;

namespace ORMHowTo.App
{
    public class Config
    {
        #region Paths
        private static string _databasePath;
        public static string DatabasePath => _databasePath ?? (_databasePath = Path.Combine(Directory.GetCurrentDirectory(), "App", "chinook.db"));

        private static string _connectionString;
        public static string ConnectionString => _connectionString ?? (_connectionString = $"Data Source={DatabasePath};Version=3;");
        #endregion Paths
    }
}
