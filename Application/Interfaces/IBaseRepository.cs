using Domain.Entities;

namespace Application.Interfaces;

/// <summary>
/// Базовый репозиторий
/// </summary>
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Добавление сущности
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность.</returns>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленная сущность.</returns>
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность.</returns>
    public Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех сущностей.</returns>
    public Task<List<TEntity>> GetByAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Сохранение изменений
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}