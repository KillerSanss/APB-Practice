using System.Security.Claims;
using Application.Dto.CardDto;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер Card
/// </summary>
[Route("api/cards")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly CardService _cardService;
    
    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    /// <summary>
    /// Создание карты
    /// </summary>
    /// <param name="cardRequest">Данные для карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная карта.</returns>
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult> CreateAsync(
        [FromForm] AddCardRequest cardRequest,
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.Parse(userIdClaim) != cardRequest.UserId)
            return StatusCode(StatusCodes.Status403Forbidden, "Вы не можете добавить карту к другому пользователю.");

        var cardResponse = await _cardService.AddAsync(
            cardRequest,
            cancellationToken);

        return Created(nameof(CreateAsync), cardResponse);
    }
    
    /// <summary>
    /// Удаление карты
    /// </summary>
    /// <param name="cardId">Идентификатор карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [Authorize]
    [HttpDelete("delete/{cardId:guid}")]
    public async Task<ActionResult> DeleteAsync(
        [FromRoute] Guid cardId,
        CancellationToken cancellationToken)
    {
        var card = await _cardService.GetCardByIdAsync(cardId, cancellationToken);
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.Parse(userIdClaim) != card.UserId)
            return StatusCode(StatusCodes.Status403Forbidden, "Вы не можете удалить карту другого пользователя");
        
        await _cardService.DeleteAsync(cardId, cancellationToken);
        return NoContent();
    }
    
    /// <summary>
    /// Получение карты по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var card = await _cardService.GetCardByIdAsync(id, cancellationToken);
        return Ok(card);
    }
    
    /// <summary>
    /// Получение карты по номеру
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Карта.</returns>
    [Authorize]
    [HttpGet("get-by-number")]
    public async Task<ActionResult> GetByNumberAsync(
        [FromForm] string cardNumber,
        CancellationToken cancellationToken)
    {
        var card = await _cardService.GetCardByNumberAsync(cardNumber, cancellationToken);
        return Ok(card);
    }
    
    /// <summary>
    /// Получение всех карт
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список карт.</returns>
    [Authorize]
    [HttpGet("all")]
    public async Task<ActionResult> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var cards = await _cardService.GetAllAsync(cancellationToken);
        return Ok(cards);
    }

    /// <summary>
    /// Снятие/пополнение баланса карты
    /// </summary>
    /// <param name="cardNumber">Номер карты.</param>
    /// <param name="value">Кол-во денег.</param>
    /// <param name="valueCurrency">Валюта пополнения.</param>
    /// <param name="cvv">CVV код.</param>
    /// <param name="pin">PIN код.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [Authorize]
    [HttpPost("manage-balance")]
    public async Task<ActionResult> ManageBalanceAsync(
        [FromForm] string cardNumber,
        [FromForm] decimal value,
        [FromForm] Currency valueCurrency,
        [FromForm] string cvv,
        [FromForm] string pin,
        CancellationToken cancellationToken)
    {
        var card = await _cardService.GetCardByNumberAsync(cardNumber, cancellationToken);
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.Parse(userIdClaim) != card.UserId)
            return StatusCode(StatusCodes.Status403Forbidden, "Вы не можете управлять балансом карты другого пользователя.");
        if (card.Currency != valueCurrency)
            return BadRequest(new { message = "Пополнить карту можно только подходящей валютой" });
        if (card.Cvv != cvv)
            return BadRequest(new { message = "Неверный CVV код." });
        if (card.Pin != pin)
            return BadRequest(new { message = "Неверный PIN код." });
        if (value == 0)
            return BadRequest(new { message = "Выберите сумму." });
        
        await _cardService.ManageBalance(cardNumber, value, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Перевод денег на другую карту
    /// </summary>
    /// <param name="cardNumber">Номер карты отправителя.</param>
    /// <param name="receiverCardNumber">Номер карты получателя.</param>
    /// <param name="value">Кол-во денег.</param>
    /// <param name="valueCurrency">Валюта перевода.</param>
    /// <param name="cvv">CVV код.</param>
    /// <param name="pin">PIN код.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [Authorize]
    [HttpPost("send")]
    public async Task<ActionResult> SendMoneyAsync(
        [FromForm] string cardNumber,
        [FromForm] string receiverCardNumber,
        [FromForm] decimal value,
        [FromForm] Currency valueCurrency,
        [FromForm] string cvv,
        [FromForm] string pin,
        CancellationToken cancellationToken)
    {
        var card = await _cardService.GetCardByNumberAsync(cardNumber, cancellationToken);
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.Parse(userIdClaim) != card.UserId)
            return StatusCode(StatusCodes.Status403Forbidden, "Вы не можете управлять балансом карты другого пользователя.");
        if (card.Currency != valueCurrency)
            return BadRequest(new { message = "Осуществить перевод на карту можно только подходящей валютой" });
        if (card.Cvv != cvv)
            return BadRequest(new { message = "Неверный CVV код." });
        if (card.Pin != pin)
            return BadRequest(new { message = "Неверный PIN код." });
        if (value == 0)
            return BadRequest(new { message = "Выберите сумму." });

        await _cardService.SendMoneyAsync(cardNumber, receiverCardNumber, value, cancellationToken);
        return Ok();
    }
}