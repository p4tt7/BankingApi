public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;

    public TransactionService(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<TransferResponse> TransactionAsync(TransferRequest request)
    {
        if (request.Amount <= 0) throw new Exception("");
        if (request.SourceAccountId > request.DestinationAccountId) throw new Exception("");
        var transaction = await _repo.TransactionAsync(request);
        return new TransferResponse
        {
            Id = transaction.Id,
            SourceAccountId = transaction.SourceAccountId,
            DestinationAccountId = transaction.DestinationAccountId,
            Amount = transaction.Amount,
        };
    }


    public async Task<TransferResponse> DepositAsync(DepositRequest request)
    {
        if (request.Amount <= 0) throw new Exception("");
        var deposit = await _repo.DepositAsync(request);
        return new TransferResponse
        {
            Id = deposit.Id,
            SourceAccountId = deposit.SourceAccountId,
            DestinationAccountId = deposit.DestinationAccountId,
            Amount = deposit.Amount,
        };

    }
    public async Task<TransferResponse> WithdrawalAsync(WithdrawalRequest request)
    {
        if (request.Amount <= 0) throw new Exception("");
        var withdrawal = await _repo.WithdrawalAsync(request);
        return new TransferResponse
        {
            Id = withdrawal.Id,
            SourceAccountId = withdrawal.SourceAccountId,
            DestinationAccountId = withdrawal.DestinationAccountId,
            Amount = withdrawal.Amount,
        };

    }
    public async Task<TransferResponse> GetTransactionById(Guid id)
    {
        var transaction = await _repo.GetTransactionById(id);
        return new TransferResponse
        {
            Id = transaction.Id,
            SourceAccountId = transaction.SourceAccountId,
            DestinationAccountId = transaction.DestinationAccountId,
            Amount = transaction.Amount,
        };

    }
}