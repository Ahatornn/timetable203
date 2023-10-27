using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.Configuration.ConfigurationDB
{
    internal class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("TGroup");

            builder.Property(x => x.Name)
               .HasMaxLength(200)
               .IsRequired();


            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasDatabaseName($"IX_{nameof(Group)}_" +
                                 $"{nameof(Group.Name)}_" +
                                 $"{nameof(Group.CreatedAt)}")
                .HasFilter($"{nameof(Group.DeletedAt)} is null");
        }
    }
}
