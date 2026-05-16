using Microsoft.EntityFrameworkCore;

namespace ErpDotNet.Logic

{
    public class SqliteContext(string dbPath) : ErpContext
    {
        public string DbPath { get; set; } = dbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}