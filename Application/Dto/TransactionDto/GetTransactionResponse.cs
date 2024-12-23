using Domain.Enums;

namespace Application.Dto.TransactionDto;

/// <summary>
/// Дто получения Transaction
/// </summary>
public class GetTransactionResponse
{
    /// <summary>
    /// Идентификатор транзакции
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата транзакции
    /// </summary>
    public DateTime TransactionDate { get; init; }
    
    /// <summary>
    /// Кол-во денег
    /// </summary>
    public decimal MoneyAmount { get; init; }
    
    /// <summary>
    /// Тип транзакции
    /// </summary>
    public TransactionType TransactionType { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; init; }
    
    /// <summary>
    /// Идентификатор карты
    /// </summary>
    public Guid CardId { get; init; }
}