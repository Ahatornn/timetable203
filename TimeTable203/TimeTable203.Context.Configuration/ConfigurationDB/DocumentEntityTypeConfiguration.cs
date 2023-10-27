using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("TDocument");

            builder.Property(x => x.Number)
               .HasMaxLength(8)
               .IsRequired();

            builder.Property(x => x.Series)
              .HasMaxLength(12)
              .IsRequired();

            builder.Property(x => x.IssuedAt)
               .HasDefaultValue(DateTime.Now);

            builder.HasIndex(x => new { x.Number, x.Series })
                .IsUnique()
                .HasDatabaseName($"IX_{nameof(Document)}_" +
                                 $"{nameof(Document.Number)}_" +
                                 $"{nameof(Document.Series)}_" +
                                 $"{nameof(Document.CreatedAt)}")
                .HasFilter($"{nameof(Document.DeletedAt)} is null");
        }
    }
}
