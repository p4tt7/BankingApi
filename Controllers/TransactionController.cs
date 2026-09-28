using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[transfer]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;
    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(Guid id)
    {
        var tranfer = await _service.GetTransactionById(id);
        return Ok(tranfer);  
    }

    [HttpPost]
    public async Task<IActionResult> Transfer(TransferRequest request)
    {
        var response = await _service.TransactionAsync(request);
        return Ok(response);   
    }

    [HttpPost]
    public async Task<IActionResult> Withdraw(WithdrawalRequest request)
    {
        var response = await _service.WithdrawalAsync(request);
        return Ok(response);   
        
    }

    [HttpPost]
    public async Task<IActionResult> Deposit(DepositRequest request)
    {
        var response = await _service.DepositAsync(request);
        return Ok(response);   
    }


}