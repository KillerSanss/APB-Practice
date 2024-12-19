using Application.Dto.CardDto;
using Application.Dto.TransactionDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

/// <summary>
/// Сервис Card
/// </summary>
public class CardService
{
    private readonly TransactionService _transactionService;
    private readonly ICardRepository _cardRepository;
    private readonly IMapper _mapper;

    public CardService(
        TransactionService transactionService,
        ICardRepository cardRepository,
        IMapper mapper)
    {
        _transactionService = transactionService;
        _cardRepository = cardRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Добавление карты
    /// </summary>
    /// <param name="cardRequest">Данные для создания карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная карта.</returns>
    public async Task<AddCardResponse> AddAsync(
        AddCardRequest cardRequest,
        CancellationToken cancellationToken)
    {
        var card = _mapper.Map<Card>(cardRequest);

        await _cardRepository.AddAsync(card, cancellationToken);
        await _cardRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AddCardResponse>(card);
    }
    
    /// <summary>
    /// Удаление карты
    /// </summary>
    /// <param name="id">Идентификатор карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var card = await _cardRepository.GetByIdAsync(id, cancellationToken);
        
        await _cardRepository.DeleteAsync(card, cancellationToken);
        await _cardRepository.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Изменение баланса карты
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="value">Кол-во денег.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task ManageBalance(
        string cardNumber,
        decimal value,
        CancellationToken cancellationToken)
    {
        var card = await _cardRepository.GetByNumberAsync(cardNumber, cancellationToken);
        card.ManageBalance(value);
        
        await _cardRepository.UpdateAsync(card, cancellationToken);

        var transactionRequest = new AddTransactionRequest { TransactionDate = DateTime.UtcNow.ToUniversalTime().ToLocalTime(), MoneyAmount = value, TransactionType = value > 0 ? TransactionType.Deposit : TransactionType.Withdrawal, UserId = card.UserId, CardId = card.Id };
        await _transactionService.AddAsync(transactionRequest, cancellationToken);
    }
    
    /// <summary>
    /// Перевод на другую карту
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="receiverCardNumber">Номер карты получателя.</param>
    /// <param name="value">Кол-во денег.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task SendMoneyAsync(
        string cardNumber,
        string receiverCardNumber,
        decimal value,
        CancellationToken cancellationToken)
    {
        var card = await _cardRepository.GetByNumberAsync(cardNumber, cancellationToken);
        card.ManageBalance(-value);
        
        var receiverCard = await _cardRepository.GetByNumberAsync(receiverCardNumber, cancellationToken);
        receiverCard.ManageBalance(value * 0.98m);
        
        await _cardRepository.UpdateAsync(card, cancellationToken);
        await _cardRepository.UpdateAsync(receiverCard, cancellationToken);
        
        var sendTransactionRequest = new AddTransactionRequest { TransactionDate = DateTime.Now.ToUniversalTime().ToLocalTime(), MoneyAmount = -value, TransactionType = TransactionType.Transfer, UserId = card.UserId, CardId = card.Id };
        await _transactionService.AddAsync(sendTransactionRequest, cancellationToken);
        var receiveTransactionRequest = new AddTransactionRequest { TransactionDate = DateTime.Now.ToUniversalTime().ToLocalTime(), MoneyAmount = value * 0.98m, TransactionType = TransactionType.ReceivedTransfer, UserId = receiverCard.UserId, CardId = receiverCard.Id };
        await _transactionService.AddAsync(receiveTransactionRequest, cancellationToken);
    }
    
    /// <summary>
    /// Получение карты по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    public async Task<GetCardResponse> GetCardByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var card = await _cardRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<GetCardResponse>(card);
    }
    
    /// <summary>
    /// Получение карты по номеру
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    public async Task<GetCardResponse> GetCardByNumberAsync(
        string cardNumber,
        CancellationToken cancellationToken)
    {
        var card = await _cardRepository.GetByNumberAsync(cardNumber, cancellationToken);
        return _mapper.Map<GetCardResponse>(card);
    }
    
    /// <summary>
    /// Получение всех карт
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список карт.</returns>
    public async Task<List<GetCardResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var cards = await _cardRepository.GetByAllAsync(cancellationToken);
        return _mapper.Map<List<GetCardResponse>>(cards);
    }
}