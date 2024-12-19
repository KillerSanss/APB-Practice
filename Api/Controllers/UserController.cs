using System.Security.Claims;
using Application.Dto.UserDto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер User
/// </summary>
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="userRequest">Данные для регистрации.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленный пользователь.</returns>
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(
        [FromForm] AddUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var userResponse = await _userService.RegisterAsync(
            userRequest,
            cancellationToken);

        if (userResponse == null)
            return BadRequest(new { message = "Пользователь с данной почтой уже существует." });

        return Created(nameof(RegisterAsync), userResponse);
    }
    
    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="userRequest">Данные для обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленный пользователь.</returns>
    [Authorize]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateAsync(
        [FromForm] UpdateUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.Parse(userIdClaim) != userRequest.Id)
            return StatusCode(StatusCodes.Status403Forbidden, "Вы не можете изменить данные другого пользователя.");
        
        var userResponse = await _userService.UpdateAsync(
            userRequest,
            cancellationToken);

        return Ok(userResponse);
    }
    
    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [Authorize]
    [HttpDelete("delete/{userId:guid}")]
    public async Task<ActionResult> DeleteAsync(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.Parse(userIdClaim) != userId)
            return StatusCode(StatusCodes.Status403Forbidden, "Вы не можете удалить другого пользователя");
        
        await _userService.DeleteAsync(userId, cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Вход в аккаунт
    /// </summary>
    /// <param name="loginRequest">Данные для входа.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT токен.</returns>
    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync(
        [FromForm] LoginRequest loginRequest,
        CancellationToken cancellationToken)
    {
        var loginResponse = await _userService.LoginAsync(loginRequest, cancellationToken);

        if (loginResponse == null)
            return BadRequest(new { message = "Неверный адрес электронной почты или пароль" });
    
        return Ok(loginResponse);
    }
    
    /// <summary>
    /// Получение текущего пользователя
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пользователь.</returns>
    [Authorize]
    [HttpGet("current")]
    public async Task<ActionResult> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        var user = await _userService.GetUserByIdAsync(Guid.Parse(userIdClaim), cancellationToken);
        return Ok(user);
    }
}