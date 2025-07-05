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
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FromDate).IsRequired();
            builder.Property(c => c.ToDate).IsRequired();
            builder.HasOne(c => c.User)
                   .WithMany(u => u.CartItems)
                   .HasForeignKey(ForeignKeys.UserId);
            builder.HasOne(c => c.Room)
                   .WithMany(r => r.CartItems)
                   .HasForeignKey(ForeignKeys.RoomId);
        }
    }
}
