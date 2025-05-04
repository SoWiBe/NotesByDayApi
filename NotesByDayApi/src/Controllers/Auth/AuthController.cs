using Microsoft.AspNetCore.Mvc;
using NotesByDayApi.Common.Endpoints.Auth.Requests;
using NotesByDayApi.Services;

namespace NotesByDayApi.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/v1/register/")]
    public async Task<IActionResult> Registration(RegistrationUserRequest request, CancellationToken cancellationToken = default)
    {
        var token = await _authService.Register(request, cancellationToken);
        return Ok(token);
    }
    
    [HttpPost("/v1/login/")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var token = await _authService.Login(request, cancellationToken);
        return Ok(token);
    }
}