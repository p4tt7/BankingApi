using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[accounts]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;
    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var account = await _service.GetByIdAsync(id);
        if(account == null) return NotFound();
        return Ok(account);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var accounts = await _service.GetAllAsync();
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountRequest request)
    {
        var account = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new {id = account.Id}, account);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {        
        var deleted = await _service.DeleteAsync(id);
        if(!deleted) return NotFound();
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CreateAccountRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }
}