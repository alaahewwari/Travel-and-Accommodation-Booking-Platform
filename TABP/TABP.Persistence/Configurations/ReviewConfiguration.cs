using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(TableNames.Reviews);
            builder.HasKey(r => r.Id);
            builder.Property(h => h.Rating)
                   .IsRequired()
                   .HasDefaultValue(0);
            builder.Property(r => r.Comment)
                   .HasMaxLength(1000);
            builder.Property(r => r.CreatedAt)
                   .IsRequired();
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reviews)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(r => r.Hotel)
                   .WithMany(h => h.Reviews)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}