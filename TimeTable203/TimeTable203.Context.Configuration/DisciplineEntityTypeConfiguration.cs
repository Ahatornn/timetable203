using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration
{
    public class DisciplineEntityTypeConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable("Disciplines");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasMany(x => x.TimeTableItem)
                .WithOne(x => x.Discipline)
                .HasForeignKey(x => x.DisciplineId)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(Discipline.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Discipline)}_{nameof(Discipline.Name)}");
        }
    }
}
