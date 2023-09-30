﻿using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts.Interface
{
    /// <summary>
    /// Репозиторий чтения <see cref="Document"/>
    /// </summary>
    public interface IDocumentReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Document"/>
        /// </summary>
        Task<List<Document>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Document"/> по идентификатору
        /// </summary>
        Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
