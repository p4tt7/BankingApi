public interface IAuthRepository
{
    Task<Account> Register(Account account);
    Task<Account> GetByEmail(string email);
    Task<Account> GetById(Guid id);
    Task<Account> Update(Account account);
}