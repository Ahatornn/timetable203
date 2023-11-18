using TimeTable203.Api.Models.Enums;
using TimeTable203.Services.Contracts.Models;

namespace TimeTable203.Api.ModelsRequest.Employee
{
    public class CreateEmployeeRequest
    {
        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public EmployeeTypesResponse EmployeeType { get; set; } = EmployeeTypesResponse.Student;

        /// <summary>
        /// <inheritdoc cref="PersonModel"/>
        /// </summary>
        public Guid PersonId { get; set; }
    }
}
