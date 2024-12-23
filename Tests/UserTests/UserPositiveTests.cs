using Bogus;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Tests.Generators;
using Xunit;

namespace Tests.UserTests;

/// <summary>
/// Позитивные тесты для User
/// </summary>
public class UserPositiveTests
{
    private readonly Faker _faker = new();
    
    /// <summary>
    /// Проверка, что у сущности User корректно создается экземпляр
    /// </summary>
    [Fact]
    public void Add_User_ReturnUser()
    {
        // Arrange
        var id = Guid.NewGuid();
        var fullName = new FullName(_faker.Random.String2(5), _faker.Random.String2(5), _faker.Random.String2(5));
        var email = _faker.Internet.Email();
        var password = _faker.Random.String2(8);
        
        // Act
        var user = new User(id, fullName, email, password);

        // Assert
        user.Id.Should().Be(id);
        user.FullName.Should().Be(fullName);
        user.Email.Should().Be(email);
        user.Password.Should().Be(password);
    }
    
    /// <summary>
    /// Проверка, что метод Update корректно обновляет экземпляр сущности User.
    /// </summary>
    [Fact]
    public void Update_User_ReturnUpdatedUser()
    {
        // Arrange
        var user = UserGenerator.GenerateUser();
        var newUser = UserGenerator.GenerateUser();
        
        // Act
        user.Update(newUser.FullName, newUser.Email, newUser.Password);

        // Assert
        user.Should().BeEquivalentTo(newUser, options => options
            .Excluding(o => o.Id));
    }
}