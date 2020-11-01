using bagel_sales_control.Features.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bagel_sales_control.DataContext.Maps
{
    public class SalesMap
    {
        public SalesMap(EntityTypeBuilder<Sales> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.SaleId);
            builder.HasKey(s => s.ProductId);
            builder.Property(s => s.SoldQuantity);
            builder.Property(s => s.TypeSale);
            builder.Property(s => s.Total);
            builder.Property(s => s.Commentary);
        }
    }
}