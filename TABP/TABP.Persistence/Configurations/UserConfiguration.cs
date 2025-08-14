using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Domain.Constants;
namespace TABP.Persistence.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.Users);
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(u => u.LastName)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasIndex(u => u.Username)
                   .IsUnique();
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(255);
            builder.HasIndex(u => u.Email)
                   .IsUnique();
            builder.Property(u => u.PasswordHash)
                   .IsRequired();
            builder.Property(u=>u.Salt)
                   .IsRequired();
            builder.Property(u => u.CreatedAt)
                   .IsRequired();
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}