using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[authentication]")]
public class AuthController : ControllerBase 
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var account = await _service.Register(request);
        if(account == null)
        {
            throw new Exception("An error has occured");
        }
        return Ok(account);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _service.Login(request);
        return CreatedAtAction(user.HolderName, user);
    }

    [HttpGet]  
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _service.GetById(id);
        return Ok(user);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _service.ChangePassword(id, request);
        return NoContent();
    }
        
}