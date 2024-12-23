using Domain.Validations.Validators;
using FluentValidation;

namespace Domain.ValueObjects;

/// <summary>
/// Полное имя
/// </summary>
public class FullName
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <param name="surname">Фамилия.</param>
    /// <param name="patronymic">Отчество.</param>
    public FullName(string name, string surname, string patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        
        Validate();
    }
    
    private void Validate()
    {
        var validator = new FullNameValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errors = string.Join(" || ", result.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errors);
        }
    }
}