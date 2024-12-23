using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using Tests.Generators;
using Xunit;

namespace Tests.TransactionTests;

/// <summary>
/// Негативные тесты для Transaction
/// </summary>
public class TransactionNegativeTests
{
    public static IEnumerable<object[]> TestTransationrValidationExceptionData = NegativeTestsDataGenerator.GetTransactionValidationExceptionProperties();
    
    /// <summary>
    /// Проверка, что у сущности Transaction выбрасывается ValidationException при создании.
    /// </summary>
    [Theory]
    [MemberData(nameof(TestTransationrValidationExceptionData))]
    public void Add_Transaction_ThrowValidationException(
        DateTime transactionDate,
        decimal moneyAmount,
        TransactionType transactionType,
        Guid userId,
        Guid cardId)
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var action = () => new Transaction(id, transactionDate, moneyAmount, transactionType, userId, cardId);

        // Assert
        action.Should().Throw<ValidationException>();
    }
}