using bagel_sales_control.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bagel_sales_control.DataContext.Maps
{
    public class ProductMap
    {
        public ProductMap(EntityTypeBuilder<ProductAgg> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.NameProduct);
            builder.Property(p => p.InitialExistence);
            builder.Property(p => p.Existence);
            builder.Property(p => p.PurchasePrice);
            builder.Property(p => p.SalePrice);
            builder.Property(p => p.Wholesaleprice);
        }
    }
}