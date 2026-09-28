public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}


public class Transaction
{
    public Guid Id { get; set; } 
    public Guid SourceAccountId { get; set; }
    public Guid? DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Account SourceAccount { get; set;} = null!;
    public Account? DestinationAccount { get; set; }
}