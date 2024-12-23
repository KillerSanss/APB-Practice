using Bogus;
using Domain.Entities;
using Domain.ValueObjects;

namespace Tests.Generators;

/// <summary>
/// Генератор сущности User для тестов
/// </summary>
public abstract class UserGenerator
{
    private static readonly Faker<User> Faker = new Faker<User>()
        .CustomInstantiator(f => new User(
            Guid.NewGuid(),
            new FullName(f.Random.String2(5), f.Random.String2(5), f.Random.String2(5)),
            f.Internet.Email(),
            f.Random.String2(8)));
    
    /// <summary>
    /// Генерация User
    /// </summary>
    /// <returns>User.</returns>
    public static User GenerateUser()
    {
        return Faker.Generate();
    }
}