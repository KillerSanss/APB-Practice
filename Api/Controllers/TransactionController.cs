using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер Transaction
/// </summary>
[Route("api/transactions")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly TransactionService _transactionService;
    
    public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    /// <summary>
    /// Получение транзакции по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор транзакции.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Транзакция.</returns>
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id, cancellationToken);
        return Ok(transaction);
    }
    
    /// <summary>
    /// Получение всех транзакций
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список транзакций</returns>
    [Authorize]
    [HttpGet("all")]
    public async Task<ActionResult> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionService.GetAllAsync(cancellationToken);
        return Ok(transactions);
    }
}