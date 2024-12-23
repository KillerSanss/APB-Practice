using Bogus;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Tests.Generators;
using Xunit;
using ValidationException = FluentValidation.ValidationException;

namespace Tests.UserTests;

/// <summary>
/// Негативные тесты для User
/// </summary>
public class UserNegativeTests
{
    private readonly Faker _faker = new();
    public static IEnumerable<object[]> TestUserValidationExceptionData = NegativeTestsDataGenerator.GetUserValidationExceptionProperties();
    
    /// <summary>
    /// Проверка, что у сущности User выбрасывается ValidationException при создании.
    /// </summary>
    [Theory]
    [MemberData(nameof(TestUserValidationExceptionData))]
    public void Add_User_ThrowValidationException(
        string email,
        string password)
    {
        // Arrange
        var id = Guid.NewGuid();
        var fullName = new FullName(_faker.Random.String2(5), _faker.Random.String2(5), _faker.Random.String2(5));
        
        // Act
        var action = () => new User(id, fullName, email, password);

        // Assert
        action.Should().Throw<ValidationException>();
    }
    
    /// <summary>
    /// Проверка, что у сущности User выбрасывается ValidationException при обновлении.
    /// </summary>
    [Theory]
    [MemberData(nameof(TestUserValidationExceptionData))]
    public void Update_User_ThrowValidationException(
        string email,
        string password)
    {
        // Arrange
        var user = UserGenerator.GenerateUser();
        
        // Act
        var action = () => user.Update(user.FullName, email, password);

        // Assert
        action.Should().Throw<ValidationException>();
    }
}