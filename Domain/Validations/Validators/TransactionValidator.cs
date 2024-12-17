using FluentValidation;
using Transaction = Domain.Entities.Transaction;

namespace Domain.Validations.Validators;

/// <summary>
/// Валидация Transaction
/// </summary>
public class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        RuleFor(d => d.TransactionDate)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError)
            .Must(date => date <= DateTime.UtcNow).WithMessage(ValidationMessages.FutureDateError);

        RuleFor(d => d.MoneyAmount)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError);
        
        RuleFor(d => d.UserId)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError);
        
        RuleFor(d => d.CardId)
            .NotNull().WithMessage(ValidationMessages.NullError)
            .NotEmpty().WithMessage(ValidationMessages.EmptyError);
    }
}