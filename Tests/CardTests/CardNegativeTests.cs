using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using Tests.Generators;
using Xunit;

namespace Tests.CardTests;

/// <summary>
/// Негативные тесты для Card
/// </summary>
public class CardNegativeTests
{
    public static IEnumerable<object[]> TestCardValidationExceptionData = NegativeTestsDataGenerator.GetCardValidationExceptionProperties();
    
    /// <summary>
    /// Проверка, что у сущности Card выбрасывается ValidationException при создании.
    /// </summary>
    [Theory]
    [MemberData(nameof(TestCardValidationExceptionData))]
    public void Add_Card_ThrowValidationException(
        string number,
        string cvv,
        string pin,
        DateTime expirationDate,
        Currency currency,
        Guid userId)
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var action = () => new Card(id, number, cvv, pin, expirationDate, currency, userId);

        // Assert
        action.Should().Throw<ValidationException>();
    }
}