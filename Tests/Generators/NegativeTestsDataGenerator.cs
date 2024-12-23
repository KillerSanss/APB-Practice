using Bogus;
using Domain.Enums;

namespace Tests.Generators;

/// <summary>
/// Генерация данных для негативных тестов
/// </summary>
public class NegativeTestsDataGenerator
{
    private static readonly Faker Faker = new();
    
    /// <summary>
    /// Генерация данных для исключения ValidationException у сущности User
    /// </summary>
    public static IEnumerable<object[]> GetUserValidationExceptionProperties()
    {
        return new List<object[]>
        {
            new object[] { Faker.Random.String2(5), Faker.Random.String2(8) },
            new object[] { Faker.Internet.Email(), Faker.Random.String2(5) }
        };
    }
    
    /// <summary>
    /// Генерация данных для исключения ValidationException у значимого объекта FullName
    /// </summary>
    public static IEnumerable<object[]> GetFullNameValidationExceptionProperties()
    {
        return new List<object[]>
        {
            new object[] { null, Faker.Random.String2(5), Faker.Random.String2(5) },
            new object[] { Faker.Random.String2(5), null, Faker.Random.String2(5) },
            new object[] { Faker.Random.String2(5), Faker.Random.String2(5), null }
        };
    }
    
    /// <summary>
    /// Генерация данных для исключения ValidationException у сущности Card
    /// </summary>
    public static IEnumerable<object[]> GetCardValidationExceptionProperties()
    {
        var user = UserGenerator.GenerateUser();
        
        return new List<object[]>
        {
            new object[] { Faker.Random.String2(5), Faker.Random.Int(100, 999).ToString(), Faker.Random.Int(1000, 9999).ToString(), Faker.Date.Future(), Faker.PickRandomWithout(Currency.None), user.Id },
            new object[] { $"{Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)}", Faker.Random.String2(5), Faker.Random.Int(1000, 9999).ToString(), Faker.Date.Future(), Faker.PickRandomWithout(Currency.None), user.Id },
            new object[] { $"{Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)}", Faker.Random.Int(100, 999).ToString(), Faker.Random.String2(5), Faker.Date.Future(), Faker.PickRandomWithout(Currency.None), user.Id },
            new object[] { $"{Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)}", Faker.Random.Int(100, 999).ToString(), Faker.Random.Int(1000, 9999).ToString(), null, Faker.PickRandomWithout(Currency.None), user.Id },
            new object[] { $"{Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)}", Faker.Random.Int(100, 999).ToString(), Faker.Random.Int(1000, 9999).ToString(), Faker.Date.Future(), Currency.None, user.Id },
            new object[] { $"{Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)} {Faker.Random.Int(1000, 9999)}", Faker.Random.Int(100, 999).ToString(), Faker.Random.Int(1000, 9999).ToString(), Faker.Date.Future(), Faker.PickRandomWithout(Currency.None), null }
        };
    }
    
    /// <summary>
    /// Генерация данных для исключения ValidationException у сущности Transaction
    /// </summary>
    public static IEnumerable<object[]> GetTransactionValidationExceptionProperties()
    {
        var user = UserGenerator.GenerateUser();
        var card = CardGenerator.GenerateCard();
        
        return new List<object[]>
        {
            new object[] { null, Faker.Random.Decimal(), Faker.PickRandomWithout(TransactionType.None), user.Id, card.Id },
            new object[] { Faker.Date.Past(), null, Faker.PickRandomWithout(TransactionType.None), user.Id, card.Id },
            new object[] { Faker.Date.Past(), Faker.Random.Decimal(), TransactionType.None, user.Id, card.Id },
            new object[] { Faker.Date.Past(), Faker.Random.Decimal(), Faker.PickRandomWithout(TransactionType.None), null, card.Id },
            new object[] { Faker.Date.Past(), Faker.Random.Decimal(), Faker.PickRandomWithout(TransactionType.None), user.Id, null }
        };
    }
}