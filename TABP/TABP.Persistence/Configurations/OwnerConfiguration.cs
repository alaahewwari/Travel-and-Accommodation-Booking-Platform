using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entites;
using TABP.Persistence.Constants;
namespace TABP.Persistence.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable(TableNames.Owners);
            builder.HasKey(o => o.Id);
            builder.Property(o => o.FirstName).IsRequired();
            builder.Property(o => o.LastName).IsRequired();
            builder.Property(o => o.PhoneNumber).IsRequired();
            builder.HasQueryFilter(o => !o.IsDeleted);
        }
    }
}