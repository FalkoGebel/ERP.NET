using Microsoft.EntityFrameworkCore;

namespace ErpDotNet.Sqlite
{
    public class ErpContext(string dbPath) : DbContext
    {
        public string DbPath { get; set; } = dbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}