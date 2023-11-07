using TimeTable203.Api.Models.Enums;

namespace TimeTable203.Api.ModelsRequest
{
    public class CreateEmployeeRequest
    {
        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public EmployeeTypesResponse EmployeeType { get; set; } = EmployeeTypesResponse.Student;
    }
}
