using TimeTable203.Context.Contracts.Enums;
using TimeTable203.Services.Contracts.Models;
using TimeTable203.Services.Contracts.ModelsRequest;

namespace TimeTable203.Services.Contracts.Interface
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить список всех <see cref="EmployeeModel"/>
        /// </summary>
        Task<IEnumerable<EmployeeModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="EmployeeModel"/> по идентификатору
        /// </summary>
        Task<EmployeeModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового учителя
        /// </summary>
        Task<EmployeeModel> AddAsync(Guid id_person, EmployeeTypes employeeTypes, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего учителя
        /// </summary>
        Task<EmployeeModel> EditAsync(Guid id_person, EmployeeModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего учителя
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
