using System.Linq;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repo;
    public AccountService(IAccountRepository repo)
    {
        _repo = repo;
    }

    public async Task<AccountResponse> CreateAsync(CreateAccountRequest request)
    {
        var account = new Account
        {
            HolderName = request.HolderName,
            Balance = request.InitialBalance
        };

        var saved = await _repo.AddAsync(account);
        return MapToResponse(saved);
    }

    public async Task<AccountResponse?> GetByIdAsync(Guid Id)
    {
       var a = await _repo.GetByIdAsync(Id);
       return a is null ? null : MapToResponse(a);
    }

    public async Task<AccountResponse?> UpdateAsync(Guid Id, CreateAccountRequest req)
    {
        var account = new Account
        {
            Id = Id,
            HolderName = req.HolderName,
            Balance = req.InitialBalance,
            UpdatedAt = DateTime.UtcNow
        };

        var updated = await _repo.UpdateAsync(account);
        return updated is null ? null : MapToResponse(updated);

    }

    public Task<bool> DeleteAsync(Guid Id)
    {
        return _repo.DeleteAsync(Id);
    }

    public async Task<IEnumerable<AccountResponse>> GetAllAsync()
    {
        var accounts = await _repo.GetAllAsync();
        return accounts.Select(MapToResponse);
    }


    public static AccountResponse MapToResponse(Account a)
    {
        return new AccountResponse
        {
            Id = a.Id,
            HolderName = a.HolderName,
            Balance = a.Balance,
            CreatedAt = a.CreatedAt
        };
    }

}