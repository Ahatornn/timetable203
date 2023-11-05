using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="TimeTableItem"/>
    /// </summary>
    public interface ITimeTableItemWriteRepository : IRepositoryWriter<TimeTableItem>
    {
    }
}
