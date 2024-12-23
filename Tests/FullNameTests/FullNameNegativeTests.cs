using Domain.ValueObjects;
using FluentAssertions;
using FluentValidation;
using Tests.Generators;
using Xunit;

namespace Tests.FullNameTests;

/// <summary>
/// Негативные тесты для FullName
/// </summary>
public class FullNameNegativeTests
{
    public static IEnumerable<object[]> TestFullNameValidationExceptionData = NegativeTestsDataGenerator.GetFullNameValidationExceptionProperties();
    
    /// <summary>
    /// Проверка, что у значимого объекта FullName выбрасывается ValidationException при создании.
    /// </summary>
    [Theory]
    [MemberData(nameof(TestFullNameValidationExceptionData))]
    public void Add_FullName_ThrowValidationException(
        string name,
        string surname,
        string patronymic)
    {
        // Act
        var action = () => new FullName(name, surname, patronymic);

        // Assert
        action.Should().Throw<ValidationException>();
    }
}