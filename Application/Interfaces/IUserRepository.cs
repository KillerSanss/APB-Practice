using Domain.Entities;

namespace Application.Interfaces;

/// <summary>
/// Репозиторий User
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Проверка уникальности пользователя
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    public bool IsUniqueUser(string email);

    /// <summary>
    /// Проверка существования пользователя
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    public Task<User> IsUserExistAsync(string email, CancellationToken cancellationToken);
}