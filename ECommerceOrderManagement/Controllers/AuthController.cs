using Business.Abstract;
using Domain.Concrete.Dtos.Auth;
using Domain.Concrete.Dtos.Role;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceOrderManagement.Controllers;

[Route("[controller]/")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto request)
    {
        try
        {
            var message = await authService.RegisterAsync(request);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    [RateLimit]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto request)
    {
        try
        {
            var token = await authService.LoginAsync(request);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-role")]
    public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDto request)
    {
        try
        {
            var message = await authService.AddRoleAsync(request);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }
}

