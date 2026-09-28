public interface ITransactionService
{
    Task<TransferResponse> TransactionAsync(TransferRequest request);
    Task<TransferResponse> DepositAsync(DepositRequest request);
    Task<TransferResponse> WithdrawalAsync(WithdrawalRequest request);
    Task<TransferResponse> GetTransactionById(Guid id);
    
}