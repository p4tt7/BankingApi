using System.Transactions;
using Microsoft.EntityFrameworkCore;

public class EfTransferRepository : ITransactionRepository
{
    private readonly BankingDbContext _db;
    public EfTransferRepository(BankingDbContext db)
    {
        _db = db;
    }

    public async Task<Transaction> TransactionAsync(TransferRequest request)
    {
        using var dbTransaction = await _db.Database.BeginTransactionAsync();
        try
        {
            var source = await _db.Accounts.FindAsync(request.SourceAccountId);
            var destination = await _db.Accounts.FindAsync(request.DestinationAccountId);

            if(source == null || destination == null) throw new Exception("");
            if(source.Balance < request.Amount) throw new Exception("");

            source.Balance -= request.Amount;
            destination.Balance += request.Amount;

            var transaction = new Transaction
            {
                SourceAccountId = source.Id,
                DestinationAccountId = destination.Id,
                SourceAccount = source,
                DestinationAccount = destination,
                Type = TransactionType.Transfer
            };

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            return transaction;
        }
        catch
        {
            await dbTransaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Transaction> DepositAsync(DepositRequest request)
    {
        var account = await _db.Accounts.FindAsync(request.DestinationAccountId);
        if(account == null) throw new Exception("");
        account.Balance += request.Amount;

        var transaction = new Transaction
        {
            SourceAccountId = account.Id,
            Amount = request.Amount,
            Type = TransactionType.Deposit
        };

        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction> WithdrawalAsync(WithdrawalRequest request)
    {
        var account = await _db.Accounts.FindAsync(request.SourceAccountId);
        if(account == null) throw new Exception("");
        account.Balance -= request.Amount;

        var transaction = new Transaction
        {
            SourceAccountId = account.Id,
            Amount = request.Amount,
            Type = TransactionType.Withdrawal
        };

        _db.Transactions.Add(transaction);
        await _db.SaveChangesAsync();

        return transaction;
        
    }
    public async Task<Transaction> GetTransactionById(Guid id)
    {
        var transfer = await _db.Transactions.FindAsync(id);
        if(transfer == null) throw new Exception("");
        return transfer;
    }
}