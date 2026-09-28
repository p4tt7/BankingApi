public class TransferRequest
{
    public Guid DestinationAccountId { get; set; } 
    public Guid SourceAccountId { get; set; }
    public decimal Amount { get; set; }

}

public class DepositRequest
{
    public Guid DestinationAccountId { get; set; } 
    public decimal Amount { get; set; }
}

public class WithdrawalRequest
{
    public Guid SourceAccountId { get; set; } 
    public decimal Amount { get; set; }
}