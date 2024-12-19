namespace Application.Dto.UserDto;

/// <summary>
/// Дто ответа на логин
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// JWT токен
    /// </summary>
    public string Token { get; init; }
}