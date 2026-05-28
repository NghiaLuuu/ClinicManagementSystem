namespace AuthService.API.Controllers;

using AuthService.Application.Contracts;
using AuthService.Application.Exceptions;
using AuthService.Application.UseCases.Auth.Login;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth/login")]
public sealed class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest request,
        [FromServices] ILoginUseCase useCase,
        CancellationToken ct)
    {
        try
        {
            var response = await useCase.ExecuteAsync(request, ct);
            return Ok(response);
        }
        catch (UnauthorizedException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
