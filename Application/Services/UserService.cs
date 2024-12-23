using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dto.UserDto;
using Application.Interfaces;
using Application.Settings;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

/// <summary>
/// Сервис User
/// </summary>
public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly string _secretKey;
    
    public UserService(
        IUserRepository userRepository,
        IOptions<ApiSettings> apiSettings,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _secretKey = apiSettings.Value.Secret;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="registrationRequest">Данные пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Новый пользователь.</returns>
    public async Task<AddUserResponse> RegisterAsync(
        AddUserRequest registrationRequest,
        CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(registrationRequest);

        if (_userRepository.IsUniqueUser(user.Email))
            return null;
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        await _userRepository.AddAsync(user, cancellationToken);

        return _mapper.Map<AddUserResponse>(user);
    }
    
    /// <summary>
    /// Вход в аккаунт
    /// </summary>
    /// <param name="loginRequest">Данные для входа.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>JWT токен.</returns>
    public async Task<LoginResponse> LoginAsync(
        LoginRequest loginRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.IsUserExistAsync(loginRequest.Email, cancellationToken);
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            return null;
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.Now.AddHours(12),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        LoginResponse loginResponse = new LoginResponse { Token = tokenHandler.WriteToken(token) };

        return loginResponse;
    }
    
    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="updateRequest">Пользователь на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленный пользователь.</returns>
    public async Task<UpdateUserResponse> UpdateAsync(
        UpdateUserRequest updateRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(updateRequest.Id, cancellationToken);
        user.Update(
            new FullName(updateRequest.FullName.Name, updateRequest.FullName.Surname, updateRequest.FullName.Patronymic),
            updateRequest.Email,
            BCrypt.Net.BCrypt.HashPassword(updateRequest.Password));

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateUserResponse>(user);
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        
        await _userRepository.DeleteAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }
    
    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Пользователь.</returns>
    public async Task<GetUserResponse> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<GetUserResponse>(user);
    }
}