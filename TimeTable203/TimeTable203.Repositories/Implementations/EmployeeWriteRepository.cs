using TimeTable203.Common.Entity.InterfaceDB;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts;

namespace TimeTable203.Repositories.Implementations
{
    /// <inheritdoc cref="IEmployeeWriteRepository"/>
    public class EmployeeWriteRepository : BaseWriteRepository<Employee>,
        IEmployeeWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeWriteRepository"/>
        /// </summary>
        public EmployeeWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
