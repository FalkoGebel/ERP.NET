using ErpDotNet.Sqlite;

namespace ErpDotNet.Logic
{
    public static class DatabaseAdministration
    {
        /// <summary>
        /// Creates the SQLite database file at the specified path and returns an initialized ErpContext.
        /// </summary>
        /// <remarks>Calls Database.EnsureCreated() to initialize the database schema.</remarks>
        /// <param name="filePath">Path for the SQLite database file to create.</param>
        /// <returns>An initialized ErpContext connected to the created database file.</returns>
        /// <exception cref="ArgumentException">If filePath is empty or file already exists.</exception>
        public static ErpContext CreateSqliteDatabase(string filePath)
        {
            if (filePath == string.Empty)
                throw new ArgumentException(Texts.DatabaseAdministration_CreateSqliteDatabase_MissingFilePath);

            if (File.Exists(filePath))
                throw new ArgumentException(Texts.DatabaseAdministration_CreateSqliteDatabase_FileExists);

            var context = new ErpContext(filePath);
            context.Database.EnsureCreated();
            return context;
        }
    }
}