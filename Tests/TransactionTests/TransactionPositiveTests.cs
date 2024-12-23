using Bogus;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Tests.Generators;
using Xunit;

namespace Tests.TransactionTests;

/// <summary>
/// Позитивные тесты для Transaction
/// </summary>
public class TransactionPositiveTests
{
    private readonly Faker _faker = new();
    
    /// <summary>
    /// Проверка, что у сущности Transaction корректно создается экземпляр
    /// </summary>
    [Fact]
    public void Add_Transaction_ReturnTransaction()
    {
        // Arrange
        var user = UserGenerator.GenerateUser();
        var card = CardGenerator.GenerateCard();
        
        var id = Guid.NewGuid();
        var transactionDate = _faker.Date.Past();
        var moneyAmount = _faker.Random.Decimal();
        var transactionType = _faker.PickRandomWithout(TransactionType.None);
        
        // Act
        var transaction = new Transaction(id, transactionDate, moneyAmount, transactionType, user.Id, card.Id);

        // Assert
        transaction.Id.Should().Be(id);
        transaction.TransactionDate.Should().Be(transactionDate);
        transaction.MoneyAmount.Should().Be(moneyAmount);
        transaction.TransactionType.Should().Be(transactionType);
        transaction.UserId.Should().Be(user.Id);
        transaction.CardId.Should().Be(card.Id);
    }
}