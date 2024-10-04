using Microsoft.EntityFrameworkCore;
using NFT.Core.Entities;

namespace NFT.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<NftItem> NftItems { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<HistoryLog> HistoryLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
        }

    }

}