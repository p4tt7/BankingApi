public class TransferResponse
{
    public Guid Id { get; set; } 
    public Guid SourceAccountId { get; set; }
    public Guid? DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public bool Effected { get; set; }
}