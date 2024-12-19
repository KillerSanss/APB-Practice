using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal.Repositories;

/// <summary>
/// Репозиторий Card
/// </summary>
public class CardRepository : ICardRepository
{
    private readonly ApbDbContext _dbContext;

    public CardRepository(ApbDbContext apbDbContext)
    {
        _dbContext = apbDbContext;
    }
    
    /// <summary>
    /// Добавление карты
    /// </summary>
    /// <param name="card">Карта на добавление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленная карта.</returns>
    public async Task<Card> AddAsync(Card card, CancellationToken cancellationToken)
    {
        await _dbContext.Cards.AddAsync(card, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return card;
    }
    
    /// <summary>
    /// Обновление карты
    /// </summary>
    /// <param name="card">Карта на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленная карта.</returns>
    public async Task<Card> UpdateAsync(Card card, CancellationToken cancellationToken)
    {
        _dbContext.Cards.Update(card);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return card;
    }

    /// <summary>
    /// Удаление карты
    /// </summary>
    /// <param name="card">Карта на удаление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task DeleteAsync(Card card, CancellationToken cancellationToken)
    {
        _dbContext.Remove(card);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Получение карта по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    public async Task<Card> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Cards
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    
    /// <summary>
    /// Получение карты по номеру
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    public async Task<Card> GetByNumberAsync(string cardNumber, CancellationToken cancellationToken)
    {
        return await _dbContext.Cards
            .Include(p => p.Transactions)
            .FirstOrDefaultAsync(u => u.Number == cardNumber, cancellationToken);
    }

    /// <summary>
    /// Получение всех карт
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список карт.</returns>
    public async Task<List<Card>> GetByAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Cards
            .Include(p => p.Transactions)
            .ToListAsync(cancellationToken);
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