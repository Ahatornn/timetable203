﻿namespace TimeTable203.Context.Contracts.Models
{
    /// <summary>
    /// Предмет
    /// </summary>
    public class Discipline : BaseAuditEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// нужна для связи один ко многим по вторичному ключу <see cref="TimeTableItem"/>
        /// </summary>
        public ICollection<TimeTableItem> TimeTableItem { get; set; }
    }
}
