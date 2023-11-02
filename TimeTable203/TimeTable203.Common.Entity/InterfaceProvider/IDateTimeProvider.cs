namespace TimeTable203.Common.Entity.InterfaceProvider
{
    /// <summary>
    /// Интерфейс получения даты
    /// </summary>
    internal interface IDateTimeProvider
    {
        /// <summary>
        /// Текущий момент (utc)
        /// </summary>
        DateTimeOffset UtcNow { get; }
    }
}
