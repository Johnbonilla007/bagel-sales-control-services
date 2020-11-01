using bagel_sales_control.Features.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bagel_sales_control.DataContext.Maps
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.auth0Id);
            builder.Property(u => u.UserName);
        }
    }
}