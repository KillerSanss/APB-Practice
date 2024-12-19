namespace Domain.Validations;

/// <summary>
/// Класс сообщений ошибок валидации
/// </summary>
public static class ValidationMessages
{
    public const string NullError = "{PropertyName} не может быть null";
    public const string EmptyError = "{PropertyName} не может быть пустым";
    public const string MinimumLengthError = "{PropertyName} слишком короткий";
    public const string Strict3LengthError = "{PropertyName} должен быть содержать ровно 3 символа";
    public const string Strict4LengthError = "{PropertyName} должен быть содержать ровно 4 символа";
    public const string NegativeNumberError = "{PropertyName} не может быть отрицательным";
    public const string OnlyLettersError = "{PropertyName} должен содержать только буквы";
    public const string EmailError = "Неверный формат электронной почты. Пример: example@gmail.com";
    public const string CardNumberError = "Неверный формат номера карты. Пример: 1234 4567 7890 1234";
    public const string CurrencyError = "Неверный тип валюты";
    public const string TransactionTypeError = "Неверный тип транзакции";
    public const string FutureDateError = "Дата не может быть в будущем";
}