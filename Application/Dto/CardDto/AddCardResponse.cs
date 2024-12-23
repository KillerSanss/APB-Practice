using Domain.Enums;

namespace Application.Dto.CardDto;

/// <summary>
/// Дто ответа на добавление Card
/// </summary>
public class AddCardResponse
{
    /// <summary>
    /// Номер карты
    /// </summary>
    public string Number { get; init; }
    
    /// <summary>
    /// CVV код
    /// </summary>
    public string Cvv { get; init; }
    
    /// <summary>
    /// PIN код
    /// </summary>
    public string Pin { get; init; }
    
    /// <summary>
    /// Даты окончания срока действия карты
    /// </summary>
    public DateTime ExpirationDate { get; init; }
    
    /// <summary>
    /// Баланс на карте
    /// </summary>
    public decimal Balance { get; init; }
    
    /// <summary>
    /// Валюта карты
    /// </summary>
    public Currency Currency { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; init; }
}