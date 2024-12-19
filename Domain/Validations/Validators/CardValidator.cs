using Domain.Entities;
using Domain.Enums;
using FluentValidation;

namespace Domain.Validations.Validators;

/// <summary>
/// Валидация Card
/// </summary>
public class CardValidator : AbstractValidator<Card>
{
    public CardValidator()
    {
        RuleFor(d => d.Number)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Matches(RegexPatterns.CardNumberPattern).WithMessage(ValidationMessages.CardNumberError);

        RuleFor(d => d.Cvv)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Length(3).WithMessage(ValidationMessages.Strict3LengthError);

        RuleFor(d => d.Pin)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Length(4).WithMessage(ValidationMessages.Strict4LengthError);

        RuleFor(d => d.ExpirationDate)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError);
        
        RuleFor(d => d.Balance)
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.NegativeNumberError);
        
        RuleFor(d => d.Currency)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Must(value => Enum.IsDefined(typeof(Currency), value)).WithMessage(ValidationMessages.CurrencyError);
        
        RuleFor(d => d.UserId)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError);
    }
}