using ErpDotNet.Repository;
using Microsoft.EntityFrameworkCore;

namespace ErpDotNet.Logic
{
    public class ErpContext() : DbContext
    {
        public DbSet<Item> Item { get; set; }
    }
}