using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entites;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable(TableNames.Rooms);
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Number)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(r => r.CreatedAt)
                   .IsRequired();
            builder.HasOne(r => r.RoomClass)
               .WithMany()
               .HasForeignKey(r=>r.RoomClassId);
            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}