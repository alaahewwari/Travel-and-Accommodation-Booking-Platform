using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable(TableNames.Discounts);
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Percentage)
                   .IsRequired();
            builder.Property(d => d.StartDate)
                   .IsRequired();
            builder.Property(d => d.EndDate)
                   .IsRequired();
            builder.Property(d => d.CreatedAt)
                   .IsRequired();
            builder.Property(d => d.UpdatedAt)
                   .IsRequired();
            builder.HasMany(d => d.RoomClasses)
                   .WithOne(rc => rc.Discount)
                   .HasForeignKey(d => d.DiscountId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasQueryFilter(d => !d.IsDeleted);
        }
    }
}