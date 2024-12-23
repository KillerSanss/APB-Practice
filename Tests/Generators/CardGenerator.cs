using Bogus;
using Domain.Entities;
using Domain.Enums;

namespace Tests.Generators;

/// <summary>
/// Генератор сущности Card для тестов
/// </summary>
public class CardGenerator
{
    private static readonly Faker<Card> Faker = new Faker<Card>()
        .CustomInstantiator(f => new Card(
            Guid.NewGuid(),
            $"{f.Random.Int(1000, 9999)} {f.Random.Int(1000, 9999)} {f.Random.Int(1000, 9999)} {f.Random.Int(1000, 9999)}",
            f.Random.Int(100, 999).ToString(),
            f.Random.Int(1000, 9999).ToString(),
            f.Date.Future(),
            f.PickRandomWithout(Currency.None),
            UserGenerator.GenerateUser().Id));
    
    /// <summary>
    /// Генерация Card
    /// </summary>
    /// <returns>Card.</returns>
    public static Card GenerateCard()
    {
        return Faker.Generate();
    }
}