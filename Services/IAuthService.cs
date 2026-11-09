public interface IAuthService
{
    Task<RegisterResponse> Register(RegisterRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task<AccountResponse> GetById(Guid id);
    Task<AccountResponse> ChangePassword(Guid id, ChangePasswordRequest request);
}