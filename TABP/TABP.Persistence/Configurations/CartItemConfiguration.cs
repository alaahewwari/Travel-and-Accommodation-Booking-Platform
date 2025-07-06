using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entites;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable(TableNames.CartItems);
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.FromDate).IsRequired();
            builder.Property(ci => ci.ToDate).IsRequired();
            builder.HasOne(ci => ci.User)
                   .WithMany()
                   .HasForeignKey(ci=>ci.UserId);
            builder.HasOne(ci => ci.Room)
                   .WithMany()
                   .HasForeignKey(ci => ci.RoomId);
        }
    }
}
