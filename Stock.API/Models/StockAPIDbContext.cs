using Microsoft.EntityFrameworkCore;

namespace Stock.API.Models
{
    public class StockAPIDbContext:DbContext
    {
        public StockAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Stock.API.Models.Entities.Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock.API.Models.Entities.Stock>().HasData(
                new Stock.API.Models.Entities.Stock { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Count = 2000 },
                new Stock.API.Models.Entities.Stock { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Count = 1000 },
                new Stock.API.Models.Entities.Stock { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Count = 3000 },
                new Stock.API.Models.Entities.Stock { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Count = 5000 },
                new Stock.API.Models.Entities.Stock { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Count = 500 }
                );
        }
    }
}
