using Application.Dto.TransactionDto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services;

/// <summary>
/// Сервис Transaction
/// </summary>
public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        _transactionRepository= transactionRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Добавление транзакции
    /// </summary>
    /// <param name="transactionRequest">Данные для создания транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная транзакция.</returns>
    public async Task<AddTransactionResponse> AddAsync(
        AddTransactionRequest transactionRequest,
        CancellationToken cancellationToken)
    {
        var transaction = _mapper.Map<Transaction>(transactionRequest);

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        return _mapper.Map<AddTransactionResponse>(transaction);
    }
    
    /// <summary>
    /// Получение транзакции по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Транзакция.</returns>
    public async Task<GetTransactionResponse> GetTransactionByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<GetTransactionResponse>(transaction);
    }
    
    /// <summary>
    /// Получение всех транзакций
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список транзакций.</returns>
    public async Task<List<GetTransactionResponse>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetByAllAsync(cancellationToken);
        return _mapper.Map<List<GetTransactionResponse>>(transactions);
    }
}