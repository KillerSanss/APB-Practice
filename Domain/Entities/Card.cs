using Domain.Enums;
using Domain.Validations.Validators;
using FluentValidation;

namespace Domain.Entities;

/// <summary>
/// Карта
/// </summary>
public class Card : BaseEntity
{
    /// <summary>
    /// Номер карты
    /// </summary>
    public string Number { get; set; }
    
    /// <summary>
    /// CVV код
    /// </summary>
    public string Cvv { get; set; }
    
    /// <summary>
    /// PIN код
    /// </summary>
    public string Pin { get; set; }
    
    /// <summary>
    /// Даты окончания срока действия карты
    /// </summary>
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// Баланс на карте
    /// </summary>
    public decimal Balance { get; set; }
    
    /// <summary>
    /// Валюта карты
    /// </summary>
    public Currency Currency { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Навигационное поле User
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Список транзакций карты
    /// </summary>
    public ICollection<Transaction> Transactions { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="number">Номер карты.</param>
    /// <param name="cvv">CVV код.</param>
    /// <param name="pin">PIN код.</param>
    /// <param name="expirationDate">Даты окончания срока действия карты.</param>
    /// <param name="currency">Валюта карты.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    public Card(
        Guid id,
        string number,
        string cvv,
        string pin,
        DateTime expirationDate,
        Currency currency,
        Guid userId)
    {
        SetId(id);
        Number = number;
        Cvv = cvv;
        Pin = pin;
        ExpirationDate = expirationDate;
        Currency = currency;
        UserId = userId;
        
        Validate();
    }

    /// <summary>
    /// Изменение баланса
    /// </summary>
    /// <param name="value">Значение изменения.</param>
    public void ManageBalance(decimal value)
    {
        Balance += value;
        
        Validate();
    }
    
    private void Validate()
    {
        var validator = new CardValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join(" || ", result.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }
    }

    public Card()
    {
    }
}