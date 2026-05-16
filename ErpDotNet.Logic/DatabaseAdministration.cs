using Microsoft.EntityFrameworkCore;

namespace ErpDotNet.Logic
{
    public static class DatabaseAdministration
    {
        /// <summary>
        /// Creates the SQLite database file at the specified path and returns an initialized SqliteContext.
        /// </summary>
        /// <remarks>Calls Database.EnsureCreated() to initialize the database schema.</remarks>
        /// <param name="filePath">Path for the SQLite database file to create.</param>
        /// <returns>An initialized SqliteContext connected to the created database file.</returns>
        /// <exception cref="ArgumentException">If filePath is empty or file already exists.</exception>
        public static SqliteContext CreateSqliteDatabase(string filePath)
        {
            if (filePath == string.Empty)
                throw new ArgumentException(Texts.DatabaseAdministration_CreateSqliteDatabase_MissingFilePath);

            if (File.Exists(filePath))
                throw new ArgumentException(Texts.DatabaseAdministration_CreateSqliteDatabase_FileExists);

            var context = new SqliteContext(filePath);
            context.Database.EnsureCreated();
            return context;
        }

        /// <summary>
        /// Opens the SQLite database file at the specified path and returns an initialized SqliteContext.
        /// </summary>
        /// <remarks>Calls Database.OpenConnection() to open the database connection.</remarks>
        /// <param name="filePath">Path for the SQLite database file to open.</param>
        /// <returns>An initialized SqliteContext connected to the opened database file.</returns>
        /// <exception cref="ArgumentException">If filePath is empty or file does not exist.</exception>
        public static SqliteContext OpenSqliteDatabase(string filePath)
        {
            if (filePath == string.Empty)
                throw new ArgumentException(Texts.DatabaseAdministration_OpenSqliteDatabase_MissingFilePath);

            if (!File.Exists(filePath))
                throw new ArgumentException(Texts.DatabaseAdministration_OpenSqliteDatabase_InvalidFilePath);

            var context = new SqliteContext(filePath);
            context.Database.OpenConnection();
            return context;
        }
    }
}