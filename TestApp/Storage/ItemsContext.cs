using Microsoft.EntityFrameworkCore;
using Storage.Models;

namespace Storage
{
    /// <summary>
    /// ItemsContext
    /// </summary>
    public class ItemsContext : DbContext
    {
        /// <summary>
        /// OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Items.db");
        }

        /// <summary>
        /// Items
        /// </summary>
        public DbSet<ItemEntity> Items { set; get; }
    }
}