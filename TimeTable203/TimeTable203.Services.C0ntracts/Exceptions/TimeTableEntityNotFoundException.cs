namespace TimeTable203.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемая сущность не найдена
    /// </summary>
    public class TimeTableEntityNotFoundException<TEntity> : TimeTableNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TimeTableEntityNotFoundException{TEntity}"/>
        /// </summary>
        public TimeTableEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
