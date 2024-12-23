using Application.Dto.UserDto;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mapping;

/// <summary>
/// Маппинг для User
/// </summary>
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, AddUserResponse>();
        CreateMap<User, UpdateUserResponse>();
        CreateMap<User, GetUserResponse>();
        CreateMap<FullName, FullNameDto>();
        CreateMap<FullNameDto, FullName>();

        CreateMap<AddUserRequest, User>()
            .ConstructUsing(dto => new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                Password = dto.Password,
                FullName = new FullName(dto.FullName.Name, dto.FullName.Surname, dto.FullName.Patronymic)
            });
        
        CreateMap<UpdateUserRequest, User>()
            .ConstructUsing(dto => new User
            {
                Id = dto.Id,
                Email = dto.Email,
                Password = dto.Password,
                FullName = new FullName(dto.FullName.Name, dto.FullName.Surname, dto.FullName.Patronymic)
            });
    }
}