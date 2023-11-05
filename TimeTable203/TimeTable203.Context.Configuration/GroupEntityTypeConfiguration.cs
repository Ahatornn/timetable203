using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration
{
    internal class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Name)
               .HasMaxLength(200)
               .IsRequired();

            builder
              .HasMany(x => x.Students)
              .WithOne(x => x.Group)
              .HasForeignKey(x => x.GroupId);

            builder
             .HasMany(x => x.TimeTableItem)
             .WithOne(x => x.Group)
             .HasForeignKey(x => x.GroupId);

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasFilter($"{nameof(Group.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Group)}_{nameof(Group.Name)}");
        }
    }
}
