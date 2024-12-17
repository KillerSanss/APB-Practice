using Domain.Validations.Validators;
using Domain.ValueObjects;
using FluentValidation;

namespace Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public FullName FullName { get; set; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Список карт пользователя
    /// </summary>
    public ICollection<Card> Cards { get; set; }
    
    /// <summary>
    /// Список транзакций пользователя
    /// </summary>
    public ICollection<Transaction> Transactions { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="fullName">Полное имя.</param>
    /// <param name="email">Почта.</param>
    /// <param name="password">Пароль.</param>
    public User(
        Guid id,
        FullName fullName,
        string email,
        string password)
    {
        SetId(id);
        FullName = fullName;
        Email = email;
        Password = password;
        
        Validate();
    }

    /// <summary>
    /// Обновление User
    /// </summary>
    /// <param name="fullName">Полное имя.</param>
    /// <param name="email">Почта.</param>
    /// <param name="password">Пароль.</param>
    public void Update(
        FullName fullName,
        string email,
        string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        
        Validate();
    }
    
    private void Validate()
    {
        var validator = new UserValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join(" || ", result.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }
    }

    public User()
    {
    }
}