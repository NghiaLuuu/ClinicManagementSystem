namespace AuthService.API.Controllers;

using AuthService.Application.Contracts;
using AuthService.Application.Exceptions;
using AuthService.Application.UseCases.Auth.RegisterDentist;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth/register/dentist")]
public sealed class RegisterDentistController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AuthResponse>> RegisterDentist(
        [FromBody] RegisterRequest request,
        [FromServices] IRegisterDentistUseCase useCase,
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
}
