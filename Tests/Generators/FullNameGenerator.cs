using Bogus;
using Domain.ValueObjects;

namespace Tests.Generators;

/// <summary>
/// Генератор значимого объекта FullName для тестов
/// </summary>
public class FullNameGenerator
{
    private static readonly Faker<FullName> Faker = new Faker<FullName>()
        .CustomInstantiator(f => new FullName(
            f.Random.String2(5),
            f.Random.String2(5),
            f.Random.String2(5)));
    
    /// <summary>
    /// Генерация FullName
    /// </summary>
    /// <returns>FullName.</returns>
    public static FullName GenerateFullName()
    {
        return Faker.Generate();
    }
}