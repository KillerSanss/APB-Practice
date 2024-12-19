namespace Application.Dto.UserDto;

/// <summary>
/// Дто ответа на обновление пользователя
/// </summary>
public class UpdateUserResponse
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