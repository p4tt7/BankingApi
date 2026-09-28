public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string HolderName {get; set;} = string.Empty;
    public decimal Balance {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}