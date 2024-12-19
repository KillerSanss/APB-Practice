namespace Domain.Enums;

/// <summary>
/// Перечисление типов транзакций
/// </summary>
public enum TransactionType
{
    Withdrawal = 1,
    Deposit = 2,
    Transfer = 3,
    ReceivedTransfer = 4
}