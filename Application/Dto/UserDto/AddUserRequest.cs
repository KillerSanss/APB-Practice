namespace Application.Dto.UserDto;

/// <summary>
/// Дто запроса на добавление User
/// </summary>
public class AddUserRequest
{
    /// <summary>
    /// Полное имя
    /// </summary>
    public FullNameDto FullName { get; init; }
    
    /// <summary>
    /// Электронная почат
    /// </summary>
    public string Email { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; init; }
}