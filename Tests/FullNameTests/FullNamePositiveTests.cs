using Bogus;
using Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Tests.FullNameTests;

/// <summary>
/// Позитивные тесты для FullName
/// </summary>
public class FullNamePositiveTests
{
    private readonly Faker _faker = new();
    
    /// <summary>
    /// Проверка, что у значимого объекта FullName корректно создается экземпляр
    /// </summary>
    [Fact]
    public void Add_FullName_ReturnFullName()
    {
        // Arrange
        var name = _faker.Random.String2(5);
        var surname = _faker.Random.String2(5);
        var patronymic = _faker.Random.String2(5);
        
        // Act
        var fullName = new FullName(name, surname, patronymic);

        // Assert
        fullName.Name.Should().Be(name);
        fullName.Surname.Should().Be(surname);
        fullName.Patronymic.Should().Be(patronymic);
    }
}