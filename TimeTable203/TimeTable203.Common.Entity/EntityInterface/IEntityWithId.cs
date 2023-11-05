namespace TimeTable203.Common.Entity.EntityInterface
{
    /// <summary>
    /// Сущность с идентификатором
    /// </summary>
    public interface IEntityWithId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
