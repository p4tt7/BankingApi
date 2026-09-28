using Microsoft.EntityFrameworkCore;

public class EfAccountRepository : IAccountRepository
{
    private readonly BankingDbContext _db;

    public EfAccountRepository(BankingDbContext db)
    {
        _db = db;
    }

    public async Task<Account?> GetByIdAsync(Guid Id)
    {
        return await _db.Accounts.FindAsync(Id);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _db.Accounts.ToListAsync();
    }

    public async Task<Account> AddAsync(Account account)
    {
        await _db.Accounts.AddAsync(account);
        await _db.SaveChangesAsync();
        return account; 
    }

    public async Task<Account?> UpdateAsync(Account account)
    {
        if(!await _db.Accounts.AnyAsync(e => e.Id == account.Id)) 
            return null;

        account.UpdatedAt = DateTime.UtcNow;
        _db.Accounts.Update(account);
        await _db.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        var account = await _db.Accounts.FindAsync(Id);
        
        if(account == null)
        {
            return false;
        }

        _db.Accounts.Remove(account);
        await _db.SaveChangesAsync();
        return true;
    }
    
}