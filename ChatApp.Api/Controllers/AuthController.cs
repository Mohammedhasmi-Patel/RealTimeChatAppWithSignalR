using ChatApp.Application.DTO.Auth.Requests;
using ChatApp.Application.ServiceContracts.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthControllerController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthControllerController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterUserRequest request)
    {
        var result = await _authService.RegisterUserAsync(request);
        return Ok(result);
    }
}
