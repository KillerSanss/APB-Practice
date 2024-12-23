using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories;

/// <summary>
/// Репозиторий Transaction
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private readonly ApbDbContext _dbContext;

    public TransactionRepository(ApbDbContext apbDbContext)
    {
        _dbContext = apbDbContext;
    }
    
    /// <summary>
    /// Создание транзакции
    /// </summary>
    /// <param name="transaction">Транзакция на добавление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленная транзакция.</returns>
    public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _dbContext.Transactions.AddAsync(transaction, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return transaction;
    }

    /// <summary>
    /// Обновление транзакции
    /// </summary>
    /// <param name="transaction">Транзакция на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обнволенная транзакция.</returns>
    public async Task<Transaction> UpdateAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        _dbContext.Transactions.Update(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return transaction;
    }

    /// <summary>
    /// Удаление транзакции
    /// </summary>
    /// <param name="transaction">Транзакция на удаление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        _dbContext.Remove(transaction);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Получение транзакции по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Транзакция.</returns>
    public async Task<Transaction> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Transactions.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <summary>
    /// Получение всех транзакций
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список транзакций.</returns>
    public async Task<List<Transaction>> GetByAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Transactions.ToListAsync(cancellationToken);
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