using Application.Dto.CardDto;
using Application.Dto.TransactionDto;

namespace Application.Dto.UserDto;

/// <summary>
/// Дто получения User
/// </summary>
public class GetUserResponse
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
    /// Электронная почта
    /// </summary>
    public string Email { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; init; }
    
    /// <summary>
    /// Список карт пользователя
    /// </summary>
    public ICollection<GetCardResponse> Cards { get; init; }
    
    /// <summary>
    /// Список транзакций пользователя
    /// </summary>
    public ICollection<GetTransactionResponse> Transactions { get; init; }
}