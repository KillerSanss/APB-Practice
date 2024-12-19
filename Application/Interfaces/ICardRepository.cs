using Domain.Entities;

namespace Application.Interfaces;

/// <summary>
/// Репозиторий Card
/// </summary>
public interface ICardRepository : IBaseRepository<Card>
{
    /// <summary>
    /// Получение карты по номеру
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    public Task<Card> GetByNumberAsync(string cardNumber, CancellationToken cancellationToken);
}