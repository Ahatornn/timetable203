using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Employee"/>
    /// </summary>

    public interface IEmployeeWriteRepository : IRepositoryWriter<Employee>
    {
    }
}
