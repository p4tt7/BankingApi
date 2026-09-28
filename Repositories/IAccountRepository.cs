public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid Id);
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account> AddAsync(Account account);
    Task<Account?> UpdateAsync (Account account);
    Task<bool> DeleteAsync(Guid id); 
}