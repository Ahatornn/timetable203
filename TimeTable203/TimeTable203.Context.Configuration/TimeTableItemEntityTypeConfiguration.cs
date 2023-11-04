using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration
{
    public class TimeTableItemEntityTypeConfiguration : IEntityTypeConfiguration<TimeTableItem>
    {
        public void Configure(EntityTypeBuilder<TimeTableItem> builder)
        {
            builder.ToTable("TimeTableItems");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.HasIndex(x => new { x.StartDate, x.EndDate })
                .HasFilter($"{nameof(TimeTableItem.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(TimeTableItem)}_{nameof(TimeTableItem.StartDate)}_{nameof(TimeTableItem.EndDate)}");
        }
    }
}
