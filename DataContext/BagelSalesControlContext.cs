using System.Net.WebSockets;
using bagel_sales_control.Features;
using bagel_sales_control.Features.Authentication;
using bagel_sales_control.Features.Sales;
using Microsoft.EntityFrameworkCore;

namespace bagel_sales_control.DataContext
{
    public class BagelSalesControlContext : DbContext
    {
        public BagelSalesControlContext(DbContextOptions<BagelSalesControlContext> options) : base(options)
        {

        }
        public DbSet<ProductAgg> Product { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Sales>().HasOne(t => t.ProductAgg).WithMany(s => s.Sales).HasForeignKey(p => p.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}