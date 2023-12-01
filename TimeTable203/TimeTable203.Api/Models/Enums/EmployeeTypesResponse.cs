using System.ComponentModel;

namespace TimeTable203.Api.Models.Enums
{
    public enum EmployeeTypesResponse
    {
        /// <summary>
        /// Студент
        /// </summary>
        [Description("Студент")]
        Student,

        /// <summary>
        /// Преподаватель
        /// </summary>
        [Description("Преподаватель")]
        Teacher
    }
}
