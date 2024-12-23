using Bogus;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Tests.Generators;
using Xunit;

namespace Tests.CardTests;

/// <summary>
/// Позитивные тесты для Card
/// </summary>
public class CardPositiveTests
{
    private readonly Faker _faker = new();
    
    /// <summary>
    /// Проверка, что у сущности Card корректно создается экземпляр
    /// </summary>
    [Fact]
    public void Add_Card_ReturnCard()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cardNumber = $"{_faker.Random.Int(1000, 9999)} {_faker.Random.Int(1000, 9999)} {_faker.Random.Int(1000, 9999)} {_faker.Random.Int(1000, 9999)}";
        var cvv = _faker.Random.Int(100, 999).ToString();
        var pin = _faker.Random.Int(1000, 9999).ToString();
        var expirationDate = _faker.Date.Future();
        var currency = _faker.PickRandomWithout(Currency.None);
        var user = UserGenerator.GenerateUser();
        
        // Act
        var card = new Card(id, cardNumber, cvv, pin, expirationDate, currency, user.Id);

        // Assert
        card.Id.Should().Be(id);
        card.Number.Should().Be(cardNumber);
        card.Cvv.Should().Be(cvv);
        card.Pin.Should().Be(pin);
        card.ExpirationDate.Should().Be(expirationDate);
        card.Currency.Should().Be(currency);
        card.UserId.Should().Be(user.Id);
    }
    
    /// <summary>
    /// Проверка, что у сущности Card корректно обновляется баланс
    /// </summary>
    [Fact]
    public void Card_ManageBalance_ReturnCardWithNewBalance()
    {
        // Arrange
        var card = CardGenerator.GenerateCard();
        
        // Act
        card.ManageBalance(25);

        // Assert
        card.Balance.Should().Be(25);
    }
}