﻿using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts.Interface
{

    /// <summary>
    /// Репозиторий чтения <see cref="Group"/>
    /// </summary>
    public interface IGroupReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Group"/>
        /// </summary>
        Task<List<Group>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Group"/> по идентификатору
        /// </summary>
        Task<Group?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }

}
