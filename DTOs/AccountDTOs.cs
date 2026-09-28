using System.ComponentModel.DataAnnotations;

public class CreateAccountRequest
{
    [Required]
    [StringLength(100,MinimumLength=1)]
    public string HolderName { get; set; } = string.Empty;

    [Range(0,double.MaxValue, ErrorMessage = "Initial balance cannot be negative.")]
    public decimal InitialBalance { get; set; } = 0;
}



public class AccountResponse
{
    public Guid Id { get; set; }
    public string HolderName { get; set; } = string.Empty;
    public decimal Balance { get; set; } 
    public DateTime CreatedAt { get; set; }
}
