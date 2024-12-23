namespace Application.Dto.UserDto;

/// <summary>
/// Дто запроса на обновление User
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; init; }
    
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