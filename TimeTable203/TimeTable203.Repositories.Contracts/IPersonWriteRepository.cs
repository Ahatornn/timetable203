﻿using TimeTable203.Context.Contracts.Models;

namespace TimeTable203.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Person"/>
    /// </summary>
    public interface IPersonWriteRepository : IRepositoryWriter<Person>
    {
    }
}
