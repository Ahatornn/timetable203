﻿using TimeTable203.Context.Contracts;
using TimeTable203.Context.Contracts.Models;
using TimeTable203.Repositories.Contracts.Interface;

namespace TimeTable203.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository
    {
        private readonly ITimeTableContext context;

        public EmployeeReadRepository(ITimeTableContext context)
        {
            this.context = context;
        }

        Task<List<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Employees.Where(x => x.DeletedAt == null)
                .OrderBy(x => x.EmployeeType)
                .ToList());

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Employees.FirstOrDefault(x => x.Id == id));
    }
}
