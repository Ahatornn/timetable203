using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    public class DisciplineEntityTypeConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable("TDiscipline");

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasDatabaseName($"IX_{nameof(Discipline)}_" +
                                 $"{nameof(Discipline.Name)}_" +
                                 $"{nameof(Discipline.CreatedAt)}")
                .HasFilter($"{nameof(Discipline.DeletedAt)} is null");
        }
    }
}
