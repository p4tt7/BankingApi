using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class EfAuthRepository : IAuthRepository
{
    private readonly BankingDbContext _db;

    public EfAuthRepository(BankingDbContext db)
    {
        _db = db;
    }

    public async Task<Account> Register(Account account)
    {
        await _db.Accounts.AddAsync(account);
        await _db.SaveChangesAsync();
        return account;
    }

    public async Task<Account> GetByEmail(string email)
    {
        var exist = await _db.Accounts.FirstOrDefaultAsync(u => u.Email == email);
        if(exist == null) throw new Exception("A user linked to this email doesn't exist");
        return exist;
    }

    public async Task<Account> GetById(Guid id)
    {
        return await _db.Accounts.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Account> Update(Account account)
    {
        _db.Accounts.Update(account);
        await _db.SaveChangesAsync();
        return account;
    }
    
}