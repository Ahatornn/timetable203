using System.ComponentModel;

namespace TimeTable203.Api.Models.Enums
{
    public enum DocumentTypesResponse
    {
        /// <summary>
        /// Не определён
        /// </summary>
        None,

        /// <summary>
        /// Пасспорт
        /// </summary>
        [Description("Паспорт")]
        Passport,

        /// <summary>
        /// Свидетельство о рождении
        /// </summary>
        [Description("Свидетельство о рождении")]
        BirthCertificate
    }
}
