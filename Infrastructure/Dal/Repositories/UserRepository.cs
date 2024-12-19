using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories;

/// <summary>
/// Репозиторий User
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApbDbContext _dbContext;

    public UserRepository(ApbDbContext apbDbContext)
    {
        _dbContext = apbDbContext;
    }

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="user">Пользователь на добавление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленный пользователь.</returns>
    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }
    
    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="user">Пользователь на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленный пользователь.</returns>
    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="user">Пользователь на удаление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        _dbContext.Remove(user);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(p => p.Cards)
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список пользователей.</returns>
    public async Task<List<User>> GetByAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(p => p.Cards)
            .Include(p => p.Transactions)
            .ToListAsync(cancellationToken);
    }
    
    /// <summary>
    /// Проверка уникальности пользователя
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    public bool IsUniqueUser(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
            return false;

        return true;
    }
    
    /// <summary>
    /// Проверка существования пользователя
    /// </summary>
    /// <param name="email">Электронная почта.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    public async Task<User> IsUserExistAsync(string email, CancellationToken cancellationToken)
    {
        var existUser = await _dbContext.Users.FirstOrDefaultAsync(
            u => u.Email == email, cancellationToken);

        if (existUser == null)
            return null;

        return existUser;
    }

    /// <summary>
    /// Сохранение изменений
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        _dbContext.SaveChangesAsync(cancellationToken);
        return Task.CompletedTask;
    }
}