using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("TPerson");

            builder.Property(x => new
            {
                x.FirstName,
                x.LastName,
                x.Email,
                x.Phone
            }).IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName($"IX_{nameof(Person)}_" +
                                 $"{nameof(Person.Email)}_" +
                                 $"{nameof(Person.CreatedAt)}")
                .HasFilter($"{nameof(Person.DeletedAt)} is null");

            builder.HasIndex(x => x.Phone)
               .IsUnique()
               .HasDatabaseName($"IX_{nameof(Person)}_" +
                                $"{nameof(Person.Phone)}_" +
                                $"{nameof(Person.CreatedAt)}")
               .HasFilter($"{nameof(Person.DeletedAt)} is null");
        }
    }
}
