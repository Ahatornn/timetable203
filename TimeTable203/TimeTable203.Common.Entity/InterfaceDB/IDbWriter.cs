namespace TimeTable203.Common.Entity
{
    /// <summary>
    /// Интерфейс создания и модификации записей в хранилище
    /// </summary>
    public interface IDbWriter
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add<IEntities>(IEntities entiy) where IEntities : class, IEntity;

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update<IEntities>(IEntities entiy) where IEntities : class, IEntity;

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete<IEntities>(IEntities entiy) where IEntities : class, IEntity;
    }
}
