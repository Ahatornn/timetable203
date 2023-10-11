
using Microsoft.EntityFrameworkCore;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.DB
{
    public class TimeTableApplicationContext : DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TimeTableItem> TimeTableItems { get; set; }
        public TimeTableApplicationContext(DbContextOptions<TimeTableApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discipline>().HasKey(x => x.Id);
            modelBuilder.Entity<Discipline>().ToTable("DisciplineDB");

            modelBuilder.Entity<Document>().HasKey(x => x.Id);
            modelBuilder.Entity<Document>().ToTable("DocumentDB");
            modelBuilder.Entity<Document>().Property(x => x.PersonId).HasColumnName("Person_id");

            modelBuilder.Entity<Employee>().HasKey(x => x.Id);
            modelBuilder.Entity<Employee>().ToTable("EmployeeDB");
            modelBuilder.Entity<Employee>().Property(x => x.PersonId).HasColumnName("Person_id");

            modelBuilder.Entity<Group>().HasKey(x => x.Id);
            modelBuilder.Entity<Group>().ToTable("GroupDB");
            modelBuilder.Entity<Group>().Property(x => x.EmployeeId).HasColumnName("Employee_id");
            modelBuilder.Entity<Group>().Ignore(x => x.Students);

            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().ToTable("PersonDB");
            modelBuilder.Entity<Person>().Property(x => x.Group).HasColumnName("Group_id");

            modelBuilder.Entity<TimeTableItem>().HasKey(x => x.Id);
            modelBuilder.Entity<TimeTableItem>().ToTable("TimeTableItemDB");
            modelBuilder.Entity<TimeTableItem>().Property(x => x.DisciplineId).HasColumnName("Discipline_id");
            modelBuilder.Entity<TimeTableItem>().Property(x => x.GroupId).HasColumnName("Group_id");
            modelBuilder.Entity<TimeTableItem>().Property(x => x.Teacher).HasColumnName("Teacher_id");
        }
    }
}
