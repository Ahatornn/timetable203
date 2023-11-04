using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.HasMany(x => x.Group)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}
