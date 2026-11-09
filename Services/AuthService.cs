using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;


public class AuthService : IAuthService
{
    private readonly IAuthRepository _repo;
    private readonly IConfiguration _configuration;
    public AuthService(IAuthRepository repo, IConfiguration configuration)
    {
        _repo = repo;
        _configuration = configuration;
    }

    public async Task<RegisterResponse> Register(RegisterRequest request)
    {

        var hashed_pw = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var account = new Account
        {
            Id = new Guid(),
            HolderName = request.HolderName,
            HolderLastName = request.HolderName,
            Email = request.HolderLastName,
            Balance = 0,
            Password = hashed_pw,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var saved = await _repo.Register(account);

        return new RegisterResponse
        {
            Email = saved.Email,
            HolderName = saved.HolderName,
            HolderLastName = saved.HolderLastName,
            Id = saved.Id
        };

    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var user = await _repo.GetByEmail(request.Email);
        if (user == null) throw new Exception("The email address you entered is not linked to an account");
        bool access = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!access) throw new Exception("Incorrect password");

        var token = GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Email = user.Email,
            HolderName = user.HolderName,
            HolderLastName = user.HolderLastName,
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };
    }

    public async Task<AccountResponse> GetById(Guid id)
    {
        var user = await _repo.GetById(id);
        return new AccountResponse
        {
            Id = user.Id,
            HolderName = user.HolderName,
            HolderLastName = user.HolderLastName,
            Balance = user.Balance,
            CreatedAt = user.CreatedAt
        };
    }

    public async Task<AccountResponse> ChangePassword(Guid id, ChangePasswordRequest request)
    {
        var user = await _repo.GetById(id);
        bool valid = BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password);
        if(!valid) throw new UnauthorizedAccessException();

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _repo.Update(user);
        return new AccountResponse
        {
            Id = user.Id,
            HolderName = user.HolderName,
            HolderLastName = user.HolderLastName,
            Balance = user.Balance,
            CreatedAt = user.CreatedAt
        };
    }

    private string GenerateToken(Account account)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt__Secret"]!));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new Claim(ClaimTypes.Email, account.Email),
        };

        var token = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),

            signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}