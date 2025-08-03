using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(TableNames.Reviews);
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Rating)
                   .IsRequired();
            builder.Property(r => r.Comment)
                   .HasMaxLength(1000);
            builder.Property(r => r.CreatedAt)
                   .IsRequired();
            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(r => r.Hotel)
                   .WithMany()
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}