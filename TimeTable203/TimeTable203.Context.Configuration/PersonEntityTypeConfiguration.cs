using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(80);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(80);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();

            builder
                .HasMany(x => x.Document)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            builder
               .HasMany(x => x.Employee)
               .WithOne(x => x.Person)
               .HasForeignKey(x => x.PersonId);

            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasFilter($"{nameof(Person.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.Email)}");

            builder.HasIndex(x => x.Phone)
               .IsUnique()
               .HasFilter($"{nameof(Person.DeletedAt)} is null")
               .HasDatabaseName($"IX_{nameof(Person)}_{nameof(Person.Phone)}");
        }
    }
}
