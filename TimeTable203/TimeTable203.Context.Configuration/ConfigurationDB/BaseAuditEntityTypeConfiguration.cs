using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    public class BaseAuditEntityTypeConfiguration : IEntityTypeConfiguration<BaseAuditEntity>
    {
        public void Configure(EntityTypeBuilder<BaseAuditEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("TAudit");

            builder.Property(x => x.Id)
                .HasDefaultValue(Guid.NewGuid())
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValue(DateTimeOffset.Now)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.UpdatedAt)
              .HasDefaultValue(DateTimeOffset.Now)
              .ValueGeneratedOnUpdate();
        }
    }
}
