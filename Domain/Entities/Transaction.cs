using Domain.Enums;
using Domain.Validations.Validators;
using FluentValidation;

namespace Domain.Entities;

/// <summary>
/// Транзакция
/// </summary>
public class Transaction : BaseEntity
{
    /// <summary>
    /// Дата транзакции
    /// </summary>
    public DateTime TransactionDate { get; set; }
    
    /// <summary>
    /// Кол-во денег
    /// </summary>
    public decimal MoneyAmount { get; set; }
    
    /// <summary>
    /// Тип транзакции
    /// </summary>
    public TransactionType TransactionType { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Навигационное поле User
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Идентификатор карты
    /// </summary>
    public Guid CardId { get; set; }
    
    /// <summary>
    /// Навигационное поле Card
    /// </summary>
    public Card Card { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="transactionDate">Дата транзакции.</param>
    /// <param name="moneyAmount">Кол-во денег.</param>
    /// <param name="transactionType">Тип транзакции.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cardId">Идентификатор карты.</param>
    public Transaction(
        Guid id,
        DateTime transactionDate,
        decimal moneyAmount,
        TransactionType transactionType,
        Guid userId,
        Guid cardId)
    {
        SetId(id);
        TransactionDate = transactionDate;
        MoneyAmount = moneyAmount;
        TransactionType = transactionType;
        UserId = userId;
        CardId = cardId;
        
        Validate();
    }
    
    private void Validate()
    {
        var validator = new TransactionValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join(" || ", result.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }
    }

    public Transaction()
    {
    }
}