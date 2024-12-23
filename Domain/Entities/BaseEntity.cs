namespace Domain.Entities;

/// <summary>
/// Базовая сущность
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Установка идентификатор
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    public void SetId(Guid id)
    {
        Id = id;
    }
}