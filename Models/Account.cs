public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string HolderName {get; set;} = string.Empty;
    public string HolderLastName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public decimal Balance {get; set;}
    public string Password {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}