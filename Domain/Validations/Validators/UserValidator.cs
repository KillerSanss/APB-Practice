using Domain.Entities;
using FluentValidation;

namespace Domain.Validations.Validators;

/// <summary>
/// Валидация User
/// </summary>
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(d => d.Email)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Matches(RegexPatterns.EmailPattern).WithMessage(ValidationMessages.EmailError);

        RuleFor(d => d.Password)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .MinimumLength(8).WithMessage(ValidationMessages.MinimumLengthError);
    }
}