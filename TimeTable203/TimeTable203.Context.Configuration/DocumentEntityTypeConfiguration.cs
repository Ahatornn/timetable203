using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number)
               .HasMaxLength(8)
               .IsRequired();

            builder.Property(x => x.Series)
              .HasMaxLength(12)
              .IsRequired();

            builder.HasIndex(x => new { x.Number, x.Series })
                .IsUnique()
                .HasFilter($"{nameof(Document.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Document)}_{nameof(Document.Number)}_{nameof(Document.Series)}");
        }
    }
}
