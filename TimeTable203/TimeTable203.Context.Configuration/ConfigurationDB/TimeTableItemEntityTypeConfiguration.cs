using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    public class TimeTableItemEntityTypeConfiguration : IEntityTypeConfiguration<TimeTableItem>
    {
        public void Configure(EntityTypeBuilder<TimeTableItem> builder)
        {
            builder.ToTable("TableTimeTableItem");

            builder.Property(x => new
            {
                x.StartDate,
                x.EndDate,
                x.DisciplineId,
                x.GroupId,
                x.RoomNumber
            }).IsRequired();

            builder.HasIndex(x => new { x.StartDate, x.EndDate })
                .HasDatabaseName($"IX_{nameof(TimeTableItem)}_" +
                                 $"{nameof(TimeTableItem.StartDate)}_" +
                                  $"{nameof(TimeTableItem.EndDate)}_" +
                                 $"{nameof(TimeTableItem.CreatedAt)}")
                .HasFilter($"{nameof(TimeTableItem.DeletedAt)} is null");
        }
    }
}
