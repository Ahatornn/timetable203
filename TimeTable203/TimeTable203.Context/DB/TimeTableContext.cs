using Microsoft.EntityFrameworkCore;
using TimeTable203.Common.Entity;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Anchors;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context.DB
{
    public class TimeTableContext : DbContext,
        ITimeTableContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork,
        IContextAnchor
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration<Discipline>).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();
        void IDbWriter.Add<IEntities>(IEntities entiy)
            => base.Entry(entiy).State = EntityState.Added;
        void IDbWriter.Update<IEntities>(IEntities entiy)
              => base.Entry(entiy).State = EntityState.Modified;
        void IDbWriter.Delete<IEntities>(IEntities entiy)
              => base.Entry(entiy).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }
    }
}
