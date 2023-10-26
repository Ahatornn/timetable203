using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.DB
{
    public class TimeTableContext : DbContext, ITimeTableContext
    {

        public DbSet<Discipline> Disciplines { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<TimeTableItem> TimeTableItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeTableContext(DbContextOptions<TimeTableContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration<Discipline>).Assembly);
        }
    }
}
