namespace AuthService.API.Controllers;

using AuthService.Application.Contracts;
using AuthService.Application.Exceptions;
using AuthService.Application.UseCases.Auth.Login;
using AuthService.Application.UseCases.Auth.RegisterDentist;
using AuthService.Application.UseCases.Auth.RegisterPatient;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("register/patient")]
    public async Task<ActionResult<AuthResponse>> RegisterPatient(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterPatientUseCase useCase,
        CancellationToken ct)
    {
        try
        {
            var response = await useCase.ExecuteAsync(request, ct);
            return Created($"/users/{response.UserId}", response);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register/dentist")]
    public async Task<ActionResult<AuthResponse>> RegisterDentist(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterDentistUseCase useCase,
        CancellationToken ct)
    {
        try
        {
            var response = await useCase.ExecuteAsync(request, ct);
            return Created($"/users/{response.UserId}", response);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginUseCase useCase,
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
