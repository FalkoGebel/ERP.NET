using ErpDotNet.Logic;
using FluentAssertions;

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
            string filePath = Path.Join(path, "sqlite.db");

            if (File.Exists(filePath))
                File.Delete(filePath);

            // Act
            var context = DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Assert
            File.Exists(filePath).Should().BeTrue();
        }

        [TestMethod]
        public void Create_SQLite_Database_With_File_Path_For_Existing_File_And_Get_Exception()
        {
            // Arrange
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string filePath = Path.Join(path, "sqlite.db");

            if (File.Exists(filePath))
                File.Delete(filePath);

            var context = DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Arrange + Act
            Action act = () => DatabaseAdministration.CreateSqliteDatabase(filePath);

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("File already exists.");
        }
    }
}