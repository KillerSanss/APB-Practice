using Application.Dto.TransactionDto;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

/// <summary>
/// Маппинг для Transaction
/// </summary>
public class TransactionMappingProfile : Profile
{
    public TransactionMappingProfile()
    {
        CreateMap<Transaction, AddTransactionResponse>();
        CreateMap<Transaction, GetTransactionResponse>();
        
        CreateMap<AddTransactionRequest, Transaction>()
            .ConstructUsing(dto => new Transaction(
                Guid.NewGuid(),
                dto.TransactionDate,
                dto.MoneyAmount,
                dto.TransactionType,
                dto.UserId,
                dto.CardId));
    }
}