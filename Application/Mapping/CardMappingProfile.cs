using Application.Dto.CardDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

/// <summary>
/// Маппинг для Card
/// </summary>
public class CardMappingProfile : Profile
{
    public CardMappingProfile()
    {
        CreateMap<Card, AddCardResponse>();
        CreateMap<Card, GetCardResponse>();
        
        CreateMap<AddCardRequest, Card>()
            .ConstructUsing(dto => new Card(
                Guid.NewGuid(),
                dto.Number,
                dto.Cvv,
                dto.Pin,
                dto.ExpirationDate,
                dto.Currency,
                dto.UserId));
    }
}