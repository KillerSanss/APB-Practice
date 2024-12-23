using Domain.ValueObjects;
using FluentValidation;

namespace Domain.Validations.Validators;

/// <summary>
/// Валидация FullName
/// </summary>
public class FullNameValidator : AbstractValidator<FullName>
{
    public FullNameValidator()
    {
        RuleFor(d => d.Name)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Matches(RegexPatterns.LettersPattern).WithMessage(ValidationMessages.OnlyLettersError);
        
        RuleFor(d => d.Surname)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Matches(RegexPatterns.LettersPattern).WithMessage(ValidationMessages.OnlyLettersError);
        
        RuleFor(d => d.Patronymic)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Matches(RegexPatterns.LettersPattern).WithMessage(ValidationMessages.OnlyLettersError);
    }
}