public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string HolderName {get; set;} = string.Empty;
    public string HolderLastName {get; set;} = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class RegisterResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string HolderName { get; set; } = string.Empty;
    public string HolderLastName { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string HolderName {get; set;} = string.Empty;
    public string HolderLastName {get; set;} = string.Empty; 
    public string Email { get; set; } = string.Empty; 
    public DateTime ExpiresAt {get; set;}
}

public class ChangePasswordRequest
{
    public string OldPassword {get; set;} = string.Empty;
    public string NewPassword {get; set;} = string.Empty;
}