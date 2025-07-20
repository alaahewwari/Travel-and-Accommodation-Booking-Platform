using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableNames.Roles);
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired();
            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}