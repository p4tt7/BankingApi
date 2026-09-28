public interface ITransactionRepository
{
    Task<Transaction> TransactionAsync(TransferRequest request);
    Task<Transaction> DepositAsync(DepositRequest request);
    Task<Transaction> WithdrawalAsync(WithdrawalRequest request);
    Task<Transaction> GetTransactionById(Guid id);
}