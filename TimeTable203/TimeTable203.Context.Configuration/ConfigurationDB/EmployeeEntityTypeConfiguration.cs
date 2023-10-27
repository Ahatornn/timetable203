using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("TEmployee");

            builder.HasIndex(x => x.EmployeeType)
                .HasDatabaseName($"IX_{nameof(Employee)}_" +
                                 $"{nameof(Employee.EmployeeType)}_" +
                                 $"{nameof(Employee.Person.FirstName)}_" +
                                 $"{nameof(Employee.Person.LastName)}_")
                .HasFilter($"{nameof(Employee.EmployeeType)} is Teacher");
        }
    }
}
