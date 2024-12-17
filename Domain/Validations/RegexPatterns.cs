using System.Text.RegularExpressions;

namespace Domain.Validations;

/// <summary>
/// Класс регулярных выражений
/// </summary>
public static class RegexPatterns
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    public static readonly Regex EmailPattern = new (@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
    
    /// <summary>
    /// Только буквы
    /// </summary>
    public static readonly Regex LettersPattern = new ("\\p{L}'?$");
    
    /// <summary>
    /// Номер карты
    /// </summary>
    public static readonly Regex CardNumberPattern = new (@"^\d{4}\s\d{4}\s\d{4}\s\d{4}$");
}