using System.Collections;

public interface IAccountService
{
    Task<AccountResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<AccountResponse>> GetAllAsync();
    Task<AccountResponse> CreateAsync(CreateAccountRequest req);
    Task<AccountResponse?> UpdateAsync(Guid id, CreateAccountRequest req);
    Task<bool> DeleteAsync(Guid id);
}