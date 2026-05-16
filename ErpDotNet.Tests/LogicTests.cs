using ErpDotNet.Logic;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ErpDotNet.Tests
{
    [TestClass]
    public sealed class LogicTests
    {
        [TestMethod]
        public void Create_SQLite_Database_Without_File_Path_And_Get_Exception()
        {
            // Arrange + Act
            Action act = () => DatabaseAdministration.CreateSqliteDatabase("");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("File path is missing.");
        }

        [TestMethod]
        public void Create_SQLite_Database_With_Valid_File_Path_And_Database_File_Exists()
        {
            // Arrange
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string filePath = Path.Join(path, "sqlite1.db");

            if (File.Exists(filePath))
                File.Delete(filePath);

            // Act
            var context = DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();

            // Clean up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void Create_SQLite_Database_With_File_Path_For_Existing_File_And_Get_Exception()
        {
            // Arrange
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string filePath = Path.Join(path, "sqlite2.db");

            if (File.Exists(filePath))
                File.Delete(filePath);

            var context = DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Act
            Action act = () => DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("File already exists.");

            // Clean up
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void Open_SQLite_Database_Without_File_Path_And_Get_Exception()
        {
            // Arrange + Act
            Action act = () => DatabaseAdministration.OpenSqliteDatabase("");

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("File path is missing.");
        }

        [TestMethod]
        public void Open_Not_Existing_SQLite_Database_And_Get_Exception()
        {
            // Arrange
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string filePath = Path.Join(path, "sqlite3.db");

            // Act
            Action act = () => DatabaseAdministration.OpenSqliteDatabase(filePath);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("File does not exist.");
        }

        [TestMethod]
        public void Open_Existing_SQLite_Database_And_Get_Open_Connection()
        {
            // Arrange
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string filePath = Path.Join(path, "sqlite4.db");

            if (File.Exists(filePath))
                File.Delete(filePath);

            DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Act
            var context = DatabaseAdministration.OpenSqliteDatabase(filePath);

            // Assert
            context.Database.GetDbConnection().State.Should().Be(System.Data.ConnectionState.Open);

            // Clean up
            context.Database.CloseConnection();
            context.Database.EnsureDeleted();
        }
    }
}