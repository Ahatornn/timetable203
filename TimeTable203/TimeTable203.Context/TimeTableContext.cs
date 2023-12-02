using Microsoft.EntityFrameworkCore;
using Serilog;
using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Configuration;
using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project TimeTable203.Context\TimeTable203.Context.csproj
    /// 4) dotnet ef database update --project TimeTable203.Context\TimeTable203.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --TimeTable203.Context\TimeTable203.Context.csproj
    /// </remarks>
    public class TimeTableContext : DbContext,
        ITimeTableContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
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
            Log.Information("Инициализирована бд");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            Log.Information("Идет сохранение данных в бд");
            var count = await base.SaveChangesAsync(cancellationToken);
            SkipTracker();
            return count;
        }

        public void SkipTracker()
        {
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
