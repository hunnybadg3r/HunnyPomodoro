using HunnyPomodoro.Application.Services.Authentication;
using HunnyPomodoro.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace HunnyPomodoro.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(request.Name, request.Email, request.Password);
        
        var response = new AuthenticationResponse(authResult.Id, authResult.Name, authResult.Email, authResult.Token);
        
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);
        
        var response = new AuthenticationResponse(authResult.Id, authResult.Name, authResult.Email, authResult.Token);
        
        return Ok(response);
    }
}